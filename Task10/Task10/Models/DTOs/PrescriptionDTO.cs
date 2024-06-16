namespace Task10.Models.DTOs;

public class PrescriptionDTO
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDTO Doctor { get; set; }
    public PatientDTO Patient { get; set; }
    public IEnumerable<MedicamentDTO> Medicaments { get; set; }
}
public class PrescriptionGetDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentGetDTO> Medicaments { get; set; }
    public DoctorGetDTO Doctor { get; set; }
}