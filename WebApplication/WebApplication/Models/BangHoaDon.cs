//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BangHoaDon
    {
        public BangHoaDon()
        {
            this.ChiTietHDs = new HashSet<ChiTietHD>();
        }
    
        public int id { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
    
        public virtual ICollection<ChiTietHD> ChiTietHDs { get; set; }
    }
}
