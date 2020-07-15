﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF_CRUD.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CellableEntities : DbContext
    {
        public CellableEntities()
            : base("name=CellableEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<PhoneVersion> PhoneVersions { get; set; }
        public virtual DbSet<PossibleDefect> PossibleDefects { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<StorageCapacity> StorageCapacities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<UserPhone> UserPhones { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Promo> Promos { get; set; }
        public virtual DbSet<DefectGroup> DefectGroups { get; set; }
        public virtual DbSet<VersionCapacity> VersionCapacities { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<SlideShow> SlideShows { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<VersionCarrier> VersionCarriers { get; set; }
        public virtual DbSet<DefectTemplate> DefectTemplates { get; set; }
        public virtual DbSet<OrderQuestion> OrderQuestions { get; set; }
    }
}
