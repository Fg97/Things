﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DIC : DbContext
    {
        public DIC()
            : base("name=DIC")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<communication> communication { get; set; }
        public virtual DbSet<companies> companies { get; set; }
        public virtual DbSet<fields> fields { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
