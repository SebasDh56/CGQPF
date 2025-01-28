using Microsoft.EntityFrameworkCore;
using APIagua.Data;
using APIagua.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace APIagua.Controllers;

public static class CGQIncidenciumEndpoints
{
    public static void MapIncidenciumEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Incidencium").WithTags(nameof(Incidencium));

        group.MapGet("/", async (SistemaAguaContext db) =>
        {
            return await db.Incidencia.ToListAsync();
        })
        .WithName("GetAllIncidencia")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Incidencium>, NotFound>> (int idincidencia, SistemaAguaContext db) =>
        {
            return await db.Incidencia.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdIncidencia == idincidencia)
                is Incidencium model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetIncidenciumById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idincidencia, Incidencium incidencium, SistemaAguaContext db) =>
        {
            var affected = await db.Incidencia
                .Where(model => model.IdIncidencia == idincidencia)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdIncidencia, incidencium.IdIncidencia)
                    .SetProperty(m => m.IdUsuario, incidencium.IdUsuario)
                    .SetProperty(m => m.IdMedidor, incidencium.IdMedidor)
                    .SetProperty(m => m.TipoIncidencia, incidencium.TipoIncidencia)
                    .SetProperty(m => m.FechaReporte, incidencium.FechaReporte)
                    .SetProperty(m => m.Estado, incidencium.Estado)
                    .SetProperty(m => m.DetalleResolucion, incidencium.DetalleResolucion)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateIncidencium")
        .WithOpenApi();

        group.MapPost("/", async (Incidencium incidencium, SistemaAguaContext db) =>
        {
            db.Incidencia.Add(incidencium);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Incidencium/{incidencium.IdIncidencia}",incidencium);
        })
        .WithName("CreateIncidencium")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idincidencia, SistemaAguaContext db) =>
        {
            var affected = await db.Incidencia
                .Where(model => model.IdIncidencia == idincidencia)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteIncidencium")
        .WithOpenApi();
    }
}
