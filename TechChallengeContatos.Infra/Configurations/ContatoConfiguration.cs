using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechChallengeContatos.Domain.Contatos;

namespace TechChallengeContatos.Infra.Configurations;

public class ContatoConfiguration: IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(p => p.Telefone)
            .IsRequired()
            .HasColumnType($"varchar(20)");
        
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType($"varchar(500)");
        
        builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnType($"varchar(500)");
        
        builder.OwnsOne(p => p.Ddd)
            .Property(p => p.Codigo)
            .HasColumnType("varchar(3)")
            .IsRequired();
    }

  
}