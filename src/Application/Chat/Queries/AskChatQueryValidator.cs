using FluentValidation;

namespace Application.Chat.Queries;
public class AskChatQueryValidator : AbstractValidator<AskChatReqQuery>
{
    public AskChatQueryValidator() { RuleFor(x => x.Question).NotEmpty().MaximumLength(2048); }
}
