//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoveTap.Model
{
    using LoveTap.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class SANPHAM : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.BAOHANHs = new HashSet<BAOHANH>();
            this.CTHDs = new HashSet<CTHD>();
            this.TONKHOes = new HashSet<TONKHO>();
        }
    
        public string MASP { get; set; }
        public string TEN { get; set; }
        public Nullable<double> GIA { get; set; }
        public string NUOCSX { get; set; }
        public string CHITIET { get; set; }
        public string MADM { get; set; }
        public Nullable<bool> DELETED { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAOHANH> BAOHANHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        public virtual DANHMUC DANHMUC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TONKHO> TONKHOes { get; set; }
    }
}
