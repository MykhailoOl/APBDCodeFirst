using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Task10.Models;
[Table ("prescriptions")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } = new HashSet<Prescription_Medicament>();
}