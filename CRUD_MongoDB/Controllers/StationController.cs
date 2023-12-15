using CRUD_MongoDB.Dto;
using CRUD_MongoDB.Models;
using CRUD_MongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_MongoDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StationController : ControllerBase
{
    private readonly StationsService _stationsService;
    private readonly LinesService _linesService;

    public StationController(StationsService stationsService, LinesService linesService)
    {
        _stationsService = stationsService;
        _linesService = linesService;
    }

    [HttpGet]
    public async Task<List<Station>> Get() =>
        await _stationsService.GetAsync();

    [HttpGet("{id}")]
    public async Task<Station?> Get(string id) =>
        await _stationsService.GetAsync(id);

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StationDto newStationDto)
    {
        if (_linesService.GetAsync(newStationDto.LineId) is null)
            return NotFound("Нет линии с таким id");
        var newStation = new Station
        {
            Id = null,
            Name = newStationDto.Name,
            LineId = newStationDto.LineId
        };
        
        await _stationsService.CreateAsync(newStation);
        return CreatedAtAction(nameof(Get), new { id = newStation.Id }, newStation);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, [FromBody] StationDto updatedStationDto)
    {
        if (_linesService.GetAsync(updatedStationDto.LineId) is null)
            return NotFound("Нет линии с таким id");
        var station = new Station
        {
            Id = id,
            Name = updatedStationDto.Name,
            LineId = updatedStationDto.LineId
        };
        await _stationsService.UpdateAsync(id, station);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var station = await _stationsService.GetAsync(id);
        if (station is null)
            return NotFound();
        await _stationsService.RemoveAsync(id);
        return NoContent();
    }
    
}