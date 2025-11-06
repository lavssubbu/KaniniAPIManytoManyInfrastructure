namespace APIMMwithoutJunctionModel.DTOs
{
    public class GetDocDto
    {
        public string? DocName { get; set; }
        public string? Specialization { get; set; }
        public List<string> PatientName { get; set; }
    }
}
