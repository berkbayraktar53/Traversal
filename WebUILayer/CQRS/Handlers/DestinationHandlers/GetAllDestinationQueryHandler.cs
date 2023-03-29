using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebUILayer.CQRS.Results.DestinationResults;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.DestinationHandlers
{
    public class GetAllDestinationQueryHandler
    {
        private readonly DatabaseContext _databaseContext;

        public GetAllDestinationQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<GetAllDestinationQueryResult> Handle()
        {
            var values = _databaseContext.Destinations.Select(x => new GetAllDestinationQueryResult
            {
                Id = x.Id,
                Capacity = x.Capacity,
                City = x.City,
                DayNight = x.DayNight,
                Price = x.Price
            }).AsNoTracking().ToList();
            return values;
        }
    }
}