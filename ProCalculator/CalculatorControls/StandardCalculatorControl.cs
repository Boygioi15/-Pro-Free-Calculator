using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProCalculator
{
    internal partial class StandardCalculatorControl
    {
        static private StandardCalculator form1;
        
        static public void Init(StandardCalculator f)
        {
            form1 = f;
        }
        static public void InsertContentAtCursor(string content)
        {

        }
        //number panel
        static public void OpenNumberPanel()
        {
            form1.MainPanel_OpenNumberPanelButton.BackColor = Color.FromArgb(205,239,139);
        }
        static public void CloseNumberPanel()
        {
            form1.MainPanel_OpenNumberPanelButton.BackColor = Color.White;
        }
        //func panel
        static public void OpenFuncPanel()
        {
            form1.MainPanel_OpenFuncPanelButton.BackColor = Color.FromArgb(255, 242, 204);
        }
        static public void CloseFuncPanel()
        {
            form1.MainPanel_OpenFuncPanelButton.BackColor = Color.White;
        }
        //memory panel
        static public void OpenMemoryPanel()
        {
            form1.MainPanel_OpenMemoryPanelButton.BackColor = Color.FromArgb(248, 206, 204);
        }
        static public void CloseMemoryPanel()
        {
            form1.MainPanel_OpenMemoryPanelButton.BackColor = Color.White;
        }
    }
}