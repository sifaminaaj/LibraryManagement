namespace LibraryManagement.Models
{
    public class Library
    {
        public int LibraryId {  get; set; }
        public List<Book> Books { get; set; }
        public List<Faculty> Faculties { get; set; }
        public List<Student> Students {  get; set; }
    }
    public class Book
    {
        public int BookId { get; set; }
        public string Title {  get; set; }
        public string Author { get; set; }
        public List<Borrow> Borrows { get; set; }

    }
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string FacultyDepartment { get; set; }
        public List<Borrow> Borrows { get; set; }


    }
    public class Student
    {
        public int StudentId { get; set; }
        public string SName { get; set; }
        public string SDepartment { get; set; }
        public List<Borrow> Borrows { get; set; }
    }
    public class Borrow
    {
        public int BorrowId { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set;  }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }

}
