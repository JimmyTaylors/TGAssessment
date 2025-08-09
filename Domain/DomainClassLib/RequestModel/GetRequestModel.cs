using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClassLib.RequestModel
{
    public class GetRequestModel
    {
        public string? UserID { get; set; }

        public int ToDoID { get; set; }
    }
}
