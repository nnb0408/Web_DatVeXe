using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_DatVeXe.Models
{
    public class VeXeViewModel
    {
        public VEXE VeXe { get; set; }
        public IEnumerable<VEXE> ListVeXe { get; set; }
        public IEnumerable<DANHGIA> ListDanhGia { get; set; }
    }
}