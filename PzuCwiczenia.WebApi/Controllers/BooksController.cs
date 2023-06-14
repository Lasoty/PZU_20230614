using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PzuCwiczenia.Infrastructure.ModelDto;
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using PzuCwiczenia.WebApi.ViewModel;
using System.Collections;

namespace PzuCwiczenia.WebApi.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<BookDto> books = bookService.GetBooks();
        IEnumerable<BookViewModel> result = mapper.Map<IEnumerable<BookViewModel>>(books);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (!bookService.Exists(id))
        {
            return NotFound();
        }

        return Ok(mapper.Map<BookViewModel>(bookService.GetBook(id)));
    }
}
