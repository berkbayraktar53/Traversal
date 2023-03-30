using MediatR;
using System.Threading;
using EntityLayer.Concrete;
using System.Threading.Tasks;
using WebUILayer.CQRS.Commands.GuideCommands;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer.CQRS.Handlers.GuideHandlers
{
    public class CreateGuideCommandHandler : IRequestHandler<CreateGuideCommand>
    {
        private readonly DatabaseContext _databaseContext;

        public CreateGuideCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Unit> Handle(CreateGuideCommand request, CancellationToken cancellationToken)
        {
            _databaseContext.Guides.Add(new Guide
            {
                Description = request.Description,
                GuideImage = request.GuideImage,
                InstagramLink = request.InstagramLink,
                NameSurname = request.NameSurname,
                TwitterLink = request.TwitterLink,
                Status = true
            });
            await _databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}