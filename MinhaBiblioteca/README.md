# Sistema de Gerenciamento de Biblioteca Universitária

Web API desenvolvida em C# e ASP.NET Core (.NET 10), com Swagger e armazenamento em memória.

## Dados que já vêm cadastrados

Ao iniciar a API, o repositório cria automaticamente 3 livros e 3 revistas de exemplo. Os IDs são gerados pelo sistema e começam em 1. Para visualizar todos, use `GET /api/Materiais` no Swagger. Como os dados ficam somente em memória, eles voltam aos exemplos iniciais quando a aplicação é reiniciada.

## Requisitos
- .NET 10 SDK
- Visual Studio Code (ou outra IDE)

## Como executar
1. Abra o terminal na pasta que contém `MinhaBiblioteca.csproj`.
2. Restaure as dependências:
   ```bash
   dotnet restore
   ```
3. Compile o projeto:
   ```bash
   dotnet build
   ```
4. Execute a API:
   ```bash
   dotnet run
   ```
5. Abra no navegador o endereço HTTPS mostrado no terminal, acrescentando `/swagger`.

## Endpoints
| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/Materiais` | Lista todos os materiais |
| GET | `/api/Materiais/{id}` | Busca material por ID |
| POST | `/api/Materiais/livro` | Cadastra livro |
| POST | `/api/Materiais/revista` | Cadastra revista |
| PUT | `/api/Materiais/livro/{id}` | Atualiza livro |
| PUT | `/api/Materiais/revista/{id}` | Atualiza revista |
| DELETE | `/api/Materiais/{id}` | Exclui material |
| GET | `/api/Materiais/{id}/descricao` | Exibe descrição e prazo de empréstimo |
| POST | `/api/Materiais/{id}/emprestar` | Tenta emprestar o material |

## Exemplos de JSON

### Livro
```json
{
  "titulo": "O Programador Pragmático",
  "anoPublicacao": 1999,
  "autor": "Andrew Hunt",
  "paginas": 352
}
```

### Revista
```json
{
  "titulo": "Revista de Tecnologia",
  "anoPublicacao": 2026,
  "edicao": 20,
  "mesPublicacao": 11
}
```

O ID é gerado pelo Repository; não precisa ser enviado no cadastro.

## Conceitos demonstrados
- Herança e classe abstrata (`MaterialBiblioteca`)
- `abstract`, `virtual` e `override`
- Polimorfismo entre `Livro` e `Revista`
- Interface `IEmprestimo`, implementada somente por `Livro`
- Repository Pattern e `Dictionary<int, MaterialBiblioteca>`
- Injeção de dependência e Singleton
- Validações e códigos HTTP
- Swagger e CORS

## Observação sobre os dados
Os dados são mantidos somente em memória. Eles permanecem enquanto a aplicação estiver em execução e são reiniciados quando a aplicação é encerrada.
