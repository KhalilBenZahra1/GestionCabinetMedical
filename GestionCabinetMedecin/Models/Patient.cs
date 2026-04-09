using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace GestionCabinetMedecin.Models

{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        [Required]
        public string ? PatientName { get; set; }
        [Required]
        public string ? PatientSurname { get; set; }
        [DataType(DataType.Date)]
        public DateTime PatientBirthDate { get; set; }
        [Required]
        public string ? Sexe { get; set; }
        [Required]
        public string ? NumeroTel { get; set; }
        public string ? Adresse { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public virtual DossierMedical? DossierMedical { get; set; }
        public virtual ICollection<RendezVous>? RendezVous { get; set; } = new List<RendezVous>();

    }
}
