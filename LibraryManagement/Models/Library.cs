namespace LibraryManagement.Models
{
    public class Library
    {
        public int LibraryId {  get; set; }
        public List<Book> Books { get; set; }
        public List<User> Users {  get; set; }
    }
    
    
   

}
