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
    using System;
    using System.Collections.Generic;
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.CHINHANHs = new HashSet<CHINHANH>();
            this.HOADONs = new HashSet<HOADON>();
            this.KHACHHANGs = new HashSet<KHACHHANG>();
            this.KHOes = new HashSet<KHO>();
            this.PHIEUNHAPs = new HashSet<PHIEUNHAP>();
        }
    
        public int MANV { get; set; }
        public string HOTEN { get; set; }
        public Nullable<System.DateTime> NTNS { get; set; }
        public string SDT { get; set; }
        public string DIACHI { get; set; }
        public Nullable<double> HESOLUONG { get; set; }
        public Nullable<double> LUONGCB { get; set; }
        public Nullable<int> MACN { get; set; }
        public Nullable<bool> VAITRO { get; set; }
        public string EMAIL { get; set; }
        public Nullable<bool> DELETED { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHINHANH> CHINHANHs { get; set; }
        public virtual CHINHANH CHINHANH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHACHHANG> KHACHHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHO> KHOes { get; set; }
        public virtual LOGIN LOGIN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
    }
}
