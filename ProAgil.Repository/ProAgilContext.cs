using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
  public class ProAgilContext : IdentityDbContext<User, Role, int,
                                                  IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                  IdentityRoleClaim<int>, IdentityUserToken<int>>
  {
    public ProAgilContext(DbContextOptions<ProAgilContext> options) : base(options) { }

    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Orador> Oradores { get; set; }
    public DbSet<OradorEvento> OradorEventos { get; set; }
    public DbSet<Lote> Lotes { get; set; }
    public DbSet<RedeSocial> RedesSociais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Relações N para N

      // Autenticação
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserRole>(userRole =>
        {
          userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
          userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();
          userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
        }
      );
      // Fim da autenticação

      modelBuilder.Entity<OradorEvento>().HasKey(PE => new { PE.eventoId, PE.oradorId });
    }
  }
}