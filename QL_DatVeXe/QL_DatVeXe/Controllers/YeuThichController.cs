using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_DatVeXe.Models;

namespace QL_DatVeXe.Controllers
{
    public class YeuThichController : Controller
    {
        QL_DATVEXEDataContext db = new QL_DATVEXEDataContext();
        // GET: YeuThich
        public ActionResult VeXeYeuThich()
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                Session["user"] = string.Empty;

            Session["favorite"] = db.VEXEYEUTHICHes.Where(t => t.KHACHHANG.TENKH == user).Count();

            var setve = db.VEXEs.OrderBy(t => t.TENVE).ToList();
            if (setve.Count > 0)
            {
                for (int i = 0; i < setve.Count; i++)
                {
                    setve[i].YEUTHICH = false;
                    db.SubmitChanges();
                }
            }

            if (!string.IsNullOrEmpty(user))
            {
                var kh = db.KHACHHANGs.SingleOrDefault(t => t.TENKH == user);
                var veyeuthich = db.VEXEYEUTHICHes.Where(t => t.MAKH == kh.MAKH).ToList();

                if (veyeuthich.Count > 0)
                {
                    for (int i = 0; i < setve.Count; i++)
                    {
                        for (int j = 0; j < veyeuthich.Count; j++)
                        {
                            if (setve[i].MAVE == veyeuthich[j].MAVE)
                            {
                                setve[i].YEUTHICH = true;
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }

            var favorite = db.VEXEYEUTHICHes.Where(t => t.KHACHHANG.TENKH == user).ToList();
            if (favorite.Count == 0)
                return RedirectToAction("YeuThichRong", "YeuThich");
            return View(favorite);
        }

        public ActionResult YeuThichRong()
        {
            return View();
        }

        public ActionResult ThemVeXeYeuThich(VEXEYEUTHICH spyt, int mave)
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                return RedirectToAction("DangNhap", "NguoiDung");

            var sp = db.VEXEYEUTHICHes.Where(n => n.KHACHHANG.TENKH == user).Where(m => m.MAVE == mave).ToList();
            var kh = db.KHACHHANGs.SingleOrDefault(k => k.TENKH.Equals(user));
            if (sp.Count == 0)
            {
                spyt.MAKH = kh.MAKH;
                spyt.MAVE = mave;
                db.VEXEYEUTHICHes.InsertOnSubmit(spyt);
                db.SubmitChanges();
                ViewBag.TB = "Đã thêm vé xe vào danh sách yêu thích!";
            }
            else
                ViewBag.TB = "Vé xe đã được yêu thích!";

            return RedirectToAction("VeXeYeuThich", "YeuThich");
        }

        public ActionResult XoaVeXeYeuThich(int mave)
        {
            var user = Session["user"] as string;

            VEXEYEUTHICH sp = db.VEXEYEUTHICHes.Where(n => n.KHACHHANG.TENKH == user).Where(m => m.MAVE == mave).Single();
            var favorite = db.VEXEYEUTHICHes.Where(t => t.KHACHHANG.TENKH == user).ToList();
            if (sp != null)
            {
                db.VEXEYEUTHICHes.DeleteOnSubmit(sp);
                db.SubmitChanges();
                ViewBag.TB = "Đã xóa vé xe khỏi danh sách yêu thích!";
                return RedirectToAction("VeXeYeuThich", "YeuThich");
            }
            else
                ViewBag.TB = "Xóa thất bại!";
            if(favorite.Count == 0)
                return RedirectToAction("YeuThichRong", "YeuThich");
            return RedirectToAction("VeXeYeuThich", "YeuThich");
        }
    }
}