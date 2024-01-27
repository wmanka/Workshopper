using Workshopper.Api.Sessions.Contracts.CancelSession;
using Workshopper.Application.Sessions.Commands.CancelSession;

namespace Workshopper.Api.Sessions.CancelSession;

public class CancelSessionMapper : Mapper<CancelSessionRequest, EmptyResponse, CancelSessionCommand>
{
    public override CancelSessionCommand ToEntity(CancelSessionRequest request)
    {
        return new CancelSessionCommand
        {
            Id = request.Id
        };
    }
}