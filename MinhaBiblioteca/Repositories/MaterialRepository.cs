using MinhaBiblioteca.Models;

namespace MinhaBiblioteca.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly Dictionary<int, MaterialBiblioteca> _materiais = new();
    private int _proximoId = 1;

    public MaterialRepository()
    {
        // Cadastros iniciais para que a API já abra com dados de exemplo no Swagger.
        Adicionar(new Livro
        {
            Titulo = "O Programador Pragmático",
            AnoPublicacao = 1999,
            Autor = "Andrew Hunt e David Thomas",
            Paginas = 352
        });

        Adicionar(new Livro
        {
            Titulo = "Código Limpo",
            AnoPublicacao = 2008,
            Autor = "Robert C. Martin",
            Paginas = 425
        });

        Adicionar(new Livro
        {
            Titulo = "A Revolução dos Bichos",
            AnoPublicacao = 1945,
            Autor = "George Orwell",
            Paginas = 152
        });

        Adicionar(new Revista
        {
            Titulo = "Revista de Tecnologia",
            AnoPublicacao = 2026,
            Edicao = 20,
            MesPublicacao = 11
        });

        Adicionar(new Revista
        {
            Titulo = "Ciência Hoje",
            AnoPublicacao = 2025,
            Edicao = 10,
            MesPublicacao = 6
        });

        Adicionar(new Revista
        {
            Titulo = "Mundo da Informática",
            AnoPublicacao = 2024,
            Edicao = 5,
            MesPublicacao = 3
        });
    }

    private int GerarNovoId()
    {
        // O contador evita reutilizar IDs após exclusões.
        return _proximoId++;
    }

    public IEnumerable<MaterialBiblioteca> ObterTodos()
    {
        return _materiais.Values;
    }

    public MaterialBiblioteca? ObterPorId(int id)
    {
        return _materiais.TryGetValue(id, out var material) ? material : null;
    }

    public MaterialBiblioteca Adicionar(MaterialBiblioteca material)
    {
        material.Id = GerarNovoId();
        _materiais.Add(material.Id, material);
        return material;
    }

    public bool Atualizar(int id, MaterialBiblioteca material)
    {
        if (!_materiais.ContainsKey(id))
            return false;

        material.Id = id;
        _materiais[id] = material;
        return true;
    }

    public bool Remover(int id)
    {
        return _materiais.Remove(id);
    }
}
