using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using NExtensions.Core;
using StarterProject.Common;
using StarterProject.Data;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.Mediatr
{
    public class CommandAuditPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly AppDbContext context;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public CommandAuditPipelineBehaviour(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            //Command Audits currently only applies to Commands, if the request does not inherit from Command<>, the exit
            //We need to find a better way (a faster way) to this other than reflection
            var cmd = request as Command;
            if (cmd == null) return await next();

            TResponse result;
            stopwatch.Start();

            try
            {
                result = await next();
            }
            catch (Exception ex)
            {
                cmd.Result = new FailureResult(ex.Message);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                if (cmd.AuditThisMessage) await AuditCommand(cmd, stopwatch.Elapsed);
            }

            return result;

        }

        private async Task AuditCommand(Command cmd, TimeSpan timeTakenToExecuteCommand)
        {
            var audit = new CommandAudit
            {
                LoggedOnUserId = cmd.LoggedOnUserId,
                MessageId = cmd.MessageId,
                IsSuccess = cmd.Result?.IsSuccess ?? false,
                ExceptionMessage = GetExceptionMessageFrom(cmd),
                CommandType = cmd.GetType().Name,
                CommandData = JsonConvert.SerializeObject(cmd),
                Milliseconds = (int)timeTakenToExecuteCommand.TotalMilliseconds
            };

            context.Add(audit);
            await context.SaveChangesAsync();

        }

        private string GetExceptionMessageFrom(Command cmd)
        {
            return cmd.Result?.Exception?.Message ?? cmd.Result?.Failures?.JoinWithComma().TakeCharacters(4000) ?? string.Empty;
        }

    }
}