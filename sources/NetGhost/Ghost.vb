Imports System.Text.RegularExpressions
Imports System.Text

Class Ghost
    Private Quote As Char = Convert.ToChar(34)
    Private Names As New List(Of String)
    Private Words As String() = ":.addhandler.addressof.alias.and.andalso.as.boolean.byref.byte.byval.call.case.catch.cbool.cbyte.cchar.cdate.cdec.cdbl.char.cint.class.clng.cobj.const.continue.csbyte.cshort.csng.cstr.ctype.cuint.culng.cushort.date.decimal.declare.default.delegate.dim.directcast.do.double.each.else.elseif.end.endif.enum.erase.error.event.exit.false.finally.for.friend.function.get.gettype.getxmlnamespace.global.gosub.goto.handles.if.implements.imports.in.inherits.integer.interface.is.isnot.let.lib.like.long.loop.me.mod.module.mustinherit.mustoverride.mybase.myclass.namespace.narrowing.new.next.not.nothing.notinheritable.notoverridable.object.of.on.operator.option.optional.or.orelse.overloads.overridable.overrides.paramarray.partial.private.property.protected.public.raiseevent.readonly.redim.rem.removehandler.resume.return.sbyte.select.set.shadows.shared.short.single.static.step.stop.string.structure.sub.synclock.then.throw.to.true.try.trycast.typeof.variant.wend.uinteger.ulong.ushort.using.when.while.widening.with.withevents.writeonly.xor".Split("."c)
    Private Range As String() = "ÀÁÂÃÄÅ.ÈÉÊË.ÌÍÎÏ.ÒÓÔÕÖ.ÙÚÛÜ".Split("."c)
    Private Section As Integer, Data, Clean As String, UTF8 As New UTF8Encoding

    Event ProgressMaximum(ByVal maximum As Integer)
    Event ProgressCurrent(ByVal current As Integer)
    Event Status(ByVal status As String)
    Event Complete(ByVal code As String, ByVal mode As Byte)

    Public Options(4) As Boolean

    Public StringMatches As New List(Of String), NameMatches As New List(Of String)
    Sub Load(ByVal code As Object)
        Current = 0
        Data = Environment.NewLine & DirectCast(code, String)
        Data = Regex.Replace(Data, "(#Region .+|#End Region)", String.Empty)
        Clean = Data

        StringMatches.Clear()
        RaiseEvent Status("Parsing strings..")
        RaiseEvent ProgressMaximum(4)
        For Each M As Match In Regex.Matches(Data, "(""(.*?)"")(\s|\)|,|\.|})")
            If Not StringMatches.Contains(M.Groups(1).Value) Then
                StringMatches.Add(M.Groups(1).Value)
            End If
        Next
        Increment()

        NameMatches.Clear()
        RaiseEvent Status("Parsing names..")
        For Each M As Match In Regex.Matches(Data, "\s((Function|Sub|Namespace|Module|Class|Structure|Interface|Const|Dim|Event|Friend|Private|Property|Public|ReadOnly|WithEvents|For|Each|Using|Catch) )+([^(\(|\s|,)]+)")
            Store(M.Groups(M.Groups.Count - 1).Value)
        Next
        Increment()
        For Each M As Match In Regex.Matches(Data, "(ParamArray|ByVal|ByRef) (\w+)")
            Store(M.Groups(2).Value)
        Next
        Increment()
        For Each M As Match In Regex.Matches(Data, "Enum (.+?)End Enum", RegexOptions.Singleline)
            For Each L As String In Parse(M.Groups(1).Value)
                Store(Regex.Match(L, "\w+").Value)
            Next
        Next
        Increment()
        RaiseEvent Status("Waiting..")
        RaiseEvent Complete(String.Empty, 0)
    End Sub
    Private Sub Store(ByVal data As String)
        If data.Length = 0 Then Return
        If Not Array.IndexOf(Words, data.ToLower) = -1 Then Return
        If NameMatches.Contains(data) Then Return
        NameMatches.Add(data)
    End Sub

    Private Current As Integer
    Sub Process()
        Data = Clean

        Current = 0
        Dim Maximum As Integer
        For I As Integer = 0 To Options.Length - 1
            If Options(I) Then Maximum += 1
        Next
        RaiseEvent ProgressMaximum(Maximum)

        If Options(1) Then ProcessStrings()
        If Options(0) Then ProcessNames()
        If Options(2) Then ProcessNumbers()

        If Options(3) Then
            RaiseEvent Status("Removing comments..")
            Data = Regex.Replace(Data, "'.+", String.Empty)
            Increment()
        End If

        If Options(4) Then
            RaiseEvent Status("Removing unused lines..")
            Dim O As New StringBuilder(Data.Length)
            For Each L As String In Parse(Data.Trim)
                O.AppendLine(L)
            Next
            Data = O.ToString
            Increment()
        End If

        RaiseEvent Status("Complete!")
        RaiseEvent Complete(Data, 1)
    End Sub

    Public StringExclusion As String() = New String() {}
    Private Sub ProcessStrings()
        Dim Table As String = Name()
        Dim Strings As New List(Of String)

        RaiseEvent Status("Replacing strings..")
        For Each M As String In StringMatches
            If Array.IndexOf(StringExclusion, M) = -1 Then
                Strings.Add(M)
                Data = Data.Replace(M, String.Format("{0}({1})", Table, Strings.Count - 1))
            End If
        Next

        If Strings.Count > 0 Then
            RaiseEvent Status("Encoding strings..")
            Dim Code As New StringBuilder("{"), R As Integer

            For I As Integer = 0 To Strings.Count - 1 Step 3
                R = Strings.Count - I - 1
                If R > 2 Then R = 2
                For X As Integer = I To I + R
                    Code.Append(Format(Strings(X)) & ",")
                Next
                Code.AppendLine(" _")
            Next

            Code.Remove(Code.Length - 5, 5)
            Code.Append("}")

            Data = Data & Environment.NewLine & String.Format(My.Resources.Table, Table, Name, Name, Name, Name).Replace("!R", Code.ToString)
        End If
        Increment()
    End Sub
    Private Function Format(ByVal data As String) As String
        Return Quote & Convert.ToBase64String(UTF8.GetBytes(data.Substring(1, data.Length - 2))) & Quote
    End Function

    Public NameExclusion As String() = New String() {}
    Private Sub ProcessNames()
        Names.Clear()
        RaiseEvent Status("Renaming declarations..")
        For Each N As String In NameMatches
            If Array.IndexOf(NameExclusion, N) = -1 Then
                Data = Regex.Replace(Data, "(\(|\s|{)" & N & "(\(|\)|\s|\.|,|})", "$1" & Name() & "$2")
            End If
        Next
        Increment()
    End Sub
    Private Function Name() As String
        Do
            Name = String.Empty
            Section = (New Random(Guid.NewGuid.GetHashCode)).Next(4)
            For I As Integer = 1 To 3
                Name &= Range(Section)((New Random(Guid.NewGuid.GetHashCode)).Next(Range(Section).Length))
            Next
        Loop Until Not Names.Contains(Name)
        Names.Add(Name)
    End Function

    Public Numbers As Integer, Equations As Long

    Private Scan As New List(Of Integer), RS1, RS2 As New List(Of String), Index As Integer
    Private Sub ProcessNumbers()
        RS1.Clear()
        RS2.Clear()
        Scan.Clear()

        RaiseEvent Status("Parsing numbers..")
        Dim NumberMatches As New List(Of Group)
        For Each M As Match In Regex.Matches(Data, "( |\(|{)(-?\d{1,7})(\s|\)|,|})")
            NumberMatches.Add(M.Groups(2))
            Scan.Add(CInt(M.Groups(2).Value))
        Next
        Numbers = NumberMatches.Count

        Dim R As Random, R1, R2, Min, Max As Integer
        Min = -255
        Max = 255
        Equations = 0

        RaiseEvent Status("Generating equations..")
        While Scan.Count > 0
            R = New Random(Guid.NewGuid.GetHashCode)
            R1 = R.Next(Min, Max)

            R = New Random(Guid.NewGuid.GetHashCode)
            R2 = R.Next(Min, Max)

            ProcessNumber(R1 Or R2, R1 & " Or " & R2)
            ProcessNumber(R1 Xor R2, R1 & " Xor " & R2)
            ProcessNumber(R1 << R2, R1 & " << " & R2)
            ProcessNumber(R1 >> R2, R1 & " >> " & R2)
            ProcessNumber(R1 + R2, R1 & " + " & R2)
            ProcessNumber(R1 - R2, R1 & " - " & R2)

            Min -= 1
            Max += 1
            Equations += 6
        End While

        RaiseEvent Status("Obfuscating numbers..")
        Dim T As New StringBuilder(Data), Index, Offset As Integer
        For Each G As Group In NumberMatches
            Index = RS1.IndexOf(G.Value)
            T.Remove(G.Index + Offset, G.Length)
            T.Insert(G.Index + Offset, RS2(Index))
            Offset += RS2(Index).Length - G.Length
            RS1.RemoveAt(Index)
            RS2.RemoveAt(Index)
        Next

        Data = T.ToString
        Increment()
    End Sub
    Private Sub ProcessNumber(ByVal value As Integer, ByVal result As String)
        Index = Scan.IndexOf(value)
        If Index = -1 Then Return
        Scan.RemoveAt(Index)
        RS1.Add(value.ToString)
        RS2.Add("(" & result & ")")
    End Sub

    Private Function Parse(ByVal data As String) As String()
        Dim R As New IO.StringReader(data)
        Dim T As New List(Of String)
        While R.Peek > -1
            data = R.ReadLine.Trim
            If data.Length > 0 Then T.Add(data)
        End While
        R.Close()
        Return T.ToArray
    End Function

    Private Sub Increment()
        Current += 1
        RaiseEvent ProgressCurrent(Current)
    End Sub
End Class