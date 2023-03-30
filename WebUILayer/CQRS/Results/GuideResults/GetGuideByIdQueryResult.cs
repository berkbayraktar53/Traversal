namespace WebUILayer.CQRS.Results.GuideResults
{
    public class GetGuideByIdQueryResult
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Description { get; set; }
    }
}