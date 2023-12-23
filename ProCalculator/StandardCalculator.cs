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
        bool dockingOn = false;
        bool modeChoosingPanelOn = false;

        private bool onINVMode = false;
        private bool radianMode = true, degreeMode = false;

        //display invalid result timeout
        public bool onDisplayingResult = false;
        public int invalidModeTimeout = 10000; //in ms
        public int invalidModeTimeLeft;
        public Timer invalidModeTimeoutTimer;

        //fake label onTop of in/output
        bool fakeInputLabelOn = true, fakeOutputLabelOn = true;

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
            NumberPanel.Visible = false;
            FunctionPanel.Visible = false;
            MemoryPanel.Visible = false;
            STATE_Radian.PerformClick();
            //MainPanel_OutputTextbox.Text = MainPanel_InputTextBox.Text.Substring(0, 10);
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
        /// <open panel>
        /// open panel
        /// </open panel>
        private void MainPanel_OpenNumberPanelButton_Click(object sender, EventArgs e)
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
        private void MainPanel_OpenFuncPanelButton_Click(object sender, EventArgs e)
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
        private void MainPanel_OpenMemoryPanelButton_Click(object sender, EventArgs e)
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

        //on top
        private void MainPanel_OnTopButton_Click(object sender, EventArgs e)
        {

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
                StandardCalculatorControl.InsertContentAtCursor("*");
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
            StandardCalculatorControl.InsertContentAtCursor("%");
        }
        private void OPER_Div_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("÷");
        }
        private void OPER_Mul_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("*");
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
            StandardCalculatorControl.InsertContentAtCursor("sin(");
        }
        private void FUNC_Cos_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("cos(");
        }
        private void FUNC_Tan_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("tan(");
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
            StandardCalculatorControl.InsertContentAtCursor("√");
        }
        private void FUNC_Not_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("!");
        }
        private void FUNC_Pi_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("𝝅");
        }
        private void FUNC_EulerNumber_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("e");
        }
        private void STATE_INV_Click(object sender, EventArgs e)
        {
            if (onINVMode == false)
            {
                onINVMode = true;
                StandardCalculatorControl.Toggle_INVMode();
            }
            else
            {
                onINVMode = false;
                StandardCalculatorControl.Toggle_NormalMode();
            }
        }

        private void FUNC_Logarit_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("log(");
        }

        private void FUNC_Ln_Click(object sender, EventArgs e)
        {
            StandardCalculatorControl.InsertContentAtCursor("ln(");
        }
        private void STATE_Radian_Click(object sender, EventArgs e)
        {
            if (degreeMode == true)
            {
                radianMode = true;
                degreeMode = false;
                StandardCalculatorControl.RadianMode();

                STATE_Radian.BackColor = Color.FromArgb(205, 239, 139);
                STATE_Degree.BackColor = Color.FromArgb(245, 247, 250);

                STATE_Radian.Enabled = false;
                STATE_Degree.Enabled = true;
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
            if (radianMode == true)
            {
                radianMode = false;
                degreeMode = true;
                StandardCalculatorControl.DegreeMode();

                STATE_Degree.BackColor = Color.FromArgb(205, 239, 139);
                STATE_Radian.BackColor = Color.FromArgb(245, 247, 250);

                STATE_Degree.Enabled = false;
                STATE_Radian.Enabled = true;
            }
        }
        private void BUGBUG_MouseClick(object sender, MouseEventArgs e)
        {
            StandardCalculatorControl.CheckValidityAndComputeResult();
        }
        
    }
}
