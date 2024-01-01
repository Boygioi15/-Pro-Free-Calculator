using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCalculator
{
    public partial class EquationSolving : Form
    {
        private int buttonClicked = -1;
        private CustomUserControls.RoundedButton.RoundedButton[] tabButtons;
        private Panel[] screenList;

        public EquationSolving()
        {
            InitializeComponent();

            tabButtons = new CustomUserControls.RoundedButton.RoundedButton[] { MenuPanel_SecondDeg, MenuPanel_ThirdDeg, MenuPanel_FourthDeg, MenuPanel_TwoUnknowns, MenuPanel_ThreeUnknowsMenu, MenuPanel_FourUnknowns };
            screenList = new Panel[] { PolyTwoDegreeScreen, PolyThreeDegreeScreen, PolyFourDegreeScreen, EquaTwoUnknowsScreen, EquaThreeUnknowsScreen, EquaFourUnknowsScreen };
        }
        private void SwitchScreen(Panel[] screenList)
        {
            foreach (Panel panel in screenList)
            {
                panel.Visible = false;
                panel.Enabled = false;
            }
        }

        private void Button_Deallocate(CustomUserControls.RoundedButton.RoundedButton[] tabButtons)
        {
            foreach (CustomUserControls.RoundedButton.RoundedButton button in tabButtons)
            {
                button.InteriorColor = SystemColors.Control;
                button.Enabled = true;
            }
        }

        private void Page1Button_Click(object sender, EventArgs e)
        {
            if (buttonClicked != 1)
            {
                //Swich screen button
                Button_Deallocate(tabButtons);
                buttonClicked = 1;
                MenuPanel_SecondDeg.InteriorColor = Color.WhiteSmoke;
                NamePageLabel.Text = "Polynomial Two Degree:";
                MenuPanel_SecondDeg.Enabled = false;

                //Switch Panel
                SwitchScreen(screenList);
                PolyTwoDegreeScreen.Visible = true;
                PolyTwoDegreeScreen.Enabled = true;
            }
        }

        private void Page2Button_Click(object sender, EventArgs e)
        {
            if (buttonClicked != 2)
            {
                //Swich screen button
                Button_Deallocate(tabButtons);
                buttonClicked = 2;
                MenuPanel_ThirdDeg.InteriorColor = Color.WhiteSmoke;
                NamePageLabel.Text = "Polynomial Three Degree:";
                MenuPanel_ThirdDeg.Enabled = false;

                //Switch Panel
                SwitchScreen(screenList);
                PolyThreeDegreeScreen.Visible = true;
                PolyThreeDegreeScreen.Enabled = true;
            }
        }

        private void Page3Button_Click(object sender, EventArgs e)
        {
            if (buttonClicked != 3)
            {
                //Swich screen button
                Button_Deallocate(tabButtons);
                buttonClicked = 3;
                MenuPanel_FourthDeg.InteriorColor = Color.WhiteSmoke;
                NamePageLabel.Text = "Polynomial Four Degree:";
                MenuPanel_FourthDeg.Enabled = false;

                //Switch Panel
                SwitchScreen(screenList);
                PolyFourDegreeScreen.Visible = true;
                PolyFourDegreeScreen.Enabled = true;
            }
        }

        private void Page4Button_Click(object sender, EventArgs e)
        {
            if (buttonClicked != 4)
            {
                //Swich screen button
                Button_Deallocate(tabButtons);
                buttonClicked = 4;
                MenuPanel_TwoUnknowns.InteriorColor = Color.WhiteSmoke;
                NamePageLabel.Text = "Equation Two Unknows:";
                MenuPanel_TwoUnknowns.Enabled = false;

                //Switch Panel
                SwitchScreen(screenList);
                EquaTwoUnknowsScreen.Visible = true;
                EquaTwoUnknowsScreen.Enabled = true;
            }
        }

        private void Page5Button_Click(object sender, EventArgs e)
        {
            if (buttonClicked != 5)
            {
                //Swich screen button
                Button_Deallocate(tabButtons);
                buttonClicked = 5;
                MenuPanel_ThreeUnknowsMenu.InteriorColor = Color.WhiteSmoke;
                NamePageLabel.Text = "Equation Three Unknows:";
                MenuPanel_ThreeUnknowsMenu.Enabled = false;

                //Switch Panel
                SwitchScreen(screenList);
                EquaThreeUnknowsScreen.Visible = true;
                EquaThreeUnknowsScreen.Enabled = true;
            }
        }

        private void Page6Button_Click(object sender, EventArgs e)
        {
            if (buttonClicked != 6)
            {
                //Swich screen button
                Button_Deallocate(tabButtons);
                buttonClicked = 6;
                MenuPanel_FourUnknowns.InteriorColor = Color.WhiteSmoke;
                NamePageLabel.Text = "Equation Four Unknows:";
                MenuPanel_FourUnknowns.Enabled = false;

                //Switch Panel
                SwitchScreen(screenList);
                EquaFourUnknowsScreen.Visible = true;
                EquaFourUnknowsScreen.Enabled = true;
            }
        }

        private void EquaFourUnknowsScreen_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.Black, 2);
                //Vertical line
                var point1 = new Point(30, 20);
                var point2 = new Point(30, 190);

                //Head horizontal line
                var point3 = new Point(30, 20);
                var point4 = new Point(40, 20);

                //Tail horizontal line
                var point5 = new Point(30, 190);
                var point6 = new Point(40, 190);

                g.DrawLine(p, point1, point2);
                g.DrawLine(p, point3, point4);
                g.DrawLine(p, point5, point6);
            }
        }

        private void EquaThreeUnknowsScreen_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.Black, 2);
                //Vertical line
                var point1 = new Point(80, 40);
                var point2 = new Point(80, 180);

                //Head horizontal line
                var point3 = new Point(80, 40);
                var point4 = new Point(90, 40);

                //Tail horizontal line
                var point5 = new Point(80, 180);
                var point6 = new Point(90, 180);

                g.DrawLine(p, point1, point2);
                g.DrawLine(p, point3, point4);
                g.DrawLine(p, point5, point6);
            }
        }

        private void EquaTwoUnknowsScreen_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.Black, 2);
                //Vertical line
                var point1 = new Point(100, 60);
                var point2 = new Point(100, 160);

                //Head horizontal line
                var point3 = new Point(100, 60);
                var point4 = new Point(110, 60);

                //Tail horizontal line
                var point5 = new Point(100, 160);
                var point6 = new Point(110, 160);

                g.DrawLine(p, point1, point2);
                g.DrawLine(p, point3, point4);
                g.DrawLine(p, point5, point6);
            }
        }

        private void NamePageLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
