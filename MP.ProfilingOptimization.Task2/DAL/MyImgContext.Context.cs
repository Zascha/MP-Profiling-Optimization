namespace ProfileSample.DAL
{
    using System.Data.Entity;
    
    public class ProfileSampleEntities : DbContext
    {
        public ProfileSampleEntities()
            : base("name=ProfileSampleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    
        public virtual DbSet<ImgSource> ImgSources { get; set; }
    }
}
