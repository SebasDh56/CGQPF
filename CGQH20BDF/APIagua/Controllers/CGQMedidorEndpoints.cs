using Microsoft.EntityFrameworkCore;
using APIagua.Data;
using APIagua.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace APIagua.Controllers;

public static class CGQMedidorEndpoints
{
    public static void MapMedidorEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Medidor").WithTags(nameof(Medidor));

        group.MapGet("/", async (SistemaAguaContext db) =>
        {
            return await db.Medidors.ToListAsync();
        })
        .WithName("GetAllMedidors")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Medidor>, NotFound>> (int idmedidor, SistemaAguaContext db) =>
        {
            return await db.Medidors.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdMedidor == idmedidor)
                is Medidor model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMedidorById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idmedidor, Medidor medidor, SistemaAguaContext db) =>
        {
            var affected = await db.Medidors
                .Where(model => model.IdMedidor == idmedidor)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdMedidor, medidor.IdMedidor)
                    .SetProperty(m => m.NumeroSerie, medidor.NumeroSerie)
                    .SetProperty(m => m.FechaInstalacion, medidor.FechaInstalacion)
                    .SetProperty(m => m.TipoMedidor, medidor.TipoMedidor)
                    .SetProperty(m => m.Ubicacion, medidor.Ubicacion)
                    .SetProperty(m => m.Estado, medidor.Estado)
                    .SetProperty(m => m.FechaUltimaRevision, medidor.FechaUltimaRevision)
                    .SetProperty(m => m.IdUsuario, medidor.IdUsuario)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMedidor")
        .WithOpenApi();

        group.MapPost("/", async (Medidor medidor, SistemaAguaContext db) =>
        {
            db.Medidors.Add(medidor);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Medidor/{medidor.IdMedidor}",medidor);
        })
        .WithName("CreateMedidor")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idmedidor, SistemaAguaContext db) =>
        {
            var affected = await db.Medidors
                .Where(model => model.IdMedidor == idmedidor)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMedidor")
        .WithOpenApi();
    }
}
