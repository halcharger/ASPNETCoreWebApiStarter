using MediatR;
using StarterProject.Common;

namespace StarterProject.Queries
{
    //Marker class to explicitly differentiate between commands and queries
    public abstract class Query<T> : Message, IRequest<T>
    {
        
    }
}