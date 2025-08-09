using DomainClassLib.Model;

namespace DomainClassLib.ResponseModel
{
    public class GetRespondModel: ApiResponse
    {
        public ToDo ToDoInfo { get; set; }
    }
}
