﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace A4AAssessment.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class A4AAssessmentEntities1 : DbContext
    {
        public A4AAssessmentEntities1()
            : base("name=A4AAssessmentEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BusinessEntity> BusinessEntities { get; set; }
        public virtual DbSet<MarkUpPlan> MarkUpPlans { get; set; }
    }
}
