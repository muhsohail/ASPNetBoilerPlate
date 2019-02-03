using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Helper
{
    public class IAPIResponse<T>
    {
        public T Result { get; set; }
        public object TargetUrl { get; set; }
        public bool Success { get; set; }
        public object Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
        public bool __Abp { get; set; }
    }
}
