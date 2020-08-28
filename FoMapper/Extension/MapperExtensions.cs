using FoMapper.Atribute;
using FoMapper.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoMapper.Extension
{
    /// <summary>
    /// Extension methods for FoMapper
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// This method will clear postfix from given type name
        /// </summary>
        /// <param name="type">Type name</param>
        /// <param name="postFixList">Postfix list to clear from type name</param>
        /// <returns></returns>
        public static string ClearPostFix(this string type, string[] postFixList)
        {
            try
            {
                foreach (var postFix in postFixList)
                {
                    if (type.ToLower().EndsWith(postFix.ToLower()))
                    {
                        type = type.Substring(0, (type.Length - postFix.Length));
                    }
                }

                return type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will clear prefix from given type name
        /// </summary>
        /// <param name="type">Type name</param>
        /// <param name="postFixList">Prefix list to clear from type name</param>
        /// <returns></returns>
        public static string ClearPreFix(this string type, string[] preFixList)
        {
            try
            {
                foreach (var preFix in preFixList)
                {
                    if (type.ToLower().StartsWith(preFix.ToLower()))
                    {
                        type = type.Substring((preFix.Length - 1), (type.Length - preFix.Length));
                    }
                }

                return type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will return Base type from given fullname of the class
        /// </summary>
        /// <param name="fullName">Class name with namespace</param>
        /// <returns></returns>
        public static Type ToBaseType(this string fullName)
        {
            try
            {
                if (fullName != null && fullName.Trim().Length > 0)
                {
                    var splitter = fullName.Split('.');
                    var typeName = splitter[splitter.Length - 1];
                    var nameSpace = "";
                    for (int i = 0; i < splitter.Length; i++)
                    {
                        if (i != splitter.Length - 1)
                        {
                            nameSpace += splitter[i] + ".";
                        }
                    }
                    nameSpace = nameSpace.Substring(0, nameSpace.Length - 1);
                    return
                        AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(t => t.GetTypes())
                            .Where
                            (
                                t => t.IsClass
                                && t.Name == typeName
                                && t.Namespace != null
                                && t.Namespace.Contains(nameSpace)
                            ).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method returns types of class from given layer (Base type is optional)
        /// </summary>
        /// <param name="Layer">Layer namespace of classes</param>
        /// <param name="FoType">You can specify base class of your classes</param>
        /// <returns></returns>
        public static List<Type> GetFoTypes(string Layer, Type baseType = null)
        {
            try
            {
                return
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(t => t.GetTypes())
                        .Where
                        (
                            t => t.IsClass
                            && (baseType != null ? t.BaseType == baseType : true)
                            && t.Namespace != null
                            && t.Namespace.Contains(Layer)
                        )
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Type> GetFoTypesWithAttr()
        {
            try
            {
                return
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(t => t.GetTypes())
                        .Where
                        (
                            t => t.IsClass
                            && t.Namespace != null
                            && (t.GetCustomAttributes(typeof(FoSource), false).Count() > 0)
                        )
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method clears the postfixes and prefixes at the same time depends on your lists in appsettings.json
        /// </summary>
        /// <param name="typeName">Typename which you want to clear</param>
        /// <param name="itemModel">Mapping properties from appsettings.json</param>
        /// <returns></returns>
        public static string ClearMapType(this string typeName, FoMappingItem itemModel)
        {
            try
            {
                var result = "";
                if (itemModel.PrefixList != null && itemModel.PrefixList.Length > 0)
                {
                    result = ClearPreFix(typeName, itemModel.PrefixList);
                }

                if (itemModel.PostFixList != null && itemModel.PostFixList.Length > 0)
                {
                    result = ClearPostFix(typeName, itemModel.PostFixList);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}