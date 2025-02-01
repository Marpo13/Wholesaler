using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.DataAccess;

namespace Wholesaler.Backend.Api;

public static class DbUpdater
{
    public static void UseDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WholesalerContext>();
        if (dbContext.Database.IsRelational())
            dbContext.Database.Migrate();
    }
}
