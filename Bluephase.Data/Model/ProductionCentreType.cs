//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bluephase.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionCentreType
    {
        public ProductionCentreType()
        {
            this.ProductionCentres = new HashSet<ProductionCentre>();
        }
    
        public int ProductionCentreTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> DefaultHourlyRate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<ProductionCentre> ProductionCentres { get; set; }
    }
}
