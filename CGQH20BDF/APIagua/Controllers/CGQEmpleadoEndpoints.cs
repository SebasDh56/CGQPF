using Microsoft.EntityFrameworkCore;
using APIagua.Data;
using APIagua.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace APIagua.Controllers;

public static class CGQEmpleadoEndpoints
{
    public static void MapEmpleadoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Empleado").WithTags(nameof(Empleado));

        group.MapGet("/", async (SistemaAguaContext db) =>
        {
            return await db.Empleados.ToListAsync();
        })
        .WithName("GetAllEmpleados")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Empleado>, NotFound>> (int idempleado, SistemaAguaContext db) =>
        {
            return await db.Empleados.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdEmpleado == idempleado)
                is Empleado model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEmpleadoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idempleado, Empleado empleado, SistemaAguaContext db) =>
        {
            var affected = await db.Empleados
                .Where(model => model.IdEmpleado == idempleado)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdEmpleado, empleado.IdEmpleado)
                    .SetProperty(m => m.Nombre, empleado.Nombre)
                    .SetProperty(m => m.Apellido, empleado.Apellido)
                    .SetProperty(m => m.Cargo, empleado.Cargo)
                    .SetProperty(m => m.FechaContratacion, empleado.FechaContratacion)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEmpleado")
        .WithOpenApi();

        group.MapPost("/", async (Empleado empleado, SistemaAguaContext db) =>
        {
            db.Empleados.Add(empleado);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Empleado/{empleado.IdEmpleado}",empleado);
        })
        .WithName("CreateEmpleado")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idempleado, SistemaAguaContext db) =>
        {
            var affected = await db.Empleados
                .Where(model => model.IdEmpleado == idempleado)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEmpleado")
        .WithOpenApi();
    }
}
