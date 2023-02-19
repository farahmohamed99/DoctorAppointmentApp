using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(List<Patient>))]
    public IActionResult GetPatients()
    {
        try
        {
            var response = _patientService.GetPatients();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{patientId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(Patient))]
    public IActionResult GetPatientById(int patientId)
    {
        try
        {
            var response = _patientService.GetPatientById(patientId);
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

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult CreatePatient([FromBody] Patient patient)
    {
        try
        {
            _patientService.CreatePatient(patient);
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
    public IActionResult UpdatePatient([FromBody] Patient patient)
    {
        try
        {
            _patientService.UpdatePatient(patient);
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

    [HttpDelete("{patientId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult DeletePatient(int patientId)
    {
        try
        {
            _patientService.DeletePatient(patientId);
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
