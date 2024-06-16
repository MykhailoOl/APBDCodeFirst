using Task10.Models;
using Task10.Models.DTOs;

namespace Task10.Services;

public interface IPatientService
{
    public Task<Patient> GetPatient(PatientDTO patientDTO, CancellationToken cancellationToken);
    public Task<PatientGetDTO> GetPatientWithPrescriptionsAsync(int patientId, CancellationToken cancellationToken);
}