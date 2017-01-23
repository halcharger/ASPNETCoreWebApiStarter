using MediatR;
using StarterProject.Common;

namespace StarterProject.Commands
{
    //Base class used to indicate to the mediatr pipeline that this is a command and this needs to be auditted.
    public abstract class Command : Message
    {
        public IResult Result { get; set; }
    }
}