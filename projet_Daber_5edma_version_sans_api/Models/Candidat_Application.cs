namespace projet_Daber_5edma_version_sans_api.Models
{
    public class Candidat_Application
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Speciality { get; set; } = null!;
        public string NameCompany { get; set; } = null!;
        public string TelCompany { get; set; } = null!;
        public string EmailCompany { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
