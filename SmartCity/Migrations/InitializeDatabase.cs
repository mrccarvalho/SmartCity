using SmartCity.Data;
using SmartCity.Models;

namespace SmartCity.Migrations
{
    public class InitializeDatabase
    {
        public static void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                using (var db = serviceProvider.GetService<SmartCityDbContext>())
                {
                    SeedEstacionamentos(db);

                }
            }
        }

        

        private static void SeedEstacionamentos(SmartCityDbContext db)
        {
            if (!db.Estacionamentos.Any())
            {
                db.Estacionamentos.Add(new Estacionamento
                {
                    EstacionamentoId = 1,
                    Nome = "Estacionamento1",
                    Descricao = "Estacionamento1"
                });
                db.Estacionamentos.Add(new Estacionamento
                {
                    EstacionamentoId = 2,
                    Nome = "Estacionamento2",
                    Descricao = "Estacionamento2"
                });
                db.Estacionamentos.Add(new Estacionamento
                {
                    EstacionamentoId = 3,
                    Nome = "Estacionamento3",
                    Descricao = "Estacionamento3"
                });
                db.Estacionamentos.Add(new Estacionamento
                {
                    EstacionamentoId = 4,
                    Nome = "Estacionamento4",
                    Descricao = "Estacionamento4"
                });
                db.Estacionamentos.Add(new Estacionamento
                {
                    EstacionamentoId = 5,
                    Nome = "Estacionamento5",
                    Descricao = "Estacionamento5"
                });
                db.Estacionamentos.Add(new Estacionamento
                {
                    EstacionamentoId = 6,
                    Nome = "Estacionamento6",
                    Descricao = "Estacionamento6"
                });
                db.SaveChanges();
            }
        }

      
}
}