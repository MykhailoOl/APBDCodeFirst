using Task10.Models.DTOs;

namespace Task10.Services;

public interface IPrescriptionService
{ 
    public Task<int?> AddPrescription(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken);
}