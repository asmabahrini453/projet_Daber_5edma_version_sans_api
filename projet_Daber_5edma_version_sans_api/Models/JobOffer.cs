using Microsoft.AspNetCore.Builder;

namespace projet_Daber_5edma_version_sans_api.Models
{
    public class JobOffer
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime PostedDate { get; set; }

        public string Speciality { get; set; } = null!;
        public string Location { get; set; } = null!;

        //[ForeignKey("Company")]
        public int CompanyId { get; set; }

        public virtual Company? Company { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
