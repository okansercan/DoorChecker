using DevExpress.Maui;
using DoorChecker.Data;

namespace DoorChecker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseDevExpress()
            .UseDevExpressCollectionView()
            .UseDevExpressControls()
            .UseDevExpressEditors()
            .UseDevExpressDataGrid()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<CheckListPage>();
        builder.Services.AddTransient<CheckItemPage>();
        builder.Services.AddSingleton<DoorCheckDatabase>();

        return builder.Build();
	}
}
