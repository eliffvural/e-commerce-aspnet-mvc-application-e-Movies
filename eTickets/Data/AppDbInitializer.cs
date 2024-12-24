namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)

        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any()) 
                {

                }

                //Actors
                if (!context.Actors.Any())
                {

                }


                //Producers
                if (!context.Producers.Any())
                {

                }


                //Movies
                if (!context.Movies.Any())
                {

                }


                //Actors-Movies
                if (!context.Actor_Movies.Any())
                {

                }



            }
        }
    }
}
