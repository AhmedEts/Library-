namespace Task_3

{
    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public bool IsAvailable { get; set; }


        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = true; 
        }

        // Override ToString 
        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Available: {IsAvailable}";
        }
    }

    public class Library
    {
        private List<Book> books; 

        // Constructor
        public Library()
        {
            books = new List<Book>();
        }

        // Add a new book 
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
        }

        // Search for books 
        public List<Book> SearchBook(string query)
        {
            var results = books
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count > 0)
            {
                Console.WriteLine("\nSearch Results:");
                foreach (var book in results)
                    Console.WriteLine(book);
            }
            else
            {
                Console.WriteLine($"\nNo books found matching '{query}'.");
            }

            return results;
        }

        // Borrow a book
        public void BorrowBook(string query)
        {
            var results = SearchBook(query);
            if (results.Count > 0)
            {
                var bookToBorrow = results.FirstOrDefault(b => b.IsAvailable);
                if (bookToBorrow != null)
                {
                    bookToBorrow.IsAvailable = false;
                    Console.WriteLine($"You have successfully borrowed '{bookToBorrow.Title}'.");
                }
                else
                {
                    Console.WriteLine($"Sorry, '{query}' is currently not available.");
                }
            }
        }

        // Return a book
        public void ReturnBook(string query)
        {
            var bookToReturn = books.FirstOrDefault(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase));
            if (bookToReturn != null)
            {
                if (!bookToReturn.IsAvailable)
                {
                    bookToReturn.IsAvailable = true;
                    Console.WriteLine($"You have successfully returned '{bookToReturn.Title}'.");
                }
                else
                {
                    Console.WriteLine($"'{bookToReturn.Title}' was not borrowed.");
                }
            }
            else
            {
                Console.WriteLine($"No book titled '{query}' exists in the library.");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            // Adding books to the library
            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565"));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
            library.AddBook(new Book("1984", "George Orwell", "9780451524935"));

            // Searching and borrowing books
            Console.WriteLine("Searching and borrowing books...");
            library.BorrowBook("Gatsby");
            library.BorrowBook("1984");
            library.BorrowBook("Harry Potter"); // For test This book is not in the library

            // Returning books
            Console.WriteLine("\nReturning books...");
            library.ReturnBook("Gatsby");
            library.ReturnBook("Harry Potter"); // For test This book is not borrowed

            Console.ReadLine();
        }
    }
}
