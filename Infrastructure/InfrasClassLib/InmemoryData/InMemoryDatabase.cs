using DomainClassLib.Model;
using DomainClassLib.RequestModel;
using System.Collections.Concurrent;

namespace InfrasClassLib.InmemoryData
{
    public static class InMemoryDatabase
    {
        public static ConcurrentDictionary<string, List<ToDo>> _userToDoList = new ConcurrentDictionary<string, List<ToDo>>() {
            ["abc"] = new List<ToDo>
            {
                new ToDo { Id = 1, Title = "Complete project documentation", Description = "test 1", IsCompleted = false, CreatedDate = DateTime.Now, CreatedBy = "1" },
                new ToDo { Id = 2, Title = "Review pull requests", Description = "test 2",IsCompleted = true, CreatedDate = DateTime.Now, CreatedBy = "1"  }
            },
        };

        public static List<ToDo>? GetAllByUserID(string id)
        {
            try
            {
                if (_userToDoList.TryGetValue(id, out var todoList) && todoList != null)
                    return todoList ?? null;

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ToDo? GetByUserIDByToDoID(string userId, int todoId)
        {
            try
            {
                if (_userToDoList.TryGetValue(userId, out var todoList) && todoList != null)
                    return todoList?.FirstOrDefault(i => i.Id == todoId) ?? null;

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool CreateByUserId(string userId, ToDo updateDto)
        {
            try
            {
                _userToDoList.AddOrUpdate(userId,
                // If the key does not exist, initialize a new list and add the task
                key => new List<ToDo> { updateDto },
                // If the key already exists, update the list by adding the new task
                (key, existingList) => { existingList.Add(updateDto); return existingList; });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateByUserId(string userId, ToDo updateDto)
        {
            try
            {
                if (_userToDoList.TryGetValue(userId, out var todoList) && todoList != null)
                {
                    var record = todoList?.FirstOrDefault(i => i.Id == updateDto.Id);

                    if (record != null && updateDto != null)
                    {
                        record.Id = updateDto.Id;
                        record.Title = updateDto.Title;
                        record.Description = updateDto.Description;
                        record.UpdatedBy = updateDto.UpdatedBy;
                        record.UpdatedDate = DateTime.Now;
                    }

                    return true;
                }
                // Suggestion: Can change return from bool for ResponseDto for handling error message.
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteByUserIdByToDoID(string userId, int toDoId)
        {
            try
            {
                if (_userToDoList.TryGetValue(userId, out var userToDoList) && userToDoList != null)
                {
                    var taskToRemoveObj = userToDoList?.FirstOrDefault(t => t.Id == toDoId);

                    if (taskToRemoveObj != null)
                    {
                        // Remove the task
                        userToDoList?.Remove(taskToRemoveObj);
                        return true;
                    }
                }

                // Above if condition doesnt meet, will throw this error.
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Validation Method from InMemory 
        public static bool CheckIfUserExists(string id)
        {
            if (_userToDoList != null && _userToDoList.Any() && _userToDoList.ContainsKey(id))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
