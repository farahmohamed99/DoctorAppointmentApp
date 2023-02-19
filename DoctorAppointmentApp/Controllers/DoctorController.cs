using AutoMapper;
using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public DoctorController(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(List<DoctorDto>))]
    public IActionResult GetDoctors()
    {
        try
        {
            var response = _doctorService.GetDoctors();
            return Ok(_mapper.Map<List<DoctorDto>>(response));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{doctorId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(DoctorDto))]
    public IActionResult GetDoctorById(int doctorId)
    {
        try
        {
            var response = _doctorService.GetDoctorById(doctorId);
            return Ok(_mapper.Map<DoctorDto>(response));
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult CreateDoctor([FromBody] DoctorDto doctorDto)
    {
        try
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            _doctorService.CreateDoctor(doctor);
            return Ok("Added successfully");
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult UpdateDoctor([FromBody] DoctorDto doctorDto)
    {
        try
        {
            var doctor = _mapper.Map<Doctor>(doctorDto); 
            _doctorService.UpdateDoctor(doctor);
            return Ok("Updated successfully");
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{doctorId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult DeleteDoctor(int doctorId)
    {
        try
        {
            _doctorService.DeleteDoctor(doctorId);
            return Ok("Deleted successfully");
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("GetDoctorSchedule/{doctorId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(List<int>))]
    public IActionResult GetDoctorSchedule(int doctorId)
    {
        try
        {
            var response = _doctorService.GetDoctorSchedule(doctorId);
            return Ok(response);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("AddTimeSlotToDoctorSchedule")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult AddTimeSlotToDoctorSchedule([FromBody] AddTimeSlotToDoctorScheduleRequest request)
    {
        try
        {
            var doctorSchedule = _mapper.Map<DoctorSchedule>(request);
            _doctorService.AddTimeSlotToDoctorSchedule(doctorSchedule);
            return Ok("Added successfully");
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("RemoveTimeSlotToDoctorSchedule/{doctorScheduleId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult RemoveTimeSlotDoctorSchedule(int doctorScheduleId)
    {
        try
        {
            _doctorService.RemoveTimeSlotFromDoctorSchedule(doctorScheduleId);
            return Ok("Removed successfully");
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
