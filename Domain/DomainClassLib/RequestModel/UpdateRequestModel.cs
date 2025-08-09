using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClassLib.RequestModel
{
    public class UpdateRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; //Optional
        public bool IsCompleted { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
