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
    
    public partial class CustomerAddress
    {
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
