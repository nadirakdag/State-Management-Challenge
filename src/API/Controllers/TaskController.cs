using System;
using System.Threading.Tasks;
using API.Models;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("api/tasks/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var task = await _taskService.Get(id);
            return Ok(task);
        }
        
        [HttpPost("api/tasks/{id}/to-next-stage")]
        public async Task<IActionResult> ToNextStage(Guid id)
        {
            var task = await _taskService.Get(id);
            return Ok(task);
        }
        
        [HttpPost("api/tasks/{id}/to-prev-stage")]
        public async Task<IActionResult> ToPrevStage(Guid id)
        {
            var task = await _taskService.Get(id);
            return Ok(task);
        }

        [HttpGet("api/flows/{flowId}/tasks")]
        public async Task<IActionResult> GetTaskByFlowId(Guid flowId)
        {
            var tasks = await _taskService.GetTasksByFlowId(flowId);
            return Ok(tasks);
        }

        [HttpGet("api/states/{stateId}/tasks")]
        public async Task<IActionResult> GetTasksByStateId(Guid stateId)
        {
            var tasks = await _taskService.GetTasksByStateId(stateId);
            return Ok(tasks);
        }

        [HttpPost("api/flows/{flowId}/tasks")]
        public async Task<IActionResult> Post(Guid flowId, TaskModel model)
        {
            var task = await _taskService.Create(new StateTask()
            {
                FlowId = flowId,
                Title = model.Title
            });
            
            return Ok(task);
        }

        [HttpPut("api/tasks/{id}")]
        public async Task<IActionResult> Put(Guid id, TaskModel model)
        {
            var task = await _taskService.Update(new StateTask()
            {
                Id = id,
                Title = model.Title  
            });
            
            return Ok(task);
        }

        [HttpDelete("api/tasks/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.Delete(id);
            return Ok();
        }
    }
}