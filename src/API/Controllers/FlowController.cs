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
    [Route("api/[controller]")]
    public class FlowController : ControllerBase
    {
        private readonly IFlowService _flowService;
        private readonly IMapper _mapper;
        public FlowController(IFlowService flowService, IMapper mapper)
        {
            _flowService = flowService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FlowResponseModel>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var flows = await _flowService.Get();
            return Ok(_mapper.Map<List<FlowResponseModel>>(flows));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FlowResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var flow = await _flowService.Get(id);
            if (flow == null)
                return NotFound();

            return Ok(_mapper.Map<FlowResponseModel>(flow));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FlowResponseModel), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post(FlowRequestModel model)
        {
            var flow = await _flowService.Create(new Flow()
            {
                Title = model.Title
            });
            return CreatedAtAction(nameof(Get), new {id = flow.Id}, _mapper.Map<FlowResponseModel>(flow));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FlowResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, FlowRequestModel model)
        {
            var flow = await _flowService.Update(new Flow()
            {
                Id = id,
                Title = model.Title
            });

            if (flow == null)
                return NotFound();

            return Ok(_mapper.Map<FlowResponseModel>(flow));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _flowService.Delete(id);
            return NoContent();
        }
    }
}