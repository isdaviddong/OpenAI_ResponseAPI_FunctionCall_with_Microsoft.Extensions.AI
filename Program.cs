using System.ComponentModel;
using Microsoft.Extensions.AI;
using OpenAI;

#pragma warning disable OPENAI001

// 1. 建立 OpenAI Client
string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
    ?? throw new Exception("請設定 OPENAI_API_KEY");

OpenAIClient openai = new(apiKey);

// 2. 加上自動執行 Function 的功能
IChatClient client = openai.GetResponsesClient()
    .AsIChatClient("gpt-4o-mini")
    .AsBuilder()
    .UseFunctionInvocation()
    .Build();

// 3. 呼叫 AI
var result = await client.GetResponseAsync(
    "請使用 Add 函式計算 25 加 17",
    new ChatOptions
    {
        Tools = [AIFunctionFactory.Create(Add)]
    });

Console.WriteLine($"AI 回答：{result}");

#pragma warning restore OPENAI001

// 4. 自己的 C# Function
[Description("計算兩個整數的總和")]
static int Add(int a, int b)
{
    Console.WriteLine($"[Function Call] Add({a}, {b})");
    return a + b;
}
