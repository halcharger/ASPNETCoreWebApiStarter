using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using StarterProject.Common;

namespace StarterProject.Commands.Mediatr
{
    public class FluentValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>[] validators;

        public FluentValidationPipelineBehaviour(IValidator<TRequest>[] validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            //Fluent Validation currently only applies to Commands, if the request does not inherit from Command<>, the exit
            //We need to find a better way (a faster way) to this other than reflection
            var cmd = request as Command;
            if (cmd == null) return await next();

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
                var response = Activator.CreateInstance<TResponse>();
                
                var result = response as Result;
                result?.SetFailures(failures.Select(f => f.ErrorMessage).ToList());

                return response;
            }

            return await next();
        }
    }
}