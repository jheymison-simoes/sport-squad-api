using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SportSquad.Core.Interfaces;

namespace SportSquad.Core.Command;

public class CommandPipelineValidationBehavior<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ICommandResponse, new()
{
    private readonly IEnumerable<IValidator> _validators;

    public CommandPipelineValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => this._validators = (IEnumerable<IValidator>) validators;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        
        var validationContext = new ValidationContext<TRequest>(request);
        
        var list = (await Task.WhenAll(
                _validators.Select((Func<IValidator, Task<ValidationResult>>) (v => 
                        v.ValidateAsync(validationContext, cancellationToken))
                )
            )).SelectMany((Func<ValidationResult, IEnumerable<ValidationFailure>>) (r => r.Errors))
            .Where((Func<ValidationFailure, bool>) (f => f != null)).ToList();
        
        if (!list.Any()) return await next();
        
        var response = new TResponse()
        {
            ValidationResult = new ValidationResult()
        };
        
        response.ValidationResult.Errors.AddRange(list);
        return response;
    }
}