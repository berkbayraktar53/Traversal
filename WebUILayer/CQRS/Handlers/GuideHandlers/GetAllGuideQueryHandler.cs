using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebUILayer.CQRS.Results.GuideResults;
using WebUILayer.CQRS.Queries.GuideQueries;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.GuideHandlers
{
    public class GetAllGuideQueryHandler : IRequestHandler<GetAllGuideQuery, List<GetAllGuideQueryResult>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetAllGuideQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<GetAllGuideQueryResult>> Handle(GetAllGuideQuery request, CancellationToken cancellationToken)
        {
            return await _databaseContext.Guides.Select(x => new GetAllGuideQueryResult
            {
                Id = x.Id,
                Description = x.Description,
                GuideImage = x.GuideImage,
                NameSurname = x.NameSurname,
                InstagramLink = x.InstagramLink,
                TwitterLink = x.TwitterLink
            }).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        }
    }
}