using PzuCwiczenia.Infrastructure.ModelDto;
using PzuCwiczenia.Infrastructure.ServiceInterfaces;

namespace PzuCwiczenia.Services.Books;

public class BookService : IBookService
{
    static List<BookDto> Books = new List<BookDto>();

    public BookService()
    {
        GenerateFakeBooks();
    }    

    public int AddBook(BookDto book)
    {
        int newId = Books.Max(x => x.Id) + 1;
        Books.Add(book);

        return newId;
    }

    public bool DeleteBook(int id)
    {
        BookDto book = Books.FirstOrDefault(x => x.Id == id);
        if (book == null) return false;

        Books.Remove(book);
        return true;
    }

    public BookDto GetBook(int id)
    {
        return Books.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<BookDto> GetBooks()
    {
        return Books;
    }

    public bool UpdateBook(BookDto book)
    {
        BookDto dbBook = Books.First(x => x.Id == book.Id);
        int idx = Books.IndexOf(dbBook);
        Books[idx] = book;
        return true;
    }

    private void GenerateFakeBooks()
    {
        Books.Add(new BookDto
        {
            Id = 1,
            Author = "Leszek Lewandowski",
            Title = "Asp.Net WebApi",
            IsBorrowed = false,
            PageCount = 123
        });

        Books.Add(new BookDto
        {
            Id = 1,
            Author = "Robert C. Martin",
            Title = "Clean Architecture",
            PageCount = 456,
            IsBorrowed = true,
            Borrower = new CustomerDto
            {
                Id=1,
                FirstName = "Adam",
                LastName = "Nowak"
            }
        });
    }
}
