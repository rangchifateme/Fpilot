using Application.Abstractions;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace Infrastructure.Llm;
public class AzureOpenAiClient : ILlmClient
{
    private readonly ChatClient _chat;
    public AzureOpenAiClient(IConfiguration cfg)
    {
        var client = new AzureOpenAIClient(new Uri(cfg["OpenAI:Endpoint"]!), new AzureKeyCredential(cfg["OpenAI:Key"]!));
        _chat = client.GetChatClient(cfg["OpenAI:Deployment"]!);
    }
    public async Task<string> CompleteAsync(string prompt, CancellationToken ct)
    {
        var resp = await _chat.CompleteChatAsync([new SystemChatMessage(prompt)]);
        return resp.Value.Content[0].Text;
    }
}
