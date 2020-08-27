namespace EntityLayer.Extension
{
    public static class MapperExtensions
    {
        private static readonly string[] postfixList = { "dto", "entity" };

        public static string ClearPostFix(this string type)
        {
            foreach (var postFix in postfixList)
            {
                if (type.ToLower().EndsWith(postFix.ToLower()))
                {
                    type = type.Substring(0, (type.Length - postFix.Length));
                }
            }

            return type;
        }
    }
}