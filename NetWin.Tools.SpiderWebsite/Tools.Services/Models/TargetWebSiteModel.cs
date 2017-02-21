using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Services.Models
{
    public class TargetWebSiteModel
    {
        public int PrimaryID { set; get; }
        public string WebSiteName { set; get; }
        public string WebSiteUrl { set; get; }
        public int Weights { set; get; }
        public string Group_Name { set; get; }
    }
}
