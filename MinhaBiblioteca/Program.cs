using MinhaBiblioteca.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers da Web API.
builder.Services.AddControllers();

// Swagger para testar os endpoints.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Uma única instância do Repository durante a execução da aplicação.
builder.Services.AddSingleton<IMaterialRepository, MaterialRepository>();

// Política CORS para permitir o consumo futuro por um frontend.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("PermitirFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
