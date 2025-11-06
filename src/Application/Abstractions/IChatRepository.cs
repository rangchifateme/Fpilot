using Domain.Entities;

namespace Application.Abstractions
{
    public interface IChatRepository
    {
        Task<ChatSession> UpsertMessageAsync(string userId, string question, string answer, IEnumerable<SearchDoc> docs, CancellationToken ct);
    }
}
