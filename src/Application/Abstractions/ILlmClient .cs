
namespace Application.Abstractions
{
    public interface ILlmClient
    {
        Task<string> CompleteAsync(string prompt, CancellationToken ct);
    }
}
