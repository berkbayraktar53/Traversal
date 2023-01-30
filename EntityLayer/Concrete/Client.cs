using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Concrete
{
    public class Client : IEntity
    {
        public int Id { get; set; }
        public string ClientImage { get; set; }
        public string NameSurname { get; set; }
        public string ClientComment { get; set; }
        public bool Status { get; set; }
    }
}