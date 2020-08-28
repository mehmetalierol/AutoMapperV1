using EntityLayer.Base;

namespace EntityLayer.Entity
{
    public class UserEntity : FoEntity
    {
        public string NameSurname { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }

        public int Price { get; set; }
    }
}