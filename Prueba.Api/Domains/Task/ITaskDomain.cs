using Prueba.Api.DTOs.Task;
using Prueba.Api.DTOs.Universal;
using Prueba.Api.Models;

namespace Prueba.Api.Domains.Task
{
    public interface ITaskDomainCreate
    {
        Task<UniversalResponseDto<object>> CreateTask(TaskDto task);
    }

    public interface ITaskDomainDelete
    {
        Task<UniversalResponseDto<object>> DeleteTask(int Id);
    }

    public interface ITaskDomainUpdate
    {
        Task<UniversalResponseDto<object>> UpdateTask(TaskDto task);
    }

    public interface ITaskDomainGet
    {
        Task<UniversalResponseDto<object>> GetTask(int State);
    }

    public interface ITaskDomainGetSumTasksFilterState
    {
        Task<UniversalResponseDto<object>> GetSumTasksFilterState();
    }
}
