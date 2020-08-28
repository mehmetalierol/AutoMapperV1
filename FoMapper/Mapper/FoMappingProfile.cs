using AutoMapper;
using FoMapper.Config;
using FoMapper.Extension;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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