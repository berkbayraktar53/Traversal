using EntityLayer.Concrete;
using WebUILayer.CQRS.Commands.DestinationCommands;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.DestinationHandlers
{
    public class CreateDestinationCommandHandler
    {
        private readonly DatabaseContext _databaseContext;

        public CreateDestinationCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Handle(CreateDestinationCommand createDestinationCommand)
        {
            _databaseContext.Destinations.Add(new Destination
            {
                City = createDestinationCommand.City,
                Price = createDestinationCommand.Price,
                DayNight = createDestinationCommand.DayNight,
                Capacity = createDestinationCommand.Capacity,
                Status = true
            });
            _databaseContext.SaveChanges();
        }
    }
}