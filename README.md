# AI Summit 2023 - GPT Console

> [!IMPORTANT] 
> Níže uvedená dokumentace k projektu je automaticky generovaná službou GitHub Copilot.

Jednoduchá konzolová aplikace pro komunikaci s ChatGPT 4.0 prostřednictvím OpenAI API. Aplikace slouží jako ukázka a bonusový materiál pro účastníky **AI Online Summit 2023**.

## Co aplikace dělá

GPT Console je interaktivní konzolová aplikace, která vám umožňuje:

- **Chatovat s ChatGPT 4.0** přímo z příkazové řádky
- **Udržovat konverzaci** - aplikace si pamatuje posledních 4 zpráv pro kontext
- **Bezpečně ukládat přihlašovací údaje** - API klíč a Organization ID jsou šifrovány pomocí AES
- **Streamovat odpovědi** - text se zobrazuje postupně, jak ho ChatGPT generuje
- **Pracovat offline po nastavení** - bez nutnosti webového prohlížeče

Aplikace používá optimalizované parametry pro ChatGPT:
- **Temperature**: 0.8 (kreativita odpovědí)
- **TopP**: 0.7 (fokus na relevantní tokeny)
- **Model**: GPT-4

## Systémové požadavky

### Pro spuštění předkompilované aplikace
- **Windows**: Windows 10/11 (x64, x86, ARM64)
- **macOS**: macOS 10.15+ (Intel x64, Apple Silicon ARM64)  
- **Linux**: Libovolná distribuce s glibc 2.17+

### Pro kompilaci ze zdrojového kódu
- **.NET 8.0 SDK** nebo novější
- **Aktivní internetové připojení** pro stažení balíčků NuGet

## Rychlé spuštění

### 1. Stažení aplikace
Stáhněte si příslušnou verzi z adresáře `build/`:
- **Windows 64-bit**: `win-x64.exe`
- **Windows 32-bit**: `win-x86.exe` 
- **Windows ARM64**: `win-arm64.exe`
- **macOS Intel**: `osx-x64`
- **macOS Apple Silicon**: `osx-arm64`

### 2. Získání OpenAI API přístupů

1. Registrujte se na [platform.openai.com](https://platform.openai.com)
2. Přihlaste se do svého účtu
3. Jděte do sekce [API Keys](https://platform.openai.com/account/api-keys)
4. Klikněte na **"Create new secret key"** a pojmenujte si ho
5. **Důležité**: Zkopírujte a uložte API klíč na bezpečné místo (zobrazí se pouze jednou)
6. Jděte do sekce [Manage account](https://platform.openai.com/account/org-settings)
7. Zkopírujte **Organization ID** a uložte ho také na bezpečné místo

### 3. Spuštění aplikace

#### Windows
- Poklepejte na stažený `.exe` soubor
- Případně spusťte z příkazové řádky: `./win-x64.exe`

#### macOS/Linux
```bash
# Nastavte oprávnění ke spuštění
chmod +x ./osx-x64  # nebo osx-arm64 pro Apple Silicon

# Spusťte aplikaci
./osx-x64
```

### 4. První konfigurace
Při prvním spuštění se zobrazí konfigurátor:

1. **Zadejte Organization ID** (zkopírovaný z OpenAI platformy)
2. **Zadejte API Key** (zkopírovaný z OpenAI platformy)
3. Údaje se bezpečně zašifrují a uloží

### 5. Použití
- Po spuštění začněte psát vaše dotazy
- Stiskněte **Enter** pro odeslání
- ChatGPT bude odpovídat postupně (streaming)
- Aplikace si pamatuje kontext z posledních 4 zpráv
- Pro ukončení použijte **Ctrl+C**

## Kompilace ze zdrojového kódu

### Požadavky
- .NET 8.0 SDK: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### Postup kompilace

```bash
# Klonování repozitáře
git clone https://github.com/mholec/event-2023-ai-summit.git
cd event-2023-ai-summit/src

# Obnovení závislostí
dotnet restore

# Kompilace aplikace
dotnet build

# Spuštění ze zdrojového kódu
dotnet run
```

### Vytvoření samostatného spustitelného souboru

```bash
# Windows x64
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# macOS Intel
dotnet publish -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true

# macOS Apple Silicon  
dotnet publish -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=true

# Linux x64
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
```

Výsledné soubory najdete v `bin/Release/net8.0/[runtime]/publish/`

## Konfigurace

### Umístění konfiguračního souboru
Aplikace ukládá šifrovanou konfiguraci do:
- **Windows**: `%APPDATA%\gptconfigk.txt`
- **macOS**: `~/.config/gptconfigk.txt` 
- **Linux**: `~/.config/gptconfigk.txt`

### Reset konfigurace
Pokud potřebujete zadat nové přihlašovací údaje:
1. Smažte konfigurační soubor `gptconfigk.txt`
2. Spusťte aplikaci znovu - zobrazí se konfigurátor

### Bezpečnost
- API klíč a Organization ID jsou šifrovány pomocí **AES-256**
- Šifrovací klíč je uložen v aplikaci (pro zvýšení bezpečnosti byste měli změnit klíč v kódu)
- Konfigurační soubor je uložen v uživatelském profilu

## Řešení problémů

### "Autentizační údaje k API nejsou správné"
- Zkontrolujte, zda jste správně zkopírovali API klíč a Organization ID
- Ověřte, že váš OpenAI účet má aktivní kredit
- Ujistěte se, že API klíč má správná oprávnění

### Aplikace se nespustí (Windows)
- Nainstalujte [Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist)
- Ověřte, že máte Windows 10/11

### Aplikace se nespustí (macOS)
```bash
# Povolte spuštění aplikace z neznámého vývojáře
sudo xattr -rd com.apple.quarantine ./osx-x64
```

### Aplikace se nespustí (Linux)
```bash
# Nastavte oprávnění
chmod +x ./osx-x64

# Případně nainstalujte chybějící knihovny
sudo apt update
sudo apt install libc6 libgcc1 libssl3
```

### Pomalé odpovědi nebo chyby připojení
- Zkontrolujte internetové připojení
- Ověřte, že OpenAI API není nedostupné
- Zkuste aplikaci restartovat

## Technické informace

### Použité technologie
- **.NET 8.0** - Runtime framework
- **OpenAI .NET SDK v1.7.2** - Komunikace s OpenAI API
- **System.Security.Cryptography** - AES šifrování konfigurace
- **System.Text.Json** - Serializace konfigurace

### Architektura aplikace
```
Program.cs          - Hlavní smyčka aplikace a UI
Configuration.cs    - Správa a šifrování konfigurace
GptConsole.csproj  - Definice projektu a závislostí
```

### API parametry
- **Model**: GPT-4
- **Temperature**: 0.8 (ovlivňuje kreativitu)
- **TopP**: 0.7 (ovlivňuje fokus na relevantní tokeny)
- **Conversation Length**: 4 zprávy (kontext konverzace)
- **User**: "Holec" (identifikace uživatele v API)

## O projektu

Tento projekt je součástí **AI Online Summit 2023** a slouží jako praktická ukázka integrace s OpenAI API. 

**Autor**: Miroslav Holec ([www.holec.ai](https://www.holec.ai))

**Licence**: MIT License - viz soubor `src/LICENSE`

**Další informace**: [www.holec.ai/aisummit/bonus](https://www.holec.ai/aisummit/bonus)

---

⚠️ **Upozornění**: Používejte aplikaci na vlastní riziko. Dbejte na bezpečnost vašich API klíčů a sledujte spotřebu OpenAI kreditů.
