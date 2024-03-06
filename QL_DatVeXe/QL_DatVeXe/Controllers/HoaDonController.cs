using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_DatVeXe.Models;
namespace QL_DatVeXe.Controllers
{
    public class HoaDonController : Controller
    {
        QL_DATVEXEDataContext db = new QL_DATVEXEDataContext();
        // GET: HoaDon
        public ActionResult HoaDonChoXacNhan()
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                Session["user"] = string.Empty;

            ChoXacNhan cxn = new ChoXacNhan();
            cxn.LstCTHD = new List<CHITIETHOADON>();
            cxn.lstHoaDon = new List<HOADON>();

            var lsthoadon = db.HOADONs.Where(t => t.KHACHHANG.TENKH == user && t.TRANGTHAI == false).ToList();
            cxn.lstHoaDon = lsthoadon;
            if (lsthoadon.Count > 0)
            {
                for (int i = 0; i < lsthoadon.Count; i++)
                {
                    var lstcthd = db.CHITIETHOADONs.Where(t => t.MAHD == lsthoadon[i].MAHD).ToList();
                    for (int j = 0; j < lstcthd.Count; j++)
                    {
                        var cthd = db.CHITIETHOADONs.SingleOrDefault(t => t.MAVE == lstcthd[j].MAVE && t.MAHD == lstcthd[j].MAHD);
                        cxn.LstCTHD.Add(cthd);
                    }
                }
            }
            return View(cxn);
        }

        public ActionResult HoaDonDaXacNhan()
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                Session["user"] = string.Empty;

            ChoXacNhan cxn = new ChoXacNhan();
            cxn.LstCTHD = new List<CHITIETHOADON>();
            cxn.lstHoaDon = new List<HOADON>();

            var lsthoadon = db.HOADONs.Where(t => t.KHACHHANG.TENKH == user && t.TRANGTHAI == true).ToList();
            cxn.lstHoaDon = lsthoadon;
            if (lsthoadon.Count > 0)
            {
                for (int i = 0; i < lsthoadon.Count; i++)
                {
                    var lstcthd = db.CHITIETHOADONs.Where(t => t.MAHD == lsthoadon[i].MAHD).ToList();
                    for (int j = 0; j < lstcthd.Count; j++)
                    {
                        var cthd = db.CHITIETHOADONs.SingleOrDefault(t => t.MAVE == lstcthd[j].MAVE && t.MAHD == lstcthd[j].MAHD);
                        cxn.LstCTHD.Add(cthd);
                    }
                }
            }
            return View(cxn);
        }
    }
}