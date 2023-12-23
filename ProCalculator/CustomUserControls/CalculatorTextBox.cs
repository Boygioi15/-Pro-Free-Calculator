using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace CustomUserControls.CalculatorTextBox
{
    //draw fat cursor
    public partial class CalculatorTextbox : System.Windows.Forms.TextBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        //blinking cursor drawing
        Timer tmr = null;
        bool phase = false;
        int tickInterval = 1000;
        PictureBox verticalBar = null;
        Label useToMeasureLength = null;

        //blinking cursor position
        int currentCursorPosition = 0;

        //public properties
        [Category("Blinking Cursor")]
        public int TickInterval
        {
            get
            {
                return tickInterval;
            }
            set
            {
                if (tickInterval != value)
                {
                    if (value < 1)
                    {
                        tickInterval = 1;
                    }
                    else
                    {
                        tickInterval = value;
                    }
                    
                    tmr.Interval = tickInterval;
                }
            }
        }
        [Category("Blinking Cursor")]
        public int BlinkingCursorWidth
        {
            get
            {
                return verticalBar.Width;
            }
            set
            {
                if(verticalBar.Width != value)
                {
                    if (value < 0)
                    {
                        verticalBar.Width = 0;
                    }
                    else
                    {
                        verticalBar.Width = value;
                    }
                }
            }
        }
        [Category("Blinking Cursor")]
        public int CurrentCursorPosition
        {
            get
            {
                return currentCursorPosition;
            }
            set
            {
                if (currentCursorPosition != value)
                {
                    if (value < 0 || value > Text.Length)
                    {
                        if (value == 0)
                        {
                            currentCursorPosition = 0;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }     
                    }
                    else
                    {
                        currentCursorPosition = value;
                    }
                }
            }
        }

        public CalculatorTextbox()
        {
            //v bar
            verticalBar = new PictureBox();
            verticalBar.Size = new Size(2, Height);
            verticalBar.BackColor = Color.Black;

            //tmr
            tmr = new Timer();
            tmr.Interval = tickInterval;
            tmr.Tick += Tmr_Tick;
            tmr.Enabled = true;
            tmr.Start();

            useToMeasureLength = new Label();
            useToMeasureLength.Location = new Point(-6, 15);
            useToMeasureLength.Font = new Font(Font.Name, (float)(Font.Size - 0.6));
            useToMeasureLength.AutoSize = true;
            useToMeasureLength.BackColor = Color.White;
            useToMeasureLength.UseMnemonic = false;
            useToMeasureLength.Hide();

            //hide the old cursor
            GotFocus += (sender, args) =>
            {
                HideCaret(Handle);
            };
            SizeChanged += (sender, args) =>
            {
                verticalBar.Height = Height;   
            };
            FontChanged += (sender, args) =>
            {
                useToMeasureLength.Font = new Font(Font.Name, (float)(Font.Size - 0.6));
            };
            
            Controls.Add(verticalBar);
            Controls.Add(useToMeasureLength);
        }
        protected override void OnHandleCreated( EventArgs e)
        {
            base.OnHandleCreated( e );
        }
        public void ShowBlinkingCursor()
        {
            tmr.Enabled = true;
            tmr.Start();
            verticalBar.Show();
        }
        public void HideBlinkingCursor()
        {
            tmr.Enabled = false;
            tmr.Stop();
            verticalBar.Hide();
        }
        public void ImmediatelyDrawBlinkingCursor()
        {
            phase = true;
            DrawBlinkingCursor();
        }      
        //redraw blinking cursor
        private void Tmr_Tick(object sender, EventArgs e)
        {
            DrawBlinkingCursor();
        }
        private void DrawBlinkingCursor()
        {
            //useToMeasureTextLength.Text = currentCursorPosition.ToString();
            if (phase)
            {
                if (currentCursorPosition == 0)
                {
                    verticalBar.Location = new Point(0, 0);
                }
                else
                {

                    useToMeasureLength.Text = Text.Substring(0, currentCursorPosition).ToString();
                    int realXOffset = useToMeasureLength.Width - 6 - 8;
                    int startIndex = GetCharIndexFromPosition(new Point(0, 0));
                    if (startIndex != 0)
                    {
                        useToMeasureLength.Text = Text.Substring(0, startIndex);
                        int startXOffset = useToMeasureLength.Width - 6 - 10;
                        int finalXOffset = realXOffset - startXOffset;
                        if (finalXOffset > Width - verticalBar.Width)
                        {
                            finalXOffset = Width - verticalBar.Width;
                        }
                        verticalBar.Location = new Point(finalXOffset, 0);
                    }
                    else
                    {
                        if (realXOffset > Width - verticalBar.Width)
                        {
                            realXOffset = Width - verticalBar.Width;
                        }
                        verticalBar.Location = new Point(realXOffset, 0);
                    }
                }
                verticalBar.Show();
            }
            else
            {
                verticalBar.Hide();
            }
            phase = !phase;
            //this.Text = xInPoints.ToString();
        }

    }
}