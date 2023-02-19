using AutoMapper;
using DoctorAppointmentApp.DTOs;
using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(List<AppointmentDto>))]
    public IActionResult GetAppointments([FromQuery] int? doctorId , int? patientId , bool? isPaid = null)
    {
        try
        {
            var response = _appointmentService.GetAppointments(doctorId, patientId, isPaid);
            return Ok(_mapper.Map<List<AppointmentDto>>(response));
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{appointmentId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(AppointmentDto))]
    public IActionResult GetAppointmentById(int appointmentId)
    {
        try
        {
            var response = _appointmentService.GetAppointmentById(appointmentId);
            return Ok(_mapper.Map<AppointmentDto>(response));
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
    public IActionResult CreateAppointment([FromBody] AppointmentDto appointmentDto)
    {
        try
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            _appointmentService.CreateAppointment(appointment);
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
    public IActionResult UpdateAppointmentStatus([FromBody] UpdateAppointmentStatusRequest request)
    {
        try
        {
            var appointment = _mapper.Map<Appointment>(request);
            _appointmentService.UpdateAppointmentStatus(appointment);
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

    [HttpDelete("{appointmentId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult DeleteAppointment(int appointmentId)
    {
        try
        {
            _appointmentService.DeleteAppointment(appointmentId);
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
}
