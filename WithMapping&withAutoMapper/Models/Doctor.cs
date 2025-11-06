using System.ComponentModel.DataAnnotations;

namespace APIMMwithoutJunctionModel.Models
{
    public class Doctor
    {
        [Key]
        public int DocId { get; set; }
        public string? DocName { get; set; }
        public string? Specialization { get; set; }

        public ICollection<Patient>? Patients { get; set; }
    }
}
