using System.Text.Json.Serialization;

namespace MinhaBiblioteca.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "tipoMaterial")]
[JsonDerivedType(typeof(Livro), "Livro")]
[JsonDerivedType(typeof(Revista), "Revista")]
public abstract class MaterialBiblioteca
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public int AnoPublicacao { get; set; }

    public abstract int CalcularPrazo();

    public virtual string Descricao()
    {
        return $"Título: {Titulo}";
    }
}
