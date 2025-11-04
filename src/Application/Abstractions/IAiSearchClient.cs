
namespace Application.Abstractions
{
    public interface IAiSearchClient
    {
        Task<IReadOnlyList<SearchDoc>> SearchAsync(string query, int topK, CancellationToken ct);
    }
}
