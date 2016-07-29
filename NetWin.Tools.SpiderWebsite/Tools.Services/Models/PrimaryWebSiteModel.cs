using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Services.Enum;
namespace Tools.Services.Models
{
    public class PrimaryWebSiteModel
    {
        public int ID { set; get; }
        public string WebSiteUrl { set; get; }
        public int SourceID { set; get; }
        public int Level { set; get; }
        public SpiderEnum.PrimaryStatus Status { set; get; }
    }
}
