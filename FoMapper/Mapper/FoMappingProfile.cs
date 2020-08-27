using AutoMapper;
using FoMapper.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoMapper
{
    public class FoMappingProfile : Profile
    {
        private string DtoLayer { get; set; }
        private string EntityLayer { get; set; }
        private string VmLayer { get; set; }
        private bool ReverseMap { get; set; }
        private Type EntityBase { get; set; }
        private Type DtoBase { get; set; }
        private Type VmBase { get; set; }
        public string[] PostFixList { get; set; }
        public string[] PrefixList { get; set; }

        public FoMappingProfile()
        {
        }

        public void UseEntitytoDto(string entityLayer, string dtoLayer, bool reverseMap, Type entityBase = null, Type dtoBase = null)
        {
            EntityLayer = entityLayer;
            DtoLayer = dtoLayer;
            ReverseMap = reverseMap;
            EntityBase = entityBase;
            DtoBase = dtoBase;
            EntityDtoAutoMapping();
        }

        public void UseDtotoVM(string dtoLayer, string vmLayer, bool reverseMap, Type dtoBase = null, Type vmBase = null)
        {
            DtoLayer = dtoLayer;
            VmLayer = vmLayer;
            ReverseMap = reverseMap;
            VmBase = vmBase;
            DtoBase = dtoBase;
            DtoVmAutoMapping();
        }

        //EntityDto arasındaki mappingleri otomatik ekler
        private void EntityDtoAutoMapping()
        {
            var dtoTypes = GetFoTypes(DtoLayer, DtoBase);
            var entityTypes = GetFoTypes(EntityLayer, EntityBase);

            foreach (var item in dtoTypes)
            {
                Type entityType = null;
                if (PostFixList.Length > 0)
                {
                    entityType = entityTypes.FirstOrDefault(x => MapperExtensions.ClearPostFix(x.Name, PostFixList) == MapperExtensions.ClearPostFix(item.Name, PostFixList));
                }
                else if (PrefixList.Length > 0)
                {
                    entityType = entityTypes.FirstOrDefault(x => MapperExtensions.ClearPreFix(x.Name, PrefixList) == MapperExtensions.ClearPreFix(item.Name, PrefixList));
                }

                if (entityType != null)
                {
                    if (ReverseMap)
                    {
                        CreateMap(entityType, item).ReverseMap();
                    }
                    else
                    {
                        CreateMap(entityType, item);
                    }
                }
            }
        }

        private void DtoVmAutoMapping()
        {
            var dtoTypes = GetFoTypes(DtoLayer, DtoBase);
            var vmTypes = GetFoTypes(VmLayer, VmBase);

            foreach (var item in vmTypes)
            {
                Type dtoType = null;
                if (PostFixList.Length > 0)
                {
                    dtoType = dtoTypes.FirstOrDefault(x => MapperExtensions.ClearPostFix(x.Name, PostFixList) == MapperExtensions.ClearPostFix(item.Name, PostFixList));
                }
                else if (PrefixList.Length > 0)
                {
                    dtoType = dtoTypes.FirstOrDefault(x => MapperExtensions.ClearPreFix(x.Name, PrefixList) == MapperExtensions.ClearPreFix(item.Name, PrefixList));
                }

                if (dtoType != null)
                {
                    if (ReverseMap)
                    {
                        CreateMap(dtoType, item).ReverseMap();
                    }
                    else
                    {
                        CreateMap(dtoType, item);
                    }
                }
            }
        }

        //Projenin VM tiplerini getirir
        private List<Type> GetFoTypes(string Layer, Type FoType = null)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(t => t.GetTypes())
                    .Where
                    (
                        t => t.IsClass
                        && (FoType != null ? t.BaseType == FoType : true)
                        && t.Namespace != null
                        && t.Namespace.Contains(Layer)
                    )
                    .ToList();
        }
    }
}