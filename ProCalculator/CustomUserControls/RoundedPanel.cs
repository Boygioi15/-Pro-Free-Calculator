using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System;

using ProCalculator.ClassLibraries;
namespace CustomUserControls.RoundedPanel
{
    partial class RoundedPanel : Panel
    {

        protected int _borderWidth = 3;
        protected Color _borderColor = Color.Black;
        protected Color _interiorColor = Color.White;
        protected int _arcSize = 15;

        //only use odd number for consistency
        [Category("Border")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (value != _borderWidth)
                {
                    if (value < 0)
                    {
                        _borderWidth = 0;
                    }
                    _borderWidth = value;
                    Invalidate();
                }
            }
        }
        [Category("Border")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (value != _borderColor)
                {
                    _borderColor = value;
                    Invalidate();
                }
            }
        }
        [Category("Border")]
        public Color InteriorColor
        {
            get { return _interiorColor; }
            set
            {
                if (value != _interiorColor)
                {
                    _interiorColor = value;
                    Invalidate();
                }
            }
        }
        [Category("Border")]
        public int ArcSize
        {
            get { return _arcSize; }
            set
            {
                if (value != _arcSize)
                {
                    if (value < 1)
                    {
                        _arcSize = 1;
                    }
                    _arcSize = value;
                    Invalidate();
                }
            }
        }
        public RoundedPanel()
        {
            DoubleBuffered = true;
            
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            isPlayingAnimation = false;
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            BackColor = Color.Transparent;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //fill inside
            SolidBrush b = new SolidBrush(InteriorColor);

            Rectangle mainRect = new Rectangle(ArcSize, 0, Width - 2 * ArcSize, Height);
            Rectangle leftRect = new Rectangle(0, ArcSize, ArcSize, Height - 2 * ArcSize);
            Rectangle rightRect = new Rectangle(Width - ArcSize, ArcSize, ArcSize, Height - 2 * ArcSize);

            Rectangle ArcTopLeft = new Rectangle(_borderWidth / 2, _borderWidth / 2, ArcSize * 2, ArcSize * 2);
            Rectangle ArcTopRight = new Rectangle(Width - ArcSize * 2 - _borderWidth / 2 - 1, _borderWidth / 2, ArcSize * 2, ArcSize * 2);
            Rectangle ArcBotLeft = new Rectangle(_borderWidth / 2, Height - ArcSize * 2 - _borderWidth / 2 - 1, ArcSize * 2, ArcSize * 2);
            Rectangle ArcBotRight = new Rectangle(Width - ArcSize * 2 - _borderWidth / 2 - 1, Height - ArcSize * 2 - _borderWidth / 2 - 1, ArcSize * 2, ArcSize * 2);

            g.FillRectangle(b, mainRect);
            g.FillRectangle(b, leftRect);
            g.FillRectangle(b, rightRect);

            g.FillEllipse(b, ArcTopLeft);
            g.FillEllipse(b, ArcTopRight);
            g.FillEllipse(b, ArcBotLeft);
            g.FillEllipse(b, ArcBotRight);

            //draw border
            if (BorderWidth == 0)
            {
                return;
            }
            Pen p = new Pen(BorderColor, _borderWidth);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //draw Border;
            //because pen draw at middle, so we need to offset borderWidth/2
            Point LineTopStart = new Point(_arcSize, _borderWidth / 2);
            Point LineTopEnd = new Point(Width - _arcSize, _borderWidth / 2);

            Point LineLeftStart = new Point(_borderWidth / 2, _arcSize);
            Point LineLeftEnd = new Point(_borderWidth / 2, Height - _arcSize);

            //offset so that border is fully shown
            Point LineRightStart = new Point(Width - _borderWidth / 2 - 1, _arcSize);
            Point LineRightEnd = new Point(Width - _borderWidth / 2 - 1, Height - _arcSize);

            Point LineBotStart = new Point(_arcSize, Height - _borderWidth / 2 - 1);
            Point LineBotEnd = new Point(Width - _arcSize, Height - _borderWidth / 2 - 1);

            g.DrawLine(p, LineTopStart, LineTopEnd);
            g.DrawLine(p, LineLeftStart, LineLeftEnd);
            g.DrawLine(p, LineRightStart, LineRightEnd);
            g.DrawLine(p, LineBotStart, LineBotEnd);

            g.DrawArc(p, ArcTopLeft, 180, 90);
            g.DrawArc(p, ArcTopRight, 270, 90);
            g.DrawArc(p, ArcBotLeft, 90, 90);
            g.DrawArc(p, ArcBotRight, 0, 90);
        }
    }
    //animation
    partial class RoundedPanel : Panel
    {
        Timer animationTmr;
        public bool isPlayingAnimation = false;
        double realPosX, realPosY;
        public void StartSliding(Point start, Point end, int duration, int deltaT)
        {
            if (isPlayingAnimation)
            {
                return;
            }
            int step = duration / deltaT;
            double offsetXForEachStep = (end.X - start.X) / (double)step;
            double offsetYForEachStep = (end.Y - start.Y) / (double)step;

            animationTmr = new Timer();
            animationTmr.Interval = deltaT;
            animationTmr.Tick += (sender, args) =>
            {
                realPosX += offsetXForEachStep;
                realPosY += offsetYForEachStep;
                Location = new Point((int)realPosX, (int)realPosY);
                //finish
                if (step <= 1)
                {
                    isPlayingAnimation = false;
                    Location = end;
                    animationTmr.Stop();
                    animationTmr.Dispose(); 
                }
                step--;
            };
            Location = start;
            realPosX = Location.X; 
            realPosY = Location.Y;
            isPlayingAnimation = true;
            animationTmr.Enabled = true;
            animationTmr.Start();
            
        }
        public void StopSliding() {
            animationTmr.Stop();
            isPlayingAnimation = false;
        }
    }
}