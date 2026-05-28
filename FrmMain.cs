using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Ghostscript.NET;
using ImageMagick;
using SevenZip;

namespace Extrai_Imagens
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            btnConverter.Left = (this.Size.Width / 2) - (btnConverter.Width / 2);

            var v = Assembly.GetExecutingAssembly().GetName().Version;
            txtVersao.Text = "v. " + v.Major + "." + v.Minor + "." + v.Build;

            LoadConfigs();
        }


        private void LoadConfigs()
        {
            txtPastaOrigem.Text = Properties.Settings.Default.PastaOrigem;
            txtPastaDestino.Text = Properties.Settings.Default.PastaDestino;

            chkPdf.Checked = Properties.Settings.Default.ProcessaPdf;
            chkCbr.Checked = Properties.Settings.Default.ProcessaCbr;
            chkCbz.Checked = Properties.Settings.Default.ProcessaCbz;
            chkCb7.Checked = Properties.Settings.Default.ProcessaCb7;

            comboResolucao.SelectedIndex = Properties.Settings.Default.ResolucaoFinal;
            comboFormato.SelectedIndex = Properties.Settings.Default.FormatoFinal;
            comboQtdPaginas.SelectedIndex = Properties.Settings.Default.QtdPaginas;
        }


        private void SaveConfigs()
        {
            Properties.Settings.Default.PastaOrigem = txtPastaOrigem.Text;
            Properties.Settings.Default.PastaDestino = txtPastaDestino.Text;

            Properties.Settings.Default.ProcessaPdf = chkPdf.Checked;
            Properties.Settings.Default.ProcessaCbr = chkCbr.Checked;
            Properties.Settings.Default.ProcessaCbz = chkCbz.Checked;
            Properties.Settings.Default.ProcessaCb7 = chkCb7.Checked;

            Properties.Settings.Default.ResolucaoFinal = comboResolucao.SelectedIndex;
            Properties.Settings.Default.FormatoFinal = comboFormato.SelectedIndex;
            Properties.Settings.Default.QtdPaginas = comboQtdPaginas.SelectedIndex;

            Properties.Settings.Default.Save();
        }


        private void btnPastaOrigem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtPastaOrigem.Text))
            {
                folderBrowserOrigem.SelectedPath = txtPastaOrigem.Text;
            }
            else
            {
                folderBrowserOrigem.SelectedPath = string.Empty;
            }

            folderBrowserOrigem.ShowDialog();
            var path = folderBrowserOrigem.SelectedPath;

            if (path != string.Empty)
            {
                txtPastaOrigem.Text = path;
                btnConverter.Enabled = true;

                if (txtPastaDestino.Text == string.Empty)
                    txtPastaDestino.Text = path;
            }
        }



        private void btnPastaDestino_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtPastaDestino.Text))
            {
                folderBrowserDestino.SelectedPath = txtPastaDestino.Text;
            }
            else if (Directory.Exists(txtPastaOrigem.Text))
            {
                folderBrowserDestino.SelectedPath = txtPastaOrigem.Text;
            }
            else
            {
                folderBrowserDestino.SelectedPath = string.Empty;
            }

            folderBrowserDestino.ShowDialog();
            var path = folderBrowserDestino.SelectedPath;

            if (path != string.Empty)
                txtPastaDestino.Text = path;
        }



        private void btnConverter_Click(object sender, EventArgs e)
        {
            btnConverter.Enabled = false;
            try
            {
                if (Directory.Exists(txtPastaOrigem.Text) == false)
                {
                    AddToLog("Caminho de origem inexistente: " + txtPastaOrigem.Text, true);
                    AddToLog("Selecione um caminho válido!", true);
                    return;
                }

                if (Directory.Exists(txtPastaDestino.Text) == false)
                {
                    AddToLog("Caminho de destino inexistente: " + txtPastaDestino.Text, true);
                    AddToLog("Selecione um caminho válido!", true);
                    return;
                }

                List<string> extensoes = new List<string>();
                if (chkPdf.Checked) extensoes.Add("pdf");
                if (chkCbr.Checked) extensoes.Add("cbr");
                if (chkCbz.Checked) extensoes.Add("cbz");
                if (chkCb7.Checked) extensoes.Add("cb7");

                if (extensoes.Count == 0)
                {
                    AddToLog("Nenhum tipo de arquivo selecionado para processar.", true);
                    return;
                }

                AddToLog("");
                AddToLog(" ---- Início da extração ----");

                foreach (string extensao in extensoes)
                {
                    int qtdArquivos = 0;

                    AddToLog("(**) Extraindo páginas de arquivos " + extensao.ToUpperInvariant() + " ...");
                    try
                    {
                        qtdArquivos = ProcessaPasta(txtPastaOrigem.Text,
                                                    txtPastaDestino.Text, extensao,
                                                    comboFormato.Text.ToLowerInvariant(),
                                                    Convert.ToInt32(comboResolucao.Text),
                                                    Convert.ToInt32(comboQtdPaginas.Text),
                                                    chkSubdirs.Checked);
                    }
                    catch (Exception ex)
                    {
                        AddToLog(ex.Message, true);
                        continue;
                    }

                    if (qtdArquivos == 0)
                        AddToLog("Nenhum arquivo deste tipo encontrado.");
                    else
                        AddToLog("(**) " + qtdArquivos.ToString() + " arquivo(s) " + extensao.ToUpperInvariant() + " processado(s).");
                    AddToLog("");
                }

                AddToLog("----- Fim da extração ------");
            }
            finally
            {
                btnConverter.Enabled = true;
            }
        }



        /// <summary>
        /// Processa os arquivos suportados da pasta escolhida
        /// </summary>
        private int ProcessaPasta(string pastaOrigem, string pastaDestino, string tipoArquivo, string formatoImg, int resolucao, int maxPaginas, bool subdirs)
        {
            int qtdArquivos = 0;

            var opcao = subdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var caminho = Directory.EnumerateFiles(pastaOrigem, "*." + tipoArquivo.ToLowerInvariant(), opcao);

            foreach (string file in caminho)
            {
                AddToLog("Processando " + Path.GetFileName(file));
                try
                {
                    if (string.Equals(tipoArquivo, "pdf", StringComparison.OrdinalIgnoreCase))
                        ExtraiImagensDoPdf(file, pastaDestino, formatoImg, resolucao, maxPaginas);
                    else
                        ExtraiImagensCompactadas(file, pastaDestino, formatoImg, resolucao, maxPaginas);

                    qtdArquivos++;
                }
                catch (Exception ex)
                {
                    AddToLog("Falha ao processar " + Path.GetFileName(file) + ": " + ex.Message, true);
                }
            }

            return qtdArquivos;
        }



        /// <summary>
        /// Extrai as primeiras imagens (até <paramref name="maxPaginas"/>) de um arquivo compactado (CBR/CBZ/CB7).
        /// Os arquivos internos são extraídos para uma pasta temporária — nada é gravado na pasta de origem.
        /// </summary>
        private void ExtraiImagensCompactadas(string caminhoArquivo, string pastaDestino, string formatoImg, int resolucao, int maxPaginas)
        {
            string baseNome = Path.Combine(pastaDestino, Path.GetFileNameWithoutExtension(caminhoArquivo));

            var settings = new MagickReadSettings();
            settings.Density = new Density(resolucao, resolucao);

            // informa onde encontrar o 7z.dll (fica ao lado do .exe)
            SevenZipExtractor.SetLibraryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "7z.dll"));

            // pasta temporária exclusiva por arquivo — evita poluir a pasta de origem e isola eventuais falhas
            string tempDir = Path.Combine(Path.GetTempPath(), "ExtraiImagens_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);

            try
            {
                using (var extractor = new SevenZipExtractor(caminhoArquivo))
                {
                    List<string> arquivos = new List<string>();

                    foreach (string file in extractor.ArchiveFileNames)
                    {
                        // ignora qualquer coisa dentro da pasta "__MACOSX" (quando existir),
                        // checando como segmento de caminho — qualquer separador
                        if (ContemSegmento(file, "__MACOSX")) continue;

                        if (!ExtensoesImagem.Contains(Path.GetExtension(file))) continue;

                        arquivos.Add(file);
                    }

                    // a indexação dos arquivos internos pode não bater com a ordem alfabética/numérica
                    // dos nomes (que é o que o usuário espera) — ordena por nome com "natural sort" (page-2 < page-10)
                    arquivos.Sort(NaturalComparer);

                    int paginaAtual = 1;
                    for (int i = 0; i < arquivos.Count; i++)
                    {
                        if (paginaAtual > maxPaginas) break;

                        extractor.ExtractFiles(tempDir, arquivos[i]);

                        // o SevenZipSharp preserva a estrutura interna dentro de tempDir; normaliza
                        // qualquer separador "/" para "\" antes de combinar
                        string nomeInterno = arquivos[i].Replace('/', Path.DirectorySeparatorChar);
                        string arquivoExtraido = Path.Combine(tempDir, nomeInterno);

                        string destino = (maxPaginas == 1)
                            ? baseNome + "." + formatoImg
                            : baseNome + "-" + paginaAtual.ToString("D2") + "." + formatoImg;

                        using (var image = new MagickImage(arquivoExtraido, settings))
                        {
                            image.Resize(LarguraFinalCbx, 0);
                            image.Write(destino);
                        }

                        paginaAtual++;
                    }
                }
            }
            finally
            {
                // sempre limpa a pasta temporária (recursivo cuida de subdirs em qualquer profundidade)
                try { Directory.Delete(tempDir, recursive: true); } catch { }
            }
        }



        /// <summary>
        /// Extrai imagens de um arquivo PDF
        /// </summary>
        private void ExtraiImagensDoPdf(string caminhoArquivo, string pastaDestino, string formatoImg, int resolucao, int maxPaginas)
        {
            string filename = Path.Combine(pastaDestino, Path.GetFileNameWithoutExtension(caminhoArquivo));

            // não é possível criar uma única variável para os devices abaixo com "var",
            // pq teria que inicializar antes (e são devices diferentes)
            GhostscriptJpegDevice devJpg;
            GhostscriptPngDevice devPng;

            switch (formatoImg)
            {
                case "jpg":
                    devJpg = new GhostscriptJpegDevice(GhostscriptJpegDeviceType.Jpeg);
                    devJpg.JpegQuality = 75;
                    devJpg.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                    devJpg.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                    devJpg.ResolutionXY = new GhostscriptImageDeviceResolution(resolucao, resolucao);
                    devJpg.InputFiles.Add(caminhoArquivo);
                    devJpg.Pdf.FirstPage = 1;
                    devJpg.Pdf.LastPage = maxPaginas;

                    if (maxPaginas == 1)
                        devJpg.OutputPath = filename + "." + formatoImg;
                    else
                        devJpg.OutputPath = filename + "-%02d." + formatoImg;

                    devJpg.Process();
                    break;

                case "png":
                    devPng = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png16m);
                    devPng.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                    devPng.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                    devPng.ResolutionXY = new GhostscriptImageDeviceResolution(resolucao, resolucao);
                    devPng.InputFiles.Add(caminhoArquivo);
                    devPng.Pdf.FirstPage = 1;
                    devPng.Pdf.LastPage = maxPaginas;

                    if (maxPaginas == 1)
                        devPng.OutputPath = filename + "." + formatoImg;
                    else
                        devPng.OutputPath = filename + "-%02d." + formatoImg;

                    devPng.Process();
                    break;
            }
        }



        private void AddToLog(string msg, bool isErro = false)
        {
            if (msg.Length == 0)
            {
                txtLog.AppendText(Environment.NewLine);
                return;
            }

            string cabecalho = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]  ";
            if (isErro)
                cabecalho = cabecalho + "(!!!)  ";

            txtLog.AppendText(cabecalho + msg + Environment.NewLine);
        }


        private void txtLimparLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void txtCaminho_TextChanged(object sender, EventArgs e)
        {
            // só habilita se realmente existir uma pasta válida
            btnConverter.Enabled = Directory.Exists(txtPastaOrigem.Text);
        }


        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0)
                return;

            // só pergunta se há ao menos um arquivo suportado entre os arrastados
            bool temSuportado = false;
            foreach (var f in files)
            {
                if (ExtensoesEntrada.Contains(Path.GetExtension(f)) && TipoHabilitadoNaUI(Path.GetExtension(f)))
                {
                    temSuportado = true;
                    break;
                }
            }
            if (!temSuportado)
            {
                AddToLog("Nenhum arquivo arrastado corresponde aos tipos selecionados na interface.", true);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Deseja extrair imagens do(s) arquivo(s) arrastado(s)?",
                                                        "Extrair arquivos arrastados",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.No)
                return;

            txtPastaOrigem.Text = Path.GetDirectoryName(files[0]) ?? string.Empty;

            if (txtPastaDestino.Text.Length == 0)
            {
                txtPastaDestino.Text = Path.GetDirectoryName(files[0]) ?? string.Empty;
            }
            else if (Directory.Exists(txtPastaDestino.Text) == false)
            {
                AddToLog("");
                AddToLog("Caminho de destino inexistente: " + txtPastaDestino.Text, true);
                AddToLog("Selecione um caminho válido!", true);
                return;
            }

            AddToLog("");
            AddToLog(" ---- Início da extração ----");
            int qtdArquivos = 0;

            string formato = comboFormato.Text.ToLowerInvariant();
            int resolucao = Convert.ToInt32(comboResolucao.Text);
            int qtdPaginas = Convert.ToInt32(comboQtdPaginas.Text);

            foreach (string file in files)
            {
                string extensao = Path.GetExtension(file);
                if (!ExtensoesEntrada.Contains(extensao)) continue;
                if (!TipoHabilitadoNaUI(extensao)) continue;

                AddToLog("Processando " + Path.GetFileName(file));
                try
                {
                    if (string.Equals(extensao, ".pdf", StringComparison.OrdinalIgnoreCase))
                        ExtraiImagensDoPdf(file, txtPastaDestino.Text, formato, resolucao, qtdPaginas);
                    else
                        ExtraiImagensCompactadas(file, txtPastaDestino.Text, formato, resolucao, qtdPaginas);

                    qtdArquivos++;
                }
                catch (Exception ex)
                {
                    AddToLog("Falha ao processar " + Path.GetFileName(file) + ": " + ex.Message, true);
                }
            }

            AddToLog("(**) " + qtdArquivos.ToString() + " arquivo(s) processado(s).");
            AddToLog("");
            AddToLog("----- Fim da extração ------");
        }


        private bool TipoHabilitadoNaUI(string extensao)
        {
            if (string.Equals(extensao, ".pdf", StringComparison.OrdinalIgnoreCase)) return chkPdf.Checked;
            if (string.Equals(extensao, ".cbr", StringComparison.OrdinalIgnoreCase)) return chkCbr.Checked;
            if (string.Equals(extensao, ".cbz", StringComparison.OrdinalIgnoreCase)) return chkCbz.Checked;
            if (string.Equals(extensao, ".cb7", StringComparison.OrdinalIgnoreCase)) return chkCb7.Checked;
            return false;
        }


        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }


        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfigs();
        }
    }
}
