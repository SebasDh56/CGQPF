using Microsoft.EntityFrameworkCore;
using APIagua.Data;
using APIagua.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace APIagua.Controllers;

public static class CGQPagoEndpoints
{
    public static void MapPagoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Pago").WithTags(nameof(Pago));

        group.MapGet("/", async (SistemaAguaContext db) =>
        {
            return await db.Pagos.ToListAsync();
        })
        .WithName("GetAllPagos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Pago>, NotFound>> (int idpago, SistemaAguaContext db) =>
        {
            return await db.Pagos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdPago == idpago)
                is Pago model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPagoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idpago, Pago pago, SistemaAguaContext db) =>
        {
            var affected = await db.Pagos
                .Where(model => model.IdPago == idpago)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdPago, pago.IdPago)
                    .SetProperty(m => m.IdFactura, pago.IdFactura)
                    .SetProperty(m => m.FechaPago, pago.FechaPago)
                    .SetProperty(m => m.MontoPagado, pago.MontoPagado)
                    .SetProperty(m => m.MetodoPago, pago.MetodoPago)
                    .SetProperty(m => m.Estado, pago.Estado)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePago")
        .WithOpenApi();

        group.MapPost("/", async (Pago pago, SistemaAguaContext db) =>
        {
            db.Pagos.Add(pago);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Pago/{pago.IdPago}",pago);
        })
        .WithName("CreatePago")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idpago, SistemaAguaContext db) =>
        {
            var affected = await db.Pagos
                .Where(model => model.IdPago == idpago)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePago")
        .WithOpenApi();
    }
}
