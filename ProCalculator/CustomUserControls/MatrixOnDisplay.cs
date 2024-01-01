using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System;

using ProCalculator.ClassLibraries;
namespace CustomUserControls.MatrixOnDisplay
{
    public class TextBoxWithPosition : TextBox
    {
        public int row, col;
        public bool filledIn = false;
    }
    public class MatrixOnDisplay : Panel
    {
        //Fields
        protected int spacing_Hor = 10;
        protected int spacing_Ver = 10;
            
        protected int row = 3;
        protected int col = 3;

        protected int maxRow = 5;
        protected int maxCol = 5;

        protected int fixedHeight;
        protected int fixedWidth;

        public bool edited = false;

        protected int[] collumnWidths;
        public TextBoxWithPosition[,] textBoxMatrix;

        //text box properties
        protected Color textboxBackColor = Color.White;
        protected int numOfDisplayedDigitInTextbox = 4;
        Label useToMeasureLength;

        //Properties
        [Category("Dimension")]
        public int Row
        {
            get { return row; }
            set
            {
                if (row != value)
                {
                    if (value < 1)
                    {
                        row = 1;
                    }
                    else if (value > maxRow)
                    {
                        row = maxRow;
                    }
                    else
                    {
                        row = value;
                    }
                    showSectionOfTextboxes(row, col);
                    updateControlSize();
                    this.Invalidate();
                }
            }
        }
        [Category("Dimension")]
        public int Collumn
        {
            get { return col; }
            set
            {
                if (col != value)
                {
                    if (value < 1)
                    {
                        col = 1;
                    }
                    else if (value > maxCol)
                    {
                        col = maxCol;
                    }
                    else
                    {
                        col = value;
                    }
                    edited = true;
                    showSectionOfTextboxes(row, col);
                    updateControlSize();
                    this.Invalidate();
                }
            }
        }
        [Category("Spacing")]
        public int SpacingHor
        {
            get { return spacing_Hor; }
            set
            {
                if (spacing_Hor != value)
                {
                    if (spacing_Hor < 0)
                    {
                        spacing_Hor = 0;
                    }
                    else
                    {
                        spacing_Hor = value;
                    }
                    edited = true;
                    updateTextboxesPosition();
                    updateControlSize();
                    this.Invalidate();
                }    
            }
        }
        [Category("Spacing")]
        public int SpacingVer
        {
            get { return spacing_Ver; }
            set
            {
                if (spacing_Ver != value)
                {
                    if (spacing_Ver < 0)
                    {
                        spacing_Ver = 0;
                    }
                    else
                    {
                        spacing_Ver = value;
                    }
                    edited = true;
                    updateTextboxesPosition();
                    updateControlSize();
                    this.Invalidate();
                }
            }
        }

        [Category("TextBox")]
        public Color TextboxBackColor
        {
            get
            {
                return textboxBackColor;
            }
            set
            {
                if(value != textboxBackColor)
                    textboxBackColor = value;
                    for(int i = 0; i < maxRow; i++)
                    {
                        for(int j = 0; j < maxCol; j++) {
                            textBoxMatrix[i,j].BackColor = textboxBackColor;
                        }
                    }
                }
            }
        //this property should be set before loading number;
        [Category("TextBox")]
        public int NumberOfDisplayedDigitInTextbox
        {
            get
            {
                return numOfDisplayedDigitInTextbox;
            }
            set
            {
                if (value != numOfDisplayedDigitInTextbox)
                {
                if (value < 1)
                {
                        numOfDisplayedDigitInTextbox = 1;
                }
                numOfDisplayedDigitInTextbox = value;
                }
            }
        }
        //Constructor
        public MatrixOnDisplay()
        {
            DoubleBuffered = true;
            this.Font = new Font("Microsoft San Serif", 13);
            this.Size = new Size(150, 150);
            this.BackColor = Color.WhiteSmoke;
            this.ForeColor = Color.Black;
            base.Text = string.Empty;

            useToMeasureLength = new Label();
            useToMeasureLength.Font = this.Font;
            useToMeasureLength.Text = string.Empty;
            useToMeasureLength.Width = (int)Font.Size * NumberOfDisplayedDigitInTextbox + 15;
            useToMeasureLength.AutoSize = true;
            useToMeasureLength.Hide();

            collumnWidths = new int[5];
            this.FontChanged += MatrixOnDisplay_FontChanged;

            textBoxMatrix = new TextBoxWithPosition[maxRow, maxCol];
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    textBoxMatrix[i, j] = new TextBoxWithPosition();
                    textBoxMatrix[i, j].AutoSize = true;
                    textBoxMatrix[i, j].Font = this.Font;
                    textBoxMatrix[i, j].row = i;
                    textBoxMatrix[i, j].col = j;
                    textBoxMatrix[i, j].filledIn = false;

                    textBoxMatrix[i, j].TextChanged += Textbox_OnTextChanged;
                    textBoxMatrix[i, j].KeyDown += TextBox_OnKeyPressed;
                    this.Controls.Add(textBoxMatrix[i, j]);
                }
            }
            for (int i = 0; i < maxCol; i++)
            {
                updateCollumnWidth((int)Font.Size + 15, i);
                updateCollumn(i);
            }

            updateTextboxesPosition();

            Controls.Add(useToMeasureLength);

            
        }
        //init
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            showSectionOfTextboxes(row, col);
            
            updateControlSize();
        }
        //only let user see portion of text boxes
        protected void showSectionOfTextboxes(int toShowRow, int toShowCol)
        {
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    textBoxMatrix[i, j].Hide();
                }
            }
            for (int i = 0; i < toShowRow; i++)
            {
                for (int j = 0; j < toShowCol; j++)
                {
                    textBoxMatrix[i, j].Show();
                }
            }
        }

        //update textboxes
        protected void updateTextboxesPosition()
        {
            Point start = new Point((int)Font.Size, (int)Font.Size);

            for (int i = 0; i < maxRow; i++)
            {
                int yOffset = start.Y + i * (textBoxMatrix[0, 0].Height + spacing_Ver);
                int currentXOffset = start.X;
                for (int j = 0; j < maxCol; j++)
                {
                    textBoxMatrix[i, j].Location = new Point(currentXOffset, yOffset);
                    currentXOffset += collumnWidths[j] + spacing_Hor;
                }
            }
        }
        protected void updateCollumnWidth(int width,int col)
        {
            for (int i = 0; i < maxRow; i++)
            {
                textBoxMatrix[i, col].Width = width;
            }
        }
        protected int getMaxCollumnWidthByText(int col)
        {
            int max = 0;
            for (int i = 0; i < maxRow; i++)
            {
                useToMeasureLength.Text = textBoxMatrix[i, col].Text;
                int textWidth = useToMeasureLength.Width + 15;
                if (textWidth <= (int)Font.Size + 15)
                {
                    textWidth = (int)Font.Size + 15;
                }
                if (textWidth >= (int)Font.Size * NumberOfDisplayedDigitInTextbox + 15)
                {
                    textWidth = (int)Font.Size * NumberOfDisplayedDigitInTextbox + 15;
                }
                max = System.Math.Max(max, textWidth);
            }
            return max;
        }
        protected void updateCollumn(int col)
        {
            int currentMax = getMaxCollumnWidthByText(col);
            if (currentMax != collumnWidths[col])
            {
                collumnWidths[col] = currentMax;
                updateCollumnWidth(currentMax, col);
            }
        }
        virtual protected void updateControlSize()
        {
            Point start = new Point((int)Font.Size, (int)Font.Size);

            fixedWidth = 2 * start.X - spacing_Hor;
            for (int i = 0; i < col; i++)
            {
                fixedWidth += collumnWidths[i] + spacing_Hor;
            }
            fixedHeight = 2 * start.Y + row * (textBoxMatrix[0, 0].Size.Height + spacing_Ver) - spacing_Ver;
            Width = fixedWidth;
            Height = fixedHeight;
        }


        //event
        protected override void OnPaint(PaintEventArgs pevent)
        {

            base.OnPaint(pevent);
            //draw closing bracket
            //main line
            Graphics g = pevent.Graphics;
            Pen p = new Pen(Color.Black, 2);
            g.DrawLine(p, 5, 5, 5, Height - 5);
            g.DrawLine(p, Width - 5, 5, Width - 5, Height - 5);

            g.DrawLine(p, 5, 5, 15, 5);
            g.DrawLine(p, 5, Height - 5, 15, Height - 5);

            g.DrawLine(p, Width - 5, 5, Width - 15, 5);
            g.DrawLine(p, Width - 5, Height - 5, Width - 15, Height - 5);
        }
        protected override void OnResize(EventArgs e)
        {
            Width = fixedWidth;
            Height = fixedHeight;
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            edited = false;
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            edited = false;
        }
        protected void MatrixOnDisplay_FontChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < maxRow; i++)
            {
                for(int j = 0; j < maxCol; j++)
                {
                    textBoxMatrix[i,j].Font = this.Font;
                }
            }
            for(int i = 0; i < maxCol; i++)
            {
                updateCollumn(i);
            }
            updateTextboxesPosition();
            updateControlSize();
            useToMeasureLength.Font = this.Font;        
        }
        protected void Textbox_OnTextChanged(object sender, EventArgs e)
        {
            //calculate desiredWidth
            TextBoxWithPosition tb = (TextBoxWithPosition)sender;
            updateCollumn(tb.col);
            updateTextboxesPosition();
            updateControlSize();
            edited = true;
            tb.filledIn = true;
            this.Invalidate();
        }
        private void TextBox_OnKeyPressed(object sender, KeyEventArgs e)
        {
            TextBoxWithPosition tb = (TextBoxWithPosition)sender;
            if (e.KeyCode.Equals(Keys.Enter))
            {
                Parent.Controls[0].Focus();
                return;
            }
            else if (e.KeyCode.Equals(Keys.Down))
            {
                if (tb.row < this.row - 1)
                {
                    textBoxMatrix[tb.row + 1, tb.col].Focus();
                }
            }

            else if (e.KeyCode.Equals(Keys.Up))
            {
                if (tb.row > 0)
                {
                    textBoxMatrix[tb.row - 1, tb.col].Focus();
                }
            }
            else if (e.KeyCode.Equals(Keys.Left))
            {
                if (tb.col > 0)
                {
                    textBoxMatrix[tb.row, tb.col - 1].Focus();
                }
            }
            else if (e.KeyCode.Equals(Keys.Right))
            {
                if (tb.col < this.col - 1)
                {
                    textBoxMatrix[tb.row, tb.col + 1].Focus();
                }
            }
        }
        protected virtual void updateVisualContent()
        {
            //position textbox
            Point start = new Point((int)Font.Size, (int)Font.Size);
            int[] maxSizeArray = new int[col];

            //get max length of each col
            for(int i = 0; i < col; i++)
            {
                int maxSize = textBoxMatrix[0, i].Width;
                for(int j = 0;j<row; j++)
                {
                    maxSize = System.Math.Max(maxSize, textBoxMatrix[j, i].Width);
                }
                maxSizeArray[i] = maxSize;
            }
   
            for (int i = 0; i < row; i++)
            {
                int yOffset = start.Y + i * (textBoxMatrix[0, 0].Height+spacing_Ver);
                int currentXOffset = start.X;
                for (int j = 0; j < col; j++)
                {
                    textBoxMatrix[i,j].Location = new Point(currentXOffset, yOffset);
                    currentXOffset += maxSizeArray[j] + spacing_Hor;
                }
            }

            //update size
           
            //redraw
            this.Refresh();
        }

        //some api to work with
        public void turnOnDisplayMode()
        {
            for(int i = 0; i < maxRow; i++)
            {
                for(int j = 0;j < maxCol; j++)
                {
                    textBoxMatrix[i, j].BorderStyle = BorderStyle.None;
                    textBoxMatrix[i,j].ReadOnly = true;
                }
            }
        }
        public void turnOffDisplayMode()
        {
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    textBoxMatrix[i, j].BorderStyle = BorderStyle.FixedSingle;
                    textBoxMatrix[i, j].ReadOnly = false;
                }
            }
        }
        public void StoreIntoMatrix(MyMatrix matrix, ref bool succeeded, ref string errorMessage)
        {
            //error check
            if(matrix == null)
            {
                succeeded = false;
                errorMessage = "Matrix is invalid";
                return;
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    string text = textBoxMatrix[i, j].Text.ToString();
                    if (text.Length == 0)
                    {
                        succeeded = false;
                        errorMessage = "Please fill in all the text boxes";
                        return;
                    }
                }
            }
            matrix._row = row;
            matrix._col = col;
            matrix._matrix = new double[row, col];
            for(int i = 0;i<row;i++)
            {
                for(int j = 0; j < col; j++)
                {
                    string text = textBoxMatrix[i, j].Text.ToString();
                    if (!Expression_Helper.isNumber(text))
                    {
                        succeeded = false;
                        errorMessage = "Please ensure number was inputted correctly";
                    }
                    else
                    {
                        succeeded = true;
                        matrix._matrix[i, j] = double.Parse(text);
                    }
                }
            }
        }
        public void LoadFromMatrix(MyMatrix matrix)
        {
            this.Row = matrix._row;
            this.Collumn = matrix._col;

            for(int i = 0; i < row; i++)
            {
                for(int j =0; j < col; j++)
                {
                    textBoxMatrix[i,j].Text = System.Math.Round(matrix._matrix[i,j],3).ToString();
                }
            }
            showSectionOfTextboxes(row, col);
            updateTextboxesPosition();
        }
        public void FillInUnfilledTextboxes(double value)
        {
            for(int i = 0;i < row;i++)
            {
                for(int j = 0; j < col; j++)
                {
                    if (textBoxMatrix[i,j].filledIn == false)
                    {
                        textBoxMatrix[i,j].Text = value.ToString();
                        textBoxMatrix[i, j].filledIn = false;
                    }
                }
            }
        }
        protected void reset()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    textBoxMatrix[i, j].Text = string.Empty;
                    textBoxMatrix[i, j].filledIn = false;
                }
            }
        }

        //base event
        protected void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
    public class MatrixOnDisplay_InDetail : MatrixOnDisplay
    {
        private Button RowAdd = null, RowSub = null;
        private Button ColAdd = null, ColSub = null;
        private Button ClearAll = null;
        private Label sizeNotation;

        public MatrixOnDisplay_InDetail()
        {
            RowAdd = new Button();
            RowSub = new Button();
            ColAdd = new Button();
            ColSub = new Button();
            ClearAll = new Button();

            sizeNotation = new Label();

            RowAdd.Text = "+";
            RowAdd.BackColor = Color.White;
            RowAdd.Font = new Font("Microsoft San Serif", 12, FontStyle.Bold);

            RowSub.Text = "-";
            RowSub.BackColor = Color.White;
            RowSub.Font = new Font("Microsoft San Serif", 12, FontStyle.Bold);

            ColAdd.Text = "+";
            ColAdd.BackColor = Color.White;
            ColAdd.Font = new Font("Microsoft San Serif", 12, FontStyle.Bold);

            ColSub.Text = "-";
            ColSub.BackColor = Color.White;
            ColSub.Font = new Font("Microsoft San Serif", 12, FontStyle.Bold);

            ClearAll.Text = "x";
            ClearAll.Font = ColSub.Font;
            ClearAll.ForeColor = Color.Red;
            ClearAll.Size = new Size(30, 30);

            sizeNotation.Font = new Font("Microsoft San Serif", 12, FontStyle.Bold);
            sizeNotation.AutoSize = true;

            RowAdd.Click += RowAdd_OnClick;
            RowSub.Click += RowSub_OnClick;

            ColAdd.Click += ColAdd_OnClick;
            ColSub.Click += ColSub_OnClick;

            ClearAll.Click += ClearAll_OnClick;

            Resize += (sender, arg) =>
            {
                UpdateSizeNotation();
            };
            LocationChanged += (sender, arg) =>
            {
                UpdateSizeNotation();
            };
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            //controls belong to winform
            base.OnHandleCreated(e);

            Parent.Controls.Add(RowAdd);
            Parent.Controls.Add(ColAdd);
            Parent.Controls.Add(RowSub);
            Parent.Controls.Add(ColSub);
            Parent.Controls.Add(sizeNotation);
            Parent.Controls.Add(ClearAll);
            positionButtons();
            UpdateSizeNotation();
        }
        override protected void updateControlSize()
        {
            base.updateControlSize();
            positionButtons();
            UpdateSizeNotation();
        }
        protected void positionButtons()
        {
            //row
            int rowButtonHeight = (fixedHeight - 4) / 2;
            ColAdd.Location = new Point(fixedWidth+base.Location.X, base.Location.Y);
            ColAdd.Size = new Size(30, rowButtonHeight);

            ColSub.Location = new Point(base.Location.X+fixedWidth, base.Location.Y+rowButtonHeight + 4);
            ColSub.Size = new Size(30, rowButtonHeight);

            //col;
            int colButtonWidth = (fixedWidth - 4) / 2;
            RowAdd.Location = new Point(base.Location.X, base.Location.Y+fixedHeight);
            RowAdd.Size = new Size(colButtonWidth, 30);

            RowSub.Location = new Point(base.Location.X+colButtonWidth + 4, base.Location.Y + fixedHeight);
            RowSub.Size = new Size(colButtonWidth, 30);

            ClearAll.Location = new Point(base.Location.X + fixedWidth, base.Location.Y + fixedHeight);
        }
        
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            positionButtons();
            UpdateSizeNotation();
        }
        private void UpdateSizeNotation()
        {
            sizeNotation.Text = Row.ToString() + "x" + col.ToString();
            int start = base.Location.X + base.Width / 2 - sizeNotation.Width / 2;
            sizeNotation.Location = new Point(start, base.Location.Y - sizeNotation.Height);

        }

        public new void Show()
        {
            base.Show();
            RowAdd.Show();
            RowSub.Show();
            ColAdd.Show();
            ColSub.Show();
            sizeNotation.Show();
        }
        public new void Hide()
        {
            base.Hide();
            RowAdd.Hide();
            RowSub.Hide();
            ColAdd.Hide();
            ColSub.Hide();
            ClearAll.Hide();
            sizeNotation.Hide();
        }
        
        //buttons function
        private void RowAdd_OnClick(object sender, EventArgs e)
        {
            Row++;
        }
        private void RowSub_OnClick(object sender, EventArgs e)
        {
            Row--;
        }
        private void ColAdd_OnClick(object sender, EventArgs e)
        {
            Collumn++;
        }
        private void ColSub_OnClick(object sender, EventArgs e)
        {
            Collumn--;
        }
        private void ClearAll_OnClick(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Discard the current matrix? ", " ",MessageBoxButtons.YesNo);
            if(r == DialogResult.Yes)
            {
                base.reset();
            }
        }     
    }
    public class MatrixOnDisplayWithPostition : MatrixOnDisplay
    {
        public int index;
    }
}