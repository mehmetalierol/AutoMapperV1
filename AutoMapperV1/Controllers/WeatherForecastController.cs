using AutoMapper;
using EntityLayer.Dto;
using EntityLayer.Entity;
using EntityLayer.VM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoMapperV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMapper _mapper;

        public WeatherForecastController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public List<UserVM> Get()
        {
            UserEntity userEntity = new UserEntity { Age = 30, NameSurname = "Mehmet Ali EROL", Password = "123456" };
            UserDto userDto = _mapper.Map<UserEntity, UserDto>(userEntity);
            UserVM userVm = _mapper.Map<UserDto, UserVM>(userDto);

            List<UserEntity> userListEntity = new List<UserEntity>
            {
                new UserEntity{ Age = 30, NameSurname = "Mehmet Ali EROL1", Password = "123" },
                new UserEntity{ Age = 40, NameSurname = "Mehmet Ali EROL2", Password = "12345" },
                new UserEntity{ Age = 50, NameSurname = "Mehmet Ali EROL3", Password = "1234567" }
            };

            List<UserDto> userDtoList = _mapper.Map<List<UserEntity>, List<UserDto>>(userListEntity);
            List<UserVM> userVMList = _mapper.Map<List<UserDto>, List<UserVM>>(userDtoList);

            return userVMList;
        }
    }
}