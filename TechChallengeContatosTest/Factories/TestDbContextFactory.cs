using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.Infra.Context;

namespace TechChallengeContatosTest.Factories
{
    public static class TestDbContextFactory
    {
        public static ContatosDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ContatosDbContext>()
                .UseInMemoryDatabase(databaseName: "TechChallengeContatosTest")
                .Options;

            var context = new ContatosDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(ContatosDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
