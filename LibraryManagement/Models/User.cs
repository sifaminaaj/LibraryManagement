namespace LibraryManagement.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public List<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
