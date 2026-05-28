# Extrator de Imagens Datassette

Aplicativo Windows (WinForms / .NET Framework 4.8) que extrai as primeiras imagens de arquivos
**PDF**, **CBR**, **CBZ** e **CB7** para uso como miniaturas — pensado originalmente para os
uploads do [Datassette](https://datassette.org), mas funciona para qualquer caso de geração de
*thumbnails* a partir de revistas, *comics* ou catálogos digitalizados.

## Dependências

- **.NET Framework 4.8**: https://dotnet.microsoft.com/download/dotnet-framework/net48
- **Ghostscript 64-bit** (necessário em tempo de execução para PDFs):
  https://www.ghostscript.com/releases/gsdnld.html

> O `7z.dll` (usado para CBR/CBZ/CB7) já é distribuído junto com o executável.

## Como compilar

1. Abrir `Extrai Imagens.sln` no Visual Studio 2019 ou superior.
2. Restaurar os pacotes NuGet (Build → Restore NuGet Packages).
3. Compilar em `Release | Any CPU` (a saída é `x64`, pois o `Magick.NET-Q8-x64` é arquitetura-específico).

Pelo MSBuild via linha de comando:

```powershell
msbuild "Extrai Imagens.sln" /t:Restore
msbuild "Extrai Imagens.sln" /t:Build /p:Configuration=Release
```

O binário fica em `bin\Release\Extrai Imagens.exe`.

## Opções na interface

- **Processar subpastas** — processa arquivos de todas as pastas abaixo da selecionada
- **Formato de imagem final** (JPG/PNG) — formato das imagens extraídas
- **Resolução final** — resolução em DPI (afeta apenas PDFs)
- **Processar estes tipos de arquivos** — quais extensões considerar
- **Extrair a(s) … primeira(s) imagen(s)** — quantidade de imagens por arquivo
  - 1 imagem: arquivo de saída recebe o nome do arquivo de origem
  - mais de 1: as imagens recebem o sufixo `-01`, `-02`, …
- **Arrastar e soltar** — arrastar arquivos para a janela processa só esses arquivos
  (respeitando os tipos marcados na interface)

## Limitações conhecidas

- Sobrescreve imagens já extraídas com o mesmo nome (não pergunta antes)
- Não recomeça de onde parou em caso de erro
- A interface não tem barra de progresso e a janela fica congelada durante o processamento
  de lotes grandes (o trabalho ainda é executado na thread de UI)

## Changelog

### v1.1.0 (2026-05)
- correções de bugs:
  - `btnConverter` agora sempre é reabilitado após erro (try/finally)
  - `SevenZipExtractor` é corretamente descartado (`using`)
  - arquivos compactados são extraídos para uma pasta temporária dedicada, em vez de poluir
    a pasta de origem
  - numeração de páginas usa zero-padding consistente (`-01`, `-02`, …) via `D2`
  - ordenação dos arquivos internos passa a usar *natural sort* (`page-2` antes de `page-10`)
  - comparações de extensões/cultura usam `OrdinalIgnoreCase` em vez de `ToLower()`
  - filtro de `__MACOSX` agora trata o caminho como segmentos
  - drag-and-drop respeita os checkboxes de tipos de arquivo
  - log usa formato de data ISO (`yyyy-MM-dd HH:mm:ss`) independente da cultura
  - versão exibida na janela vem da `AssemblyVersion` em vez de string fixa
  - falha ao processar um arquivo deixa de abortar o lote inteiro

### v1.0 (2022-09-22)
- ignora o conteúdo da pasta `__MACOSX` em arquivos CBx criados no MacOS

### 2022-06-20
- correção da extração de páginas de alguns CBx, onde a indexação dos arquivos internos não
  correspondia à ordem alfabética desses arquivos

### 2022-06-17
- permite extração das imagens em uma pasta separada
- salva as configurações

### 2022-06-11
- processa arquivos individuais ao arrastar e soltar (drag and drop)

### 2022-06-10
- corrigida a extração de imagens em diretórios internos de arquivos CBx
- verifica a extensão de um arquivo dentro do CBx antes de extrair

### 2022-06-09
- versão inicial

## Licença

GNU General Public License v2 — veja [LICENSE](LICENSE).
