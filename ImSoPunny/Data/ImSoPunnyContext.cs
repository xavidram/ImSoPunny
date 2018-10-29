using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImSoPunny.Models
{
    public class ImSoPunnyContext : IdentityDbContext
    {
        public ImSoPunnyContext (DbContextOptions<ImSoPunnyContext> options)
            : base(options)
        {
        }

        public DbSet<ImSoPunny.Models.Pun> Pun { get; set; }

        public DbSet<ImSoPunny.Models.Tag> Tag { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// One to Many (User -> Puns)
			builder.Entity<Pun>()
				.HasOne<AppUser>(p => p.User)
				.WithMany(u => u.Puns)
				.HasForeignKey(k => k.UserId);

			// Many to Many, (Puns <-> Tags)
			builder.Entity<PunTag>().HasKey(pt => new { pt.PunId, pt.TagId });
			builder.Entity<PunTag>()
				.HasOne<Pun>(pt => pt.Pun)
				.WithMany(p => p.PunTags)
				.HasForeignKey(pt => pt.PunId);

			builder.Entity<PunTag>()
				.HasOne<Tag>(pt => pt.Tag)
				.WithMany(t => t.PunTags)
				.HasForeignKey(pt => pt.TagId);
		}
	}
}
