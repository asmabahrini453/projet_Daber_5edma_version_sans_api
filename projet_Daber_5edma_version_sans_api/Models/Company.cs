namespace projet_Daber_5edma_version_sans_api.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string Location { get; set; } = null!;


        public string Password { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<JobOffer>? JobOffer { get; set; }
    }
}
