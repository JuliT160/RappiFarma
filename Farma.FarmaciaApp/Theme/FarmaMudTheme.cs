using MudBlazor;

namespace Farma.FarmaciaApp.Theme;

public static class FarmaMudTheme
{
    public static MudTheme Theme { get; } = new()
    {
        Palette = new Palette
        {
            Primary = "#FF6B35",
            PrimaryContrastText = "#FFFFFF",
            Secondary = "#FF8A65",
            SecondaryContrastText = "#FFFFFF",
            Tertiary = "#FF9800",
            Background = "#FFF5F0",
            Surface = "#FFFFFF",
            AppbarBackground = "#FF6B35",
            AppbarText = "#FFFFFF",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1f1f1f",
            Success = "#4CAF50",
            Info = "#2196F3",
            Warning = "#FFC107",
            Error = "#F44336"
        },
        LayoutProperties = new LayoutProperties
        {
            AppbarHeight = "64px"
        },
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = new[] { "'Open Sans'", "Roboto", "Arial", "sans-serif" }
            },
            H4 = new H4
            {
                FontFamily = new[] { "'Open Sans'", "Roboto", "Arial", "sans-serif" },
                FontWeight = 600
            }
        }
    };
}
