using System.ComponentModel.DataAnnotations;

namespace APIMMwithoutJunctionModel.Models
{
    public class Patient
    {
        [Key]
        public int PatId {  get; set; }
        public string? PatName { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }


    }
}
