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
    
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public string HOTEN { get; set; }
        public string SDT { get; set; }
        public string DIACHI { get; set; }
        public Nullable<System.DateTime> NGDK { get; set; }
        public Nullable<double> DOANHSO { get; set; }
        public string MANV { get; set; }
        public Nullable<System.DateTime> NGSINH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
