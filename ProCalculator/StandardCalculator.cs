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
        bool numberPanelOn = false, funcPanelOn = false, memoryPanelOn = false;
        bool isDocking = false;
        bool modeChoosingPanelOn = false;
        public StandardCalculator()
        {
            StandardCalculatorControl.Init(this);
            InitializeComponent();
        }
        private void StandardCalculator_Load(object sender, EventArgs e)
        {
            
        }

        //open panel
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

        private void MainPanel_OnTopButton_Click(object sender, EventArgs e)
        {

        }
    }
}
