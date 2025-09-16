using System.Collections.Immutable;

namespace Farma.FarmaciaApp.Services;

public record PharmacyProfile(string Codigo, string Nombre, double RadioCoberturaKm, string Direccion, string Contacto);

public class PharmacyAuthState
{
    private readonly ImmutableDictionary<string, (string Password, PharmacyProfile Profile)> _pharmacies;

    public PharmacyAuthState()
    {
        _pharmacies = new Dictionary<string, (string Password, PharmacyProfile Profile)>(StringComparer.OrdinalIgnoreCase)
        {
            ["farmacia.central"] = ("central2024", new PharmacyProfile(
                Codigo: "farmacia.central",
                Nombre: "Farmacia Central",
                RadioCoberturaKm: 4.5,
                Direccion: "Av. Corrientes 1234, CABA",
                Contacto: "011-4567-8901")),
            ["farmacia.barrio"] = ("barrio2024", new PharmacyProfile(
                Codigo: "farmacia.barrio",
                Nombre: "Farmacia del Barrio",
                RadioCoberturaKm: 3.0,
                Direccion: "Av. Santa Fe 5678, CABA",
                Contacto: "011-4567-8902")),
            ["farmacia.norte"] = ("norte2024", new PharmacyProfile(
                Codigo: "farmacia.norte",
                Nombre: "Farmacia Norte Salud",
                RadioCoberturaKm: 6.0,
                Direccion: "Av. Cabildo 2345, CABA",
                Contacto: "011-4756-2233"))
        }.ToImmutableDictionary();
    }

    public PharmacyProfile? Current { get; private set; }
    public bool IsAuthenticated => Current is not null;
    public DateTimeOffset? LastLogin { get; private set; }

    public event Action? OnChange;

    public Task<bool> LoginAsync(string codigo, string password)
    {
        if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(password))
        {
            return Task.FromResult(false);
        }

        if (_pharmacies.TryGetValue(codigo.Trim(), out var entry) && password == entry.Password)
        {
            Current = entry.Profile;
            LastLogin = DateTimeOffset.UtcNow;
            NotifyStateChanged();
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public void Logout()
    {
        Current = null;
        NotifyStateChanged();
    }

    public IReadOnlyCollection<PharmacyProfile> GetRegisteredPharmacies() => _pharmacies.Values.Select(v => v.Profile).ToList();

    private void NotifyStateChanged() => OnChange?.Invoke();
}
