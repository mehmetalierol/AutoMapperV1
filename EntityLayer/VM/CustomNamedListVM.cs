using EntityLayer.Dto;
using FoMapper.Atribute;
using System;

namespace EntityLayer.VM
{
    //This class will be mapped to classes below
    [FoSource(new Type[] { typeof(UserDto), typeof(VehicleDto) })]
    public class CustomNamedListVM
    {
        public int Price { get; set; }
    }
}