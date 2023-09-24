namespace TRS.FinalPlantasy.Web.Infrastructure;

internal static class MainWebApplicationBuilder
{
    public static WebApplication Build(WebApplicationBuilder builder, Serilog.ILogger logger)
    {
        logger.Information("Building application");

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        return app;
    }
}


