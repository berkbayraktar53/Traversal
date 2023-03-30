using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUILayer.CQRS.Results.GuideResults;
using WebUILayer.CQRS.Queries.GuideQueries;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.GuideHandlers
{
    public class GetGuideByIdQueryHandler : IRequestHandler<GetGuideByIdQuery, GetGuideByIdQueryResult>
    {
        private readonly DatabaseContext _databaseContext;

        public GetGuideByIdQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<GetGuideByIdQueryResult> Handle(GetGuideByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _databaseContext.Guides.FindAsync(request.Id);

            throw new System.NotImplementedException();
        }
    }
}