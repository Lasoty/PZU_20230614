namespace PzuCwiczenia.WebApi.ViewModel;

public class BookViewModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public int PageCount { get; set; }

    public bool IsBorrowed { get; set; }

    public CustomerViewModel? Borrower { get; set; }
}
