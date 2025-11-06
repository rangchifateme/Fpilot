
using MediatR;

namespace Application.Chat.Queries;
public record AskChatReqQuery(string UserId, string Question) : IRequest<AskChatResQuery>;
