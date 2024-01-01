using CustomUserControls.RoundedButton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCalculator
{
    public partial class StandardCalculator : Form
    {
        //panel mode
        public bool numberPanelOn = false, funcPanelOn = false, memoryPanelOn = false;
        public bool formOpenerPanelOn = false;
        bool dockingOn = false;
        public bool darkModeOn = false;

        //calculating
        public bool onINVMode = false;
        public bool unitForFunction = false; //false: RAD, true: DEG
        public string ans = string.Empty;

        //display invalid result timeout
        public bool onDisplayingResult = false;
        public int invalidModeTimeout = 10000; //in ms
        public int invalidModeTimeLeft;
        public Timer invalidModeTimeoutTimer;

        //fake label onTop of in/output
        bool fakeInputLabelOn = true, fakeOutputLabelOn = true;

        //hold button
        bool isHoldingButton = false;
        Timer holdButtonTimer = null;
        RoundedButton currentlyHoldingButton = null;
        /// <INIT>
        /// 
        /// </summary>
        public StandardCalculator()
        {
            KeyPreview = true;
            StandardCalculatorControl.Init(this);
            InitializeComponent();

            initInputTextbox();
            initOutputTextbox();
            initHoldEventForButton();
            //tmr for 
            invalidModeTimeoutTimer = new Timer();
            invalidModeTimeoutTimer.Interval = 1000;//check for each 100ms
            invalidModeTimeoutTimer.Tick += (sender, args) =>
            {
                invalidModeTimeLeft -= invalidModeTimeout;
                if(invalidModeTimeLeft <= 0)
                {
                    StandardCalculatorControl.TurnOffDisplayInvalidResultMode();
                }
            };

            holdButtonTimer = new Timer();    
            holdButtonTimer.Tick += (sender, args) =>
            {
                if (isHoldingButton)
                {
                    string content = currentlyHoldingButton.TextContent;
                    if(content == "Back")
                    {
                        StandardCalculatorControl.DeleteContentAtCursor();
                    }
                    else
                    {
                        StandardCalculatorControl.InsertContentAtCursor(content);
                    }
                    holdButtonTimer.Interval = 75;
                }
            };
            GotFocus += (sender, args) =>
            {
                MainPanel_InputTextBox.ShowBlinkingCursor();
            };
            LostFocus += (sender, args) =>
            {
                MainPanel_InputTextBox.HideBlinkingCursor();
            };

            //steal focus mechanism
            Select();
        }
        private void StandardCalculator_Load(object sender, EventArgs e)
        {
            //render priority
            NumberPanel.BringToFront();
            FunctionPanel.BringToFront();
            MainPanel.BringToFront();
            DummyPanel.SendToBack();
            MemoryPanel.SendToBack();

            FunctionPanel.Location = new Point(0, MainPanel.Height - FunctionPanel.Height);
            MemoryPanel.Location = new Point(MainPanel.Width - MemoryPanel.Width, 0);
            NumberPanel.Location = new Point(0, MainPanel.Height - NumberPanel.Height);
            FormOpenerPanel.Location = new Point(-243, 12);

            StandardCalculatorControl.TurnOnNormalModeForFuncPanel();
            StandardCalculatorControl.SwitchToRadianMode();
            MainPanel_OnTopButtonWithDeclineMark.Visible = false;

        }
        private void initInputTextbox()
        {
            MainPanel_InputTextBox.Click += (sender, args) =>
            {
                MainPanel_InputTextBox.CurrentCursorPosition = MainPanel_InputTextBox.SelectionStart;
                MainPanel_InputTextBox.ImmediatelyDrawBlinkingCursor();
                MainPanel_InputTextBox.ShowBlinkingCursor();
            };
            MainPanel_InputTextBox.TextChanged += (sender, args) =>
            {
                if (fakeInputLabelOn)
                {
                    fakeInputLabelOn = false;
                    MainPanel_PromptUserToEnter.Hide();
                }
            };
            MainPanel_InputTextBox.GotFocus += (sender, args) =>
            {
                if (fakeInputLabelOn)
                {
                    MainPanel_PromptUserToEnter.Hide();
                    fakeInputLabelOn = false;
                }
                MainPanel_InputTextBox.ImmediatelyDrawBlinkingCursor();
            };
            MainPanel_InputTextBox.LostFocus += (sender, args) =>
            {
                if (MainPanel_InputTextBox.Text.Length == 0)
                {
                    if (!fakeInputLabelOn)
                    {
                        MainPanel_PromptUserToEnter.Show();
                        fakeInputLabelOn = true;
                    }
                }
            };
            MainPanel_PromptUserToEnter.Click += (sender, args) =>
            {
                MainPanel_InputTextBox.Select();
                MainPanel_InputTextBox.ShowBlinkingCursor();
            };
        }
        private void initOutputTextbox()
        {
            MainPanel_OutputTextbox.TextChanged += (sender, args) =>
            {
                if (MainPanel_OutputTextbox.Text.Length == 0)
                {
                    if (!fakeOutputLabelOn)
                    {
                        MainPanel_NotifyOutput.Show();
                        fakeOutputLabelOn = true;
                    }
                }
                else
                {
                    if (fakeOutputLabelOn)
                    {
                        MainPanel_NotifyOutput.Hide();
                        fakeOutputLabelOn = false;
                    }
                }
            };
        }

        private void initHoldEventForButton()
        {
            CTRL_Del.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = CTRL_Del;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            CTRL_Del.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            CTRL_Del.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_0.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_0;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_0.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_0.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_1.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_1;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_1.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_1.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };

            // Repeat the above structure for NUMB_2 through NUMB_9, adjusting the references accordingly
            // For NUMB_2
            NUMB_2.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_2;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_2.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_2.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_3.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_3;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_3.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_3.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_4.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_4;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_4.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_4.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };

            // For NUMB_5
            NUMB_5.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_5;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_5.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_5.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_6.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_6;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_6.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_6.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_7.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_7;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_7.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_7.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };

            // For NUMB_8
            NUMB_8.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_8;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_8.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_8.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };

            // For NUMB_9
            NUMB_9.MouseDown += (sender, args) =>
            {
                isHoldingButton = true;
                currentlyHoldingButton = NUMB_9;
                holdButtonTimer.Enabled = true; holdButtonTimer.Interval = 500;
            };
            NUMB_9.MouseUp += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
            NUMB_9.MouseLeave += (sender, args) =>
            {
                isHoldingButton = false;
                holdButtonTimer.Enabled = false;
            };
        }
        /// <open panel>
        /// open panel
        /// </open panel>
        /// 
        private void MainPanel_OpenNumberPanelButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!numberPanelOn)
            {
                StandardCalculatorControl.OpenNumberPanel();
            }
            else
            {
                StandardCalculatorControl.CloseNumberPanel();
            }
            numberPanelOn = !numberPanelOn;
        }
        private void MainPanel_OpenFuncPanelButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!funcPanelOn)
            {

                StandardCalculatorControl.OpenFuncPanel();
            }
            else
            {
                StandardCalculatorControl.CloseFuncPanel();
            }
            funcPanelOn = !funcPanelOn;
        }

        private void MainPanel_OpenMemoryPanelButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!memoryPanelOn)
            {
                StandardCalculatorControl.OpenMemoryPanel();
            }
            else
            {
                StandardCalculatorControl.CloseMemoryPanel();
            }
            memoryPanelOn = !memoryPanelOn;
        }
        private void FormOpenerPanel_CloseButton_Click(object sender, EventArgs e)
        {
            if (formOpenerPanelOn)
            {
                StandardCalculatorControl.CloseFormOpenerPanel();
            }
            else
            {
                Console.WriteLine("Error in form opener");
            }
            formOpenerPanelOn = false;
        }
        private void MainPanel_OpenFormOpenerPanelButton_Click(object sender, EventArgs e)
        {
            if (!formOpenerPanelOn)
            {
                StandardCalculatorControl.OpenFormOpenerPanel();
            }
            else
            {
                Console.WriteLine("Error in form opener");
            }
            formOpenerPanelOn = true;
        }
        //on top
        private void MainPanel_OnTopButton_Click(object sender, EventArgs e)
        {
            if (!dockingOn)
            {
                StandardCalculatorControl.TurnOnDockingMode();
            }
            else
            {
                Console.WriteLine("? Error in On top button");
            }
            dockingOn = true;
        }
        private void MainPanel_OnTopButtonWithDeclineMark_Click(object sender, EventArgs e)
        {
            if (dockingOn)
            {
                StandardCalculatorControl.TurnOffDockingMode();
            }
            else
            {
                Console.WriteLine("? Error in On top with decline button");
            }
            dockingOn = false;
        }
        /// <Insert from keyboard>
        /// 
        /// <insert>
        private void StandardCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            //keys that is the same at all mode
            //numpad
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                //number case
                string key = e.KeyCode.ToString();
                if (key.Length > 1)
                    key = key.Replace("NumPad", "").Replace("D", "");
                StandardCalculatorControl.InsertContentAtCursor(key);
                return;
            }

            //dot, comma, backslash           
            else if (e.KeyCode == Keys.Oemcomma)
            {
                StandardCalculatorControl.InsertContentAtCursor(",");
            }
            else if (e.KeyCode == Keys.OemQuestion || e.KeyCode == Keys.Divide)
            {
                StandardCalculatorControl.InsertContentAtCursor("/");
            }
            else if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
            {
                StandardCalculatorControl.InsertContentAtCursor(".");
            }

            //+,-
            else if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                StandardCalculatorControl.InsertContentAtCursor("+");
            }
            else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            {
                StandardCalculatorControl.InsertContentAtCursor("-");
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                StandardCalculatorControl.ClearCalculatorScreen();
            }

            //* at the right
            else if (e.KeyCode == Keys.Multiply)
            {
                StandardCalculatorControl.InsertContentAtCursor("x");
            }
            //char
            else if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                StandardCalculatorControl.InsertContentAtCursor(e.KeyCode.ToString().ToLower());
            }

            //left, right
            else if (e.KeyCode.Equals(Keys.Right))
            {
                if (MainPanel_InputTextBox.CurrentCursorPosition < MainPanel_InputTextBox.Text.Length)
                {
                    MainPanel_InputTextBox.CurrentCursorPosition++;
                }
                else
                {
                    MainPanel_InputTextBox.CurrentCursorPosition = 0;
                }
                MainPanel_InputTextBox.ImmediatelyDrawBlinkingCursor();
            }
            else if (e.KeyCode.Equals(Keys.Left))
            {
                if (MainPanel_InputTextBox.CurrentCursorPosition > 0)
                {
                    MainPanel_InputTextBox.CurrentCursorPosition--;
                }
                else
                {
                    MainPanel_InputTextBox.CurrentCursorPosition = MainPanel_InputTextBox.Text.Length;
                }
                MainPanel_InputTextBox.ImmediatelyDrawBlinkingCursor();
            }

            //enter 
            else if (e.KeyCode == Keys.Enter)
            {
                StandardCalculatorControl.CheckValidityAndComputeResult();
                return;
            }
            else if( e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                StandardCalculatorControl.DeleteContentAtCursor();
                return;
            }

            ///Key that differ betwwen mode
            //shift mode mode
            if (ModifierKeys == Keys.Shift)
            {
                if (e.KeyCode == Keys.D1)
                {
                    StandardCalculatorControl.InsertContentAtCursor("!");
                }
                else if (e.KeyCode == Keys.D5)
                {
                    StandardCalculatorControl.InsertContentAtCursor("%");
                }
                else if (e.KeyCode == Keys.D6)
                {
                    StandardCalculatorControl.InsertContentAtCursor("^");
                }
                else if (e.KeyCode == Keys.D8)
                {
                    StandardCalculatorControl.InsertContentAtCursor("*");
                }
                else if (e.KeyCode == Keys.D9)
                {
                    StandardCalculatorControl.InsertContentAtCursor("(");
                }                  
                else if (e.KeyCode == Keys.D0)
                {
                    StandardCalculatorControl.InsertContentAtCursor(")");
                }               
            }
            //normal mode
            else
            {
                if (e.KeyCode>=Keys.D0 && e.KeyCode<=Keys.D9)
                {
                    //number case
                    string key = e.KeyCode.ToString();
                    if (key.Length > 1)
                        key = key.Replace("NumPad", "").Replace("D", "");
                    StandardCalculatorControl.InsertContentAtCursor(key);
                    return;
                }
            }
            
        }
        /// <insert>
        /// 
        /// <insert>

        //Insert Number Panel
        private void NUMB_0_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("0");
        }
        private void NUMB_1_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("1");
        }
        private void NUMB_2_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("2");
        }
        private void NUMB_3_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("3");
        }
        private void NUMB_4_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("4");
        }
        private void NUMB_5_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("5");
        }
        private void NUMB_6_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("6");
        }
        private void NUMB_7_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("7");
        }
        private void NUMB_8_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("8");
        }
        private void NUMB_9_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("9");
        }
        private void FUNC_Percentage_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.NegateCurrentInput();
        }
        private void OPER_Div_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("/");
        }
        private void OPER_Mul_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("x");
        }
        private void OPER_Plus_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("+");
        }
        private void OPER_Sub_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("-");
        }
        private void MISC_Dot_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor(".");
        }

        //Insert Function Panel
        private void FUNC_Sin_Click(object sender, EventArgs e)
        {
            if (onINVMode)
            {
                StandardCalculatorControl.InsertContentAtCursor("asin(");
            }
            else
            {
                StandardCalculatorControl.InsertContentAtCursor("sin(");
            }
        }
        private void FUNC_Cos_Click(object sender, EventArgs e)
        {

            if (onINVMode)
            {
                StandardCalculatorControl.InsertContentAtCursor("acos(");
            }
            else
            {
                StandardCalculatorControl.InsertContentAtCursor("cos(");
            }
        }
        private void FUNC_Tan_Click(object sender, EventArgs e)
        {
            if (onINVMode)
            {
                StandardCalculatorControl.InsertContentAtCursor("atan(");
            }
            else
            {
                StandardCalculatorControl.InsertContentAtCursor("tan(");
            }
        }
        private void MISC_OpenBracket_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("(");
        }
        private void MISC_CloseBracket_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor(")");
        }
        private void FUNC_Pow_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("^");
        }
        private void FUNC_Sqrt_Click(object sender, EventArgs e)
        {
            if (!onINVMode)
            {
                StandardCalculatorControl.InsertContentAtCursor("√");
            }
            else
            {
                StandardCalculatorControl.InsertContentAtCursor("^2");
            }
        }
        private void FUNC_Not_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("!");
        }
        private void FUNC_Pi_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("π");
        }
        private void FUNC_EulerNumber_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("e");
        }
        private void STATE_INV_Click(object sender, EventArgs e)
        {
            if (!onINVMode)
            {
                StandardCalculatorControl.TurnOnINVModeForFuncPanel();
            }
            else
            {
                StandardCalculatorControl.TurnOnNormalModeForFuncPanel();
            }
            onINVMode = !onINVMode;
        }

        private void FUNC_Logarit_Click(object sender, EventArgs e)
        {
            if (!onINVMode)
            {
                StandardCalculatorControl.InsertContentAtCursor("log(");
            }
            else
            {
                StandardCalculatorControl.InsertContentAtCursor("10^");
            }
            
        }

        private void FUNC_Ln_Click(object sender, EventArgs e)
        {
            if (!onINVMode)
            {
                StandardCalculatorControl.InsertContentAtCursor("ln(");
            }
            else
            {
                StandardCalculatorControl.InsertContentAtCursor("e^");
            }
        }
        private void STATE_Radian_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.SwitchToRadianMode();
            unitForFunction = false;
        }

        private void CTRL_Ac_MouseUp(object sender, MouseEventArgs e)
        {
            StandardCalculatorControl.ClearCalculatorScreen();
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormOpenerPanel_MatrixOpener_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.OpenMatrixCalculator();
        }

        private void FormOpenerPanel_EquationOpener_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.OpenEquationSolving();
        }

        private void FormOpenerPanel_GraphOpener_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.OpenGraphCalculator();
        }
        private void FormOpenerPanel_ConverterOpener_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.OpenConverter();
        }



        private void FormOpenerPanel_LightTheme_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.SwitchToLightTheme();
            darkModeOn = false;
        }

        private void FormOpenerPanel_DarkTheme_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.SwitchToDarkTheme();
            darkModeOn = true;
        }

        private void FunctionPanel_LocationChanged(object sender, EventArgs e)
        {
            int currentHeight = System.Math.Max(MainPanel.Height, NumberPanel.Location.Y + NumberPanel.Height);
            currentHeight = System.Math.Max(currentHeight, FunctionPanel.Location.Y + FunctionPanel.Height);
            Height = currentHeight+47;
            MemoryPanel.Height = currentHeight;
        }

        private void NumberPanel_LocationChanged(object sender, EventArgs e)
        {
            int currentHeight = System.Math.Max(MainPanel.Height, NumberPanel.Location.Y + NumberPanel.Height);
            currentHeight = System.Math.Max(currentHeight, FunctionPanel.Location.Y + FunctionPanel.Height);
            Height = currentHeight+47;
            MemoryPanel.Height = currentHeight;
        }

        private void MemoryPanel_LocationChanged(object sender, EventArgs e)
        {
            Width = MemoryPanel.Width + MemoryPanel.Location.X + 20;
        }

        private void MISC_Ans_Click(object sender, EventArgs e)
        {
            if (ans != string.Empty)
            {
                StandardCalculatorControl.InsertContentAtCursor(ans);
            }
        }

        private void CTRL_Equal_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.CheckValidityAndComputeResult();
        }

        private void CTRL_Del_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.DeleteContentAtCursor();
        }

        private void STATE_Degree_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.SwitchToDegreeMode();
            unitForFunction = true;
        }
        
    }
}
