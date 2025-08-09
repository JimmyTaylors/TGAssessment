using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClassLib.RequestModel
{
    public class CreateRequestModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; //Optional
        public bool IsCompleted { get; set; } = false; //Preset to false as its new task
        public string CreatedBy { get; set; } = string.Empty;
    }
}
