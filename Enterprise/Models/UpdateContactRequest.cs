namespace Enterprise.Models
{
    public class UpdateContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long PhoneNo { get; set; }
        public string Address { get; set; }
    }
}
