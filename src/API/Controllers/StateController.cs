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
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;

        public StateController(IStateService stateService, IMapper mapper)
        {
            _stateService = stateService;
            _mapper = mapper;
        }

        [HttpGet("api/states/{id}")]
        [ProducesResponseType(typeof(StateResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var state = await _stateService.Get(id);
            if (state == null)
                return NotFound();
            
            return Ok(_mapper.Map<StateResponseModel>(state));
        }

        [HttpGet("api/flow/{id}/states")]
        [ProducesResponseType(typeof(List<StateResponseModel>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetStatesByFlowId(Guid id)
        {
            var states = await _stateService.GetByFlowId(id);
            return Ok(_mapper.Map<List<StateResponseModel>>(states));
        }

        [HttpPost("api/flow/{flowId}/states")]
        [ProducesResponseType(typeof(List<StateResponseModel>), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post(Guid flowId, StateRequestModel model)
        {
            var state = await _stateService.Create(new State()
            {
                FlowId = flowId,
                Title = model.Title
            });
            
            return CreatedAtAction(nameof(Get), new {id = state.Id}, _mapper.Map<StateResponseModel>(state));
        }

        [HttpPut("api/states/{id}")]
        [ProducesResponseType(typeof(StateResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        async Task<IActionResult> Put(Guid id, StateRequestModel model)
        {
            var state = await _stateService.Update(new State()
            {
                Id = id,
                Title = model.Title
            });

            if (state == null)
                return NotFound();

            return Ok(_mapper.Map<StateResponseModel>(state));
        }

        [HttpDelete("api/states/{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _stateService.Delete(id);
            return Ok();
        }
    }
}