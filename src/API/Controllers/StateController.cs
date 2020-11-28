using System;
using System.Threading.Tasks;
using API.Models;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet("api/states/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var state = await _stateService.Get(id);
            return Ok(state);
        }

        [HttpGet("api/flow/{id}/states")]
        public async Task<IActionResult> GetStatesByFlowId(Guid id)
        {
            var states = await _stateService.GetByFlowId(id);
            return Ok(states);
        }

        [HttpPost("api/flow/{flowId}/states")]
        public async Task<IActionResult> Post(Guid flowId, StateModel model)
        {
            var state = await _stateService.Create(new State()
            {
                FlowId = flowId,
                Title = model.Title
            });
            return Ok(state);
        }

        [HttpPut("api/states/{id}")]
        public async Task<IActionResult> Put(Guid id, StateModel model)
        {
            var state = await _stateService.Update(new State()
            {
                Id = id,
                Title = model.Title
            });
            return Ok(state);
        }

        [HttpDelete("api/states/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _stateService.Delete(id);
            return Ok();
        }
    }
}