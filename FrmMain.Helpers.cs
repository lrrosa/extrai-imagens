using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Extrai_Imagens
{
    // Helpers e constantes do formulário FrmMain.
    // Está em um arquivo parcial separado para evitar que campos estáticos,
    // P/Invoke e demais detalhes técnicos confundam o WinForms Designer.
    public partial class FrmMain
    {
        // Extensões de imagem reconhecidas dentro dos arquivos compactados (CBx).
        private static readonly HashSet<string> ExtensoesImagem = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".jpe", ".png", ".gif", ".bmp", ".tif", ".tiff", ".webp"
        };

        // Tipos de arquivo suportados como entrada.
        private static readonly HashSet<string> ExtensoesEntrada = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf", ".cbr", ".cbz", ".cb7"
        };

        // Largura final (em pixels) usada ao redimensionar imagens extraídas de arquivos CBx.
        private const int LarguraFinalCbx = 720;

        // checa se um caminho interno do arquivo compactado contém um segmento com o nome dado,
        // tratando "/" e "\" como separadores
        private static bool ContemSegmento(string caminho, string segmento)
        {
            if (string.IsNullOrEmpty(caminho)) return false;
            var partes = caminho.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in partes)
            {
                if (string.Equals(p, segmento, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        // ordenação "natural" (page-2 < page-10), igual à do Explorer do Windows
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int StrCmpLogicalW(string x, string y);

        private static readonly Comparison<string> NaturalComparer = (x, y) => StrCmpLogicalW(x, y);
    }
}
