using Microsoft.AspNetCore.Mvc;
using Task10.Services;

namespace Task10.Controllers;

public class PatientController : ControllerBase
{
    private IPatientService _patientService;

    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatient(int patientId, CancellationToken cancellationToken)
    {
        return Ok(await _patientService.GetPatientWithPrescriptionsAsync(patientId, cancellationToken));
    }
}