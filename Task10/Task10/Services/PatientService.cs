using Microsoft.EntityFrameworkCore;
using Task10.Context;
using Task10.Models;
using Task10.Models.DTOs;

namespace Task10.Services;

public class PatientService : IPatientService
{
    private ApplicationContext _applicationContext;

    public PatientService(ApplicationContext _applicationContext)
    {
        this._applicationContext = _applicationContext;
    }

    public async Task<Patient> GetPatient(PatientDTO patientDTO, CancellationToken cancellationToken)
    {
        Patient? patient = await _applicationContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == patientDTO.IdPatient, cancellationToken);
        if (patient == null)
        {
            patient = MapPatientDTOToPatient(patientDTO);
            await _applicationContext.Patients.AddAsync(patient, cancellationToken);
            await _applicationContext.SaveChangesAsync(cancellationToken);
        }
        return patient;
    }
    private Patient MapPatientDTOToPatient(PatientDTO patientDTO)
    {
        return new Patient
        {
            IdPatient = patientDTO.IdPatient,
            FirstName = patientDTO.FirstName,
            LastName = patientDTO.LastName,
            BirthDate = patientDTO.Birthdate
        };
    }
    public async Task<PatientGetDTO> GetPatientWithPrescriptionsAsync(int patientId, CancellationToken cancellationToken)
    {
        var patient = await _applicationContext.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicaments)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor)
            .Where(p => p.IdPatient == patientId)
            .FirstOrDefaultAsync(cancellationToken);

        if (patient == null) return null;

        var patientDto = new PatientGetDTO()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionGetDTO()
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorGetDTO()
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentGetDTO()
                    {
                        IdMedicament = pm.Medicaments.IdMedicament,
                        Name = pm.Medicaments.Name,
                        Dose = pm.Dose,
                        Description = pm.Details
                    }).ToList()
                }).ToList()
        };

        return patientDto;
    }
}