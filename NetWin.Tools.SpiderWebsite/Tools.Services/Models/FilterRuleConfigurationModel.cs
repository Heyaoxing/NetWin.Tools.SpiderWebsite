using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Services.Models
{
    public class FilterRuleConfigurationModel
    {
        public int ID { set; get; }
        public string FilterKeyWord { set; get; }
        public int FilterType { set; get; }
        public int FilterPosition { set; get; }
    }
}
