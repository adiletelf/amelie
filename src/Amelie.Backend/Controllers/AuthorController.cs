using Amelie.Backend.Models;
using Amelie.Backend.Models.Dto;
using Amelie.Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Amelie.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _repository;

    public AuthorController(IAuthorRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Author>), 200)]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Author), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author is null)
        {
            return NotFound($"The record with id: {id} was not found.");
        }

        return Ok(author);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Author), 201)]
    public async Task<IActionResult> Create(CreateUpdateAuthor dto)
    {
        var created = await _repository.Create(dto.FromDto());
        return CreatedAtAction(nameof(GetById), new { id = created.AuthorId }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Author), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(CreateUpdateAuthor dto, int id)
    {
        var author = dto.FromDto();
        author.AuthorId = id;

        var updated = await _repository.Update(author);
        if (updated is null)
        {
            return NotFound($"The record with id: {id} was not found.");
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public IActionResult Remove(int id)
    {
        var author = new Author { AuthorId = id };
        _repository.Remove(author);

        return NoContent();
    }

    [HttpPost("{id}/add-books")]
    public async Task<IActionResult> AddBooks(int id, IEnumerable<int> bookIds)
    {
        var exists = await _repository.Exists(id);
        if (!exists)
        {
            return NotFound($"The author with authorId: {id} was not found.");
        }

        await _repository.AddBooks(id, bookIds);
        return NoContent();
    }

    [HttpPost("{id}/remove-books")]
    public async Task<IActionResult> RemoveBooks(int id, IEnumerable<int> bookIds)
    {
        var exists = await _repository.Exists(id);
        if (!exists)
        {
            return NotFound($"The author with authorId: {id} was not found.");
        }

        await _repository.RemoveBooks(id, bookIds);
        return NoContent();
    }
}
