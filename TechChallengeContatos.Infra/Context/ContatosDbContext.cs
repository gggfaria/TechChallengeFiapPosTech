using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.Infra.Configurations;

namespace TechChallengeContatos.Infra.Context;

public class ContatosDbContext(DbContextOptions<ContatosDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationFailure>();
        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.ApplyConfiguration(new ContatoConfiguration());
        
        
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            if (!relationship.IsOwnership) // !VO
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        
        base.OnModelCreating(modelBuilder);

    }

}