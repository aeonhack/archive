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
class StudioTheme : ThemeContainer152
{


    private GraphicsPath Path;
    public StudioTheme()
    {
        MoveHeight = 30;
        BackColor = Color.FromArgb(20, 40, 70);
        TransparencyKey = Color.Fuchsia;

        SetColor("Sides", 50, 70, 100);
        SetColor("Gradient1", 65, 85, 115);
        SetColor("Gradient2", 50, 70, 100);
        SetColor("Hatch1", 20, 40, 70);
        SetColor("Hatch2", 40, 60, 90);
        SetColor("Shade1", 15, Color.Black);
        SetColor("Shade2", Color.Transparent);
        SetColor("Border1", 12, 32, 62);
        SetColor("Border2", 20, Color.Black);
        SetColor("Border3", 30, Color.White);
        SetColor("Border4", Color.Black);
        SetColor("Text", Color.White);

        Path = new GraphicsPath();
    }

    private Color C1;
    private Color C2;
    private Color C3;
    private Color C4;
    private Color C5;
    private Pen P1;
    private Pen P2;
    private Pen P3;
    private Pen P4;
    private Pen P5;
    private HatchBrush B1;

    private SolidBrush B2;
    protected override void ColorHook()
    {
        P1 = new Pen(TransparencyKey, 3);
        P2 = new Pen(GetColor("Border1"));
        P3 = new Pen(GetColor("Border2"), 2);
        P4 = new Pen(GetColor("Border3"));
        P5 = new Pen(GetColor("Border4"));

        C1 = GetColor("Sides");
        C2 = GetColor("Gradient1");
        C3 = GetColor("Gradient2");
        C4 = GetColor("Shade1");
        C5 = GetColor("Shade2");

        B1 = new HatchBrush(HatchStyle.LightDownwardDiagonal, GetColor("Hatch1"), GetColor("Hatch2"));
        B2 = new SolidBrush(GetColor("Text"));

        BackColor = GetColor("Hatch2");
    }


    private Rectangle RT1;
    protected override void PaintHook()
    {
        G.DrawRectangle(P1, ClientRectangle);
        G.SetClip(Path);

        G.Clear(C1);
        DrawGradient(C2, C3, 0, 0, Width, 30);

        RT1 = new Rectangle(12, 30, Width - 24, Height - 12 - 30);
        G.FillRectangle(B1, RT1);

        DrawGradient(C4, C5, 12, 30, Width - 24, 30);

        DrawBorders(P2, RT1);
        DrawBorders(P3, 14, 32, Width - 26, Height - 12 - 32);

        DrawText(B2, HorizontalAlignment.Left, 12, 0);

        DrawBorders(P4, 1);

        G.ResetClip();
        G.DrawPath(P5, Path);
    }

    protected override void OnResize(EventArgs e)
    {
        Path.Reset();
        Path.AddLines(new Point[] {
			new Point(2, 0),
			new Point(Width - 3, 0),
			new Point(Width - 1, 2),
			new Point(Width - 1, Height - 3),
			new Point(Width - 3, Height - 1),
			new Point(2, Height - 1),
			new Point(0, Height - 3),
			new Point(0, 2),
			new Point(2, 0)
		});

        base.OnResize(e);
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
class StudioButton : ThemeControl152
{

    public StudioButton()
    {
        Transparent = true;
        BackColor = Color.Transparent;

        SetColor("DownGradient1", 45, 65, 95);
        SetColor("DownGradient2", 65, 85, 115);
        SetColor("NoneGradient1", 65, 85, 115);
        SetColor("NoneGradient2", 45, 65, 95);
        SetColor("Shine1", 30, Color.White);
        SetColor("Shine2A", 30, Color.White);
        SetColor("Shine2B", Color.Transparent);
        SetColor("Shine3", 20, Color.White);
        SetColor("TextShade", 50, Color.Black);
        SetColor("Text", Color.White);
        SetColor("Glow", 10, Color.White);
        SetColor("Border", 20, 40, 70);
        SetColor("Corners", 20, 40, 70);
    }

    private Color C1;
    private Color C2;
    private Color C3;
    private Color C4;
    private Color C5;
    private Color C6;
    private Color C7;
    private Pen P1;
    private Pen P2;
    private Pen P3;
    private SolidBrush B1;
    private SolidBrush B2;

    private SolidBrush B3;
    protected override void ColorHook()
    {
        C1 = GetColor("DownGradient1");
        C2 = GetColor("DownGradient2");
        C3 = GetColor("NoneGradient1");
        C4 = GetColor("NoneGradient2");
        C5 = GetColor("Shine2A");
        C6 = GetColor("Shine2B");
        C7 = GetColor("Corners");

        P1 = new Pen(GetColor("Shine1"));
        P2 = new Pen(GetColor("Shine3"));
        P3 = new Pen(GetColor("Border"));

        B1 = new SolidBrush(GetColor("TextShade"));
        B2 = new SolidBrush(GetColor("Text"));
        B3 = new SolidBrush(GetColor("Glow"));
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

        G.DrawLine(P1, 1, 1, Width, 1);
        DrawGradient(C5, C6, 1, 1, 1, Height);
        DrawGradient(C5, C6, Width - 2, 1, 1, Height);
        G.DrawLine(P2, 1, Height - 2, Width, Height - 2);

        if (State == MouseState.Down)
        {
            DrawText(B1, HorizontalAlignment.Center, 2, 2);
            DrawText(B2, HorizontalAlignment.Center, 1, 1);
        }
        else
        {
            DrawText(B1, HorizontalAlignment.Center, 1, 1);
            DrawText(B2, HorizontalAlignment.Center, 0, 0);
        }

        if (State == MouseState.Over)
        {
            G.FillRectangle(B3, ClientRectangle);
        }

        DrawBorders(P3);
        DrawCorners(C7, 1, 1, Width - 2, Height - 2);

        DrawCorners(BackColor);
    }
}