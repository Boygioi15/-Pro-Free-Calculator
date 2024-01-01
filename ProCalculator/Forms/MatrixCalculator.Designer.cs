namespace ProCalculator
{
    partial class MatrixCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StoragePanel_Label = new System.Windows.Forms.Label();
            this.StoreMatrixLabel = new System.Windows.Forms.Label();
            this.ChooseWhereToSaveComboBox = new System.Windows.Forms.ComboBox();
            this.DoneButton = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.MessageContent = new System.Windows.Forms.Label();
            this.StoragePanel_ScrollBar = new System.Windows.Forms.HScrollBar();
            this.Creating = new System.Windows.Forms.Button();
            this.Calculating = new System.Windows.Forms.Button();
            this.ResultPanel_DivideLine = new System.Windows.Forms.PictureBox();
            this.ResultPanel = new System.Windows.Forms.Panel();
            this.CALCULATING_ChooseLogToSave = new System.Windows.Forms.TextBox();
            this.StoragePanel_LabelBackground = new System.Windows.Forms.Panel();
            this.ModePanel = new System.Windows.Forms.Panel();
            this.CALCULATING_ResultPanel = new System.Windows.Forms.Panel();
            this.ResultPanel_StrikeLine = new System.Windows.Forms.PictureBox();
            this.ResultPanel_Label = new System.Windows.Forms.Label();
            this.ResultPanel_ScrollPanel = new System.Windows.Forms.Panel();
            this.ResultPanel_ScrollBar = new System.Windows.Forms.VScrollBar();
            this.CALCULATING_InputPanel = new System.Windows.Forms.Panel();
            this.InputPanel_InputTextbox = new System.Windows.Forms.TextBox();
            this.InputPanel_ShowMatrixKeyBoard = new System.Windows.Forms.Button();
            this.InputPanel_Go = new System.Windows.Forms.Button();
            this.StoragePanel = new System.Windows.Forms.Panel();
            this.StoragePanel_SlideClose = new System.Windows.Forms.PictureBox();
            this.WorkingPanel = new System.Windows.Forms.Panel();
            this.CREATING_WorkingPanel = new CustomUserControls.RoundedPanel.RoundedPanel();
            this.CREATING_MatrixOnDisplay = new CustomUserControls.MatrixOnDisplay.MatrixOnDisplay_InDetail();
            this.CREATING_MatrixLabel = new System.Windows.Forms.Label();
            this.CREATING_LoadFromLabel = new System.Windows.Forms.Label();
            this.CREATING_InitValueLabel = new System.Windows.Forms.Label();
            this.CREATING_InitValueTextBox = new System.Windows.Forms.TextBox();
            this.CREATING_LoadFromComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPanel_DivideLine)).BeginInit();
            this.ResultPanel.SuspendLayout();
            this.StoragePanel_LabelBackground.SuspendLayout();
            this.ModePanel.SuspendLayout();
            this.CALCULATING_ResultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPanel_StrikeLine)).BeginInit();
            this.ResultPanel_ScrollPanel.SuspendLayout();
            this.CALCULATING_InputPanel.SuspendLayout();
            this.StoragePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StoragePanel_SlideClose)).BeginInit();
            this.WorkingPanel.SuspendLayout();
            this.CREATING_WorkingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StoragePanel_Label
            // 
            this.StoragePanel_Label.AutoSize = true;
            this.StoragePanel_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(204)))));
            this.StoragePanel_Label.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoragePanel_Label.Location = new System.Drawing.Point(3, 2);
            this.StoragePanel_Label.Name = "StoragePanel_Label";
            this.StoragePanel_Label.Size = new System.Drawing.Size(99, 28);
            this.StoragePanel_Label.TabIndex = 20;
            this.StoragePanel_Label.Text = "Storage: ";
            // 
            // StoreMatrixLabel
            // 
            this.StoreMatrixLabel.AutoSize = true;
            this.StoreMatrixLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoreMatrixLabel.Location = new System.Drawing.Point(391, 8);
            this.StoreMatrixLabel.Name = "StoreMatrixLabel";
            this.StoreMatrixLabel.Size = new System.Drawing.Size(164, 28);
            this.StoreMatrixLabel.TabIndex = 7;
            this.StoreMatrixLabel.Text = "Save log         as";
            // 
            // ChooseWhereToSaveComboBox
            // 
            this.ChooseWhereToSaveComboBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ChooseWhereToSaveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseWhereToSaveComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseWhereToSaveComboBox.FormattingEnabled = true;
            this.ChooseWhereToSaveComboBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H"});
            this.ChooseWhereToSaveComboBox.Location = new System.Drawing.Point(561, 3);
            this.ChooseWhereToSaveComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChooseWhereToSaveComboBox.Name = "ChooseWhereToSaveComboBox";
            this.ChooseWhereToSaveComboBox.Size = new System.Drawing.Size(55, 34);
            this.ChooseWhereToSaveComboBox.TabIndex = 9;
            // 
            // DoneButton
            // 
            this.DoneButton.BackColor = System.Drawing.Color.Moccasin;
            this.DoneButton.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoneButton.Location = new System.Drawing.Point(441, 44);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(109, 39);
            this.DoneButton.TabIndex = 31;
            this.DoneButton.Text = "DONE";
            this.DoneButton.UseVisualStyleBackColor = false;
            this.DoneButton.Click += new System.EventHandler(this.CREATING_DoneButton_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(5, 4);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(99, 28);
            this.MessageLabel.TabIndex = 44;
            this.MessageLabel.Text = "Message:";
            // 
            // MessageContent
            // 
            this.MessageContent.AutoSize = true;
            this.MessageContent.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageContent.ForeColor = System.Drawing.Color.DarkGreen;
            this.MessageContent.Location = new System.Drawing.Point(19, 35);
            this.MessageContent.Name = "MessageContent";
            this.MessageContent.Size = new System.Drawing.Size(267, 28);
            this.MessageContent.TabIndex = 46;
            this.MessageContent.Text = "Successfully saved into slot";
            // 
            // StoragePanel_ScrollBar
            // 
            this.StoragePanel_ScrollBar.LargeChange = 702;
            this.StoragePanel_ScrollBar.Location = new System.Drawing.Point(0, 209);
            this.StoragePanel_ScrollBar.Maximum = 1280;
            this.StoragePanel_ScrollBar.Name = "StoragePanel_ScrollBar";
            this.StoragePanel_ScrollBar.Size = new System.Drawing.Size(684, 20);
            this.StoragePanel_ScrollBar.SmallChange = 70;
            this.StoragePanel_ScrollBar.TabIndex = 81;
            this.StoragePanel_ScrollBar.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // Creating
            // 
            this.Creating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(204)))));
            this.Creating.Font = new System.Drawing.Font("Comic Sans MS", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Creating.Location = new System.Drawing.Point(0, 0);
            this.Creating.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Creating.Name = "Creating";
            this.Creating.Size = new System.Drawing.Size(175, 39);
            this.Creating.TabIndex = 120;
            this.Creating.Text = "Creating/Updating";
            this.Creating.UseVisualStyleBackColor = false;
            this.Creating.Click += new System.EventHandler(this.Creating_Click);
            // 
            // Calculating
            // 
            this.Calculating.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Calculating.Font = new System.Drawing.Font("Comic Sans MS", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Calculating.Location = new System.Drawing.Point(169, 0);
            this.Calculating.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Calculating.Name = "Calculating";
            this.Calculating.Size = new System.Drawing.Size(175, 39);
            this.Calculating.TabIndex = 121;
            this.Calculating.Text = "Calculating";
            this.Calculating.UseVisualStyleBackColor = false;
            this.Calculating.Click += new System.EventHandler(this.button1_Click);
            // 
            // ResultPanel_DivideLine
            // 
            this.ResultPanel_DivideLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.ResultPanel_DivideLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultPanel_DivideLine.Location = new System.Drawing.Point(376, -1);
            this.ResultPanel_DivideLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResultPanel_DivideLine.Name = "ResultPanel_DivideLine";
            this.ResultPanel_DivideLine.Size = new System.Drawing.Size(2, 89);
            this.ResultPanel_DivideLine.TabIndex = 133;
            this.ResultPanel_DivideLine.TabStop = false;
            // 
            // ResultPanel
            // 
            this.ResultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultPanel.Controls.Add(this.CALCULATING_ChooseLogToSave);
            this.ResultPanel.Controls.Add(this.ChooseWhereToSaveComboBox);
            this.ResultPanel.Controls.Add(this.StoreMatrixLabel);
            this.ResultPanel.Controls.Add(this.DoneButton);
            this.ResultPanel.Controls.Add(this.ResultPanel_DivideLine);
            this.ResultPanel.Controls.Add(this.MessageContent);
            this.ResultPanel.Controls.Add(this.MessageLabel);
            this.ResultPanel.Location = new System.Drawing.Point(16, 660);
            this.ResultPanel.Name = "ResultPanel";
            this.ResultPanel.Size = new System.Drawing.Size(650, 90);
            this.ResultPanel.TabIndex = 75;
            // 
            // CALCULATING_ChooseLogToSave
            // 
            this.CALCULATING_ChooseLogToSave.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CALCULATING_ChooseLogToSave.Location = new System.Drawing.Point(481, 3);
            this.CALCULATING_ChooseLogToSave.Name = "CALCULATING_ChooseLogToSave";
            this.CALCULATING_ChooseLogToSave.Size = new System.Drawing.Size(40, 35);
            this.CALCULATING_ChooseLogToSave.TabIndex = 3;
            // 
            // StoragePanel_LabelBackground
            // 
            this.StoragePanel_LabelBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(204)))));
            this.StoragePanel_LabelBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StoragePanel_LabelBackground.Controls.Add(this.StoragePanel_Label);
            this.StoragePanel_LabelBackground.Location = new System.Drawing.Point(0, 0);
            this.StoragePanel_LabelBackground.Name = "StoragePanel_LabelBackground";
            this.StoragePanel_LabelBackground.Size = new System.Drawing.Size(684, 31);
            this.StoragePanel_LabelBackground.TabIndex = 141;
            // 
            // ModePanel
            // 
            this.ModePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(217)))), ((int)(((byte)(252)))));
            this.ModePanel.Controls.Add(this.Creating);
            this.ModePanel.Controls.Add(this.Calculating);
            this.ModePanel.Location = new System.Drawing.Point(1, 240);
            this.ModePanel.Name = "ModePanel";
            this.ModePanel.Size = new System.Drawing.Size(1300, 40);
            this.ModePanel.TabIndex = 142;
            // 
            // CALCULATING_ResultPanel
            // 
            this.CALCULATING_ResultPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.CALCULATING_ResultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CALCULATING_ResultPanel.Controls.Add(this.ResultPanel_StrikeLine);
            this.CALCULATING_ResultPanel.Controls.Add(this.ResultPanel_Label);
            this.CALCULATING_ResultPanel.Controls.Add(this.ResultPanel_ScrollPanel);
            this.CALCULATING_ResultPanel.Location = new System.Drawing.Point(16, 20);
            this.CALCULATING_ResultPanel.Name = "CALCULATING_ResultPanel";
            this.CALCULATING_ResultPanel.Size = new System.Drawing.Size(650, 310);
            this.CALCULATING_ResultPanel.TabIndex = 81;
            // 
            // ResultPanel_StrikeLine
            // 
            this.ResultPanel_StrikeLine.BackColor = System.Drawing.Color.Black;
            this.ResultPanel_StrikeLine.Location = new System.Drawing.Point(10, 38);
            this.ResultPanel_StrikeLine.Name = "ResultPanel_StrikeLine";
            this.ResultPanel_StrikeLine.Size = new System.Drawing.Size(120, 2);
            this.ResultPanel_StrikeLine.TabIndex = 1;
            this.ResultPanel_StrikeLine.TabStop = false;
            // 
            // ResultPanel_Label
            // 
            this.ResultPanel_Label.AutoSize = true;
            this.ResultPanel_Label.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultPanel_Label.Location = new System.Drawing.Point(5, 4);
            this.ResultPanel_Label.Name = "ResultPanel_Label";
            this.ResultPanel_Label.Size = new System.Drawing.Size(124, 33);
            this.ResultPanel_Label.TabIndex = 0;
            this.ResultPanel_Label.Text = "Result log";
            // 
            // ResultPanel_ScrollPanel
            // 
            this.ResultPanel_ScrollPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ResultPanel_ScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ResultPanel_ScrollPanel.Controls.Add(this.ResultPanel_ScrollBar);
            this.ResultPanel_ScrollPanel.Location = new System.Drawing.Point(-1, 46);
            this.ResultPanel_ScrollPanel.Name = "ResultPanel_ScrollPanel";
            this.ResultPanel_ScrollPanel.Size = new System.Drawing.Size(650, 261);
            this.ResultPanel_ScrollPanel.TabIndex = 5;
            // 
            // ResultPanel_ScrollBar
            // 
            this.ResultPanel_ScrollBar.LargeChange = 0;
            this.ResultPanel_ScrollBar.Location = new System.Drawing.Point(630, 2);
            this.ResultPanel_ScrollBar.Maximum = 1;
            this.ResultPanel_ScrollBar.Name = "ResultPanel_ScrollBar";
            this.ResultPanel_ScrollBar.Size = new System.Drawing.Size(18, 255);
            this.ResultPanel_ScrollBar.SmallChange = 0;
            this.ResultPanel_ScrollBar.TabIndex = 2;
            this.ResultPanel_ScrollBar.ValueChanged += new System.EventHandler(this.ResultPanel_ScrollBar_ValueChanged);
            // 
            // CALCULATING_InputPanel
            // 
            this.CALCULATING_InputPanel.BackColor = System.Drawing.Color.Transparent;
            this.CALCULATING_InputPanel.Controls.Add(this.InputPanel_InputTextbox);
            this.CALCULATING_InputPanel.Controls.Add(this.InputPanel_ShowMatrixKeyBoard);
            this.CALCULATING_InputPanel.Controls.Add(this.InputPanel_Go);
            this.CALCULATING_InputPanel.Location = new System.Drawing.Point(125, 335);
            this.CALCULATING_InputPanel.Name = "CALCULATING_InputPanel";
            this.CALCULATING_InputPanel.Size = new System.Drawing.Size(438, 35);
            this.CALCULATING_InputPanel.TabIndex = 3;
            // 
            // InputPanel_InputTextbox
            // 
            this.InputPanel_InputTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.InputPanel_InputTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputPanel_InputTextbox.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputPanel_InputTextbox.Location = new System.Drawing.Point(0, 0);
            this.InputPanel_InputTextbox.Name = "InputPanel_InputTextbox";
            this.InputPanel_InputTextbox.Size = new System.Drawing.Size(320, 35);
            this.InputPanel_InputTextbox.TabIndex = 3;
            this.InputPanel_InputTextbox.Text = "Delete this to start";
            this.InputPanel_InputTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputPanel_InputTextbox_KeyDown_1);
            // 
            // InputPanel_ShowMatrixKeyBoard
            // 
            this.InputPanel_ShowMatrixKeyBoard.BackColor = System.Drawing.Color.LightGreen;
            this.InputPanel_ShowMatrixKeyBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InputPanel_ShowMatrixKeyBoard.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputPanel_ShowMatrixKeyBoard.Location = new System.Drawing.Point(378, 0);
            this.InputPanel_ShowMatrixKeyBoard.Name = "InputPanel_ShowMatrixKeyBoard";
            this.InputPanel_ShowMatrixKeyBoard.Size = new System.Drawing.Size(60, 35);
            this.InputPanel_ShowMatrixKeyBoard.TabIndex = 2;
            this.InputPanel_ShowMatrixKeyBoard.UseVisualStyleBackColor = false;
            // 
            // InputPanel_Go
            // 
            this.InputPanel_Go.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(204)))));
            this.InputPanel_Go.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InputPanel_Go.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputPanel_Go.Location = new System.Drawing.Point(319, 0);
            this.InputPanel_Go.Name = "InputPanel_Go";
            this.InputPanel_Go.Size = new System.Drawing.Size(60, 35);
            this.InputPanel_Go.TabIndex = 1;
            this.InputPanel_Go.Text = "G0!";
            this.InputPanel_Go.UseVisualStyleBackColor = false;
            this.InputPanel_Go.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputPanel_Go_MouseDown);
            // 
            // StoragePanel
            // 
            this.StoragePanel.BackColor = System.Drawing.Color.White;
            this.StoragePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StoragePanel.Controls.Add(this.StoragePanel_SlideClose);
            this.StoragePanel.Controls.Add(this.StoragePanel_ScrollBar);
            this.StoragePanel.Controls.Add(this.StoragePanel_LabelBackground);
            this.StoragePanel.Location = new System.Drawing.Point(0, 0);
            this.StoragePanel.Name = "StoragePanel";
            this.StoragePanel.Size = new System.Drawing.Size(688, 240);
            this.StoragePanel.TabIndex = 143;
            // 
            // StoragePanel_SlideClose
            // 
            this.StoragePanel_SlideClose.BackColor = System.Drawing.Color.LightGray;
            this.StoragePanel_SlideClose.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.StoragePanel_SlideClose.InitialImage = null;
            this.StoragePanel_SlideClose.Location = new System.Drawing.Point(0, 229);
            this.StoragePanel_SlideClose.Name = "StoragePanel_SlideClose";
            this.StoragePanel_SlideClose.Size = new System.Drawing.Size(687, 10);
            this.StoragePanel_SlideClose.TabIndex = 142;
            this.StoragePanel_SlideClose.TabStop = false;
            // 
            // WorkingPanel
            // 
            this.WorkingPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.WorkingPanel.Controls.Add(this.CALCULATING_InputPanel);
            this.WorkingPanel.Controls.Add(this.CREATING_WorkingPanel);
            this.WorkingPanel.Controls.Add(this.CALCULATING_ResultPanel);
            this.WorkingPanel.Location = new System.Drawing.Point(0, 280);
            this.WorkingPanel.Name = "WorkingPanel";
            this.WorkingPanel.Size = new System.Drawing.Size(685, 375);
            this.WorkingPanel.TabIndex = 144;
            // 
            // CREATING_WorkingPanel
            // 
            this.CREATING_WorkingPanel.ArcSize = 10;
            this.CREATING_WorkingPanel.BackColor = System.Drawing.Color.Transparent;
            this.CREATING_WorkingPanel.BorderColor = System.Drawing.Color.Black;
            this.CREATING_WorkingPanel.BorderWidth = 2;
            this.CREATING_WorkingPanel.Controls.Add(this.CREATING_MatrixOnDisplay);
            this.CREATING_WorkingPanel.Controls.Add(this.CREATING_MatrixLabel);
            this.CREATING_WorkingPanel.Controls.Add(this.CREATING_LoadFromLabel);
            this.CREATING_WorkingPanel.Controls.Add(this.CREATING_InitValueLabel);
            this.CREATING_WorkingPanel.Controls.Add(this.CREATING_InitValueTextBox);
            this.CREATING_WorkingPanel.Controls.Add(this.CREATING_LoadFromComboBox);
            this.CREATING_WorkingPanel.InteriorColor = System.Drawing.Color.White;
            this.CREATING_WorkingPanel.Location = new System.Drawing.Point(16, 20);
            this.CREATING_WorkingPanel.Name = "CREATING_WorkingPanel";
            this.CREATING_WorkingPanel.Size = new System.Drawing.Size(650, 340);
            this.CREATING_WorkingPanel.TabIndex = 140;
            // 
            // CREATING_MatrixOnDisplay
            // 
            this.CREATING_MatrixOnDisplay.BackColor = System.Drawing.Color.Transparent;
            this.CREATING_MatrixOnDisplay.Collumn = 5;
            this.CREATING_MatrixOnDisplay.Font = new System.Drawing.Font("Comic Sans MS", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CREATING_MatrixOnDisplay.ForeColor = System.Drawing.Color.Black;
            this.CREATING_MatrixOnDisplay.Location = new System.Drawing.Point(254, 63);
            this.CREATING_MatrixOnDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CREATING_MatrixOnDisplay.Name = "CREATING_MatrixOnDisplay";
            this.CREATING_MatrixOnDisplay.NumberOfDisplayedDigitInTextbox = 6;
            this.CREATING_MatrixOnDisplay.Row = 5;
            this.CREATING_MatrixOnDisplay.Size = new System.Drawing.Size(199, 229);
            this.CREATING_MatrixOnDisplay.SpacingHor = 10;
            this.CREATING_MatrixOnDisplay.SpacingVer = 5;
            this.CREATING_MatrixOnDisplay.TabIndex = 68;
            this.CREATING_MatrixOnDisplay.TextboxBackColor = System.Drawing.Color.White;
            // 
            // CREATING_MatrixLabel
            // 
            this.CREATING_MatrixLabel.AutoSize = true;
            this.CREATING_MatrixLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CREATING_MatrixLabel.Location = new System.Drawing.Point(159, 163);
            this.CREATING_MatrixLabel.Name = "CREATING_MatrixLabel";
            this.CREATING_MatrixLabel.Size = new System.Drawing.Size(89, 29);
            this.CREATING_MatrixLabel.TabIndex = 6;
            this.CREATING_MatrixLabel.Text = "Matrix: ";
            this.CREATING_MatrixLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CREATING_LoadFromLabel
            // 
            this.CREATING_LoadFromLabel.AutoSize = true;
            this.CREATING_LoadFromLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CREATING_LoadFromLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATING_LoadFromLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CREATING_LoadFromLabel.Location = new System.Drawing.Point(5, 5);
            this.CREATING_LoadFromLabel.Name = "CREATING_LoadFromLabel";
            this.CREATING_LoadFromLabel.Size = new System.Drawing.Size(110, 30);
            this.CREATING_LoadFromLabel.TabIndex = 34;
            this.CREATING_LoadFromLabel.Text = "Load from";
            // 
            // CREATING_InitValueLabel
            // 
            this.CREATING_InitValueLabel.AutoSize = true;
            this.CREATING_InitValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CREATING_InitValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATING_InitValueLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CREATING_InitValueLabel.Location = new System.Drawing.Point(175, 5);
            this.CREATING_InitValueLabel.Name = "CREATING_InitValueLabel";
            this.CREATING_InitValueLabel.Size = new System.Drawing.Size(101, 30);
            this.CREATING_InitValueLabel.TabIndex = 36;
            this.CREATING_InitValueLabel.Text = "Init value";
            // 
            // CREATING_InitValueTextBox
            // 
            this.CREATING_InitValueTextBox.BackColor = System.Drawing.Color.White;
            this.CREATING_InitValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATING_InitValueTextBox.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CREATING_InitValueTextBox.Location = new System.Drawing.Point(276, 4);
            this.CREATING_InitValueTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CREATING_InitValueTextBox.Name = "CREATING_InitValueTextBox";
            this.CREATING_InitValueTextBox.Size = new System.Drawing.Size(61, 33);
            this.CREATING_InitValueTextBox.TabIndex = 37;
            this.CREATING_InitValueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdditionalTextBox2_KeyDown);
            // 
            // CREATING_LoadFromComboBox
            // 
            this.CREATING_LoadFromComboBox.BackColor = System.Drawing.Color.White;
            this.CREATING_LoadFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CREATING_LoadFromComboBox.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CREATING_LoadFromComboBox.FormattingEnabled = true;
            this.CREATING_LoadFromComboBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H"});
            this.CREATING_LoadFromComboBox.Location = new System.Drawing.Point(115, 4);
            this.CREATING_LoadFromComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CREATING_LoadFromComboBox.Name = "CREATING_LoadFromComboBox";
            this.CREATING_LoadFromComboBox.Size = new System.Drawing.Size(60, 32);
            this.CREATING_LoadFromComboBox.TabIndex = 47;
            this.CREATING_LoadFromComboBox.DropDownClosed += new System.EventHandler(this.CREATING_ChooseWhereToLoadFrom_DropDownClosed);
            // 
            // MatrixCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(685, 758);
            this.Controls.Add(this.ResultPanel);
            this.Controls.Add(this.ModePanel);
            this.Controls.Add(this.StoragePanel);
            this.Controls.Add(this.WorkingPanel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(703, 805);
            this.MinimumSize = new System.Drawing.Size(703, 805);
            this.Name = "MatrixCalculator";
            this.Text = "Matrix";
            this.Load += new System.EventHandler(this.MatrixCalculator_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MatrixCalculator_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.ResultPanel_DivideLine)).EndInit();
            this.ResultPanel.ResumeLayout(false);
            this.ResultPanel.PerformLayout();
            this.StoragePanel_LabelBackground.ResumeLayout(false);
            this.StoragePanel_LabelBackground.PerformLayout();
            this.ModePanel.ResumeLayout(false);
            this.CALCULATING_ResultPanel.ResumeLayout(false);
            this.CALCULATING_ResultPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPanel_StrikeLine)).EndInit();
            this.ResultPanel_ScrollPanel.ResumeLayout(false);
            this.CALCULATING_InputPanel.ResumeLayout(false);
            this.CALCULATING_InputPanel.PerformLayout();
            this.StoragePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StoragePanel_SlideClose)).EndInit();
            this.WorkingPanel.ResumeLayout(false);
            this.CREATING_WorkingPanel.ResumeLayout(false);
            this.CREATING_WorkingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Label StoragePanel_Label;
        internal System.Windows.Forms.Label StoreMatrixLabel;
        internal System.Windows.Forms.ComboBox ChooseWhereToSaveComboBox;
        internal System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Label MessageLabel;
        internal System.Windows.Forms.Label MessageContent;
        internal System.Windows.Forms.HScrollBar StoragePanel_ScrollBar;
        private System.Windows.Forms.PictureBox ResultPanel_DivideLine;
        private System.Windows.Forms.Panel ResultPanel;
        private System.Windows.Forms.Panel StoragePanel_LabelBackground;
        private System.Windows.Forms.Panel ModePanel;
        private System.Windows.Forms.PictureBox ResultPanel_StrikeLine;
        private System.Windows.Forms.Label ResultPanel_Label;
        private System.Windows.Forms.Button InputPanel_Go;
        private System.Windows.Forms.Button InputPanel_ShowMatrixKeyBoard;
        internal System.Windows.Forms.Panel CALCULATING_ResultPanel;
        internal System.Windows.Forms.Panel CALCULATING_InputPanel;
        internal System.Windows.Forms.Button Creating;
        internal System.Windows.Forms.Button Calculating;
        internal System.Windows.Forms.Panel ResultPanel_ScrollPanel;
        internal System.Windows.Forms.VScrollBar ResultPanel_ScrollBar;
        internal System.Windows.Forms.TextBox InputPanel_InputTextbox;
        private System.Windows.Forms.Panel StoragePanel;
        private System.Windows.Forms.PictureBox StoragePanel_SlideClose;
        private System.Windows.Forms.Panel WorkingPanel;
        internal System.Windows.Forms.TextBox CALCULATING_ChooseLogToSave;
        internal CustomUserControls.RoundedPanel.RoundedPanel CREATING_WorkingPanel;
        internal CustomUserControls.MatrixOnDisplay.MatrixOnDisplay_InDetail CREATING_MatrixOnDisplay;
        internal System.Windows.Forms.Label CREATING_MatrixLabel;
        internal System.Windows.Forms.Label CREATING_LoadFromLabel;
        internal System.Windows.Forms.Label CREATING_InitValueLabel;
        internal System.Windows.Forms.TextBox CREATING_InitValueTextBox;
        internal System.Windows.Forms.ComboBox CREATING_LoadFromComboBox;

        //internal CustomUserControls.MatrixOnDisplay.MatrixOnDisplay CREATING_MatrixOnDisplay;
    }
}