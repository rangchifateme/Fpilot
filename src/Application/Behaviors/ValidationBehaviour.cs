using FluentValidation;
using MediatR;

namespace Application.Behaviors;
public class ValidationBehavior<TReq, TRes> : IPipelineBehavior<TReq, TRes> where TReq : notnull
{
   
    private readonly IEnumerable<IValidator<TReq>> _validators;
  
    public ValidationBehavior(IEnumerable<IValidator<TReq>> validators) => _validators = validators;

    public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken ct)
    {
        var ctx = new ValidationContext<TReq>(request);
        var failures = (await Task.WhenAll(_validators.Select(v => v.ValidateAsync(ctx, ct))))
          .SelectMany(r => r.Errors).Where(f => f != null).ToList();
        if (failures.Count != 0) throw new FluentValidation.ValidationException(failures);
        return await next();
    }
}
