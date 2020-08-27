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
            foreach (var postFix in postFixList)
            {
                if (type.ToLower().EndsWith(postFix.ToLower()))
                {
                    type = type.Substring(0, (type.Length - postFix.Length));
                }
            }

            return type;
        }

        /// <summary>
        /// This method will clear prefix from given type name 
        /// </summary>
        /// <param name="type">Type name</param>
        /// <param name="postFixList">Prefix list to clear from type name</param>
        /// <returns></returns>
        public static string ClearPreFix(this string type, string[] preFixList)
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
    }
}