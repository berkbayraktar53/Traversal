namespace WebUILayer.CQRS.Results.GuideResults
{
    public class GetAllGuideQueryResult
    {
        public int Id { get; set; }
        public string GuideImage { get; set; }
        public string NameSurname { get; set; }
        public string Description { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
    }
}