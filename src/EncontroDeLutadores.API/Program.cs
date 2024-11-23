using EncontroDeLutadores.API.Configuracoes;
using EncontroDeLutadores.Infra.RabbitMQ.Consumers.Implementacao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.EntityFrameworkConfiguracao();
builder.Services.IdentityConfiguracao();
builder.Services.InjecaoDepenciaConfiguracao();
builder.Services.MongoDBConfiguration();

builder.Services.AddHostedService<EmailConsumerCadastroUsuario>();

var app = builder.Build(); 

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options.WithOrigins().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();

app.Run();
