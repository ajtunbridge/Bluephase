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
    
    public partial class ContactDetail
    {
        public ContactDetail()
        {
            this.PersonContactDetails = new HashSet<PersonContactDetail>();
        }
    
        public int ContactDetailId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<PersonContactDetail> PersonContactDetails { get; set; }
    }
}
