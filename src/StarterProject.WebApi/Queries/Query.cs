using MediatR;
using StarterProject.WebApi.Common;

namespace StarterProject.WebApi.Queries
{
    //Marker class to explicitly differentiate between commands and queries
    public abstract class Query<T> : Message, IRequest<T>
    {
        
    }
}