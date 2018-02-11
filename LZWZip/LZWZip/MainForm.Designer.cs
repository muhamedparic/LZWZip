namespace LZWZip
{
    partial class MainForm
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
            this.compressionTabControl = new System.Windows.Forms.TabControl();
            this.CompressionTab = new System.Windows.Forms.TabPage();
            this.DecompressionTab = new System.Windows.Forms.TabPage();
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.importFileButton = new System.Windows.Forms.Button();
            this.maxSymbolLengthLabel = new System.Windows.Forms.Label();
            this.symbolLengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.automaticSymbolLengthCheckbox = new System.Windows.Forms.CheckBox();
            this.compressionModeLabel = new System.Windows.Forms.Label();
            this.compressionModeComboBox = new System.Windows.Forms.ComboBox();
            this.decompressionFileLocationTextBox = new System.Windows.Forms.TextBox();
            this.decompressionImportButton = new System.Windows.Forms.Button();
            this.compressionTabControl.SuspendLayout();
            this.CompressionTab.SuspendLayout();
            this.DecompressionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolLengthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // compressionTabControl
            // 
            this.compressionTabControl.Controls.Add(this.CompressionTab);
            this.compressionTabControl.Controls.Add(this.DecompressionTab);
            this.compressionTabControl.Location = new System.Drawing.Point(-4, 0);
            this.compressionTabControl.Name = "compressionTabControl";
            this.compressionTabControl.SelectedIndex = 0;
            this.compressionTabControl.Size = new System.Drawing.Size(517, 253);
            this.compressionTabControl.TabIndex = 0;
            // 
            // CompressionTab
            // 
            this.CompressionTab.Controls.Add(this.compressionModeComboBox);
            this.CompressionTab.Controls.Add(this.compressionModeLabel);
            this.CompressionTab.Controls.Add(this.automaticSymbolLengthCheckbox);
            this.CompressionTab.Controls.Add(this.symbolLengthUpDown);
            this.CompressionTab.Controls.Add(this.maxSymbolLengthLabel);
            this.CompressionTab.Controls.Add(this.importFileButton);
            this.CompressionTab.Controls.Add(this.fileLocationTextBox);
            this.CompressionTab.Location = new System.Drawing.Point(4, 22);
            this.CompressionTab.Name = "CompressionTab";
            this.CompressionTab.Padding = new System.Windows.Forms.Padding(3);
            this.CompressionTab.Size = new System.Drawing.Size(509, 227);
            this.CompressionTab.TabIndex = 0;
            this.CompressionTab.Text = "Compression";
            this.CompressionTab.UseVisualStyleBackColor = true;
            // 
            // DecompressionTab
            // 
            this.DecompressionTab.Controls.Add(this.decompressionImportButton);
            this.DecompressionTab.Controls.Add(this.decompressionFileLocationTextBox);
            this.DecompressionTab.Location = new System.Drawing.Point(4, 22);
            this.DecompressionTab.Name = "DecompressionTab";
            this.DecompressionTab.Padding = new System.Windows.Forms.Padding(3);
            this.DecompressionTab.Size = new System.Drawing.Size(509, 227);
            this.DecompressionTab.TabIndex = 1;
            this.DecompressionTab.Text = "Decompression";
            this.DecompressionTab.UseVisualStyleBackColor = true;
            // 
            // fileLocationTextBox
            // 
            this.fileLocationTextBox.Location = new System.Drawing.Point(12, 15);
            this.fileLocationTextBox.Name = "fileLocationTextBox";
            this.fileLocationTextBox.Size = new System.Drawing.Size(392, 20);
            this.fileLocationTextBox.TabIndex = 0;
            // 
            // importFileButton
            // 
            this.importFileButton.Location = new System.Drawing.Point(410, 15);
            this.importFileButton.Name = "importFileButton";
            this.importFileButton.Size = new System.Drawing.Size(89, 20);
            this.importFileButton.TabIndex = 1;
            this.importFileButton.Text = "Import";
            this.importFileButton.UseVisualStyleBackColor = true;
            // 
            // maxSymbolLengthLabel
            // 
            this.maxSymbolLengthLabel.AutoSize = true;
            this.maxSymbolLengthLabel.Location = new System.Drawing.Point(12, 50);
            this.maxSymbolLengthLabel.Name = "maxSymbolLengthLabel";
            this.maxSymbolLengthLabel.Size = new System.Drawing.Size(94, 13);
            this.maxSymbolLengthLabel.TabIndex = 2;
            this.maxSymbolLengthLabel.Text = "Max symbol length";
            // 
            // symbolLengthUpDown
            // 
            this.symbolLengthUpDown.Location = new System.Drawing.Point(122, 43);
            this.symbolLengthUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.symbolLengthUpDown.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.symbolLengthUpDown.Name = "symbolLengthUpDown";
            this.symbolLengthUpDown.Size = new System.Drawing.Size(162, 20);
            this.symbolLengthUpDown.TabIndex = 3;
            this.symbolLengthUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // automaticSymbolLengthCheckbox
            // 
            this.automaticSymbolLengthCheckbox.AutoSize = true;
            this.automaticSymbolLengthCheckbox.Location = new System.Drawing.Point(122, 69);
            this.automaticSymbolLengthCheckbox.Name = "automaticSymbolLengthCheckbox";
            this.automaticSymbolLengthCheckbox.Size = new System.Drawing.Size(162, 17);
            this.automaticSymbolLengthCheckbox.TabIndex = 4;
            this.automaticSymbolLengthCheckbox.Text = "Automatic max symbol length";
            this.automaticSymbolLengthCheckbox.UseVisualStyleBackColor = true;
            this.automaticSymbolLengthCheckbox.CheckedChanged += new System.EventHandler(this.automaticSymbolLengthCheckbox_CheckedChanged);
            // 
            // compressionModeLabel
            // 
            this.compressionModeLabel.AutoSize = true;
            this.compressionModeLabel.Location = new System.Drawing.Point(12, 111);
            this.compressionModeLabel.Name = "compressionModeLabel";
            this.compressionModeLabel.Size = new System.Drawing.Size(96, 13);
            this.compressionModeLabel.TabIndex = 5;
            this.compressionModeLabel.Text = "Compression mode";
            // 
            // compressionModeComboBox
            // 
            this.compressionModeComboBox.FormattingEnabled = true;
            this.compressionModeComboBox.Items.AddRange(new object[] {
            "Auto",
            "Join-then-compress",
            "Compress-then-join"});
            this.compressionModeComboBox.Location = new System.Drawing.Point(122, 103);
            this.compressionModeComboBox.Name = "compressionModeComboBox";
            this.compressionModeComboBox.Size = new System.Drawing.Size(162, 21);
            this.compressionModeComboBox.TabIndex = 6;
            // 
            // decompressionFileLocationTextBox
            // 
            this.decompressionFileLocationTextBox.Location = new System.Drawing.Point(12, 18);
            this.decompressionFileLocationTextBox.Name = "decompressionFileLocationTextBox";
            this.decompressionFileLocationTextBox.Size = new System.Drawing.Size(371, 20);
            this.decompressionFileLocationTextBox.TabIndex = 0;
            // 
            // decompressionImportButton
            // 
            this.decompressionImportButton.Location = new System.Drawing.Point(400, 15);
            this.decompressionImportButton.Name = "decompressionImportButton";
            this.decompressionImportButton.Size = new System.Drawing.Size(90, 23);
            this.decompressionImportButton.TabIndex = 1;
            this.decompressionImportButton.Text = "Import";
            this.decompressionImportButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 245);
            this.Controls.Add(this.compressionTabControl);
            this.Name = "MainForm";
            this.Text = "LZW";
            this.compressionTabControl.ResumeLayout(false);
            this.CompressionTab.ResumeLayout(false);
            this.CompressionTab.PerformLayout();
            this.DecompressionTab.ResumeLayout(false);
            this.DecompressionTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolLengthUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl compressionTabControl;
        private System.Windows.Forms.TabPage CompressionTab;
        private System.Windows.Forms.TabPage DecompressionTab;
        private System.Windows.Forms.CheckBox automaticSymbolLengthCheckbox;
        private System.Windows.Forms.NumericUpDown symbolLengthUpDown;
        private System.Windows.Forms.Label maxSymbolLengthLabel;
        private System.Windows.Forms.Button importFileButton;
        private System.Windows.Forms.TextBox fileLocationTextBox;
        private System.Windows.Forms.ComboBox compressionModeComboBox;
        private System.Windows.Forms.Label compressionModeLabel;
        private System.Windows.Forms.Button decompressionImportButton;
        private System.Windows.Forms.TextBox decompressionFileLocationTextBox;
    }
}

