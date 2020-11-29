using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Models.RequestModel;
using API.Models.ResponseModel;
using Application.Common.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet("api/tasks/{id}")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var task = await _taskService.Get(id);
            
            if (task == null)
                return NotFound();
            
            return Ok(_mapper.Map<TaskResponseModel>(task));
        }
        
        [HttpPost("api/tasks/{id}/to-next-stage")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ToNextStage(Guid id)
        {
            var task = await _taskService.ToNextStage(id);
            
            if (task == null)
                return NotFound();
            
            return Ok(_mapper.Map<TaskResponseModel>(task));
        }
        
        [HttpPost("api/tasks/{id}/to-prev-stage")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ToPrevStage(Guid id)
        {
            var task = await _taskService.ToPrevStage(id);
            
            if (task == null)
                return NotFound();
            
            return Ok(_mapper.Map<TaskResponseModel>(task));
        }

        [HttpGet("api/flows/{flowId}/tasks")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTaskByFlowId(Guid flowId)
        {
            var tasks = await _taskService.GetTasksByFlowId(flowId);
            return Ok(_mapper.Map<List<TaskResponseModel>>(tasks));
        }

        [HttpGet("api/states/{stateId}/tasks")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTasksByStateId(Guid stateId)
        {
            var tasks = await _taskService.GetTasksByStateId(stateId);
            return Ok(_mapper.Map<List<TaskResponseModel>>(tasks));
        }

        [HttpPost("api/flows/{flowId}/tasks")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(Guid flowId, TaskRequestModel model)
        {
            var task = await _taskService.Create(new StateTask()
            {
                FlowId = flowId,
                Title = model.Title
            });
            
            return CreatedAtAction(nameof(Get), new {id = task.Id}, _mapper.Map<TaskResponseModel>(task));
        }

        [HttpPut("api/tasks/{id}")]
        [ProducesResponseType(typeof(TaskResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, TaskRequestModel model)
        {
            var task = await _taskService.Update(new StateTask()
            {
                Id = id,
                Title = model.Title  
            });
            
            return Ok(_mapper.Map<TaskResponseModel>(task));
        }

        [HttpDelete("api/tasks/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.Delete(id);
            return Ok();
        }
    }
}