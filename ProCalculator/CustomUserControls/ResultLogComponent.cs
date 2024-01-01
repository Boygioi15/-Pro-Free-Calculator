using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System;

using ProCalculator.ClassLibraries;
namespace CustomUserControls.ResultLogComponent
{
   partial class ResultLogComponent: Panel
    {
        //main panel -- inherited from panel;
        Label IndexLabel;
        TextBox ExpressionTextbox;
        Label EqualSign;
        
        public bool whatToDisplay = false; //false: matrix, true: number
        public CustomUserControls.MatrixOnDisplay.MatrixOnDisplay resultMatrix;
        public Label resultNumber;
        int _borderWidth = 3;
        Color _borderColor = Color.Black;
        Color _interiorColor = Color.White;
        int _arcSize = 15;

        //quit button;
        public Label quitButton;

        //built in index
        int _index = 0;
        public int Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    IndexLabel.Text = _index.ToString() + ".";
                }
            }
        }
        //only use odd number for consistency
        [Category("Border")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (value != _borderWidth)
                {
                    if(value < 1)
                    {
                        _borderWidth = 1;
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
        public ResultLogComponent()
        {
            DoubleBuffered = true;
            ExpressionTextbox = new TextBox();
            EqualSign = new Label();
            IndexLabel = new Label();   
            resultNumber = new Label();
            resultMatrix = new MatrixOnDisplay.MatrixOnDisplay();

            quitButton = new Label();

            ExpressionTextbox.Font = new Font("Comic Sans MS", 13F);
            ExpressionTextbox.ReadOnly = true;
            ExpressionTextbox.Width = 200;
            ExpressionTextbox.Height = 34;
            ExpressionTextbox.AutoSize = false;
            ExpressionTextbox.BackColor = InteriorColor;
            ExpressionTextbox.BorderStyle = BorderStyle.None;

            EqualSign.Size = new Size(40, 25);
            EqualSign.Font = new Font("Comic Sans MS", 14F);
            EqualSign.Text = "=";
            EqualSign.BackColor = InteriorColor;

            IndexLabel.Font = new Font("Comic Sans MS", 13F);
            IndexLabel.Width = 40;
            IndexLabel.Height = 34;
            IndexLabel.Text = "0.";
            IndexLabel.BackColor = InteriorColor;
            IndexLabel.ForeColor = Color.Black;
            IndexLabel.BorderStyle = BorderStyle.None;

            resultNumber.Font = new Font("Comic Sans MS", 13F);
            resultNumber.ForeColor = Color.Black;
            resultNumber.AutoSize = true;
            resultNumber.BackColor = InteriorColor;
   
            resultMatrix.BackColor = InteriorColor;
            resultMatrix.SpacingHor = 5;
            resultMatrix.SpacingVer = 2;
            resultMatrix.TextboxBackColor = Color.White;
            resultMatrix.NumberOfDisplayedDigitInTextbox = 3;
            resultMatrix.Font = new Font("Comic Sans MS", 9.8F);

            quitButton.ForeColor = Color.Brown;
            quitButton.Location = new Point(573, 0);
            quitButton.AutoSize = true;
            quitButton.BackColor = Color.Transparent;
            quitButton.Font = new Font("Comic Sans MS", 12F);
            quitButton.Text = "X";
            quitButton.MouseEnter += (Sender, args) =>
            {
                quitButton.ForeColor = Color.Red;
            };
            quitButton.MouseLeave += (Sender, args) =>
            {
                quitButton.ForeColor = Color.Brown;
            };
            //dispose
            quitButton.MouseClick += (Sender, args) =>
            {
                Dispose();
            };
            resultNumber.Hide();
            resultMatrix.Hide();

            Controls.Add(ExpressionTextbox);
            Controls.Add(EqualSign);
            Controls.Add(IndexLabel);

            Controls.Add(resultNumber);
            Controls.Add(resultMatrix);
            Controls.Add(quitButton);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Width = 600;    
            alignContentInMiddle();
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            BackColor = Color.Transparent;
        }

        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        protected override void OnPaint(PaintEventArgs e) {
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

            Pen p = new Pen(BorderColor, _borderWidth);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //draw Border;
            //because pen draw at middle, so we need to offset borderWidth/2
            Point LineTopStart = new Point(_arcSize, _borderWidth/2);
            Point LineTopEnd = new Point(Width-_arcSize, _borderWidth / 2);

            Point LineLeftStart = new Point(_borderWidth / 2, _arcSize);
            Point LineLeftEnd = new Point(_borderWidth / 2, Height-_arcSize);

            //offset so that border is fully shown
            Point LineRightStart = new Point(Width - _borderWidth/2 - 1, _arcSize);
            Point LineRightEnd = new Point(Width - _borderWidth/2 - 1, Height - _arcSize);

            Point LineBotStart = new Point(_arcSize, Height - _borderWidth / 2 -1);
            Point LineBotEnd = new Point(Width - _arcSize, Height - _borderWidth/2 -1);

            g.DrawLine(p, LineTopStart, LineTopEnd);
            g.DrawLine(p, LineLeftStart, LineLeftEnd);
            g.DrawLine(p, LineRightStart, LineRightEnd);
            g.DrawLine(p, LineBotStart, LineBotEnd);

            g.DrawArc(p, ArcTopLeft, 180, 90);
            g.DrawArc(p, ArcTopRight, 270, 90);
            g.DrawArc(p, ArcBotLeft, 90, 90);
            g.DrawArc(p, ArcBotRight, 0, 90);           
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            alignContentInMiddle();
        }

    }
    partial class ResultLogComponent : Panel
    {
        private void alignContentInMiddle()
        {
            //position text in middle
            int IndexLabelStartPointX = 35;
            int ExpressionTextboxStartPointX = 90;
            int EqualSignStartPointX = 280;
            int ResultStartPointX = 330;

            IndexLabel.Location = new Point(IndexLabelStartPointX, (Height - IndexLabel.Height) / 2);
            ExpressionTextbox.Location = new Point(ExpressionTextboxStartPointX, (Height-ExpressionTextbox.Height)/2);
            EqualSign.Location = new Point(EqualSignStartPointX, (Height - EqualSign.Height) / 2 - 5);
            if (whatToDisplay)
            {
                resultNumber.Location = new Point(ResultStartPointX, (Height - resultNumber.Height) / 2);
            }
            else
            {
                resultMatrix.Location = new Point(ResultStartPointX, (Height - resultMatrix.Height) / 2);
            }
        }

        //load only one, can't change later
        public void loadResultMatrix(MyMatrix matrix)
        {
            resultMatrix.Show();
            whatToDisplay = false;
            resultMatrix.LoadFromMatrix(matrix);
            Height = resultMatrix.Height + 16;        
            resultMatrix.turnOnDisplayMode();
        }
        public void loadResultNumber(double number)
        {
            resultNumber.Show();
            whatToDisplay = true;
            resultNumber.Text = number.ToString();      
            Height = resultNumber.Height + 16;
        }
        public void loadExpressionContent(string content)
        {
            ExpressionTextbox.Text = content;
        }
        
    }
}