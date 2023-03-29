using WebUILayer.CQRS.Commands.DestinationCommands;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.DestinationHandlers
{
    public class UpdateDestinationCommandHandler
    {
        private readonly DatabaseContext _databaseContext;

        public UpdateDestinationCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Handle(UpdateDestinationCommand updateDestinationCommand)
        {
            var values = _databaseContext.Destinations.Find(updateDestinationCommand.Id);
            values.City = updateDestinationCommand.City;
            values.DayNight = updateDestinationCommand.DayNight;
            values.Price = updateDestinationCommand.Price;
            _databaseContext.SaveChanges();
        }
    }
}