using AutoMapper;
using FoMapper.Atribute;
using FoMapper.Config;
using FoMapper.Extension;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace FoMapper
{
    public class FoMappingProfile : Profile
    {
        private FoConfiguration FoConfig { get; set; }
        public FoMappingProfile(IConfiguration foConfig)
        {
            try
            {
                //get configuration from appsettings.json
                FoConfig = foConfig.GetSection("FoConfiguration").Get<FoConfiguration>();
                DoMapping();
                DoAttributeBasedMappings();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DoMapping()
        {
            try
            {
                
                if (FoConfig != null)
                {
                    if (FoConfig.MappingList == null || FoConfig.MappingList.Count == 0)
                    {
                        throw new Exception("FoConfiguration is wrong or missing in appsettings.json! (FoMapper error)");
                    }
                    else
                    {
                        foreach (var item in FoConfig.MappingList)
                        {
                            SourceToTargetMapping(item);
                        }
                    }
                }
                else
                {
                    throw new Exception("FoConfiguration is wrong or missing in appsettings.json! (FoMapper error)");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DoAttributeBasedMappings()
        {
            var typesWithAttr = MapperExtensions.GetFoTypesWithAttr();
            if(typesWithAttr != null || typesWithAttr.Count() > 0)
            {
                foreach (var item in typesWithAttr)
                {
                    if (item != null && item.CustomAttributes != null && item.CustomAttributes.Count() > 0)
                    {
                        var customAttr = item.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(FoSource));
                        if (customAttr != null)
                        {
                            var sourceType = (Type)customAttr.ConstructorArguments.FirstOrDefault(x => x.ArgumentType == typeof(Type)).Value;
                            var reverseMap = (bool)customAttr.ConstructorArguments.FirstOrDefault(x => x.ArgumentType == typeof(Boolean)).Value;

                            if (sourceType != null)
                            {
                                if (reverseMap == true)
                                {
                                    CreateMap(sourceType, item).ReverseMap();
                                }
                                else
                                {
                                    CreateMap(sourceType, item);
                                }
                            }

                            var sourceTypes = (ReadOnlyCollection<CustomAttributeTypedArgument>)customAttr.ConstructorArguments.FirstOrDefault(x => x.ArgumentType == typeof(Type[])).Value;
                            if (sourceTypes != null)
                            {
                                foreach (var typeItem in sourceTypes)
                                {
                                    if (reverseMap == true)
                                    {
                                        CreateMap(typeItem.Value as Type, item).ReverseMap();
                                    }
                                    else
                                    {
                                        CreateMap(typeItem.Value as Type, item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void SourceToTargetMapping(FoMapping map)
        {
            try
            {
                if (map == null)
                {
                    throw new Exception("Invalid FoConfiguration in appsettings.json ! (FoMapper error)");
                }

                if (map.SourceMember.NameSpace == null || map.TargetMember.NameSpace == null)
                {
                    throw new Exception("You should specify a valid NameSpace for each item in appsettings.json! (FoMapper error)");
                }

                var sourceNameSpace = map.SourceMember.NameSpace;
                var sourceBaseType = map.SourceMember.BaseTypeFullName.ToBaseType();
                var sourceTypes = MapperExtensions.GetFoTypes(sourceNameSpace, sourceBaseType);

                var targetNameSpace = map.TargetMember.NameSpace;
                var targetBaseType = map.TargetMember.BaseTypeFullName.ToBaseType();
                var targetTypes = MapperExtensions.GetFoTypes(targetNameSpace, targetBaseType);

                foreach (var item in targetTypes)
                {
                    Type entityType = null;
                    entityType = sourceTypes.FirstOrDefault(x => x.Name.ClearMapType(map.SourceMember) == item.Name.ClearMapType(map.TargetMember));
                    if (entityType != null)
                    {
                        if (map.ReverseMap)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}