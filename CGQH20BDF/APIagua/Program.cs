using APIagua.Data;
using APIagua.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer<SistemaAguaContext>
    (builder.Configuration.GetConnectionString("SistemaAguaconection"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapEmpleadoEndpoints();

app.MapFacturaEndpoints();

app.MapIncidenciumEndpoints();

app.MapUsuarioEndpoints();

app.MapPagoEndpoints();

app.MapMedidorEndpoints();

app.Run();
