using WebUILayer.CQRS.Results.DestinationResults;
using WebUILayer.CQRS.Queries.DestinationQueries;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.DestinationHandlers
{
    public class GetDestinationByIdQueryHandler
    {
        private readonly DatabaseContext _databaseContext;

        public GetDestinationByIdQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public GetDestinationByIdQueryResult Handle(GetDestinationByIdQuery getDestinationByIdQuery)
        {
            var values = _databaseContext.Destinations.Find(getDestinationByIdQuery.Id);
            return new GetDestinationByIdQueryResult
            {
                Id = values.Id,
                City = values.City,
                DayNight = values.DayNight,
                Price = values.Price,
            };
        }
    }
}