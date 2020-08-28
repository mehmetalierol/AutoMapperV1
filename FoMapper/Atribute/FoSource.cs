using System;

namespace FoMapper.Atribute
{
    public class FoSource : Attribute
    {
        public FoSource(Type sourceType, bool reverseMap = false)
        {
        }

        public FoSource(Type[] sourceTypeList, bool reverseMap = false)
        {
        }
    }
}