using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using StarterProject.Common;

namespace StarterProject.Commands.Mediatr
{
    public class FluentValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result, new()
    {
        private readonly IValidator<TRequest>[] validators;

        public FluentValidationPipelineBehaviour(IValidator<TRequest> validator)
        {
            validators = new [] {validator};
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var results = new List<ValidationResult>();
            foreach (var validator in validators)
            {
                results.Add(await validator.ValidateAsync(context));
            }

            var failures = results.SelectMany(r => r.Errors)
                                  .Where(f => f != null)
                                  .ToList();

            if (failures.Any())
            {
                var result = new TResponse();
                result.SetFailures(failures.Select(f => f.ErrorMessage).ToList());
                return result;
            }

            return await next();
        }
    }
}