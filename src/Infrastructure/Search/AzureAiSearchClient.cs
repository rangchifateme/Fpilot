
using Application.Abstractions;
using Azure;
using Azure.Search.Documents;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Search;

public class AzureAiSearchClient: IAiSearchClient
{
    private readonly SearchClient _client;
    public AzureAiSearchClient(IConfiguration cfg)
    {
        _client = new SearchClient(new Uri(cfg["Search:Endpoint"]!), cfg["Search:Index"]!, new AzureKeyCredential(cfg["Search:Key"]!));
    }
    public async Task<IReadOnlyList<SearchDoc>> SearchAsync(string query, int topK, CancellationToken ct)
    {
        var opts = new SearchOptions { Size = topK };
        var res = await _client.SearchAsync<SearchDoc>(query, opts, ct);
        return res.Value.GetResults().Select(r => r.Document).ToList();
    }
}

