using Application.Features.Counters.Commands.Create;
using Application.Features.Counters.Commands.Delete;
using Application.Features.Counters.Commands.Update;
using Application.Features.Counters.Queries.GetById;
using Application.Features.Counters.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCounterCommand createCounterCommand)
    {
        CreatedCounterResponse response = await Mediator.Send(createCounterCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCounterCommand updateCounterCommand)
    {
        UpdatedCounterResponse response = await Mediator.Send(updateCounterCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCounterResponse response = await Mediator.Send(new DeleteCounterCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCounterResponse response = await Mediator.Send(new GetByIdCounterQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCounterQuery getListCounterQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCounterListItemDto> response = await Mediator.Send(getListCounterQuery);
        return Ok(response);
    }
}