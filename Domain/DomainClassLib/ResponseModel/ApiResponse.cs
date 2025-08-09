using DomainClassLib.Enum;

namespace DomainClassLib.ResponseModel
{
    public class ApiResponse
    {
        public StatusCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
