namespace FoMapper.Config
{
    public class FoConfiguration
    {
        public string EntityBase { get; set; }
        public string DtoBase { get; set; }
        public string VMBase { get; set; }
        public string[] PostFixList { get; set; }
        public string[] PrefixList { get; set; }
    }
}