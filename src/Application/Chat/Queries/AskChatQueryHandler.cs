using Application.Abstractions;
using MediatR;

namespace Application.Chat.Queries;
public class AskChatQueryHandler(IAiSearchClient search, ILlmClient llm, IChatRepository repo, ICacheProvider cache)
  : IRequestHandler<AskChatReqQuery, AskChatResQuery>
{

    public async Task<AskChatResQuery> Handle(AskChatReqQuery req, CancellationToken ct)
    {
        var key = $"ask:{req.Question.GetHashCode()}";
        var cached = await cache.GetAsync<AskChatResQuery>(key, ct);
        if (cached is not null) return cached;

        var docs = await search.SearchAsync(req.Question, 5, ct);
        var prompt = BuildPrompt(docs, req.Question);
        var answer = await llm.CompleteAsync(prompt, ct);
        var session = await repo.UpsertMessageAsync(req.UserId, req.Question, answer, docs, ct);

        var vm = new AskChatResQuery { Answer = answer, Citations = docs.Select(d => d.SourceUrl ?? "").ToList(), SessionId = session.Id };
        await cache.SetAsync(key, vm, TimeSpan.FromMinutes(5), ct);
        return vm;
    }

    static string BuildPrompt(IEnumerable<SearchDoc> docs, string q)
    {
        var sources = string.Join("\n\n", docs.Select(d => $"Source: {d.SourceUrl}\n{d.Content}"));
        return $"Use ONLY these sources:\n{sources}\n\nQuestion:\n{q}\n\nCite sources as [n].";
    }
}
