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
    
    public partial class MaterialForm
    {
        public MaterialForm()
        {
            this.MaterialForms1 = new HashSet<MaterialForm>();
            this.Materials = new HashSet<Material>();
        }
    
        public int MaterialFormId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentMaterialFormId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<MaterialForm> MaterialForms1 { get; set; }
        public virtual MaterialForm MaterialForm1 { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
