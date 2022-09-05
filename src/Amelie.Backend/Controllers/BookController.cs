using Amelie.Backend.Models;
using Amelie.Backend.Models.Dto;
using Amelie.Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Amelie.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _repository;

    public BookController(IBookRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book is null)
        {
            return NotFound($"The record with id: {id} was not found.");
        }

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), 201)]
    public async Task<IActionResult> Create(CreateUpdateBook dto)
    {
        var created = await _repository.Create(dto.FromDto());
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.BookId }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(CreateUpdateBook dto, int id)
    {
        var book = dto.FromDto();
        book.BookId = id;

        var updated = await _repository.Update(book);
        if (updated is null)
        {
            return NotFound($"The record with id: {id} was not found.");
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Delete(int id)
    {
        var book = new Book { BookId = id };
        await _repository.Remove(book);

        return NoContent();
    }


    [HttpPost("{id}/add-authors")]
    public async Task<IActionResult> AddAuthors(int id, IEnumerable<int> authorIds)
    {
        var exists = await _repository.Exists(id);
        if (!exists)
        {
            return NotFound($"The book with bookId: {id} was not found.");
        }

        await _repository.AddAuthorsAsync(id, authorIds);
        return NoContent();
    }

    [HttpPost("{id}/remove-authors")]
    public async Task<IActionResult> RemoveAuthors(int id, IEnumerable<int> authorIds)
    {
        var exists = await _repository.Exists(id);
        if (!exists)
        {
            return NotFound($"The book with bookId: {id} was not found.");
        }

        await _repository.RemoveAuthorsAsync(id, authorIds);
        return NoContent();
    }
}
