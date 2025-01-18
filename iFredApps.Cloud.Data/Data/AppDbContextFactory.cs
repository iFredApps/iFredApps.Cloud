using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredApps.Cloud.Data.Data
{
   public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
   {
      public AppDbContext CreateDbContext(string[] args)
      {
         // Obtém o caminho para o appsettings.json no projeto principal
         var basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
         var configuration = new ConfigurationBuilder()
             .SetBasePath(basePath)
             .AddJsonFile("iFredApps.Cloud.Api/appsettings.json") // Caminho para o appsettings.json do projeto principal
             .Build();

         // Obtém a string de conexão do appsettings.json
         var connectionString = configuration.GetConnectionString("DefaultConnection");

         // Configura as opções do DbContext
         var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
         optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));

         return new AppDbContext(optionsBuilder.Options);
      }
   }
}
