using MediatR;
using System.Collections.Generic;
using WebUILayer.CQRS.Results.GuideResults;

namespace WebUILayer.CQRS.Queries.GuideQueries
{
    public class GetAllGuideQuery : IRequest<List<GetAllGuideQueryResult>>
    {

    }
}