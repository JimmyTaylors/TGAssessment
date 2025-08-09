using System.ComponentModel;

namespace DomainClassLib.Enum
{
    public enum StatusCode
    {
        [Description("Success")]
        Success = 100,

        [Description("System Error")]
        SystemError = 400,
        [Description("User ID Not Found")]
        UserIDNotFound = 401,
        [Description("ToDo ID Not Found")]
        ToDoIDNotFound = 402,
       

        [Description("Invalid Null Object")]
        NullObject = 599,

        [Description("User Id Is Required")]
        UserIdIsRequired = 600,
        [Description("ToDo ID Is Required")]
        ToDoIDIsRequired = 601,
        [Description("ToDo Title Is Required")]
        ToDoTitleIsRequired = 602,
        [Description("ToDo Created By Is Required")]
        ToDoCreatedByIsRequired = 603,
        [Description("ToDo Updated By Is Required")]
        ToDoUpdatedByIsRequired = 604,
        [Description("ToDo IsCompleted Is Required")]
        ToDoIsCompletedIsRequired = 605,

        [Description("Failed To Create New ToDo")]
        FailedToCreateNewToDo = 700,
        [Description("Failed To Update ToDo")]
        FailedToUpdateToDo = 701,
        [Description("Failed To Delete ToDo")]
        FailedToDeleteToDo = 702
    }
}
