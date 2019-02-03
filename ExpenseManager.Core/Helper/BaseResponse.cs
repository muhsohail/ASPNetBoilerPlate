using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Helper
{
    public class BaseResponse
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
    }
}
