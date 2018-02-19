using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZWZip
{
    public partial class MainForm : Form
    {
        Stream importStream = null;
        string filePath = "";

        public MainForm()
        {
            InitializeComponent();
            compressionModeComboBox.SelectedIndex = 0;
        }

        private void automaticSymbolLengthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (automaticSymbolLengthCheckbox.Checked)
            {
                symbolLengthUpDown.Enabled = false;
            }
            else
            {
                symbolLengthUpDown.Enabled = true;
            }
        }

        private void importFileButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "All files (*.*)|*.*";
            // openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        importStream = myStream;
                        filePath = openFileDialog.FileName;
                        fileLocationTextBox.Text = openFileDialog.FileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void compressionButton_Click(object sender, EventArgs e)
        {

            uint maxSymbolLength = (uint) symbolLengthUpDown.Value;
            Compressor myCompressor = new Compressor();
            myCompressor.MaxSymbolLength = maxSymbolLength;

            if (automaticSymbolLengthCheckbox.Checked)
                myCompressor.MaxSymbolLength = 255;

            myCompressor.InputStreams.Add(importStream);
            Stream myStream = myCompressor.Run();
            myStream.Seek(0, SeekOrigin.Begin);

            using (FileStream fileOutput = File.Open(filePath + ".lzw", FileMode.Create))
                myStream.CopyTo(fileOutput);

            myStream.Close();
        }

        private void decompressionImportButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "lzw files (*.lzw)|*.lzw";
            // openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        importStream = myStream;
                        filePath = openFileDialog.FileName;
                        decompressionFileLocationTextBox.Text = fileLocationTextBox.Text = openFileDialog.FileName; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void decompressionButton_Click(object sender, EventArgs e)
        {
            Decompressor myDecompressor = new Decompressor();
            myDecompressor.InputStream = importStream;
            Stream myStream = myDecompressor.Run();
            myStream.Seek(0, SeekOrigin.Begin);
            using (FileStream fileOutput = File.Open(filePath.Substring(0, filePath.Length - 4), FileMode.Create))
                myStream.CopyTo(fileOutput);

            myStream.Close();
        }
    }
}
