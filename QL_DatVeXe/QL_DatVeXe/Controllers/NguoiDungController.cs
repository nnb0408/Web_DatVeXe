using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_DatVeXe.Models;

namespace QL_DatVeXe.Controllers
{
    public class NguoiDungController : Controller
    {
        QL_DATVEXEDataContext db = new QL_DATVEXEDataContext();
        // GET: NguoiDung
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh, string fullname, string username, DateTime date, string password, string gender, string repassword, string address, string email, string phone)
        {
            if (password == repassword)
            {
                KHACHHANG check = db.KHACHHANGs.SingleOrDefault(n => n.TAIKHOAN.Equals(username));
                if (check == null)
                {
                    kh.TENKH = fullname;
                    kh.GIOITINH = gender;
                    kh.NGAYSINH = date;
                    kh.DIACHI = address;
                    kh.SDT = phone;
                    kh.TAIKHOAN = username;
                    kh.MATKHAU = password;
                    kh.EMAIL = email;
                    kh.TRANGTHAI = "Không khóa";
                    db.KHACHHANGs.InsertOnSubmit(kh);
                    db.SubmitChanges();
                    ViewBag.TB = "Đăng ký thành công!";
                }
                else
                    ViewBag.TB = "Username " +username+ " đã được sử dụng!";
            }
            else
                ViewBag.TB = "Nhập lại mật khẩu không chính xác, vui lòng nhập lại!";
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TAIKHOAN.Equals(username) && n.MATKHAU.Equals(password));
                if (kh != null)
                {
                    Session["user"] = kh.TENKH;
                    //TempData["Notification"] = "Đăng nhập thành công";
                    return RedirectToAction("ShowAllVeXe", "VeXe");
                }
                else
                {
                    ViewBag.TB = "Tài khoản hoặc mật khẩu không chính xác, vui lòng nhập lại!";
                }
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["user"] = string.Empty;
            return RedirectToAction("ShowAllVeXe", "VeXe");
        }
    }
}