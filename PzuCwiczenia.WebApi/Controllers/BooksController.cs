using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PzuCwiczenia.Infrastructure.ModelDto;
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using PzuCwiczenia.WebApi.ViewModel;
using System.Collections;

namespace PzuCwiczenia.WebApi.Controllers;

/// <summary>
/// Zasób książek w bibliotece.
/// </summary>
[Route("[controller]")]
[ApiController]
public class BooksController : Controller
{
    private readonly IBookService bookService;
    private readonly IMapper mapper;

    public BooksController(IBookService bookService, IMapper mapper)
    {
        this.bookService = bookService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Pobiera wszystkie książki z zasobów bibliotecznych
    /// </summary>
    /// <returns>Kolekcja obiektów <see cref="BookViewModel"/>.</returns>
    /// <response code="200">Lista książek dostępnych w bibliotece</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookViewModel>))]
    public async Task<IActionResult> Get()
    {
        IEnumerable<BookDto> books = bookService.GetBooks();
        IEnumerable<BookViewModel> result = mapper.Map<IEnumerable<BookViewModel>>(books);

        return Ok(result);
    }

    /// <summary>
    /// Pobiera wskazany zasób biblioteczny.
    /// </summary>
    /// <param name="id">Id zasobu bibliotecznego.</param>
    /// <returns>Obiekt typu <see cref="BookViewModel"/>.</returns>
    /// <response code="200">Książka o wskazanym ID</response>
    /// <response code="404">Jeśli książka nie istnieje.</response>
    [HttpGet("{id}")]    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IActionResult> Get(int id)
    {
        if (!bookService.Exists(id))
        {
            return NotFound();
        }

        return Ok(mapper.Map<BookViewModel>(bookService.GetBook(id)));
    }

    /// <summary>
    /// Dodaje książke do zasobów bibliotecznych.
    /// </summary>
    /// <param name="book">Zdefiniowana książka w modelu <see cref="BookViewModel"/>.</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request
    /// 
    ///     POST /Books
    ///     Content-Type: application/json
    ///     {
    ///         "id":0,
    ///         "title":"Test from api",
    ///         "author":"Leszek Lewandowski",
    ///         "pageCount":1111,
    ///         "isBorrowed":false,
    ///         "borrower":null
    ///     }
    /// </remarks>
    /// <response code="201">Zwracamy link do nowego zasobu w nagłówku.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] BookViewModel book)
    {
        int id = bookService.AddBook(mapper.Map<BookDto>(book));
        return Created("/Books/" + id, null);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] BookViewModel book)
    {
        if (bookService.UpdateBook(mapper.Map<BookDto>(book)))
            return Ok();

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!bookService.Exists(id))
            return NotFound();

        if (bookService.DeleteBook(id))
            return NoContent();

        return BadRequest();
    }
}
