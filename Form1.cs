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
using System.Drawing.Drawing2D;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.IO.Ports;

namespace UselessMachineController
{
    public partial class Form1 : Form
    {
        Box leaveMeAloneBox = new Box();
        ContextMenu sequencePopupMenu;
        
        //I'm using a Pro Mini compatible with an 328p chip. 
        // This as 1024 bytes of EEPROM available.
        const int MaxMemorySize = 1024;
        int MemorySize = MaxMemorySize;
        SerialPort commPort = new SerialPort("COM13");
        
        public Form1()
        {
            InitializeComponent();
            //This form is double buffered
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            leaveMeAloneBox.Refresh += new Box.refreshHandler(leaveMeAloneBox_Refresh);
            
            sequencePopupMenu = new System.Windows.Forms.ContextMenu();
            sequencePopupMenu.MenuItems.Add(new MenuItem("Insert new sequence", InsertSequenceHandler));
            sequencePopupMenu.MenuItems.Add(new MenuItem("Delete this sequence", DeleteSequenceHandler));
            sequencePopupMenu.MenuItems.Add(new MenuItem("Edit this sequence", EditSequenceHandler));
            sequenceCollection.ContextMenu = sequencePopupMenu;

            if (!File.Exists("Sequences.xml"))
                sequenceCollection.Items.Add(DefaultSequence());
            else
                loadSequences();

            sequenceCollection.SelectedIndex = 0;

            switchTrackBar.Value = Box.switchServoStartPos;
            switchTrackBar.Minimum = Box.switchMinPos;
            switchTrackBar.Maximum = Box.switchMaxPos;
            switchMinimum.Text = Box.switchMinPos.ToString();
            switchMaximum.Text = Box.switchMaxPos.ToString();

            lidTrackBar.Value = Box.lidServoStartPos;
            lidTrackBar.Minimum = Box.lidMinPos;
            lidTrackBar.Maximum = Box.lidMaxPos;
            lidMinimum.Text = Box.lidMinPos.ToString();
            lidMaximum.Text = Box.lidMaxPos.ToString();
            label1.Text = leaveMeAloneBox.lidCurrentPos.ToString();
            loadPortSelector();

        }
        // Scans for comm ports and puts them in the comm port combo box.
        private void loadPortSelector()
        {
            commPortComboBox.Items.Clear();
            string[] portList = SerialPort.GetPortNames();

            if (portList.Count() < 1)
            {
                SetCommControlsEnabled(false);
                MessageBox.Show("No serial ports found.", "Useless Machine Controller problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetCommControlsEnabled(true);
                foreach (var portIdentifier in SerialPort.GetPortNames())
                {
                    commPortComboBox.Items.Add(portIdentifier);
                }
                opencloseButton.Text = "Open";
                commPortComboBox.SelectedIndex = 0;
                baudrateSelectionComboBox.SelectedIndex = 5;
            }
        }
        void UpdateAvailableMemory()
        {
            setCurrentSequenceSize((sequenceCollection.Items[sequenceCollection.SelectedIndex] as SequenceItem).SequenceSize);
            MemorySize = MaxMemorySize;
            foreach (var item in sequenceCollection.Items)
            {
                MemorySize -= (item as SequenceItem).SequenceSize;
            }
            AvailableMemory.Text = MemorySize.ToString();
            SequenceItem s = ((SequenceItem)sequenceCollection.Items[0]);
            //for (var i = 0; i < s.SequenceData.Length; i++)
            //{
            //    System.Console.Write("{0}, ", s.SequenceData[i]);
            //}
        }
        void setCurrentSequenceSize(int size)
        {
            currentSequenceSize.Text = string.Format("{0} bytes", size);
        }
        SequenceItem DefaultSequence()
        {
            SequenceItem item = new SequenceItem();
            item.SequenceName = "Sequence 0";
            item.Sequence = "Lid(900,1300,4000)\r\n" +
            "Delay(1000)\r\n" +
            "Lid(1300,1400,500)\r\n" +
            "Delay(1000)\r\n" +
            "Switch(1050,1900,2000)\r\n" +
            "Delay(300)\r\n" +
            "Switch(1900,2200,500)\r\n" +
            "Delay(100)\r\n" +
            "Switch(2200,1050,2000)\r\n" +
            "Delay(100)\r\n" +
            "Lid(1400,900,2000)\r\n";
            item.Parse();

            UpdateAvailableMemory();

            return item;
        }

        void InsertSequenceHandler(object sender, EventArgs e)
        {
            var sequenceItem = new SequenceItem();
            EditSequenceForm f = new EditSequenceForm(sequenceItem);
            var result = f.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                sequenceCollection.Items.Add (f.sequenceItem);
                saveSequences();
            }
            UpdateAvailableMemory();
        }
        void DeleteSequenceHandler(object sender, EventArgs e)
        {
            saveSequences();
            UpdateAvailableMemory();
        }
        void EditSequenceHandler(object sender, EventArgs e)
        {
            var sequenceItem = (sequenceCollection.SelectedItem as SequenceItem);
            EditSequenceForm f = new EditSequenceForm(sequenceItem);
            var result = f.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                sequenceCollection.Items[sequenceCollection.SelectedIndex] = f.sequenceItem;
                saveSequences();
            }
            UpdateAvailableMemory();
        }
        void saveSequences()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SequenceItemsList));
            SequenceItemsList sequencelist = new SequenceItemsList();

            foreach (var item in sequenceCollection.Items)
            {
                sequencelist.sequenceList.Add((item as SequenceItem));
                // We do not want to serialize out the source. 
                // Set to null so the XmlSerializer ignores it.
                sequencelist.sequenceList[sequencelist.sequenceList.Count - 1].Sequence = null;
            }
            
            using (TextWriter writer = new StreamWriter("Sequences.xml"))
            {
                serializer.Serialize(writer, sequencelist);
            }
            // Put back source so we can edit it if we want to
            foreach (var item in sequenceCollection.Items)
            {
                (item as SequenceItem).Decompile();
            }
        }
        public class SequenceItemsList
        {
            [XmlElement("SequenceItem")]
            public List<SequenceItem> sequenceList = new List<SequenceItem>();
        }
        void loadSequences()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(SequenceItemsList));
            TextReader reader = new StreamReader("Sequences.xml");
            SequenceItemsList sequenceList = deserializer.Deserialize(reader) as SequenceItemsList;
            reader.Close();
            foreach (var item in sequenceList.sequenceList)
            {
                item.Decompile();
                sequenceCollection.Items.Add(item);
            }
            sequenceCollection.SelectedIndex = 0;
            UpdateAvailableMemory();
        }

        void leaveMeAloneBox_Refresh()
        {
            if (animateSlidersCheckbox.Checked)
            {
                Invoke(new Action(() => switchTrackBar.Value = leaveMeAloneBox.switchCurrentPos));
                Invoke(new Action(() => lidTrackBar.Value = leaveMeAloneBox.lidCurrentPos));
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            leaveMeAloneBox.RenderObject(e.Graphics);
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            leaveMeAloneBox.switchCurrentPos = switchTrackBar.Value;
            label2.Text = leaveMeAloneBox.switchCurrentPos.ToString();
            Invalidate();
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            leaveMeAloneBox.lidCurrentPos = lidTrackBar.Value;
            label1.Text = leaveMeAloneBox.lidCurrentPos.ToString();
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            leaveMeAloneBox.Sequence0();
        }

        private void RunSelectedSequenceButton_Click(object sender, EventArgs e)
        {
            RunSelectedSequenceButton.Enabled = false;
            leaveMeAloneBox.lidCurrentPos = lidTrackBar.Minimum;
            leaveMeAloneBox.switchCurrentPos = switchTrackBar.Minimum;
            Invalidate();
            Thread.Sleep(2000);
            var sequenceItem = (sequenceCollection.Items[sequenceCollection.SelectedIndex]) as SequenceItem;
            leaveMeAloneBox.RunCompiledSequence(sequenceItem.SequenceData);
            RunSelectedSequenceButton.Enabled = true;
        }

        private void sequenceCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sequenceItem = (sequenceCollection.Items[sequenceCollection.SelectedIndex]) as SequenceItem;
            setCurrentSequenceSize(sequenceItem.SequenceData.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BehaviorEditor f = new BehaviorEditor();
            var result = f.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
            }
            else
            {
            }
            UpdateAvailableMemory();
        }
        bool OpenCommConnection()
        {
            commPort.BaudRate = 57600;
            commPort.Parity = Parity.None;
            commPort.DataBits = 8;
            commPort.StopBits = StopBits.One;
            commPort.Handshake = Handshake.None;
            commPort.DataReceived += new SerialDataReceivedEventHandler(commPort_DataReceived);
            commPort.Open();

            return true;
        }
        void CloseCommConnection()
        {
            commPort.Close();
        }
        void SetCommControlsEnabled(bool enabled)
        {
            opencloseButton.Enabled = enabled;
            commPortComboBox.Enabled = enabled;
            baudrateSelectionComboBox.Enabled = enabled;
        }
        void commPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var s = commPort.ReadExisting();
            System.Console.Write(s);
        }
        private void opencloseButton_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text.ToLower().Equals("open", StringComparison.InvariantCultureIgnoreCase))
            {

                bool ret = OpenCommConnection();

                if (ret)
                {
                    opencloseButton.Text = "Close";
                    downloadToBoxButton.Enabled = true;
                }
            }
            else
            {
                this.CloseCommConnection();
                opencloseButton.Text = "Open";
                downloadToBoxButton.Enabled = false;
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            loadPortSelector();
        }

        private void downloadToBoxButton_Click(object sender, EventArgs e)
        {
            List<byte> sequences = new List<byte>();
            sequences.Add((byte)sequenceCollection.Items.Count); // numSequences
            int i = 0;
            foreach(var item in sequenceCollection.Items)
            {
                System.Console.WriteLine("Placing sequencesdata at index {0}", i);
                var sequence = (item as SequenceItem);                
                sequences.Add((byte)sequence.SequenceData.Length); // sequenceSize
                sequences.Add((byte)sequence.AngerLevel);
                sequences.AddRange(sequence.SequenceData);
                i += sequence.SequenceData.Length;

            }
            var lowByte = (byte)sequences.Count;
            var highByte = (byte)(sequences.Count >> 8);
            sequences.Insert(0, highByte); // R
            sequences.Insert(0, lowByte); // R
            sequences.Insert(0, 18);

            System.Console.WriteLine("Sending download command");
            System.Console.WriteLine("DataSize: {0}", (sequences[2] << 8) | sequences[1]);
            foreach (var b in sequences)
            {
                commPort.Write(new byte[] { b }, 0, 1);
                Thread.Sleep(10);
            }
            //commPort.Write(sequences.ToArray(), 0, sequences.Count);
        }

    }
}
