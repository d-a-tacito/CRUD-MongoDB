using CRUD_MongoDB.Dto;
using CRUD_MongoDB.Models;
using CRUD_MongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_MongoDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly DevicesService _devicesService;
    private readonly StationsService _stationssService;

    public DeviceController(DevicesService devicesService, StationsService stationssService)
    {
        _devicesService = devicesService;
        _stationssService = stationssService;
    }

    [HttpGet]
    public async Task<List<Device>> Get() =>
        await _devicesService.GetAsync();

    [HttpGet("{id}")]
    public async Task<Device?> Get(string id) =>
        await _devicesService.GetAsync(id);

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DeviceDto newDeviceDto)
    {
        if (_stationssService.GetAsync(newDeviceDto.StationId) is null)
            return NotFound("Нет станции с таким id");
        var newStation = new Device
        {
            Id = null,
            Type = newDeviceDto.Type,
            IsWorking = newDeviceDto.IsWorking,
            StationId = newDeviceDto.StationId
        };
        
        await _devicesService.CreateAsync(newStation);
        return CreatedAtAction(nameof(Get), new { id = newStation.Id }, newStation);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, [FromBody] DeviceDto updatedDeviceDto)
    {
        if (_stationssService.GetAsync(updatedDeviceDto.StationId) is null)
            return NotFound("Нет станции с таким id");
        var device = new Device()
        {
            Id = id,
            Type = updatedDeviceDto.Type,
            IsWorking = updatedDeviceDto.IsWorking,
            StationId = updatedDeviceDto.StationId
        };
        await _devicesService.UpdateAsync(id, device);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var device = await _devicesService.GetAsync(id);
        if (device is null)
            return NotFound();
        await _devicesService.RemoveAsync(id);
        return NoContent();
    }
    
}