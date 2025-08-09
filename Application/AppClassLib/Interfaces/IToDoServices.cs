using DomainClassLib.Model;
using DomainClassLib.RequestModel;
using DomainClassLib.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClassLib.Interfaces
{
    public interface IToDoServices
    {
        GetAllResponseModel GetAll(string userId);
        GetRespondModel GetByToDoID(string userId, int toDoId);
        GetRespondModel Create(CreateRequestModel createRequestModel);
        GetRespondModel Update(UpdateRequestModel updateDto);
        ApiResponse Delete(string userId, int toDoId);
    }
}
