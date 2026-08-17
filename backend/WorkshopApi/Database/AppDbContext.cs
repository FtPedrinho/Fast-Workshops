// O DbContext é responsável por gerenciar a conexão com o banco de dados e mapear as entidades para as tabelas correspondentes.
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Models;

namespace WorkshopApi.Database;

// O AppDbContext é a ponte entre nossas entidades (Models) e o entity framework (Banco de Dados).
public class AppDbContext : DbContext // Herança do DbContext, permitindo gerenciar dados e entidades.
{
    // Injeção de dependência. O ASP.NET core irá fornecer as configurações de conexão com o Banco de Dados.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ColaboradorModel> Colaboradores { get; set; } // Tabela de colaboradores no bd.

    public DbSet<WorkshopModel> Workshops { get; set; } // Tabela de workshops no bd.

    public DbSet<ParticipacaoModel> Participacoes { get; set; } // Tabela de participantes no bd.

    // O método seguinte configura as entidades e suas propriedades.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureColaborador(modelBuilder);
        ConfigureWorkshop(modelBuilder);
        ConfigureParticipacao(modelBuilder);
    }

    // Determinando os atributos da entidade ColaboradorModel.
    private static void ConfigureColaborador(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ColaboradorModel>(entity =>
        {
            entity.HasKey(colaborador => colaborador.Id);

            entity.Property(colaborador => colaborador.Nome)
                .IsRequired()
                .HasMaxLength(150);
        });
    }

    // Determinando os atributos da entidade WorkshopModel.
    private static void ConfigureWorkshop(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkshopModel>(entity =>
        {
            entity.HasKey(workshop => workshop.Id);

            entity.Property(workshop => workshop.Nome)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(workshop => workshop.Descricao)
                .HasMaxLength(1000);

            entity.Property(workshop => workshop.DataRealizacao)
                .IsRequired();
        });
    }

    // Determinando os atributos da entidade ParticipacaoModel.
    private static void ConfigureParticipacao(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParticipacaoModel>(entity =>
        {
            entity.HasKey(participacao => participacao.Id);

            // Definindo o relacionamento entre ParticipacaoModel, ColaboradorModel e WorkshopModel.
            entity.HasOne(participacao => participacao.Colaborador)
                .WithMany()
                .HasForeignKey(participacao => participacao.ColaboradorId)
                .OnDelete(DeleteBehavior.Cascade); 

            entity.HasOne(participacao => participacao.Workshop)
                .WithMany()
                .HasForeignKey(participacao => participacao.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade);
            // Utilizamos efeito cascata para que, ao deletar um colaborador ou um workshop, todas as participações dele sejam deletadas também.

            entity.HasIndex(
                participacao => new
                {
                    participacao.ColaboradorId,
                    participacao.WorkshopId
                })
                .IsUnique();
                // Impede que um colaborador se inscreva mais de uma vez no mesmo workshop.
        });
    }
}