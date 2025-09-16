using farma_back.Models;

namespace farma_back.Data;

public static class SeedData
{
    public static void Initialize(FarmaContext context)
    {
        // Verificar si ya hay datos
        if (context.Usuarios.Any())
        {
            return; // Ya hay datos
        }

        // Crear usuarios de prueba
        var usuarios = new Usuario[]
        {
            new Usuario
            {
                Nombre = "Juan Pérez",
                Email = "juan.perez@email.com",
                Telefono = "011-1234-5678"
            },
            new Usuario
            {
                Nombre = "María García",
                Email = "maria.garcia@email.com",
                Telefono = "011-8765-4321"
            },
            new Usuario
            {
                Nombre = "Carlos López",
                Email = "carlos.lopez@email.com",
                Telefono = "011-5555-6666"
            }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();
    }
}
