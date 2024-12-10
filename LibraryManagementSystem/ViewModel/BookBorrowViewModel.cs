namespace LibraryManagementSystem.ViewModel
{
    public class BookBorrowViewModel
    {
        
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public int AvailableCopies { get; set; } 
        public decimal price { get; set; } 
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }


    }
}
