using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Api.Domains.Task;
using Prueba.Api.DTOs.Task;

namespace Prueba.Api.Controllers.Task
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(
        ITaskDomainCreate taskDomainCreate, 
        ITaskDomainDelete taskDomainDelete, 
        ITaskDomainGet taskDomainGet,
        ITaskDomainGetSumTasksFilterState taskDomainGetSumTasksFilterState,
        ITaskDomainUpdate taskDomainUpdate) : ControllerBase
    {
        [HttpGet("filter")]
        public async Task<IActionResult> GetTask([FromHeader] int stateId)
        {
            var response = await taskDomainGet.GetTask(stateId);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("sum")]
        public async Task<IActionResult> GetSumTasksFilterState()
        {
            var response = await taskDomainGetSumTasksFilterState.GetSumTasksFilterState();
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto task)
        {
            var response = await taskDomainCreate.CreateTask(task);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskDto task)
        {
            var response = await taskDomainUpdate.UpdateTask(task);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteTask([FromHeader] int taskId)
        {
            var response = await taskDomainDelete.DeleteTask(taskId);
            return StatusCode(response.StatusCode, response.Data);
        }


    }
}
