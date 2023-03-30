using MediatR;
using WebUILayer.CQRS.Results.GuideResults;

namespace WebUILayer.CQRS.Queries.GuideQueries
{
    public class GetGuideByIdQuery : IRequest<GetGuideByIdQueryResult>
    {
        public GetGuideByIdQuery(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}