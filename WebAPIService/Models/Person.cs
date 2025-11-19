namespace WebAPIService.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Age { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
