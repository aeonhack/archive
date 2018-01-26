using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

//------------------
//Creator: aeonhack
//Site: elitevs.net
//Created: 9/23/2011
//Changed: 9/23/2011
//Version: 1.0.0
//Theme Base: 1.5.2
//------------------
class GenuineTheme : ThemeContainer152
{

    public GenuineTheme()
    {
        MoveHeight = 30;
        BackColor = Color.FromArgb(41, 41, 41);
        TransparencyKey = Color.Fuchsia;

        SetColor("Back", 41, 41, 41);
        SetColor("Gradient1", 25, 25, 25);
        SetColor("Gradient2", 41, 41, 41);
        SetColor("Line1", 25, 25, 25);
        SetColor("Line2", 58, 58, 58);
        SetColor("Text", Color.White);
        SetColor("Border1", 58, 58, 58);
        SetColor("Border2", Color.Black);
    }

    private Color C1;
    private Color C2;
    private Color C3;
    private Pen P1;
    private Pen P2;
    private Pen P3;
    private Pen P4;

    private SolidBrush B1;
    protected override void ColorHook()
    {
        C1 = GetColor("Back");
        C2 = GetColor("Gradient1");
        C3 = GetColor("Gradient2");

        P1 = new Pen(GetColor("Line1"));
        P2 = new Pen(GetColor("Line2"));
        P3 = new Pen(GetColor("Border1"));
        P4 = new Pen(GetColor("Border2"));

        B1 = new SolidBrush(GetColor("Text"));

        BackColor = C1;
    }

    protected override void PaintHook()
    {
        G.Clear(C1);

        DrawGradient(C2, C3, 0, 0, Width, 28);

        G.DrawLine(P1, 0, 28, Width, 28);
        G.DrawLine(P2, 0, 29, Width, 29);

        DrawText(B1, HorizontalAlignment.Left, 7, 0);

        DrawBorders(P3, 1);
        DrawBorders(P4);

        DrawCorners(TransparencyKey);
    }

}

//------------------
//Creator: aeonhack
//Site: elitevs.net
//Created: 9/23/2011
//Changed: 9/23/2011
//Version: 1.0.0
//Theme Base: 1.5.2
//------------------
class GenuineButton : ThemeControl152
{

    public GenuineButton()
    {
        SetColor("DownGradient1", 41, 41, 41);
        SetColor("DownGradient2", 51, 51, 51);
        SetColor("NoneGradient1", 51, 51, 51);
        SetColor("NoneGradient2", 41, 41, 41);
        SetColor("Text", Color.White);
        SetColor("Border1", 12, Color.White);
        SetColor("Border2", 25, 25, 25);
    }

    private Color C1;
    private Color C2;
    private Color C3;
    private Color C4;
    private SolidBrush B1;
    private Pen P1;

    private Pen P2;
    protected override void ColorHook()
    {
        C1 = GetColor("DownGradient1");
        C2 = GetColor("DownGradient2");
        C3 = GetColor("NoneGradient1");
        C4 = GetColor("NoneGradient2");

        B1 = new SolidBrush(GetColor("Text"));

        P1 = new Pen(GetColor("Border1"));
        P2 = new Pen(GetColor("Border2"));
    }

    protected override void PaintHook()
    {
        if (State == MouseState.Down)
        {
            DrawGradient(C1, C2, ClientRectangle, 90f);
        }
        else
        {
            DrawGradient(C3, C4, ClientRectangle, 90f);
        }

        DrawText(B1, HorizontalAlignment.Center, 0, 0);

        DrawBorders(P1, 1);
        DrawBorders(P2);

        DrawCorners(BackColor);
    }

}