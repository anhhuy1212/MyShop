//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POSApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase()
        {
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
        }
    
        public int Purchase_ID { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public Nullable<int> Status { get; set; }
        public string Customer_Tel { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual PurchaseStatusEnum PurchaseStatusEnum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}