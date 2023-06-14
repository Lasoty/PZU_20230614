namespace PzuCwiczenia.WebApi.ViewModel;


/// <summary>
/// Obiekt reprezentujący książke w zasobach bibliotecznych
/// </summary>
public class BookViewModel
{
    /// <summary>
    /// Identyfikator książki
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tytuł pozycji
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Autor książki
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Liczba stron
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// Determinuje czy pozycja została wypożyczona
    /// </summary>
    public bool IsBorrowed { get; set; }

    /// <summary>
    /// Informacje o wypożyczającym
    /// </summary>
    public CustomerViewModel? Borrower { get; set; }
}
