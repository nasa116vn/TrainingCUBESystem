//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SVR_DEVICE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SVR_DEVICE()
        {
            this.SVR_VIEW = new HashSet<SVR_VIEW>();
        }
    
        public string IMEI { get; set; }
        public string TOKEN { get; set; }
        public string MODEL { get; set; }
        public string MAKER { get; set; }
        public Nullable<System.DateTime> INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public Nullable<int> DEL_FLAG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SVR_VIEW> SVR_VIEW { get; set; }
    }
}
