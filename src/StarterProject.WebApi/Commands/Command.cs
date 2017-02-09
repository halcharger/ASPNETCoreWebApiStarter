using MediatR;
using StarterProject.WebApi.Common;

namespace StarterProject.WebApi.Commands
{
    //Base class used to indicate to the mediatr pipeline that this is a command and this needs to be auditted.
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public IResult Result { get; set; }
    }
}