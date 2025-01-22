using Microsoft.EntityFrameworkCore;
using Prueba.Api.DataAccess;
using Prueba.Api.Domains.State;
using Prueba.Api.DTOs.Task;
using Prueba.Api.DTOs.Universal;
using Prueba.Api.Models;
using Prueba.Api.Services.Files;
using System.Linq;
using System.Net;

namespace Prueba.Api.Domains.Task
{
    public class TaskDomainCreate(DbContexts db, ISaveFileService saveFileService, IStateDomainGetStateById stateDomainGetStateById) : ITaskDomainCreate
    {
        public async Task<UniversalResponseDto<object>> CreateTask(TaskDto task)
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                //var fileName = saveFileService.SaveFileAsync(task.File);

                //if (string.IsNullOrEmpty(fileName))
                //{
                //    response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                //    return response;
                //}

                var stateDb = await db.States.FirstOrDefaultAsync(state => state.Id.Equals(task.StateId));

                if (stateDb == null)
                {
                    response.Code = "CTS-001";  
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    return response;
                }

                var newTask = new TaskModel
                {
                    Name = task.Name,
                    Description = task.Description,
                    State = stateDb,
                    Priority = task.Priority,
                    File = string.Empty
                };

                await db.Tasks.AddAsync(newTask);
                await db.SaveChangesAsync();
                response.StatusCode = HttpStatusCode.Created.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }

    public class TaskDomainDelete(DbContexts db) : ITaskDomainDelete
    {
        public async Task<UniversalResponseDto<object>> DeleteTask(int Id)
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                var taskDb = await db.Tasks.FindAsync(Id);
                if (taskDb == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    return response;
                }

                db.Tasks.Remove(taskDb);
                await db.SaveChangesAsync();
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }

    public class TaskDomainUpdate(DbContexts db) : ITaskDomainUpdate
    {
        public async Task<UniversalResponseDto<object>> UpdateTask(TaskDto task)
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                var taskDb = await db.Tasks.FindAsync(task.Id);
                if (taskDb == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    return response;
                }

                taskDb.Name = task.Name;
                taskDb.Description = task.Description;
                taskDb.Priority = task.Priority;
                taskDb.StateId = task.StateId;

                db.Tasks.Update(taskDb);
                await db.SaveChangesAsync();
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }

    public class TaskDomainGet(DbContexts db) : ITaskDomainGet
    {
        public async Task<UniversalResponseDto<object>> GetTask(int State)
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                var tasksDb = await db.Tasks.Include(s => s.State).Where(x => x.StateId == State).ToListAsync();

                if (tasksDb == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    return response;
                }

                response.Data = tasksDb.Select(x => new {x.Id, x.Name, x.Priority, x.Description, x.StateId}).ToList();
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }

    public class TaskDomainGetSumTasksFilterState(DbContexts db) : ITaskDomainGetSumTasksFilterState
    {
        public async Task<UniversalResponseDto<object>> GetSumTasksFilterState()
        {
            var response = new UniversalResponseDto<object>();
            try
            {
                var tasksDb = await db.Tasks.Include(s => s.State).ToListAsync();

                if (tasksDb == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    return response;
                }

                var sum1 = tasksDb.Where(x => x.State.Name.Equals("to do")).Count();
                var sum2 = tasksDb.Where(x => x.State.Name.Equals("in progress")).Count();
                var sum3 = tasksDb.Where(x => x.State.Name.Equals("done")).Count();
                response.Data = new { uno = sum1, dos = sum2, tres = sum3 };
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            }
            return response;
        }
    }
}
