using Microsoft.AspNetCore.Mvc;
using Task10.Models.DTOs;
using Task10.Services;

namespace Task10.Controllers;

public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
    [HttpPost]
    public async Task<IActionResult> AddNewPrescription(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken)
    {
        int? result = await _prescriptionService.AddPrescription(prescriptionDto, cancellationToken);
        return Created();
    }
}