using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Persistence
{
    internal class DbInitializer
    {
        public static void Initialize(UrlDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
