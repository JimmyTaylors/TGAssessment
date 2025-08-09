using AppClassLib.Interfaces;
using DomainClassLib.Enum;
using DomainClassLib.Model;
using DomainClassLib.RequestModel;
using DomainClassLib.ResponseModel;
using InfrasClassLib.InmemoryData;

namespace AppClassLib.Services
{
    public class ToDoServices: IToDoServices
    {
        public ToDoServices() { 
            
        }

        public GetAllResponseModel GetAll(string userId)
        {
            GetAllResponseModel ret = new GetAllResponseModel();

            #region Validation
            if (string.IsNullOrEmpty(userId))
            {
                ret.ResponseCode = StatusCode.UserIdIsRequired;
                ret.ResponseMessage = StatusCode.UserIdIsRequired.ToString();
                return ret;
            }
            #endregion

            try
            {
                ret.ToDoList = InMemoryDatabase.GetAllByUserID(userId) ?? new List<ToDo>();
                ret.ResponseCode = StatusCode.Success;
                ret.ResponseMessage = StatusCode.Success.ToString();

                return ret;
            }
            catch (Exception ex)
            {
                ret.ResponseCode = StatusCode.SystemError;
                ret.ResponseMessage = StatusCode.SystemError.ToString();

                return ret;
            }
        }

        public GetRespondModel GetByToDoID(string userId, int toDoId)
        {
            GetRespondModel ret = new GetRespondModel();

            #region Validation
            if (string.IsNullOrEmpty(userId))
            {
                ret.ResponseCode = StatusCode.UserIdIsRequired;
                ret.ResponseMessage = StatusCode.UserIdIsRequired.ToString();
                return ret;
            }

            if (toDoId == null || toDoId <= 0)
            {
                ret.ResponseCode = StatusCode.ToDoIDIsRequired;
                ret.ResponseMessage = StatusCode.ToDoIDIsRequired.ToString();
                return ret;
            }
            #endregion

            try
            {
                var toDoInfo = InMemoryDatabase.GetByUserIDByToDoID(userId, toDoId);

                if (toDoInfo != null)
                {
                    ret.ToDoInfo = toDoInfo;
                    ret.ResponseCode = StatusCode.Success;
                    ret.ResponseMessage = StatusCode.Success.ToString();
                }
                else
                {
                    ret.ResponseCode = StatusCode.ToDoIDNotFound;
                    ret.ResponseMessage = StatusCode.ToDoIDNotFound.ToString();
                }

                return ret;
            }
            catch (Exception ex)
            {
                ret.ResponseCode = StatusCode.SystemError;
                ret.ResponseMessage = StatusCode.SystemError.ToString();

                return ret;
            }
        }

        public GetRespondModel Create(CreateRequestModel createRequestModel)
        {
            GetRespondModel ret = new GetRespondModel();

            try
            {
                #region Validation
                if (createRequestModel == null)
                {
                    ret.ResponseCode = StatusCode.NullObject;
                    ret.ResponseMessage = StatusCode.NullObject.ToString();
                    return ret;
                }

                if (string.IsNullOrEmpty(createRequestModel.Title))
                {
                    ret.ResponseCode = StatusCode.ToDoTitleIsRequired;
                    ret.ResponseMessage = StatusCode.ToDoTitleIsRequired.ToString();
                    return ret;
                }

                if (string.IsNullOrEmpty(createRequestModel.CreatedBy))
                {
                    ret.ResponseCode = StatusCode.ToDoCreatedByIsRequired;
                    ret.ResponseMessage = StatusCode.ToDoCreatedByIsRequired.ToString();
                    return ret;
                }
                #endregion

                var userId = createRequestModel.CreatedBy;
                int toDoId = 1;

                ToDo toDo = new ToDo() 
                {
                    Id = toDoId,
                    Title = createRequestModel.Title,
                    Description = createRequestModel.Description,
                    CreatedDate = DateTime.Now,
                    CreatedBy = createRequestModel.CreatedBy
                };

                if (InMemoryDatabase.CheckIfUserExists(userId))
                {
                    var todoList = InMemoryDatabase.GetAllByUserID(userId);
                    toDoId = (todoList?.Count() ?? 0) + 1;
                    toDo.Id = toDoId;
                }

                var isSuccess = InMemoryDatabase.CreateByUserId(userId, toDo);

                if (isSuccess)
                {
                    ret = GetByToDoID(userId, toDoId);
                }
                else
                {
                    ret.ResponseCode = StatusCode.FailedToCreateNewToDo;
                    ret.ResponseMessage = StatusCode.FailedToCreateNewToDo.ToString();
                }

                return ret;
            }
            catch
            {
                ret.ResponseCode = StatusCode.SystemError;
                ret.ResponseMessage = StatusCode.SystemError.ToString();

                return ret;
            }
        }

        public GetRespondModel Update(UpdateRequestModel updateRequestModel)
        {
            GetRespondModel ret = new GetRespondModel();

            try
            {
                #region Validation
                if (updateRequestModel == null)
                {
                    ret.ResponseCode = StatusCode.NullObject;
                    ret.ResponseMessage = StatusCode.NullObject.ToString();
                    return ret;
                }

                if (updateRequestModel.Id  == null || updateRequestModel.Id <= 0)
                {
                    ret.ResponseCode = StatusCode.ToDoIDIsRequired;
                    ret.ResponseMessage = StatusCode.ToDoIDIsRequired.ToString();
                    return ret;
                }

                if (string.IsNullOrEmpty(updateRequestModel.Title))
                {
                    ret.ResponseCode = StatusCode.ToDoTitleIsRequired;
                    ret.ResponseMessage = StatusCode.ToDoTitleIsRequired.ToString();
                    return ret;
                }

                if (string.IsNullOrEmpty(updateRequestModel.UpdatedBy))
                {
                    ret.ResponseCode = StatusCode.ToDoUpdatedByIsRequired;
                    ret.ResponseMessage = StatusCode.ToDoUpdatedByIsRequired.ToString();
                    return ret;
                }
                #endregion

                var userId = updateRequestModel.UpdatedBy;

                bool isSuccessUpdate = false;
                int toDoID = 0;

                if (updateRequestModel.Id != null && int.TryParse(updateRequestModel.Id.ToString(), out toDoID))
                {
                    var toDoInfo = InMemoryDatabase.GetByUserIDByToDoID(userId, toDoID);

                    if (toDoInfo != null)
                    {
                        toDoInfo.Title = updateRequestModel.Title;
                        toDoInfo.Description = updateRequestModel.Description;
                        toDoInfo.IsCompleted = updateRequestModel.IsCompleted;
                        toDoInfo.UpdatedBy = updateRequestModel.UpdatedBy;
                        toDoInfo.UpdatedDate = DateTime.Now;

                        isSuccessUpdate = InMemoryDatabase.UpdateByUserId(userId, toDoInfo);
                    }
                    else
                    {
                        ret.ResponseCode = StatusCode.ToDoIDNotFound;
                        ret.ResponseMessage = StatusCode.ToDoIDNotFound.ToString();
                    }
                }

                if (isSuccessUpdate)
                {
                    ret = GetByToDoID(userId, toDoID);
                }
                else
                {
                    ret.ResponseCode = StatusCode.FailedToUpdateToDo;
                    ret.ResponseMessage = StatusCode.FailedToUpdateToDo.ToString();
                }

                return ret;
            }
            catch
            {
                ret.ResponseCode = StatusCode.SystemError;
                ret.ResponseMessage = StatusCode.SystemError.ToString();

                return ret;
            }
        }

        public ApiResponse Delete(string userId, int toDoId)
        {
            GetRespondModel ret = new GetRespondModel();

            try
            {
                #region Validation
                if (string.IsNullOrEmpty(userId))
                {
                    ret.ResponseCode = StatusCode.UserIdIsRequired;
                    ret.ResponseMessage = StatusCode.UserIdIsRequired.ToString();
                    return ret;
                }

                if (toDoId == null || toDoId <= 0)
                {
                    ret.ResponseCode = StatusCode.ToDoIDIsRequired;
                    ret.ResponseMessage = StatusCode.ToDoIDIsRequired.ToString();
                    return ret;
                }
                #endregion

                bool isSuccessDelete = InMemoryDatabase.DeleteByUserIdByToDoID(userId, toDoId); 

                if (isSuccessDelete)
                {
                    ret.ResponseCode = StatusCode.Success;
                    ret.ResponseMessage = StatusCode.Success.ToString();
                }
                else
                {
                    ret.ResponseCode = StatusCode.FailedToDeleteToDo;
                    ret.ResponseMessage = StatusCode.FailedToDeleteToDo.ToString();
                }

                return ret;
            }
            catch
            {
                ret.ResponseCode = StatusCode.SystemError;
                ret.ResponseMessage = StatusCode.SystemError.ToString();

                return ret;
            }
        }

        #region Private Method


        #endregion
    }
}
