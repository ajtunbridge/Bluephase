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
    
    public partial class SalesOrderItemBatch
    {
        public int SalesOrderItemBatchId { get; set; }
        public int SalesOrderItemId { get; set; }
        public int BatchQuantity { get; set; }
        public System.DateTime RequestedDeliveryDate { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual SalesOrderItem SalesOrderItem { get; set; }
    }
}
