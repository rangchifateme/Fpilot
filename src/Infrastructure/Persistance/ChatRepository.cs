using Application.Abstractions;
using Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistance;
public class ChatRepository(CosmosClient client, IConfiguration cfg) : IChatRepository
{
    private readonly Container _sessions = client.GetDatabase(cfg["Cosmos:Database"]).GetContainer("ChatSessions");
    private readonly Container _messages = client.GetDatabase(cfg["Cosmos:Database"]).GetContainer("ChatMessages");

    public async Task<ChatSession> UpsertMessageAsync(string userId, string question, string answer, IEnumerable<SearchDoc> docs, CancellationToken ct)
    {
        var session = new ChatSession { UserId = userId };
        await _sessions.UpsertItemAsync(session, new PartitionKey(userId), cancellationToken: ct);
        var msg = new ChatMessage { SessionId = session.Id, UserId = userId, Question = question, Answer = answer };
        await _messages.UpsertItemAsync(msg, new PartitionKey(userId), cancellationToken: ct);
        return session;
    }
}
