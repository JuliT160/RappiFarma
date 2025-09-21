using MudBlazor;

namespace Farma.FarmaciaApp.Theme;

public static class FarmaMudTheme
{
    public static MudTheme Theme { get; } = new()
    {
        Palette = new PaletteLight
        {
            Primary = "#FF6B35",
            PrimaryContrastText = "#FFFFFF",
            Secondary = "#FFB38A",
            SecondaryContrastText = "#1F1F1F",
            Background = "#FFFFFF",
            Surface = "#FFFFFF",
            AppbarBackground = "#FFFFFF",
            AppbarText = "#1F1F1F",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1F1F1F",
            TextPrimary = "#1F1F1F",
            TextSecondary = "#6E6E6E",
            Divider = "#E4E4E4",
            LinesDefault = "#E4E4E4",
            LinesInputs = "#E4E4E4",
            TableLines = "#E4E4E4",
            Success = "#4CAF50",
            Info = "#2196F3",
            Warning = "#FFC107",
            Error = "#F44336"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#FF6B35",
            PrimaryContrastText = "#FFFFFF",
            Secondary = "#FF8A65",
            SecondaryContrastText = "#FFFFFF",
            Background = "#121212",
            Surface = "#1F1F1F",
            AppbarBackground = "#121212",
            AppbarText = "#F2F2F2",
            DrawerBackground = "#181818",
            DrawerText = "#F2F2F2",
            TextPrimary = "#F2F2F2",
            TextSecondary = "#B5B5B5",
            Divider = "#2E2E2E",
            LinesDefault = "#2E2E2E",
            LinesInputs = "#2E2E2E",
            TableLines = "#2E2E2E",
            Success = "#4CAF50",
            Info = "#64B5F6",
            Warning = "#FFCA28",
            Error = "#EF5350"
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "12px",
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
            },
            Button = new Button
            {
                FontFamily = new[] { "'Open Sans'", "Roboto", "Arial", "sans-serif" },
                FontWeight = 600,
                LetterSpacing = "0.03em"
            }
        }
    };
}
