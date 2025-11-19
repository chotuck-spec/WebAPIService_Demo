namespace WebAPIService.Models
{
    public class MemoryPersons
    {
        public int Id { get; set; }
        public string loginID { get; set; }
        public string password { get; set; } //hash code.
        public string Name { get; set; }
        public int Age { get; set; }
        public float height { get; set; }
        public float width { get; set; }
        public string email { get; set; }
    }
}
