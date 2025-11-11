using Application.Chat.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AskController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AskController> _logger;

    public AskController(IMediator mediator, ILogger<AskController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<AskChatResQuery>> Ask([FromBody] AskRequest request, CancellationToken cancellationToken)
    {
        var query = new AskChatReqQuery(request.UserId ?? "default", request.Prompt);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    public record AskRequest(string Prompt, string? UserId = null);
}

