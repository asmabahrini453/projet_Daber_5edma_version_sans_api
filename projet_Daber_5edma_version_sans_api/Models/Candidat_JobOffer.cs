namespace projet_Daber_5edma_version_sans_api.Models
{
    public class Candidat_JobOffer
    {
        public string cName { get; set; } = null!;
        public string cSpeciality { get; set; } = null!;
        public string cExperience { get; set; } = null!;
        public string cEducation { get; set; } = null!;
        public string cTel { get; set; } = null!;
        public string cEmail { get; set; } = null!;
        public DateTime cDateNaiss { get; set; }
        public string jaStatus { get; set; } = null!;
        public int jaId { get; set; } 
    }
}
