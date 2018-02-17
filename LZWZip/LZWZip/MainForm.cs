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

            openFileDialog.InitialDirectory = "c:\\";
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
            myCompressor.InputStreams.Add(importStream);

            Stream myStream = myCompressor.Run();
            FileStream fileOutput = File.Open(filePath + ".lzw", FileMode.Create);
            myStream.CopyTo(fileOutput);
            myStream.Close(); // Cleanup
            fileOutput.Close(); // More cleanup
        }

        private void decompressionImportButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "lzw files (*.lzw)|*.lzw";
            // openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            importStream = myStream;
                            filePath = openFileDialog.FileName;
                            decompressionFileLocationTextBox.Text = fileLocationTextBox.Text = openFileDialog.FileName; 
                            // Insert code to read the stream here.
                        }
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
            //myDecompressor.InputStreams = importStream; // User file is the path to the compressed file
            //Stream myStream = myDecompressor.Run();
            FileStream fileOutput = File.Open(filePath.Substring(0, filePath.Length - 5), FileMode.Create); // Path to the decompressed file
            //myStream.CopyTo(fileOutput);
            //myStream.Close();
            fileOutput.Close(); // Again, cleanup
        }
    }
}
