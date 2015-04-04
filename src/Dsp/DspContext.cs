using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{

    public class DspContext : DbContext
    {
        static DspContext()
        {
            Database.SetInitializer<DspContext>(null);
        }

        public DbSet<account> accounts { get; set; }
        public DbSet<account_session> account_sessions { get; set; }
        public DbSet<auction_house> auction_houses { get; set; }
        public DbSet<audit_chat> audit_chats { get; set; }
        public DbSet<auto_translate_item> auto_translate_items { get; set; }
        public DbSet<auto_translate_string> auto_translate_strings { get; set; }
        public DbSet<character_exp> character_exps { get; set; }
        public DbSet<character_job> character_jobs { get; set; }
        public DbSet<character_stats> character_stats { get; set; }
        public DbSet<character> characters { get; set; }
        public DbSet<character_inventory> character_inventories { get; set; }
        public DbSet<item_basic> item_basics { get; set; }
        public DbSet<zone_settings> zone_settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(DspContext).Assembly);
        }
    }
}
