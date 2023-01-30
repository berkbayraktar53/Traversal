using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class Guide : IEntity
    {
        public int Id { get; set; }
        public string GuideImage { get; set; }
        public string NameSurname { get; set; }
        public string Description { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public bool Status { get; set; }
    }
}