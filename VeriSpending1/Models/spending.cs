//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VeriSpending1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class spending
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public spending()
        {
            this.declineSpending = new HashSet<declineSpending>();
        }
    
        public int userId { get; set; }
        public int spendingId { get; set; }
        public string spendingTitle { get; set; }
        public string spendingDescription { get; set; }
        public System.DateTime spendingDate { get; set; }
        public decimal spendingAmount { get; set; }
        public string approval { get; set; }
    
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<declineSpending> declineSpending { get; set; }
    }
}