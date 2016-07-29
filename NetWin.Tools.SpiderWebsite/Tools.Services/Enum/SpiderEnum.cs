using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Services.Enum
{
    public class SpiderEnum : IEnum
    {
        /// <summary>
        /// 抓取状态
        /// </summary>
        public enum PrimaryStatus
        {
            /// <summary>
            /// 未抓取
            /// </summary>
            NoneSpider = 100,
            /// <summary>
            /// 已抓取
            /// </summary>
            Completed = 200
        }

        /// <summary>
        /// 筛选类型
        /// </summary>
        public enum FilterType
        {
            /// <summary>
            /// 包含
            /// </summary>
            Contains=100,

            /// <summary>
            /// 开头匹配
            /// </summary>
            StartWith=200,

            /// <summary>
            /// 结尾匹配
            /// </summary>
            EndWith=300
        }


        public static FilterType IntConvertToEnum(int EnumInt)
        {
            return (FilterType)EnumInt;
        }
    }
}
