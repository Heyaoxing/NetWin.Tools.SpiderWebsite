using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Services.Models
{
    public class ResultModel<T> : IResultModel
    {
        public T Data { set; get; }
    }

    public class ResultModel: IResultModel
    {
    }
}
