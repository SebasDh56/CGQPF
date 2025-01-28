using Microsoft.EntityFrameworkCore;
using APIagua.Data;
using APIagua.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace APIagua.Controllers;

public static class CGQFacturaEndpoints
{
    public static void MapFacturaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Factura").WithTags(nameof(Factura));

        group.MapGet("/", async (SistemaAguaContext db) =>
        {
            return await db.Facturas.ToListAsync();
        })
        .WithName("GetAllFacturas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Factura>, NotFound>> (int idfactura, SistemaAguaContext db) =>
        {
            return await db.Facturas.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdFactura == idfactura)
                is Factura model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFacturaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idfactura, Factura factura, SistemaAguaContext db) =>
        {
            var affected = await db.Facturas
                .Where(model => model.IdFactura == idfactura)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdFactura, factura.IdFactura)
                    .SetProperty(m => m.IdUsuario, factura.IdUsuario)
                    .SetProperty(m => m.IdLectura, factura.IdLectura)
                    .SetProperty(m => m.FechaEmision, factura.FechaEmision)
                    .SetProperty(m => m.FechaVencimiento, factura.FechaVencimiento)
                    .SetProperty(m => m.MontoTotal, factura.MontoTotal)
                    .SetProperty(m => m.Estado, factura.Estado)
                    .SetProperty(m => m.DetalleConsumo, factura.DetalleConsumo)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFactura")
        .WithOpenApi();

        group.MapPost("/", async (Factura factura, SistemaAguaContext db) =>
        {
            db.Facturas.Add(factura);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Factura/{factura.IdFactura}",factura);
        })
        .WithName("CreateFactura")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idfactura, SistemaAguaContext db) =>
        {
            var affected = await db.Facturas
                .Where(model => model.IdFactura == idfactura)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFactura")
        .WithOpenApi();
    }
}
