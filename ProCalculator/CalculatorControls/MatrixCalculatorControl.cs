using CustomUserControls.MatrixOnDisplay;
using CustomUserControls.ResultLogComponent;
using MatrixExpression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace ProCalculator
{
    //control 
    //input
    internal partial class MatrixCalculatorControl
    {
        //form we are working with
        static private MatrixCalculator form1;
        static public void init(MatrixCalculator f)
        {
            form1 = f;
        }
        static public void SwitchToCreatingMode()
        {
            //Creating on top
            form1.Creating.BackColor = Color.FromArgb(255, 230, 204);
            form1.Calculating.BackColor = Color.WhiteSmoke;
            form1.Creating.BringToFront();

            //change save label;
            form1.StoreMatrixLabel.Text = "Save matrix as";
            form1.CALCULATING_ChooseLogToSave.Hide();

            form1.mode = false;
            //make log textbox appear
            //show and hide panel
            form1.CREATING_WorkingPanel.Show();
            form1.CALCULATING_InputPanel.Hide();
            form1.CALCULATING_ResultPanel.Hide();
        }
        static public void SwitchToCalculatingMode()
        {
            //Calculating on top
            form1.Creating.BackColor = Color.WhiteSmoke;
            form1.Calculating.BackColor = Color.FromArgb(255, 230, 204);
            form1.Calculating.BringToFront();

            //change save label;
            form1.StoreMatrixLabel.Text = "Save log         as";
            form1.CALCULATING_ChooseLogToSave.Show();

            form1.mode = true;
            //show and hide panel
            form1.CREATING_WorkingPanel.Hide();
            form1.CALCULATING_InputPanel.Show();
            form1.CALCULATING_ResultPanel.Show();
        }

        //store
        static public bool CheckBugAndStoreMainMatrixIntoMemory(MatrixOnDisplay matrix)
        {
            if (form1.ChooseWhereToSaveComboBox.Text.ToString().Length == 0)
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Unable to store matrix into memory\n + Did not specify where to save\n";
                return false;
            }
            int index = convertSymbolToIndex(form1.ChooseWhereToSaveComboBox.Text.ToString()[0]);
            bool succeeded = false;
            string errorMessage = string.Empty;
            matrix.StoreIntoMatrix(form1.matricesInMemory[index], ref succeeded, ref errorMessage);
            if (!succeeded)
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Unable to store matrix into memory\n" + " + " + errorMessage;
                return false;
            }
            else
            {
                form1.MessageContent.ForeColor = Color.Green;
                DialogResult r = MessageBox.Show("Update matrix " + form1.ChooseWhereToSaveComboBox.Text.ToString() + " in memory?", "", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    form1.MessageContent.Text = "Store matrix into " + form1.ChooseWhereToSaveComboBox.Text.ToString()[0] + " successfully";
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static public bool CheckBugAndStoreSubMatrixIntoMemory(MatrixOnDisplay matrix, int whereToSave)
        {
            bool succeeded = false;
            string errorMessage = string.Empty;
            matrix.StoreIntoMatrix(form1.matricesInMemory[whereToSave], ref succeeded, ref errorMessage);
            if (!succeeded)
            {
                MessageBox.Show("Unable to store matrix into memory\n" + " + " + errorMessage);
                return false;
            }
            else
            {
                char symbol = (char)(whereToSave + 'A');
                MessageBox.Show("Update matrix " + symbol + " in memory successfully");
                return true;
            }
        }

        //load
        static public void LoadMatrixFromMemory(MatrixOnDisplay matrix, int whereToLoadFrom)
        {
            DialogResult rs = MessageBox.Show("Discard the current matrix?", "", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                form1.CREATING_MatrixOnDisplay.LoadFromMatrix(form1.matricesInMemory[whereToLoadFrom]);
            }
        }

        //init value
        static public bool CheckBugAndInitValueForMainMatrix(MatrixOnDisplay matrix, string initValue)
        {
            if (Expression_Helper.isNumber(initValue))
            {
                double val = double.Parse(initValue);
                matrix.FillInUnfilledTextboxes(val);
                return true;
            }
            else
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Init value is invalid\n + Please enter correct nunber format";
                return false;
            }
        }

        //storage updateStoragePosition
        static public void updateStoragePositionStartAfterPos(int startPos)
        {
            for (int i = startPos + 1; i < 8; i++)
            {
                int startPosForLabel = form1.STORAGE_matricesOnDisplay[i - 1].Location.X + form1.STORAGE_matricesOnDisplay[i - 1].Width + 40;
                int startPosForMatrix = startPosForLabel + 50;
                form1.STORAGE_matricesOnDisplay_Label[i].Location = new Point(startPosForLabel, form1.STORAGE_matricesOnDisplay_Label[i - 1].Location.Y);
                form1.STORAGE_matricesOnDisplay[i].Location = new Point(startPosForMatrix, form1.STORAGE_matricesOnDisplay[i - 1].Location.Y);
            }
        }
        static public void updateStorageLength()
        {
            int size = 0;
            for (int i = 0; i < form1.STORAGE_matricesOnDisplay.Length; i++)
            {
                size += form1.STORAGE_matricesOnDisplay_Label[i].Width + form1.STORAGE_matricesOnDisplay[i].Width + 40;
            }
            form1.currentStorageLength = size;
            form1.StoragePanel_ScrollBar.Maximum = size;
            return;
        }
        static public int convertSymbolToIndex(char symbol)
        {
            if (symbol >= 'A' || symbol <= 'H')
            {
                return symbol - 'A';
            }
            else
            {
                return -1;
            }
        }

        //result log
        static public void CheckValidityAndComputeResultOfMatrixExpression(string matrixExpression)
        {
            if (matrixExpression == string.Empty)
            {
                return;
            }
            MatrixExpression.MatrixExpression e = new MatrixExpression.MatrixExpression();
            e.MatrixExpressionContent = matrixExpression;
            e.preProcess();
            e.buildComputingTree();
            if (e.checkOverallValidity())
            {
                Result res = e.computeResult();
                if (e.checkComputingValidity())
                {
                    ResultLogComponent resultLogComponent = new ResultLogComponent();
                    form1.ResultPanel_ScrollPanel.Controls.Add(resultLogComponent);
                    resultLogComponent.loadExpressionContent(matrixExpression);
                    if (res.type)
                    {
                        //load number onto the result log
                        resultLogComponent.loadResultNumber(res.number_val);
                    }
                    else
                    {
                        //load matrix onto the result log
                        resultLogComponent.loadResultMatrix(res.matrix_val);
                    }
                    //init data
                    pushNewResultLogComponentIntoList(resultLogComponent);
                }
                else
                {
                    MessageBox.Show("Calculating error!!!\n Please view the message log for detailed information");
                    form1.MessageContent.ForeColor = Color.Red;
                    form1.MessageContent.Text = e.getErrorMessage();
                }
            }
            else
            {
                MessageBox.Show("Syntax error!!!\n Please view the message log for detailed information");
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = e.getErrorMessage();
            }
        }
        static public void pushNewResultLogComponentIntoList(ResultLogComponent comp)
        {
            //handle location
            if (form1.resultLogComponentList.Count == 0)
            {
                comp.Location = new Point(10, 15);
                comp.Index = 1;
            }
            else
            {
                ResultLogComponent last = form1.resultLogComponentList[form1.resultLogComponentList.Count - 1];
                comp.Location = new Point(10, last.Location.Y + last.Height + 10);
                comp.Index = form1.resultLogComponentList.Count + 1;
            }
            //handle event
            comp.quitButton.Click += (sender, arg) =>
            {
                Label lb = sender as Label;
                ResultLogComponent parent = lb.Parent as ResultLogComponent;
                removeResultLogComponentAtPos(parent.Index - 1);
            };
            form1.resultLogComponentList.Add(comp);
            updateResultLogLength();
            updateResultScrollBarValue();
        }
        static public void removeResultLogComponentAtPos(int pos)
        {
            //handle location
            if (form1.resultLogComponentList.Count == 0)
            {
                return;
            }
            form1.resultLogComponentList.RemoveRange(pos, 1);
            for (int i = pos; i < form1.resultLogComponentList.Count; i++)
            {
                form1.resultLogComponentList[i].Index = i + 1;
            }
            if (form1.resultLogComponentList.Count == 0)
            {
                return;
            }
            if (pos == 0)
            {
                form1.resultLogComponentList[0].Location = new Point(10, 15);
                updateResultLogPositionStartAfterPos(0);
            }
            else
            {
                updateResultLogPositionStartAfterPos(pos - 1);
            }

            updateResultLogLength();
            updateResultScrollBarValue();
        }
        static public void updateResultLogLength()
        {
            int size = 0;
            for (int i = 0; i < form1.resultLogComponentList.Count; i++)
            {
                size += form1.resultLogComponentList[i].Height + 10;
            }
            form1.currentResultLogLength = size;
        }
        static public void updateResultScrollBarValue()
        {
            form1.ResultPanel_ScrollBar.Maximum = form1.currentResultLogLength + 20;
            form1.ResultPanel_ScrollBar.LargeChange = form1.ResultPanel_ScrollPanel.Height;
            form1.ResultPanel_ScrollBar.SmallChange = form1.ResultPanel_ScrollBar.LargeChange / 5;
        }
        static public void updateResultLogPositionStartAfterPos(int startPos)
        {
            for (int i = startPos + 1; i < form1.resultLogComponentList.Count; i++)
            {
                int posY = form1.resultLogComponentList[i - 1].Location.Y + form1.resultLogComponentList[i - 1].Height + 10;
                form1.resultLogComponentList[i].Location = new Point(10, posY);
            }
        }

        static public bool CheckBugAndStoreResultLogIntoMemory()
        {
            //check index is valid
            //check if index >= 1 || >= count;
            if (!Expression_Helper.isNumber(form1.CALCULATING_ChooseLogToSave.Text))
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Unable to store result\n +Log index is not a number!";
                return false;
            }
            int index;
            bool succeeded = int.TryParse(form1.CALCULATING_ChooseLogToSave.Text, out index);
            if (!succeeded)
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Unable to store result\n +Log index value is invalid!";
                return false;
            }
            if (!(index >= 1 && index <= form1.resultLogComponentList.Count))
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Unable to store result\n +Log index out of range!";
                return false;
            }

            //check where to save is valid
            if (form1.ChooseWhereToSaveComboBox.Text.ToString().Length == 0)
            {
                form1.MessageContent.ForeColor = Color.Red;
                form1.MessageContent.Text = "Unable to store matrix into memory\n +Did not specify where to save\n";
                return false;
            }
            //check result is valid
            DialogResult r = MessageBox.Show("Update matrix " + form1.ChooseWhereToSaveComboBox.Text.ToString() + " in memory?", "", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                int saveIndex = convertSymbolToIndex(form1.ChooseWhereToSaveComboBox.Text.ToString()[0]);

                bool succeeded2 = false;
                string errorMessage = string.Empty;
                form1.resultLogComponentList[index - 1].resultMatrix.StoreIntoMatrix(form1.matricesInMemory[saveIndex], ref succeeded2, ref errorMessage);
                if (form1.resultLogComponentList[index - 1].whatToDisplay)
                {
                    MessageBox.Show("Unable to store matrix into memory\n +Result to save was a number");
                    form1.MessageContent.ForeColor = Color.Red;
                    form1.MessageContent.Text = "Unable to store matrix into memory\n +Result to save was a number";
                    return false;
                }
                else
                {
                    MessageBox.Show("Updated matrix " + form1.ChooseWhereToSaveComboBox.Text.ToString() + " successfully");
                    form1.MessageContent.ForeColor = Color.Green;
                    form1.MessageContent.Text = "Updated matrix " + form1.ChooseWhereToSaveComboBox.Text.ToString() + " successfully";
                    return true;
                }
            }
            else
            {
                return false;
            }
            //prompt want to save?
            //save
        }
    }
}