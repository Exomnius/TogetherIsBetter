﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace TogetherIsBetter
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class TIB_Model : DbContext
{
    public TIB_Model()
        : base("name=TIB_Model")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }

    public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }

    public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }

    public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }

    public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }

    public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }

    public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }

    public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }

    public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }

    public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }

    public virtual DbSet<Company> Company { get; set; }

    public virtual DbSet<Contract> Contract { get; set; }

    public virtual DbSet<ContractFormula> ContractFormula { get; set; }

    public virtual DbSet<Location> Location { get; set; }

    public virtual DbSet<Reservation> Reservation { get; set; }

}

}

