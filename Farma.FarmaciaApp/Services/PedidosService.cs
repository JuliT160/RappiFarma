using Farma.FarmaciaApp.Models;
using Farma.Shared.Enums;

namespace Farma.FarmaciaApp.Services;

public class PedidosService
{
    private readonly List<PharmacyOrder> _orders;

    public PedidosService()
    {
        _orders = new List<PharmacyOrder>
        {
            new PharmacyOrder
            {
                Id = 1024,
                PacienteNombre = "María González",
                PacienteEmail = "maria.gonzalez@email.com",
                PacienteTelefono = "+54 11 4555-1122",
                Direccion = "Av. Corrientes 2233, CABA",
                DistanciaKm = 1.4,
                Estado = EstadoPedido.EN_COTIZACION,
                FechaCreacion = DateTime.UtcNow.AddMinutes(-45),
                NotasCliente = "Entrega antes de las 20 hs si es posible.",
                Recetas = new List<RecetaAdjunto>
                {
                    new("receta_antibiotico.jpg", "image/jpeg", 248_000),
                    new("orden_medica.pdf", "application/pdf", 948_000)
                },
                RequiereEnvio = true,
                MetodoEntregaPreferido = "Delivery 2 hs",
                ZonaCobertura = "Microcentro"
            },
            new PharmacyOrder
            {
                Id = 1042,
                PacienteNombre = "Daniel Ponce",
                PacienteEmail = "daniel.ponce@email.com",
                PacienteTelefono = "+54 11 4556-2211",
                Direccion = "Larrea 345, CABA",
                DistanciaKm = 2.1,
                Estado = EstadoPedido.EN_COTIZACION,
                FechaCreacion = DateTime.UtcNow.AddMinutes(-30),
                NotasCliente = "Paciente oncológico. Priorizar entrega", 
                Recetas = new List<RecetaAdjunto>
                {
                    new("orden_quimio.pdf", "application/pdf", 1_200_000)
                },
                RequiereEnvio = true,
                MetodoEntregaPreferido = "Delivery programado",
                ZonaCobertura = "Recoleta"
            },
            new PharmacyOrder
            {
                Id = 980,
                PacienteNombre = "Soledad Méndez",
                PacienteEmail = "soledad.mendez@email.com",
                PacienteTelefono = "+54 11 4333-8876",
                Direccion = "Av. Santa Fe 678, CABA",
                DistanciaKm = 0.9,
                Estado = EstadoPedido.COTIZADO,
                FechaCreacion = DateTime.UtcNow.AddHours(-3),
                FechaActualizacion = DateTime.UtcNow.AddHours(-2),
                Recetas = new List<RecetaAdjunto>
                {
                    new("receta_control.jpg", "image/jpeg", 180_000)
                },
                CotizacionEnviada = true,
                CotizacionAceptada = true,
                CotizacionMonto = 18450m,
                TiempoEntregaEstimado = TimeSpan.FromHours(1.5),
                Observaciones = "Pedido confirmado. Preparando para delivery",
                RequiereEnvio = true,
                MetodoEntregaPreferido = "Delivery 2 hs",
                ZonaCobertura = "Recoleta"
            },
            new PharmacyOrder
            {
                Id = 912,
                PacienteNombre = "Ricardo Vega",
                PacienteEmail = "ricardo.vega@email.com",
                PacienteTelefono = "+54 11 4777-2233",
                Direccion = "Charcas 1550, CABA",
                DistanciaKm = 2.7,
                Estado = EstadoPedido.EN_PREPARACION,
                FechaCreacion = DateTime.UtcNow.AddHours(-5),
                FechaActualizacion = DateTime.UtcNow.AddMinutes(-50),
                Recetas = new List<RecetaAdjunto>
                {
                    new("orden_cardiologia.pdf", "application/pdf", 875_000)
                },
                CotizacionEnviada = true,
                CotizacionAceptada = true,
                CotizacionMonto = 22500m,
                TiempoEntregaEstimado = TimeSpan.FromHours(2.5),
                Observaciones = "Cliente pasa a retirar 19:30",
                RequiereEnvio = false,
                MetodoEntregaPreferido = "Retiro en mostrador",
                ZonaCobertura = "Palermo"
            },
            new PharmacyOrder
            {
                Id = 870,
                PacienteNombre = "Natalia Romero",
                PacienteEmail = "natalia.romero@email.com",
                PacienteTelefono = "+54 11 4666-5612",
                Direccion = "Av. Alvear 2001, CABA",
                DistanciaKm = 4.2,
                Estado = EstadoPedido.COTIZADO,
                FechaCreacion = DateTime.UtcNow.AddHours(-7),
                FechaActualizacion = DateTime.UtcNow.AddHours(-6),
                Recetas = new List<RecetaAdjunto>
                {
                    new("orden_dolor.pdf", "application/pdf", 650_000)
                },
                CotizacionEnviada = true,
                CotizacionAceptada = false,
                CotizacionMonto = 15900m,
                Observaciones = "Esperando confirmación del paciente",
                RequiereEnvio = true,
                MetodoEntregaPreferido = "Delivery programado",
                ZonaCobertura = "Retiro"
            }
        };
    }

    public event Action? OrdersUpdated;

    public Task<IReadOnlyList<PharmacyOrder>> GetPendingOrdersAsync()
        => Task.FromResult((IReadOnlyList<PharmacyOrder>)_orders
            .Where(o => !o.CotizacionEnviada)
            .OrderBy(o => o.DistanciaKm)
            .ThenByDescending(o => o.FechaCreacion)
            .Select(Clone)
            .ToList());

    public Task<IReadOnlyList<PharmacyOrder>> GetAcceptedOrdersAsync()
        => Task.FromResult((IReadOnlyList<PharmacyOrder>)_orders
            .Where(o => o.CotizacionEnviada)
            .OrderByDescending(o => o.FechaActualizacion ?? o.FechaCreacion)
            .Select(Clone)
            .ToList());

    public Task<bool> SendQuoteAsync(int pedidoId, decimal montoTotal, string? mensaje, TimeSpan? tiempoEntrega)
    {
        var index = _orders.FindIndex(o => o.Id == pedidoId && !o.CotizacionEnviada);
        if (index < 0)
        {
            return Task.FromResult(false);
        }

        var updated = _orders[index] with
        {
            CotizacionEnviada = true,
            CotizacionAceptada = false,
            CotizacionMonto = Math.Round(montoTotal, 2),
            Observaciones = mensaje,
            TiempoEntregaEstimado = tiempoEntrega,
            Estado = EstadoPedido.COTIZADO,
            FechaActualizacion = DateTime.UtcNow
        };

        _orders[index] = updated;
        NotifyChanges();
        return Task.FromResult(true);
    }

    public bool AdvanceOrderStatus(int pedidoId, EstadoPedido nuevoEstado, bool cotizacionAceptada = true)
    {
        var index = _orders.FindIndex(o => o.Id == pedidoId && o.CotizacionEnviada);
        if (index < 0)
        {
            return false;
        }

        var order = _orders[index];
        var updated = order with
        {
            Estado = nuevoEstado,
            CotizacionAceptada = cotizacionAceptada || order.CotizacionAceptada,
            FechaActualizacion = DateTime.UtcNow
        };

        _orders[index] = updated;
        NotifyChanges();
        return true;
    }

    private static PharmacyOrder Clone(PharmacyOrder order)
        => order with { Recetas = order.Recetas.Select(r => r with { }).ToList() };

    private void NotifyChanges() => OrdersUpdated?.Invoke();
}
