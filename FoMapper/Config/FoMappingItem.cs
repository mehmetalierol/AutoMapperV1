namespace FoMapper.Config
{
    public class FoMappingItem
    {
        //Just name space without class name
        public string NameSpace { get; set; }

        //Full namespace with class name
        public string BaseTypeFullName { get; set; }

        //Postfix list to clear postfix from member name
        public string[] PostFixList { get; set; }

        //Prefix list to clear prefix from member name
        public string[] PrefixList { get; set; }
    }
}