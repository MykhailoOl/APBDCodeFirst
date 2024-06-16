using Task10.Context;
using Task10.Exception;
using Task10.Models;
using Task10.Models.DTOs;

namespace Task10.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly PatientService _patientService;
    private readonly ApplicationContext _applicationContext;

    public PrescriptionService(PatientService patientService, ApplicationContext applicationContext)
    {
        _patientService = patientService;
        _applicationContext = applicationContext;
    }

 public async Task<int?> AddPrescription(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken)
        {
            if (prescriptionDto.Date >= prescriptionDto.DueDate)
            {
                throw new DomainException("Wrong date");
            }

            if (prescriptionDto.Medicaments.Count() > 10)
            {
                throw new DomainException("Max 10 medicaments");
            }

            var patient = await _patientService.GetPatient(prescriptionDto.Patient, cancellationToken);

            var doctor = await _applicationContext.Doctors.FindAsync(prescriptionDto.Doctor.IdDoctor);
            if (doctor == null)
            {
                throw new DomainException("Doctor doesn't exist");
            }

            var prescriptionMedicaments = new List<Prescription_Medicament>();
            foreach (var medicamentDto in prescriptionDto.Medicaments)
            {
                var medicament = await _applicationContext.Medicaments.FindAsync(medicamentDto.IdMedicament);
                if (medicament == null)
                {
                    throw new DomainException($"Medicament with ID {medicamentDto.IdMedicament} does not exist");
                }

                var prescriptionMedicament = new Prescription_Medicament()
                {
                    IdMedicament = medicament.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Description
                };
                prescriptionMedicaments.Add(prescriptionMedicament);
            }

            var prescription = new Prescription
            {
                Date = prescriptionDto.Date,
                DueDate = prescriptionDto.DueDate,
                Doctor = doctor,
                Patient = patient,
            };

            await _applicationContext.Prescriptions.AddAsync(prescription, cancellationToken);
            await _applicationContext.SaveChangesAsync(cancellationToken);

            return prescription.IdPrescription;
        }
    }

