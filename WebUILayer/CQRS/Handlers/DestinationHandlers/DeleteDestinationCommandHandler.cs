using WebUILayer.CQRS.Commands.DestinationCommands;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.DestinationHandlers
{
    public class DeleteDestinationCommandHandler
    {
        private readonly DatabaseContext _databaseContext;

        public DeleteDestinationCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Handle(DeleteDestinationCommand deleteDestinationCommand)
        {
            var values = _databaseContext.Destinations.Find(deleteDestinationCommand.Id);
            _databaseContext.Destinations.Remove(values);
            _databaseContext.SaveChanges();
        }
    }
}