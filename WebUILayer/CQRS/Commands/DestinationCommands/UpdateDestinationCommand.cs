namespace WebUILayer.CQRS.Commands.DestinationCommands
{
    public class UpdateDestinationCommand
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string DayNight { get; set; }
        public decimal Price { get; set; }
    }
}