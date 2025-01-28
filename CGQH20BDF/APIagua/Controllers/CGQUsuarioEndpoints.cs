using Microsoft.EntityFrameworkCore;
using APIagua.Data;
using APIagua.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace APIagua.Controllers;

public static class CGQUsuarioEndpoints
{
    public static void MapUsuarioEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Usuario").WithTags(nameof(Usuario));

        group.MapGet("/", async (SistemaAguaContext db) =>
        {
            return await db.Usuarios.ToListAsync();
        })
        .WithName("GetAllUsuarios")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Usuario>, NotFound>> (int idusuario, SistemaAguaContext db) =>
        {
            return await db.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdUsuario == idusuario)
                is Usuario model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUsuarioById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idusuario, Usuario usuario, SistemaAguaContext db) =>
        {
            var affected = await db.Usuarios
                .Where(model => model.IdUsuario == idusuario)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdUsuario, usuario.IdUsuario)
                    .SetProperty(m => m.Nombre, usuario.Nombre)
                    .SetProperty(m => m.Apellido, usuario.Apellido)
                    .SetProperty(m => m.Direccion, usuario.Direccion)
                    .SetProperty(m => m.Telefono, usuario.Telefono)
                    .SetProperty(m => m.Correo, usuario.Correo)
                    .SetProperty(m => m.TipoUsuario, usuario.TipoUsuario)
                    .SetProperty(m => m.EstadoServicio, usuario.EstadoServicio)
                    .SetProperty(m => m.FechaRegistro, usuario.FechaRegistro)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUsuario")
        .WithOpenApi();

        group.MapPost("/", async (Usuario usuario, SistemaAguaContext db) =>
        {
            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Usuario/{usuario.IdUsuario}",usuario);
        })
        .WithName("CreateUsuario")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idusuario, SistemaAguaContext db) =>
        {
            var affected = await db.Usuarios
                .Where(model => model.IdUsuario == idusuario)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUsuario")
        .WithOpenApi();
    }
}
