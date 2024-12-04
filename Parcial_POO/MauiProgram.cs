using Microsoft.Extensions.Logging;
using ParcialPOO.DataAccess;
using ParcialPOO.ViewModels;
using ParcialPOO.Views;


namespace ParcialPOO
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var dbContex = new EmpleadoDbContext();
            dbContex.Database.EnsureCreated();
            dbContex.Dispose();

            builder.Services.AddDbContext<EmpleadoDbContext>();

            builder.Services.AddTransient<EmpleadoPage>();
            builder.Services.AddTransient<EmpleadoViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(EmpleadoPage),typeof(EmpleadoPage));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
