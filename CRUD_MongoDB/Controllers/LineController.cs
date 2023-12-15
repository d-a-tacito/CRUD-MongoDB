using CRUD_MongoDB.Dto;
using CRUD_MongoDB.Models;
using CRUD_MongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_MongoDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LineController : ControllerBase
{
    private readonly LinesService _lineService;

    public LineController(LinesService lineService)
    {
        _lineService = lineService;
    }

    [HttpGet]
    public async Task<List<Line>> Get() =>
        await _lineService.GetAsync();

    [HttpGet("{id}")]
    public async Task<Line?> Get(string id) =>
        await _lineService.GetAsync(id);

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LineDto newLineDto)
    {
        var newLine = new Line
        {
            Id = null,
            Name = newLineDto.Name,
        };
        
        await _lineService.CreateAsync(newLine);
        return CreatedAtAction(nameof(Get), new { id = newLine.Id }, newLine);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, [FromBody] LineDto updatedLineDto)
    {
        var line = new Line
        {
            Id = id,
            Name = updatedLineDto.Name
        };
        await _lineService.UpdateAsync(id, line);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var line = await _lineService.GetAsync(id);
        if (line is null)
            return NotFound();
        await _lineService.RemoveAsync(id);
        return NoContent();
    }
    
}