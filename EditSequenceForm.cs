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
