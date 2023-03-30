using MediatR;

namespace WebUILayer.CQRS.Commands.GuideCommands
{
    public class CreateGuideCommand : IRequest
    {
        public string GuideImage { get; set; }
        public string NameSurname { get; set; }
        public string Description { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public bool Status { get; set; }
    }
}