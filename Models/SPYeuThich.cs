//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanGiay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPYeuThich
    {
        public string maSP { get; set; }
        public string maUser { get; set; }
        public string maSPYT { get; set; }
    
        public virtual SanPham SanPham { get; set; }
        public virtual User User { get; set; }
    }
}
