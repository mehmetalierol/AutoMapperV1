using System;
using System.Collections.Generic;

namespace FoMapper.Config
{
    public class FoConfiguration
    {
        public List<FoMapping> MappingList { get; set; }

        internal object GetSection(string v)
        {
            throw new NotImplementedException();
        }
    }
}