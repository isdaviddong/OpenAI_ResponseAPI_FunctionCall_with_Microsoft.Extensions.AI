# OpenAI Responses API Function Calling

使用 .NET 10、OpenAI Responses API 與 `Microsoft.Extensions.AI`，讓模型呼叫本機 C# `Add` 函式計算兩數總和。`UseFunctionInvocation()` 會自動執行函式並將結果交回模型。

## 為什麼使用 Microsoft.Extensions.AI

直接使用 OpenAI SDK 也能呼叫 Function，但程式需要自行描述工具、解析模型回傳的參數、執行函式，再把結果送回模型並處理後續回合。本範例加入 `Microsoft.Extensions.AI`，用 `AIFunctionFactory` 從 C# 方法建立工具，並由 `UseFunctionInvocation()` 自動處理呼叫與回傳流程，讓程式更精簡。

`IChatClient` 也提供較通用的聊天介面，方便日後替換或整合其他模型供應商。若需要完全掌控每次 API 呼叫和工具執行細節，直接使用 OpenAI SDK 會更合適；這個 extension 是簡化流程的選擇，並非使用 Responses API 的必要條件。

## 執行

需要 .NET 10 SDK 與 OpenAI API key。程式從 `OPENAI_API_KEY` 環境變數讀取金鑰。

Windows PowerShell：

```powershell
$env:OPENAI_API_KEY = "你的 OpenAI API key"
dotnet run
```

## 套件

專案使用以下 NuGet 套件，執行 `dotnet run` 時會自動還原：

- `OpenAI` 2.14.0
- `Microsoft.Extensions.AI` 10.11.0：提供 `AsBuilder()`、`UseFunctionInvocation()` 等擴充方法。
- `Microsoft.Extensions.AI.OpenAI` 10.11.0：提供 OpenAI Responses API 的 `AsIChatClient()` adapter。
