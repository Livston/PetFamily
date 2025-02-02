using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure;

public static class AppExtentions
{
    public  static async Task ApplyMigration(this WebApplication app)
    {
        await using var csope = app.Services.CreateAsyncScope();
        var dbContext = csope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
