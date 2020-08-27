using FoMapper.Mapper;

namespace AutoMapperV1.MapperProfile
{
    public class CustomMapperProfile : FoCustomProfile
    {
        public CustomMapperProfile()
        {
            //You can create custom mapping inside this constructor
            /*CreateMap<Order, OrderDTO>()
            .ForMember(to, c => c.MapFrom<decimal>(from));*/
        }
    }
}