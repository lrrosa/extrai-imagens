using System.Windows.Forms;

namespace Extrai_Imagens
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnPastaOrigem = new System.Windows.Forms.Button();
            this.txtPastaOrigem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserOrigem = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.comboResolucao = new System.Windows.Forms.ComboBox();
            this.btnConverter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkPdf = new System.Windows.Forms.CheckBox();
            this.chkCbr = new System.Windows.Forms.CheckBox();
            this.chkCbz = new System.Windows.Forms.CheckBox();
            this.chkCb7 = new System.Windows.Forms.CheckBox();
            this.comboFormato = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSubdirs = new System.Windows.Forms.CheckBox();
            this.txtLimparLog = new System.Windows.Forms.Label();
            this.comboQtdPaginas = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPastaDestino = new System.Windows.Forms.TextBox();
            this.btnPastaDestino = new System.Windows.Forms.Button();
            this.folderBrowserDestino = new System.Windows.Forms.FolderBrowserDialog();
            this.txtVersao = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPastaOrigem
            // 
            this.btnPastaOrigem.Location = new System.Drawing.Point(507, 27);
            this.btnPastaOrigem.Name = "btnPastaOrigem";
            this.btnPastaOrigem.Size = new System.Drawing.Size(80, 26);
            this.btnPastaOrigem.TabIndex = 0;
            this.btnPastaOrigem.Text = "Procurar";
            this.btnPastaOrigem.UseVisualStyleBackColor = true;
            this.btnPastaOrigem.Click += new System.EventHandler(this.btnPastaOrigem_Click);
            // 
            // txtPastaOrigem
            // 
            this.txtPastaOrigem.Location = new System.Drawing.Point(12, 29);
            this.txtPastaOrigem.Name = "txtPastaOrigem";
            this.txtPastaOrigem.Size = new System.Drawing.Size(475, 22);
            this.txtPastaOrigem.TabIndex = 1;
            this.txtPastaOrigem.TextChanged += new System.EventHandler(this.txtCaminho_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pasta com os arquivos para extração das imagens:";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Font = new System.Drawing.Font("Arial", 9F);
            this.txtLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLog.Location = new System.Drawing.Point(0, 268);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(601, 124);
            this.txtLog.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log de execução:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Resolução final (DPI):";
            // 
            // comboResolucao
            // 
            this.comboResolucao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboResolucao.FormattingEnabled = true;
            this.comboResolucao.Items.AddRange(new object[] {
            "72",
            "96",
            "150",
            "300"});
            this.comboResolucao.Location = new System.Drawing.Point(147, 170);
            this.comboResolucao.Name = "comboResolucao";
            this.comboResolucao.Size = new System.Drawing.Size(67, 24);
            this.comboResolucao.TabIndex = 6;
            // 
            // btnConverter
            // 
            this.btnConverter.Enabled = false;
            this.btnConverter.Location = new System.Drawing.Point(233, 217);
            this.btnConverter.Name = "btnConverter";
            this.btnConverter.Size = new System.Drawing.Size(126, 32);
            this.btnConverter.TabIndex = 7;
            this.btnConverter.Text = "Iniciar a extração";
            this.btnConverter.UseVisualStyleBackColor = true;
            this.btnConverter.Click += new System.EventHandler(this.btnConverter_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Processar estes tipos de arquivos:";
            // 
            // chkPdf
            // 
            this.chkPdf.AutoSize = true;
            this.chkPdf.Checked = true;
            this.chkPdf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPdf.Location = new System.Drawing.Point(280, 134);
            this.chkPdf.Name = "chkPdf";
            this.chkPdf.Size = new System.Drawing.Size(52, 20);
            this.chkPdf.TabIndex = 9;
            this.chkPdf.Text = "PDF";
            this.chkPdf.UseVisualStyleBackColor = true;
            // 
            // chkCbr
            // 
            this.chkCbr.AutoSize = true;
            this.chkCbr.Checked = true;
            this.chkCbr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCbr.Location = new System.Drawing.Point(338, 134);
            this.chkCbr.Name = "chkCbr";
            this.chkCbr.Size = new System.Drawing.Size(53, 20);
            this.chkCbr.TabIndex = 10;
            this.chkCbr.Text = "CBR";
            this.chkCbr.UseVisualStyleBackColor = true;
            // 
            // chkCbz
            // 
            this.chkCbz.AutoSize = true;
            this.chkCbz.Checked = true;
            this.chkCbz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCbz.Location = new System.Drawing.Point(397, 134);
            this.chkCbz.Name = "chkCbz";
            this.chkCbz.Size = new System.Drawing.Size(51, 20);
            this.chkCbz.TabIndex = 11;
            this.chkCbz.Text = "CBZ";
            this.chkCbz.UseVisualStyleBackColor = true;
            // 
            // chkCb7
            // 
            this.chkCb7.AutoSize = true;
            this.chkCb7.Checked = true;
            this.chkCb7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCb7.Location = new System.Drawing.Point(454, 134);
            this.chkCb7.Name = "chkCb7";
            this.chkCb7.Size = new System.Drawing.Size(51, 20);
            this.chkCb7.TabIndex = 12;
            this.chkCb7.Text = "CB7";
            this.chkCb7.UseVisualStyleBackColor = true;
            // 
            // comboFormato
            // 
            this.comboFormato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFormato.FormattingEnabled = true;
            this.comboFormato.Items.AddRange(new object[] {
            "JPG",
            "PNG"});
            this.comboFormato.Location = new System.Drawing.Point(169, 132);
            this.comboFormato.Name = "comboFormato";
            this.comboFormato.Size = new System.Drawing.Size(67, 24);
            this.comboFormato.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Formato de imagem final:";
            // 
            // chkSubdirs
            // 
            this.chkSubdirs.AutoSize = true;
            this.chkSubdirs.Location = new System.Drawing.Point(345, 8);
            this.chkSubdirs.Name = "chkSubdirs";
            this.chkSubdirs.Size = new System.Drawing.Size(149, 20);
            this.chkSubdirs.TabIndex = 16;
            this.chkSubdirs.Text = "Processar subpastas";
            this.chkSubdirs.UseVisualStyleBackColor = true;
            // 
            // txtLimparLog
            // 
            this.txtLimparLog.AutoSize = true;
            this.txtLimparLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtLimparLog.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Underline);
            this.txtLimparLog.ForeColor = System.Drawing.Color.Blue;
            this.txtLimparLog.Location = new System.Drawing.Point(542, 251);
            this.txtLimparLog.Name = "txtLimparLog";
            this.txtLimparLog.Size = new System.Drawing.Size(56, 14);
            this.txtLimparLog.TabIndex = 17;
            this.txtLimparLog.Text = "Limpar log";
            this.txtLimparLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtLimparLog.Click += new System.EventHandler(this.txtLimparLog_Click);
            // 
            // comboQtdPaginas
            // 
            this.comboQtdPaginas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboQtdPaginas.FormattingEnabled = true;
            this.comboQtdPaginas.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboQtdPaginas.Location = new System.Drawing.Point(326, 170);
            this.comboQtdPaginas.Name = "comboQtdPaginas";
            this.comboQtdPaginas.Size = new System.Drawing.Size(43, 24);
            this.comboQtdPaginas.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Extrair a(s)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(223, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "primeira(s) imagen(s) de cada arquivo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(244, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "Pasta de destino das imagens extraídas:";
            // 
            // txtPastaDestino
            // 
            this.txtPastaDestino.Location = new System.Drawing.Point(12, 81);
            this.txtPastaDestino.Name = "txtPastaDestino";
            this.txtPastaDestino.Size = new System.Drawing.Size(475, 22);
            this.txtPastaDestino.TabIndex = 22;
            // 
            // btnPastaDestino
            // 
            this.btnPastaDestino.Location = new System.Drawing.Point(507, 79);
            this.btnPastaDestino.Name = "btnPastaDestino";
            this.btnPastaDestino.Size = new System.Drawing.Size(80, 26);
            this.btnPastaDestino.TabIndex = 21;
            this.btnPastaDestino.Text = "Procurar";
            this.btnPastaDestino.UseVisualStyleBackColor = true;
            this.btnPastaDestino.Click += new System.EventHandler(this.btnPastaDestino_Click);
            // 
            // txtVersao
            // 
            this.txtVersao.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersao.Location = new System.Drawing.Point(508, 2);
            this.txtVersao.Name = "txtVersao";
            this.txtVersao.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtVersao.Size = new System.Drawing.Size(91, 16);
            this.txtVersao.TabIndex = 24;
            this.txtVersao.Text = "versão";
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 392);
            this.Controls.Add(this.txtVersao);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPastaDestino);
            this.Controls.Add(this.btnPastaDestino);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboQtdPaginas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLimparLog);
            this.Controls.Add(this.chkSubdirs);
            this.Controls.Add(this.comboFormato);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkCb7);
            this.Controls.Add(this.chkCbz);
            this.Controls.Add(this.chkCbr);
            this.Controls.Add(this.chkPdf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnConverter);
            this.Controls.Add(this.comboResolucao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPastaOrigem);
            this.Controls.Add(this.btnPastaOrigem);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datassette - Extrator de Imagens";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private Button btnPastaOrigem;
        private TextBox txtPastaOrigem;
        private Label label1;
        private TextBox txtLog;
        private Label label2;
        private FolderBrowserDialog folderBrowserOrigem;
        private Label label3;
        private ComboBox comboResolucao;
        private Button btnConverter;
        private Label label4;
        private CheckBox chkPdf;
        private CheckBox chkCbr;
        private CheckBox chkCbz;
        private CheckBox chkCb7;
        private ComboBox comboFormato;
        private Label label5;
        private CheckBox chkSubdirs;
        private Label txtLimparLog;
        private ComboBox comboQtdPaginas;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtPastaDestino;
        private Button btnPastaDestino;
        private FolderBrowserDialog folderBrowserDestino;
        private Label txtVersao;
    }
}