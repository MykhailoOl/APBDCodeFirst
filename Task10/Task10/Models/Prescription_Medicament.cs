using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task10.Models;

[Table("prescription_medicaments")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class Prescription_Medicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicaments { get; set; } = null!;
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescriptions { get; set; } = null!;
    public int Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}