
Public Class HiddenTab
    Inherits TabControl

    Private _DesignerIndex As Integer
    Public Property DesignerIndex As Integer
        Get
            Return SelectedIndex
        End Get
        Set(value As Integer)
            If DesignMode Then
                SelectedIndex = value
            End If
        End Set
    End Property

    Private _ShowDesignTabs As Boolean
    Public Property ShowDesignTabs() As Boolean
        Get
            Return _ShowDesignTabs
        End Get
        Set(ByVal value As Boolean)
            _ShowDesignTabs = value
        End Set
    End Property

    Protected Overrides Sub OnControlAdded(e As ControlEventArgs)
        If TypeOf e.Control Is TabPage Then
            e.Control.BackColor = BackColor
        End If

        MyBase.OnControlAdded(e)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = 4904 AndAlso Not (_ShowDesignTabs AndAlso DesignMode) Then
            m.Result = IntPtr.Zero
        Else
            MyBase.WndProc(m)
        End If
    End Sub

End Class
