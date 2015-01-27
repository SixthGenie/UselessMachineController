/*
COPYRIGHT NOTICE for the  UseLessMachineController

Copyright (c) 2014-2015 Kjetil Næss.

This program is free software for personal use only. You may modify and/or distribute as you like as long as this message
is included in the distribution.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY whatsoever. It if works it works, if it doesn't, you have the source to fix it
(remember to let me know so that I can fix it on my end).
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UselessMachineController
{
    public partial class EditSequenceForm : Form
    {
        public SequenceItem sequenceItem { get; private set; }
        public EditSequenceForm(SequenceItem item)
        {
            InitializeComponent();
            sequenceItem = item;
            sequenceName.Text = sequenceItem.SequenceName;
            Sequence.Text = sequenceItem.Sequence;
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            sequenceItem.SequenceName = sequenceName.Text;
            sequenceItem.Sequence = Sequence.Text;

            if (!sequenceItem.Parse())
            {
                MessageBox.Show("Sequence contains errors and cannot run.");
                return;
            }
            sequenceItem.AngerLevel = int.Parse(angerLevel.Text);
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Dispose();
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Dispose();
        }
        private void testCompileButton_Click(object sender, EventArgs e)
        {
            sequenceItem.Sequence = Sequence.Text;
            if (!sequenceItem.Parse())
            {
                MessageBox.Show("Sequence contains errors and cannot run. Fix error before saving, or Cancel edit");
            }
        }
    }
}
