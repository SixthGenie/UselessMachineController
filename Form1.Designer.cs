namespace UselessMachineController
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.switchTrackBar = new System.Windows.Forms.TrackBar();
            this.lidTrackBar = new System.Windows.Forms.TrackBar();
            this.EditBehaviorButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sequenceCollection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AvailableMemory = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RunSelectedSequenceButton = new System.Windows.Forms.Button();
            this.currentSequenceSize = new System.Windows.Forms.Label();
            this.animateSlidersCheckbox = new System.Windows.Forms.CheckBox();
            this.switchMinimum = new System.Windows.Forms.Label();
            this.switchMaximum = new System.Windows.Forms.Label();
            this.lidMinimum = new System.Windows.Forms.Label();
            this.lidMaximum = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.baudrateSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.opencloseButton = new System.Windows.Forms.Button();
            this.commPortComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.downloadToBoxButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.switchTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lidTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // switchTrackBar
            // 
            this.switchTrackBar.Location = new System.Drawing.Point(382, -1);
            this.switchTrackBar.Maximum = 2200;
            this.switchTrackBar.Minimum = 900;
            this.switchTrackBar.Name = "switchTrackBar";
            this.switchTrackBar.Size = new System.Drawing.Size(438, 45);
            this.switchTrackBar.TabIndex = 0;
            this.switchTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.switchTrackBar.Value = 900;
            this.switchTrackBar.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // lidTrackBar
            // 
            this.lidTrackBar.Location = new System.Drawing.Point(859, 30);
            this.lidTrackBar.Maximum = 1400;
            this.lidTrackBar.Minimum = 900;
            this.lidTrackBar.Name = "lidTrackBar";
            this.lidTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.lidTrackBar.Size = new System.Drawing.Size(45, 460);
            this.lidTrackBar.TabIndex = 1;
            this.lidTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.lidTrackBar.Value = 900;
            this.lidTrackBar.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // EditBehaviorButton
            // 
            this.EditBehaviorButton.Location = new System.Drawing.Point(12, 104);
            this.EditBehaviorButton.Name = "EditBehaviorButton";
            this.EditBehaviorButton.Size = new System.Drawing.Size(96, 38);
            this.EditBehaviorButton.TabIndex = 2;
            this.EditBehaviorButton.Text = "Switch On";
            this.EditBehaviorButton.UseVisualStyleBackColor = true;
            this.EditBehaviorButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(818, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(604, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "List of sequences";
            // 
            // sequenceCollection
            // 
            this.sequenceCollection.FormattingEnabled = true;
            this.sequenceCollection.Location = new System.Drawing.Point(513, 104);
            this.sequenceCollection.Name = "sequenceCollection";
            this.sequenceCollection.Size = new System.Drawing.Size(236, 21);
            this.sequenceCollection.TabIndex = 7;
            this.sequenceCollection.SelectedIndexChanged += new System.EventHandler(this.sequenceCollection_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(586, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Available memory:";
            // 
            // AvailableMemory
            // 
            this.AvailableMemory.AutoSize = true;
            this.AvailableMemory.Location = new System.Drawing.Point(674, 138);
            this.AvailableMemory.Name = "AvailableMemory";
            this.AvailableMemory.Size = new System.Drawing.Size(31, 13);
            this.AvailableMemory.TabIndex = 9;
            this.AvailableMemory.Text = "1024";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(706, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "bytes";
            // 
            // RunSelectedSequenceButton
            // 
            this.RunSelectedSequenceButton.Location = new System.Drawing.Point(513, 162);
            this.RunSelectedSequenceButton.Name = "RunSelectedSequenceButton";
            this.RunSelectedSequenceButton.Size = new System.Drawing.Size(136, 32);
            this.RunSelectedSequenceButton.TabIndex = 11;
            this.RunSelectedSequenceButton.Text = "Run selected sequence";
            this.RunSelectedSequenceButton.UseVisualStyleBackColor = true;
            this.RunSelectedSequenceButton.Click += new System.EventHandler(this.RunSelectedSequenceButton_Click);
            // 
            // currentSequenceSize
            // 
            this.currentSequenceSize.AutoSize = true;
            this.currentSequenceSize.Location = new System.Drawing.Point(517, 137);
            this.currentSequenceSize.Name = "currentSequenceSize";
            this.currentSequenceSize.Size = new System.Drawing.Size(35, 13);
            this.currentSequenceSize.TabIndex = 12;
            this.currentSequenceSize.Text = "label5";
            // 
            // animateSlidersCheckbox
            // 
            this.animateSlidersCheckbox.AutoSize = true;
            this.animateSlidersCheckbox.Location = new System.Drawing.Point(724, 50);
            this.animateSlidersCheckbox.Name = "animateSlidersCheckbox";
            this.animateSlidersCheckbox.Size = new System.Drawing.Size(96, 17);
            this.animateSlidersCheckbox.TabIndex = 13;
            this.animateSlidersCheckbox.Text = "Animate sliders";
            this.animateSlidersCheckbox.UseVisualStyleBackColor = true;
            // 
            // switchMinimum
            // 
            this.switchMinimum.AutoSize = true;
            this.switchMinimum.Location = new System.Drawing.Point(398, 21);
            this.switchMinimum.Name = "switchMinimum";
            this.switchMinimum.Size = new System.Drawing.Size(35, 13);
            this.switchMinimum.TabIndex = 14;
            this.switchMinimum.Text = "label5";
            // 
            // switchMaximum
            // 
            this.switchMaximum.AutoSize = true;
            this.switchMaximum.Location = new System.Drawing.Point(785, 21);
            this.switchMaximum.Name = "switchMaximum";
            this.switchMaximum.Size = new System.Drawing.Size(35, 13);
            this.switchMaximum.TabIndex = 15;
            this.switchMaximum.Text = "label7";
            // 
            // lidMinimum
            // 
            this.lidMinimum.AutoSize = true;
            this.lidMinimum.Location = new System.Drawing.Point(836, 14);
            this.lidMinimum.Name = "lidMinimum";
            this.lidMinimum.Size = new System.Drawing.Size(35, 13);
            this.lidMinimum.TabIndex = 16;
            this.lidMinimum.Text = "label5";
            // 
            // lidMaximum
            // 
            this.lidMaximum.AutoSize = true;
            this.lidMaximum.Location = new System.Drawing.Point(836, 493);
            this.lidMaximum.Name = "lidMaximum";
            this.lidMaximum.Size = new System.Drawing.Size(35, 13);
            this.lidMaximum.TabIndex = 17;
            this.lidMaximum.Text = "label7";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Edit behavior";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refreshButton.BackgroundImage")));
            this.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.refreshButton.Location = new System.Drawing.Point(173, 30);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(24, 23);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // baudrateSelectionComboBox
            // 
            this.baudrateSelectionComboBox.Enabled = false;
            this.baudrateSelectionComboBox.FormattingEnabled = true;
            this.baudrateSelectionComboBox.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.baudrateSelectionComboBox.Location = new System.Drawing.Point(58, 60);
            this.baudrateSelectionComboBox.Name = "baudrateSelectionComboBox";
            this.baudrateSelectionComboBox.Size = new System.Drawing.Size(109, 21);
            this.baudrateSelectionComboBox.TabIndex = 4;
            // 
            // opencloseButton
            // 
            this.opencloseButton.Enabled = false;
            this.opencloseButton.Location = new System.Drawing.Point(203, 30);
            this.opencloseButton.Name = "opencloseButton";
            this.opencloseButton.Size = new System.Drawing.Size(75, 23);
            this.opencloseButton.TabIndex = 2;
            this.opencloseButton.Text = "Close";
            this.opencloseButton.UseVisualStyleBackColor = true;
            this.opencloseButton.Click += new System.EventHandler(this.opencloseButton_Click);
            // 
            // commPortComboBox
            // 
            this.commPortComboBox.Enabled = false;
            this.commPortComboBox.FormattingEnabled = true;
            this.commPortComboBox.Location = new System.Drawing.Point(58, 26);
            this.commPortComboBox.Name = "commPortComboBox";
            this.commPortComboBox.Size = new System.Drawing.Size(109, 21);
            this.commPortComboBox.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.refreshButton);
            this.groupBox1.Controls.Add(this.baudrateSelectionComboBox);
            this.groupBox1.Controls.Add(this.commPortComboBox);
            this.groupBox1.Controls.Add(this.opencloseButton);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 95);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Baud";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Port";
            // 
            // downloadToBoxButton
            // 
            this.downloadToBoxButton.Enabled = false;
            this.downloadToBoxButton.Location = new System.Drawing.Point(124, 104);
            this.downloadToBoxButton.Name = "downloadToBoxButton";
            this.downloadToBoxButton.Size = new System.Drawing.Size(117, 38);
            this.downloadToBoxButton.TabIndex = 20;
            this.downloadToBoxButton.Text = "Download to box";
            this.downloadToBoxButton.UseVisualStyleBackColor = true;
            this.downloadToBoxButton.Click += new System.EventHandler(this.downloadToBoxButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 762);
            this.Controls.Add(this.downloadToBoxButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lidMaximum);
            this.Controls.Add(this.lidMinimum);
            this.Controls.Add(this.switchMaximum);
            this.Controls.Add(this.switchMinimum);
            this.Controls.Add(this.animateSlidersCheckbox);
            this.Controls.Add(this.currentSequenceSize);
            this.Controls.Add(this.RunSelectedSequenceButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AvailableMemory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sequenceCollection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EditBehaviorButton);
            this.Controls.Add(this.lidTrackBar);
            this.Controls.Add(this.switchTrackBar);
            this.Name = "Form1";
            this.Text = "Useless machine Controller v1.0";
            ((System.ComponentModel.ISupportInitialize)(this.switchTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lidTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar switchTrackBar;
        private System.Windows.Forms.TrackBar lidTrackBar;
        private System.Windows.Forms.Button EditBehaviorButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox sequenceCollection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label AvailableMemory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button RunSelectedSequenceButton;
        private System.Windows.Forms.Label currentSequenceSize;
        private System.Windows.Forms.CheckBox animateSlidersCheckbox;
        private System.Windows.Forms.Label switchMinimum;
        private System.Windows.Forms.Label switchMaximum;
        private System.Windows.Forms.Label lidMinimum;
        private System.Windows.Forms.Label lidMaximum;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ComboBox baudrateSelectionComboBox;
        private System.Windows.Forms.Button opencloseButton;
        private System.Windows.Forms.ComboBox commPortComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button downloadToBoxButton;
    }
}

