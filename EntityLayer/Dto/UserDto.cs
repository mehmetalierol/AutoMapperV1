using EntityLayer.Base;

namespace EntityLayer.Dto
{
    public class UserDto : FoDto
    {
        public string NameSurname { get; set; }
        public int Age { get; set; }
    }
}