﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Агентство_Недвижимости
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Real_Estate_AgencyEntities : DbContext
    {
        public Real_Estate_AgencyEntities()
            : base("name=Real_Estate_AgencyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<House_Demands> House_Demands { get; set; }
        public virtual DbSet<Houses> Houses { get; set; }
        public virtual DbSet<Land_Demands> Land_Demands { get; set; }
        public virtual DbSet<Lands> Lands { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
