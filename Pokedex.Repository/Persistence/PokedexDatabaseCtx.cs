using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pokedex.Repository.Context;

namespace Pokedex.Repository.Persistence
{
    public partial class PokedexDatabaseCtx : DbContext, IDataContextAsync
    {
        public PokedexDatabaseCtx()
        {
        }

        public PokedexDatabaseCtx(DbContextOptions<PokedexDatabaseCtx> options)
            : base(options)
        {
        }

        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.Property(e => e.PokemonName).IsUnicode(false);

                entity.Property(e => e.Types).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey(s => s.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public async Task<int> SaveChangesAsync()
        {
            SyncObjectsStatePreCommit();
            int changesAsync = -1;

            try
            {
                changesAsync = await base.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }
    }
}