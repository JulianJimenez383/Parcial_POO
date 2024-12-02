using Microsoft.Extensions.Logging;
using Parcial_POO.DataAccess;
using Parcial_POO.ViewModels;
using Parcial_POO.Views;


namespace Parcial_POO
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


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
