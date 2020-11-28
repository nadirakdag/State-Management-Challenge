using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowController : ControllerBase
    {
        private readonly IFlowService _flowService;

        public FlowController(IFlowService flowService)
        {
            _flowService = flowService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Flow>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var flows = await _flowService.Get();
            return Ok(flows);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Flow), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var flow = await _flowService.Get(id);
            if (flow == null)
                return NotFound();

            return Ok(flow);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Flow), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post(FlowModel model)
        {
            var flow = await _flowService.Create(new Flow()
            {
                Title = model.Title
            });
            return CreatedAtAction(nameof(Get), new {id = flow.Id}, flow);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Flow), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, FlowModel model)
        {
            var flow = await _flowService.Update(new Flow()
            {
                Id = id,
                Title = model.Title
            });

            if (flow == null)
                return NotFound();

            return Ok(flow);
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