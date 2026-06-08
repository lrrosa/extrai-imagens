# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project type

Windows desktop app — **WinForms / .NET Framework 4.8**, target platform **x64**, written in C#.
Old-style `.csproj` with `packages.config` (NOT SDK-style). The solution and project filenames
contain a space: `Extrai Imagens.sln` / `Extrai Imagens.csproj` — always quote them.

## Build / run

NuGet packages are restored into the `packages/` directory (gitignored). Restore once after clone:

```powershell
msbuild "Extrai Imagens.sln" /t:Restore
```

Build:

```powershell
msbuild "Extrai Imagens.sln" /t:Build /p:Configuration=Debug   /p:Platform="Any CPU"
msbuild "Extrai Imagens.sln" /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU"
```

The `Platform` MSBuild property is `"Any CPU"` (the project then forces `PlatformTarget=x64`).
Locate MSBuild via `vswhere` (ships with VS Installer):

```powershell
& "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" `
    -latest -requires Microsoft.Component.MSBuild `
    -find MSBuild\**\Bin\MSBuild.exe
```

Output: `bin\Debug\Extrai Imagens.exe` (or `bin\Release\…`).

Run by launching the exe directly. **Ghostscript 64-bit must be installed separately** on the
machine — `Ghostscript.NET` finds it at runtime via the Windows registry/PATH. `7z.dll` is bundled
in `bin\<config>\` next to the exe.

There is **no test project**, no linter config, no CI.

## Architecture — the parts worth knowing

The whole app is a single `Form` (`FrmMain`). UI events run the work synchronously on the UI thread
(known limitation — tracked in issue #1, do not "fix" silently by introducing `async` without
also handling progress/cancellation and disabling inputs).

Two extraction pipelines, dispatched by file extension inside `ProcessaPasta` / `FrmMain_DragDrop`:

- **PDF** → `ExtraiImagensDoPdf` uses `Ghostscript.NET` (`GhostscriptJpegDevice` or
  `GhostscriptPngDevice`). Ghostscript writes pages directly to disk using the `%02d` pattern when
  more than one page is requested.
- **CBR / CBZ / CB7** → `ExtraiImagensCompactadas` uses `Squid-Box.SevenZipSharp` to extract image
  entries to a **per-call temp directory** (`%TEMP%\ExtraiImagens_<guid>\`), then `Magick.NET`
  resizes and writes the final image. The temp dir is always cleaned in a `finally`. Do not
  re-introduce extraction into the source folder.

User settings persist via `Properties.Settings.Default` and are saved in `FrmMain_FormClosing`.

## Critical gotchas

- **`FrmMain.cs` must keep the WinForms-Designer-friendly shape**: constructor near the top, no
  static field initializers (`HashSet<string> { ... }`) and no `[DllImport] extern` methods
  *inside the form file*. The WinForms designer parser engasga on those and reports the
  misleading error "A classe base 'System.' não pôde ser carregada". Put any such helpers in
  **`FrmMain.Helpers.cs`** (separate partial class, already wired in the `.csproj` with
  `<DependentUpon>FrmMain.cs</DependentUpon>`).

- **Source file encoding is UTF-8 *with* BOM** for all `.cs` files. The .NET Framework C# compiler
  on a non-UTF-8 system locale will mangle accented characters in string literals (e.g.
  `"Início da extração"`) if the file is UTF-8 *without* BOM. Don't strip the BOM when editing.
  `README.md` is UTF-8 *without* BOM.

- **`testes/`** is `.gitignored` because it historically contained copyrighted material (scanned
  magazines). Don't add real magazines/comics to it — use small free samples if you need fixtures.

- **String comparisons for file extensions and format names** must use `OrdinalIgnoreCase` /
  `ToLowerInvariant` — never `ToLower()`. Locales like Turkish break `"PDF".ToLower()` (dotless I).

- **Sorting of files extracted from CBx archives** uses Win32 `StrCmpLogicalW` (natural sort, same
  as Explorer) so `page-2.jpg` comes before `page-10.jpg`. The P/Invoke lives in
  `FrmMain.Helpers.cs` — keep it there (see Designer gotcha above).

- **Magick.NET native is x64-only** (`Magick.NET-Q8-x64`). Don't change `PlatformTarget` to AnyCPU
  or x86 — the native binding will fail to load. The `Magick.NET-Q8-x64.targets` import at the
  bottom of the `.csproj` copies the native blob into the output folder.

- **`7z.dll` location** is set via `SevenZipExtractor.SetLibraryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "7z.dll"))`.
  The DLL is delivered with the build output (via the SevenZipSharp package). If you move things
  around, make sure that path still resolves.

- Per-file failures inside `ProcessaPasta` are **caught and logged so the batch continues**.
  Don't replace this with `throw` — aborting the whole run on the first bad file was the v1.0
  behavior and was changed deliberately.

## Distribution

A v1.1.0 release exists on GitHub with a `extrai-imagens-v1.1.0-win-x64.zip` containing the
runtime files. When building a new release zip, include from `bin\Release\`:

- `*.exe`, `*.dll`, `*.config`, `README.md` at the root
- `x64\libSkiaSharp.dll` (the only SkiaSharp native needed on Windows x64)

Exclude `*.pdb`, `*.xml`, `LEIAME.txt`, `Extrai Imagens.7z`, and the cross-platform SkiaSharp
native subfolders (`x86\`, `arm64\`, `musl-*\`, `*.so`, `*.dylib`, …).
