using DomainClassLib.Model;

namespace DomainClassLib.ResponseModel
{
    public class GetAllResponseModel : ApiResponse
    {
        public List<ToDo> ToDoList { get; set; }
    }
}
