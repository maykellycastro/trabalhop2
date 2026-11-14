using Microsoft.AspNetCore.Mvc;
using MinhaBiblioteca.Models;
using MinhaBiblioteca.Repositories;

namespace MinhaBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriaisController : ControllerBase
{
    private readonly IMaterialRepository _repository;

    public MateriaisController(IMaterialRepository repository)
    {
        _repository = repository;
    }

    // GET /api/Materiais
    [HttpGet]
    public IActionResult ObterTodos()
    {
        return Ok(_repository.ObterTodos());
    }

    // GET /api/Materiais/{id}
    [HttpGet("{id:int}")]
    public IActionResult ObterPorId(int id)
    {
        var material = _repository.ObterPorId(id);
        if (material is null)
            return NotFound("Material não encontrado.");

        return Ok(material);
    }

    // POST /api/Materiais/livro
    [HttpPost("livro")]
    public IActionResult AdicionarLivro([FromBody] Livro livro)
    {
        var erro = ValidarMaterial(livro);
        if (erro is not null) return BadRequest(erro);

        if (string.IsNullOrWhiteSpace(livro.Autor))
            return BadRequest("O autor é obrigatório.");

        if (livro.Paginas <= 0)
            return BadRequest("O número de páginas deve ser maior que zero.");

        var cadastrado = _repository.Adicionar(livro);
        return CreatedAtAction(nameof(ObterPorId), new { id = cadastrado.Id }, cadastrado);
    }

    // POST /api/Materiais/revista
    [HttpPost("revista")]
    public IActionResult AdicionarRevista([FromBody] Revista revista)
    {
        var erro = ValidarMaterial(revista);
        if (erro is not null) return BadRequest(erro);

        erro = ValidarRevista(revista);
        if (erro is not null) return BadRequest(erro);

        var cadastrado = _repository.Adicionar(revista);
        return CreatedAtAction(nameof(ObterPorId), new { id = cadastrado.Id }, cadastrado);
    }

    // PUT /api/Materiais/livro/{id}
    [HttpPut("livro/{id:int}")]
    public IActionResult AtualizarLivro(int id, [FromBody] Livro livro)
    {
        if (_repository.ObterPorId(id) is not Livro)
            return NotFound("Livro não encontrado.");

        var erro = ValidarMaterial(livro);
        if (erro is not null) return BadRequest(erro);

        if (string.IsNullOrWhiteSpace(livro.Autor))
            return BadRequest("O autor é obrigatório.");

        if (livro.Paginas <= 0)
            return BadRequest("O número de páginas deve ser maior que zero.");

        livro.Id = id; // O ID da URL sempre prevalece.
        _repository.Atualizar(id, livro);
        return Ok(livro);
    }

    // PUT /api/Materiais/revista/{id}
    [HttpPut("revista/{id:int}")]
    public IActionResult AtualizarRevista(int id, [FromBody] Revista revista)
    {
        if (_repository.ObterPorId(id) is not Revista)
            return NotFound("Revista não encontrada.");

        var erro = ValidarMaterial(revista);
        if (erro is not null) return BadRequest(erro);

        erro = ValidarRevista(revista);
        if (erro is not null) return BadRequest(erro);

        revista.Id = id; // O ID da URL sempre prevalece.
        _repository.Atualizar(id, revista);
        return Ok(revista);
    }

    // DELETE /api/Materiais/{id}
    [HttpDelete("{id:int}")]
    public IActionResult Remover(int id)
    {
        if (!_repository.Remover(id))
            return NotFound("Material não encontrado.");

        return NoContent();
    }

    // GET /api/Materiais/{id}/descricao
    [HttpGet("{id:int}/descricao")]
    public IActionResult ObterDescricao(int id)
    {
        var material = _repository.ObterPorId(id);
        if (material is null)
            return NotFound("Material não encontrado.");

        return Ok(new
        {
            id = material.Id,
            tipo = material.GetType().Name,
            descricao = material.Descricao(),
            prazoEmprestimo = material.CalcularPrazo()
        });
    }

    // POST /api/Materiais/{id}/emprestar
    [HttpPost("{id:int}/emprestar")]
    public IActionResult Emprestar(int id)
    {
        var material = _repository.ObterPorId(id);
        if (material is null)
            return NotFound("Material não encontrado.");

        if (material is not IEmprestimo emprestimo)
            return BadRequest("Este material não permite empréstimo.");

        return Ok(new { mensagem = emprestimo.Emprestar() });
    }

    private static string? ValidarMaterial(MaterialBiblioteca material)
    {
        if (string.IsNullOrWhiteSpace(material.Titulo))
            return "O título é obrigatório e não pode conter somente espaços.";

        if (material.AnoPublicacao < 0)
            return "O ano de publicação não pode ser menor que zero.";

        return null;
    }

    private static string? ValidarRevista(Revista revista)
    {
        if (revista.Edicao <= 0)
            return "O número da edição deve ser maior que zero.";

        if (revista.MesPublicacao < 1 || revista.MesPublicacao > 12)
            return "O mês de publicação deve estar entre 1 e 12.";

        return null;
    }
}
