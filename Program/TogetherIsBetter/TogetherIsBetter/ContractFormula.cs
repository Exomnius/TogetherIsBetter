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
    using System.Collections.Generic;
    
    public partial class ContractFormula
    {
        public ContractFormula()
        {
            this.Contract = new HashSet<Contract>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> MaxUsageHoursPerPeriod { get; set; }
        public Nullable<int> PeriodInMonths { get; set; }
        public Nullable<int> NoticePeriodInMonths { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual ICollection<Contract> Contract { get; set; }
    }
}
