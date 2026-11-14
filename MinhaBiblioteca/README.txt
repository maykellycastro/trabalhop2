Projeto MinhaBiblioteca - ASP.NET Core Web API

Requisitos: .NET 10 SDK.

Para executar:
1. Abra a pasta MinhaBiblioteca no Visual Studio Code.
2. Abra o terminal nessa pasta.
3. Execute: dotnet restore
4. Execute: dotnet run

A API utiliza o Repository Pattern e injecao de dependencia.
Os dados ficam em memoria e sao reiniciados quando a aplicacao e encerrada.

Rotas:
GET    /api/Materiais
GET    /api/Materiais/{id}
POST   /api/Materiais/livro
POST   /api/Materiais/revista
PUT    /api/Materiais/livro/{id}
PUT    /api/Materiais/revista/{id}
DELETE /api/Materiais/{id}
GET    /api/Materiais/{id}/descricao
POST   /api/Materiais/{id}/emprestar


DADOS INICIAIS: a API inicia com 3 livros e 3 revistas de exemplo. Use GET /api/Materiais no Swagger para visualizar. Os dados ficam em memória e são reiniciados quando a aplicação é reiniciada.
