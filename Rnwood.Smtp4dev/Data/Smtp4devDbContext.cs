using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;
using Rnwood.Smtp4dev.DbModel;


namespace Rnwood.Smtp4dev.Data
{

public partial class Smtp4devDbContext : DbContext
{
    public Smtp4devDbContext()
    {
    }

    public Smtp4devDbContext(DbContextOptions<Smtp4devDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ImapState> ImapState { get; set; }

    public virtual DbSet<Mailbox> Mailboxes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageRelay> MessageRelays { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=15432;Database=smtp4dev;User Id=root;Password=Jie9toof");
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UtcDateTimeValueConverter.Apply(modelBuilder);

            modelBuilder.Entity<MessageRelay>()
            .HasOne(r => r.Message)
            .WithMany(x => x.Relays)
            .HasForeignKey(x => x.MessageId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            modelBuilder.Entity<Mailbox>()
                .HasMany<Message>()
                    .WithOne(m => m.Mailbox)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne<Session>(x => x.Session)
               .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

}
