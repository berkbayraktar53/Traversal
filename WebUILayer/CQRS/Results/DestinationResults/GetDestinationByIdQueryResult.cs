namespace WebUILayer.CQRS.Results.DestinationResults
{
    public class GetDestinationByIdQueryResult
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string DayNight { get; set; }
        public decimal Price { get; set; }
    }
}