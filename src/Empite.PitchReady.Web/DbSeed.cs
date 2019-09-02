using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empite.PitchReady.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Empite.PitchReady.Web
{
    public class DbSeed
    {
        public static void InitializePortle(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            

        }
    }
}
