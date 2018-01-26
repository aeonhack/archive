    Private Sub DrawPokeBall(g As Graphics, x As Integer, y As Integer, size As Integer)

        Dim B_ As SmoothingMode = g.SmoothingMode
        Dim E_ As New SolidBrush(Color.FromArgb(210, 210, 210))
        Dim N_ As New SolidBrush(Color.FromArgb(200, 0, 0))

        Dim I_ As New Rectangle(x, y, size, size)
        Dim S_ As Integer = size \ 2

        Dim A_ As Integer = CInt(Math.Max(size * 0.05, 3))

        Dim P_ As New LinearGradientBrush(I_, Color.Red, Color.FromArgb(150, 0, 0), 90.0F)
        Dim O_ As Integer = CInt((S_ * 1.1) / 2)
        Dim K_ As New Rectangle(x + (O_ \ 2) + A_, y + (O_ \ 2) - A_, O_, O_)
        Dim E__ As New LinearGradientBrush(K_, Color.FromArgb(30, 255, 255, 255), Color.FromArgb(5, 255, 255, 255), 60.0F)
        Dim M_ As New Pen(Color.Black, A_)
        Dim O__ As Integer = y + (size \ 2)
        Dim N__ As Integer = A_ * 2

        Dim N___ As Integer = A_ \ 2
        Dim O___ As Integer = size \ 3
        Dim O____ As Integer = (size \ 2) - (O___ \ 2)
        Dim B__ As New LinearGradientBrush(I_, Color.White, Color.FromArgb(220, 220, 220), 90.0F)

        g.SmoothingMode = SmoothingMode.AntiAlias
        g.FillEllipse(B__, I_)
        g.FillEllipse(E_, x + O____ - N___, y + O____ - N___, O___ + A_, O___ + A_)
        g.SetClip(New Rectangle(x, y, size, size \ 2))
        g.FillEllipse(P_, I_)
        g.FillEllipse(N_, x + O____ - N___, y + O____ - N___, O___ + A_, O___ + A_)
        g.ResetClip()
        g.FillEllipse(E__, K_)
        g.DrawEllipse(Pens.Black, I_)

        Dim X_ As Integer = O___ - (N__)
        Dim X__ As Integer = (size \ 2) - (X_ \ 2)

        g.SmoothingMode = SmoothingMode.None
        g.DrawLine(M_, x, O__, x + size, O__)
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.FillEllipse(Brushes.Black, x + O____, y + O____, O___, O___)
        g.FillEllipse(Brushes.White, x + X__, y + X__, X_, X_)

        X_ -= A_
        X__ += (A_ \ 2)

        Dim X___ As New Rectangle(x + X__, y + X__, X_, X_)
        Dim X____ As New LinearGradientBrush(X___, Color.FromArgb(220, 220, 220), Color.White, 90.0F)

        g.FillEllipse(X____, X___)
        g.DrawEllipse(Pens.White, x + X__, y + X__, X_, X_)
        g.DrawEllipse(Pens.Black, x + X__ - 1, y + X__ - 1, X_ + 2, X_ + 2)
        g.SmoothingMode = B_

        X____.Dispose()
        P_.Dispose()
        B__.Dispose()
        E__.Dispose()
        M_.Dispose()
        E_.Dispose()
        N_.Dispose()
    End Sub