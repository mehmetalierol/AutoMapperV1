using EntityLayer.Dto;
using FoMapper.Atribute;

namespace EntityLayer.VM
{
    //This class will be mapped to class below
    [FoSource(typeof(UserDto), true)]
    public class CustomNamedVM
    {
        public int Age { get; set; }
    }
}