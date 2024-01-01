using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System;

using ProCalculator.ClassLibraries;
namespace CustomUserControls.RoundedButton
{
    partial class RoundedButton : RoundedPanel.RoundedPanel
    {
     
        /// <Button behavior>
        /// 
        /// </summary>
        Color _mouseHoveringBackColor = Color.DimGray;
        Color _mouseClickBackColor = Color.Gray;

        bool _mouseIsHovering = false;
        bool _mouseIsHolding = false;
        /// <Text>
        /// 
        /// </Text>
        Label buttonLabel;
        [Category("Behavior")]
        public Color MouseHoveringBackColor
        {
            get
            {
                return _mouseHoveringBackColor;
            }
            set{
                if (value != _mouseHoveringBackColor)
                {
                    _mouseHoveringBackColor = value;
                    Invalidate();
                }
            }
        }
        [Category("Behavior")]
        public Color MouseClickBackColor
        {
            get
            {
                return _mouseClickBackColor;
            }
            set
            {
                if (value != _mouseClickBackColor)
                {
                    _mouseClickBackColor = value;
                    Invalidate();
                }
            }
        }
        [Category("Text")]
        public string TextContent
        {
            get { return buttonLabel.Text; }
            set
            {
                if (value != buttonLabel.Text)
                {
                    if (value != string.Empty)
                    {
                        buttonLabel.Text = value;
                    }
                }
            }
        }
        [Category("Text")]
        public Color TextColor
        {
            get { return buttonLabel.ForeColor; }
            set
            {
                if (value != buttonLabel.ForeColor)
                {
                    buttonLabel.ForeColor = value;
                }
            }
        }
        [Category("Text")]
        public Font TextFont
        {
            get { return buttonLabel.Font; }
            set
            {
                if (value != buttonLabel.Font)
                {
                    if (value != null)
                    {
                        buttonLabel.Font = value;
                    }
                }
            }
        }
        [Category("Text")]
        public ContentAlignment TextAlignment
        {
            get { return buttonLabel.TextAlign; }
            set
            {
                if (value != buttonLabel.TextAlign)
                {
                    buttonLabel.TextAlign = value;
                }
            }
        }
        public RoundedButton()
        {
            DoubleBuffered = true;

            buttonLabel = new Label();
            buttonLabel.AutoSize = false;
            buttonLabel.TextAlign = ContentAlignment.MiddleCenter;
            buttonLabel.SendToBack();
            buttonLabel.BackColor = Color.Transparent;
            updateLabel();

            buttonLabel.MouseDown += (sender, args) =>
            {
                _mouseIsHolding = true;
                base.OnMouseDown(args);
                base.OnClick(args);
                Invalidate();
            };
            buttonLabel.MouseUp += (sender, args) =>
            {
                _mouseIsHolding = false;
                base.OnMouseUp(args);
                Invalidate();
            };
            buttonLabel.MouseLeave += (sender, args) =>
            {
                base.OnMouseLeave(args);
                _mouseIsHovering = false;
                Invalidate();
            };
            buttonLabel.MouseEnter += (sender, args) =>
            {
                base.OnMouseEnter(args);
                _mouseIsHovering = true;
                Invalidate();
            };
            Controls.Add(buttonLabel);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            Color backgroundColor = InteriorColor;
            if (_mouseIsHolding)
            {
                backgroundColor = MouseClickBackColor;
            }
            else if(_mouseIsHovering)
            {
                backgroundColor = MouseHoveringBackColor;
            }
            //fill inside
            SolidBrush b = new SolidBrush(backgroundColor);

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
            if(BorderWidth == 0)
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

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            BackColor = Color.Transparent;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
            updateLabel();
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            this.Invalidate();
        }

        private void updateLabel()
        {
            buttonLabel.Width = Width;
            buttonLabel.Height = Height;
            buttonLabel.Location = new Point(0, 0);
        }
    }
}