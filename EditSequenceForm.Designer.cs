namespace UselessMachineController
{
    partial class EditSequenceForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.sequenceName = new System.Windows.Forms.TextBox();
            this.Sequence = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.testCompileButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.angerLevel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // sequenceName
            // 
            this.sequenceName.Location = new System.Drawing.Point(61, 17);
            this.sequenceName.Name = "sequenceName";
            this.sequenceName.Size = new System.Drawing.Size(136, 20);
            this.sequenceName.TabIndex = 1;
            // 
            // Sequence
            // 
            this.Sequence.Location = new System.Drawing.Point(26, 98);
            this.Sequence.Multiline = true;
            this.Sequence.Name = "Sequence";
            this.Sequence.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Sequence.Size = new System.Drawing.Size(240, 154);
            this.Sequence.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sequence";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(111, 303);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(192, 303);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // testCompileButton
            // 
            this.testCompileButton.Location = new System.Drawing.Point(20, 303);
            this.testCompileButton.Name = "testCompileButton";
            this.testCompileButton.Size = new System.Drawing.Size(75, 23);
            this.testCompileButton.TabIndex = 6;
            this.testCompileButton.Text = "Compile";
            this.testCompileButton.UseVisualStyleBackColor = true;
            this.testCompileButton.Click += new System.EventHandler(this.testCompileButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Anger Level";
            // 
            // angerLevel
            // 
            this.angerLevel.Location = new System.Drawing.Point(92, 51);
            this.angerLevel.Name = "angerLevel";
            this.angerLevel.Size = new System.Drawing.Size(27, 20);
            this.angerLevel.TabIndex = 8;
            // 
            // EditSequenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 333);
            this.Controls.Add(this.angerLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.testCompileButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Sequence);
            this.Controls.Add(this.sequenceName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditSequenceForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Sequence";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sequenceName;
        private System.Windows.Forms.TextBox Sequence;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button testCompileButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox angerLevel;
    }
}