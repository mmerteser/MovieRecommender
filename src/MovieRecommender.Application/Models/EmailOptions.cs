namespace MovieRecommender.Application.Models
{
    public class EmailOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string FromEmailPassword { get; set; }
    }
}
