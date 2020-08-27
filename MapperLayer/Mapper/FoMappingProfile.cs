using AutoMapper;
using EntityLayer.Base;
using EntityLayer.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityLayer.Mapper
{
    public class FoMappingProfile : Profile
    {
        private readonly string dtoLayer = "EntityLayer.Dto";
        private readonly string entityLayer = "EntityLayer.Entity";

        public FoMappingProfile()
        {
            CreateAutoMapping();
        }

        //Standart mapleme işlemlerini yapar
        private void CreateAutoMapping()
        {
            EntityDtoAutoMapping();
        }

        //EntityDto arasındaki mappingleri otomatik ekler
        private void EntityDtoAutoMapping()
        {
            var dtoTypes = GetDtoTypes();
            var entityTypes = GetEntityTypes();

            foreach (var item in dtoTypes)
            {
                var entityType = entityTypes.FirstOrDefault(x => MapperExtensions.ClearPostFix(x.Name) == MapperExtensions.ClearPostFix(item.Name));
                if (entityType != null)
                {
                    CreateMap(entityType, item).ReverseMap();
                }
            }
        }

        //Projenin entity tiplerini getirir
        private List<Type> GetEntityTypes()
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(t => t.GetTypes())
                    .Where
                    (
                        t => t.IsClass 
                        && t.BaseType == typeof(FoEntity)
                        && t.Namespace != null
                        && t.Namespace.Contains(entityLayer)
                    )
                    .ToList();
        }

        //Projenin dto tiplerini getirir
        private List<Type> GetDtoTypes()
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(t => t.GetTypes())
                    .Where
                    (
                        t => t.IsClass
                        && t.BaseType == typeof(FoDto)
                        && t.Namespace != null
                        && t.Namespace.Contains(dtoLayer)
                    )
                    .ToList();
        }
    }
}