using PzuCwiczenia.Infrastructure.ModelDto;

namespace PzuCwiczenia.Infrastructure.ServiceInterfaces;

public interface IBookService
{
    IEnumerable<BookDto> GetBooks();
    
    BookDto GetBook(int id);

    int AddBook(BookDto book);

    bool UpdateBook(BookDto book);

    bool DeleteBook(int id);
}
