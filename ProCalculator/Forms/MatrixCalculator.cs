using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomUserControls.MatrixOnDisplay;
using CustomUserControls.ResultLogComponent;
using ProCalculator.ClassLibraries;
using ProCalculator.ClassLibraries;

namespace ProCalculator
{
    public partial class MatrixCalculator : Form
    {
        //storage
        public MyMatrix[] matricesInMemory;
        public MatrixOnDisplayWithPostition[] STORAGE_matricesOnDisplay;
        public Label[] STORAGE_matricesOnDisplay_Label;
        public int currentStorageLength;
        bool isHoldingSlideBar = false;

        public bool mode = false; //0 - creating, 1 - calculating
        //result log
        internal List<ResultLogComponent> resultLogComponentList;
        public int currentResultLogLength = 0;
        public MatrixCalculator()
        {

            InitializeComponent();
            MatrixCalculatorControl.init(this);

            matricesInMemory = new MyMatrix[8];
            STORAGE_matricesOnDisplay = new MatrixOnDisplayWithPostition[8];
            STORAGE_matricesOnDisplay_Label = new Label[8];
            resultLogComponentList = new List<ResultLogComponent>();

            for (int i = 0; i < 8; i++)
            {
                matricesInMemory[i] = new MyMatrix(3, 3, 3);
                STORAGE_matricesOnDisplay[i] = new MatrixOnDisplayWithPostition();
                STORAGE_matricesOnDisplay_Label[i] = new Label();
                STORAGE_matricesOnDisplay[i].index = i;
            }
            initStorage();
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty
                          | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null,
                          StoragePanel, new object[] { true });
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty
                          | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null,
                          ResultPanel_ScrollPanel, new object[] { true });
            DoubleBuffered = true;
        }
        private void MatrixCalculator_Load(object sender, EventArgs e)
        {
            MatrixCalculatorControl.SwitchToCreatingMode();
            MatrixCalculatorControl.updateStoragePositionStartAfterPos(0);
            MatrixCalculatorControl.updateStorageLength();
            ResultPanel_ScrollPanel.MouseWheel += ResultPanel_ScrollPanel_MouseWheel;
            CREATING_MatrixOnDisplay.SizeChanged += (sender2, args) =>
            {
                reAlignMatrixMiddle();
            };
            WorkingPanel.SizeChanged += (sender2, args) =>
            {
                reAlignMatrixMiddle();
            };
        }

        //manually init
        private void initStorage()
        {
            STORAGE_matricesOnDisplay[0].Location = new Point(65, 40);
            STORAGE_matricesOnDisplay_Label[0].Location = new Point(5, 70);
            for (int i = 0; i < 8; i++)
            {
                STORAGE_matricesOnDisplay[i].BackColor = System.Drawing.Color.White;
                STORAGE_matricesOnDisplay[i].Collumn = 3;
                STORAGE_matricesOnDisplay[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
                STORAGE_matricesOnDisplay[i].Row = 3;
                STORAGE_matricesOnDisplay[i].SpacingHor = 5;
                STORAGE_matricesOnDisplay[i].SpacingVer = 5;
                STORAGE_matricesOnDisplay[i].Text = string.Empty;

                STORAGE_matricesOnDisplay[i].LoadFromMatrix(matricesInMemory[i]);

                STORAGE_matricesOnDisplay[i].SizeChanged += (sender, args) => {
                    MatrixOnDisplayWithPostition m = (sender as MatrixOnDisplayWithPostition);
                    MatrixCalculatorControl.updateStoragePositionStartAfterPos(m.index);
                    MatrixCalculatorControl.updateStorageLength();
                };
                STORAGE_matricesOnDisplay[i].Leave += (sender, args) =>
                {
                    MatrixOnDisplayWithPostition m = sender as MatrixOnDisplayWithPostition;
                    if (m.edited)
                    {
                        PromptSaveDown(m.index);
                    }
                };


                STORAGE_matricesOnDisplay_Label[i].Text = ((char)('A' + i)).ToString();
                STORAGE_matricesOnDisplay_Label[i].Text = ((char)('A' + i)).ToString() + " = ";
                STORAGE_matricesOnDisplay_Label[i].AutoSize = true;
                STORAGE_matricesOnDisplay_Label[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                STORAGE_matricesOnDisplay_Label[i].Size = new System.Drawing.Size(50, 30);

                StoragePanel.Controls.Add(STORAGE_matricesOnDisplay[i]);
                StoragePanel.Controls.Add(STORAGE_matricesOnDisplay_Label[i]);
            }
            MatrixExpression.MatrixStorage.init(matricesInMemory);
            StoragePanel_SlideClose.MouseDown += (sender, args) =>
            {
                isHoldingSlideBar = true;
            };
            StoragePanel_SlideClose.MouseUp += (sender, args) =>
            {
                isHoldingSlideBar = false;
            };
            StoragePanel_SlideClose.MouseMove += StoragePanel_SlideClose_MouseMove;
            StoragePanel.SizeChanged += StoragePanel_SizeChanged;
            StoragePanel.MouseWheel += StoragePanel_MouseWheel;
        }


        //open and close storage
        private void StoragePanel_SizeChanged(object sender, EventArgs e)
        {
            StoragePanel_SlideClose.Location = new Point(1, StoragePanel.Height - StoragePanel_SlideClose.Height);
            if (StoragePanel.Height > 70)
            {
                StoragePanel_ScrollBar.Location = new Point(1, StoragePanel.Height - StoragePanel_SlideClose.Height - StoragePanel_ScrollBar.Height);
            }
            ModePanel.Location = new Point(0, StoragePanel.Height + 1);
            WorkingPanel.Location = new Point(0, StoragePanel.Height + ModePanel.Height + 1);


            CREATING_WorkingPanel.Height = 340 + 240 - StoragePanel.Height;
            CALCULATING_ResultPanel.Height = 310 + 240 - StoragePanel.Height;
            ResultPanel_ScrollPanel.Height = 260 + 240 - StoragePanel.Height;
            WorkingPanel.Height = 375 + 240 - StoragePanel.Height;

            CALCULATING_InputPanel.Location = new Point(125, WorkingPanel.Height - 40);
            ResultPanel_ScrollBar.Height = ResultPanel_ScrollPanel.Height;
            ResultPanel_ScrollBar.LargeChange = ResultPanel_ScrollBar.Height;

        }
        private void StoragePanel_SlideClose_MouseMove(object sender, MouseEventArgs e)
        {
            int topBoundary = 50, botBoundary = 240;
            if (isHoldingSlideBar)
            {
                int desiredHeight = StoragePanel.Height + e.Y;
                if (desiredHeight > topBoundary && desiredHeight < botBoundary)
                {
                    StoragePanel.Height += e.Y;
                }
            }
        }

        //wheel storage
        private void StoragePanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (StoragePanel_ScrollBar.Value + StoragePanel_ScrollBar.SmallChange >= StoragePanel_ScrollBar.Maximum - StoragePanel_ScrollBar.LargeChange)
                {
                    StoragePanel_ScrollBar.Value = StoragePanel_ScrollBar.Maximum - StoragePanel_ScrollBar.LargeChange;
                }
                else
                {
                    StoragePanel_ScrollBar.Value += StoragePanel_ScrollBar.SmallChange;
                }

            }
            else
            {
                if (StoragePanel_ScrollBar.Value - StoragePanel_ScrollBar.SmallChange <= StoragePanel_ScrollBar.Minimum)
                {
                    StoragePanel_ScrollBar.Value = StoragePanel_ScrollBar.Minimum;
                }
                else
                {
                    StoragePanel_ScrollBar.Value -= StoragePanel_ScrollBar.SmallChange;
                }
            }
        }
        //control mechanic
        private void MatrixCalculator_MouseDown(object sender, MouseEventArgs e)
        {
            Controls[0].Select();
        }

        //change mode
        private void Creating_Click(object sender, EventArgs e)
        {
            MatrixCalculatorControl.SwitchToCreatingMode();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MatrixCalculatorControl.SwitchToCalculatingMode();
        }

        //done button
        private void CREATING_DoneButton_Click(object sender, EventArgs e)
        {
            if (!mode)
            {
                if (MatrixCalculatorControl.CheckBugAndStoreMainMatrixIntoMemory(CREATING_MatrixOnDisplay))
                {
                    //storage load matrix to display
                    int index = MatrixCalculatorControl.convertSymbolToIndex(ChooseWhereToSaveComboBox.Text[0]);
                    STORAGE_matricesOnDisplay[index].LoadFromMatrix(matricesInMemory[index]);
                }
            }
            else
            {
                if (MatrixCalculatorControl.CheckBugAndStoreResultLogIntoMemory())
                {
                    //storage load matrix to display
                    int index = MatrixCalculatorControl.convertSymbolToIndex(ChooseWhereToSaveComboBox.Text[0]);
                    STORAGE_matricesOnDisplay[index].LoadFromMatrix(matricesInMemory[index]);
                }
            }

        }

        private void AdditionalTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                MatrixCalculatorControl.CheckBugAndInitValueForMainMatrix(CREATING_MatrixOnDisplay, CREATING_InitValueTextBox.Text);
            }
        }


        //load main matrix from memory
        private void CREATING_ChooseWhereToLoadFrom_DropDownClosed(object sender, EventArgs e)
        {
            if (CREATING_LoadFromComboBox.Text == string.Empty)
            {
                return;
            }
            int index = MatrixCalculatorControl.convertSymbolToIndex(CREATING_LoadFromComboBox.Text.ToString()[0]);
            MatrixCalculatorControl.LoadMatrixFromMemory(CREATING_MatrixOnDisplay, index);
            CREATING_LoadFromComboBox.Text = String.Empty;
        }

        //relocation matrix so that it stays in middle
        private void reAlignMatrixMiddle()
        {
            //matrix
            int matrixX = (CREATING_WorkingPanel.Width - CREATING_MatrixOnDisplay.Width) / 2 + 40;
            int matrixY = (CREATING_WorkingPanel.Height - CREATING_MatrixOnDisplay.Height) / 2 + 10;
            CREATING_MatrixOnDisplay.Location = new Point(matrixX, matrixY);
            int labelX = matrixX - 100;
            int labelY = matrixY + (CREATING_MatrixOnDisplay.Height - CREATING_MatrixLabel.Height) / 2;
            CREATING_MatrixLabel.Location = new Point(labelX, labelY);
        }

        //storage
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            STORAGE_matricesOnDisplay[0].Location = new Point(65 - StoragePanel_ScrollBar.Value, STORAGE_matricesOnDisplay[0].Location.Y);
            STORAGE_matricesOnDisplay_Label[0].Location = new Point(5 - StoragePanel_ScrollBar.Value, STORAGE_matricesOnDisplay_Label[0].Location.Y);
            MatrixCalculatorControl.updateStoragePositionStartAfterPos(0);
        }
        private void PromptSaveDown(int index)
        {
            char symbol = (char)(index + 'A');
            DialogResult r = MessageBox.Show("Update " + symbol.ToString() + " in memory?", "", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                if (!MatrixCalculatorControl.CheckBugAndStoreSubMatrixIntoMemory(STORAGE_matricesOnDisplay[index], index))
                {
                    STORAGE_matricesOnDisplay[index].LoadFromMatrix(matricesInMemory[index]);
                }

            }
            else
            {
                STORAGE_matricesOnDisplay[index].LoadFromMatrix(matricesInMemory[index]);
            }
        }


        //result panel
        private void ResultPanel_ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            ResultPanel_ScrollPanel.Invalidate();
            if (resultLogComponentList.Count > 0)
            {
                resultLogComponentList[0].Location = new Point(10, 15 - ResultPanel_ScrollBar.Value);
                MatrixCalculatorControl.updateResultLogPositionStartAfterPos(0);
            }
        }

        private void ResultPanel_ScrollPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (ResultPanel_ScrollBar.Value + ResultPanel_ScrollBar.SmallChange >= ResultPanel_ScrollBar.Maximum - ResultPanel_ScrollBar.LargeChange)
                {
                    //init case
                    if (ResultPanel_ScrollBar.Maximum - ResultPanel_ScrollBar.LargeChange > 0)
                    {
                        ResultPanel_ScrollBar.Value = ResultPanel_ScrollBar.Maximum - ResultPanel_ScrollBar.LargeChange;
                    }
                }
                else
                {
                    ResultPanel_ScrollBar.Value += ResultPanel_ScrollBar.SmallChange;
                }

            }
            else
            {
                if (ResultPanel_ScrollBar.Value - ResultPanel_ScrollBar.SmallChange <= ResultPanel_ScrollBar.Minimum)
                {
                    ResultPanel_ScrollBar.Value = ResultPanel_ScrollBar.Minimum;
                }
                else
                {
                    ResultPanel_ScrollBar.Value -= ResultPanel_ScrollBar.SmallChange;
                }
            }
        }


        //input panel
        private void InputPanel_Go_MouseDown(object sender, MouseEventArgs e)
        {
            MatrixCalculatorControl.CheckValidityAndComputeResultOfMatrixExpression(InputPanel_InputTextbox.Text);
        }
        private void InputPanel_InputTextbox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                MatrixCalculatorControl.CheckValidityAndComputeResultOfMatrixExpression(InputPanel_InputTextbox.Text);
            }
        }

    }
}