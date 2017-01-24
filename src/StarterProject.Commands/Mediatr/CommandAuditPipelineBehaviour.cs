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
    public class CommandAuditPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse> where TResponse : Result, new()
    {
        private readonly AppDbContext context;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public CommandAuditPipelineBehaviour(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TResponse> Handle(TRequest cmd, RequestHandlerDelegate<TResponse> next)
        {
            TResponse result;
            stopwatch.Start();

            try
            {
                cmd.Result = result = await next();
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

        private async Task AuditCommand(TRequest cmd, TimeSpan timeTakenToExecuteCommand)
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

        private string GetExceptionMessageFrom(TRequest cmd)
        {
            return cmd.Result?.Exception?.Message ?? cmd.Result?.Failures?.JoinWithComma().TakeCharacters(4000) ?? string.Empty;
        }

    }
}