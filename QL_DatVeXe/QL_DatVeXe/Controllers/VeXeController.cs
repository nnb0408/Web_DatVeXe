using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_DatVeXe.Models;

namespace QL_DatVeXe.Controllers
{
    public class VeXeController : Controller
    {
        QL_DATVEXEDataContext db = new QL_DATVEXEDataContext();
        // GET: VeXe
        public ActionResult ShowAllVeXe(int? page)
        {
            Session["TB"] = "";

            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                Session["user"] = string.Empty;

            Session["favorite"] = db.VEXEYEUTHICHes.Where(t => t.KHACHHANG.TENKH == user).Count();

            int itemsPerPage = 9;
            int pageNumber = (page ?? 1);

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

            var listVeXe = db.VEXEs.OrderBy(t => t.TENVE).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            ViewBag.TotalPages = Math.Ceiling((double)db.VEXEs.Count() / itemsPerPage);
            ViewBag.CurrentPage = pageNumber;
            return View(listVeXe);
        }

        public ActionResult TimVe(int? page, string diemdon, string diemden, DateTime? ngaydi, DateTime? ngayve)
        {
            Session["TB"] = "";

            var listVeXe = new List<VEXE>();
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                Session["user"] = string.Empty;

            int itemsPerPage = 9;
            int pageNumber = (page ?? 1);

            if (diemdon == null && diemden == null && ngaydi == null && ngayve == null)
                listVeXe = db.VEXEs.OrderBy(t => t.TENVE).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
            {
                listVeXe = db.VEXEs.OrderBy(t => t.TENVE).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDON == diemdon && t.DIEMDEN == diemden && t.NGAYDI == ngaydi && t.NGAYVE == ngayve).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDON == diemdon && t.DIEMDEN == diemden && t.NGAYDI == ngaydi).ToList();
            }
            else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDON == diemdon && t.DIEMDEN == diemden).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDEN == diemden).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDON == diemdon).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.NGAYDI == ngaydi).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve != null)
            {
                listVeXe = db.VEXEs.Where(t => t.NGAYVE == ngayve).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
            {
                listVeXe = db.VEXEs.Where(t => t.NGAYDI == ngaydi && t.NGAYVE == ngayve).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDON == diemdon && t.NGAYDI == ngaydi && t.NGAYVE == ngayve).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDON == diemdon && t.NGAYDI == ngaydi).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDEN == diemden && t.NGAYDI == ngaydi && t.NGAYVE == ngayve).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
            {
                listVeXe = db.VEXEs.Where(t => t.DIEMDEN == diemden && t.NGAYDI == ngaydi).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            if (listVeXe.Count == 0)
            {
                if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Bạn chưa chọn mục tiêu để tìm kiếm";
                else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Không có vé xe đi từ " + diemdon + " đến " + diemden + " ngày " + ngaydi + " - " + ngayve;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Không có vé xe đi từ " + diemdon + " đến " + diemden + " ngày " + ngaydi;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Không có vé xe đi từ " + diemdon + " đến " + diemden;
                else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Không có vé xe đến " + diemden;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Không có vé xe đi từ " + diemdon;
                else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Không có vé xe đi từ ngày " + ngaydi;
                else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve != null)
                    Session["TB"] = "Không có vé xe đi về ngày " + ngayve;
                else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Không có vé xe đi từ ngày " + ngaydi + " - " + ngayve;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Không có vé xe đi từ " + diemdon + " ngày " + ngaydi + " - " + ngayve;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Không có vé xe đi từ " + diemdon + " ngày " + ngaydi;
                else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Không có vé xe đến " + diemden + " ngày " + ngaydi + " - " + ngayve;
                else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Không có vé xe đến " + diemden + " ngày " + ngaydi;
            }
            else
            {
                if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Bạn chưa chọn mục tiêu để tìm kiếm";
                else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ " + diemdon + " đến " + diemden + " ngày " + ngaydi + " - " + ngayve;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ " + diemdon + " đến " + diemden + " ngày " + ngaydi;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ " + diemdon + " đến " + diemden;
                else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đến " + diemden;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ " + diemdon;
                else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ ngày " + ngaydi;
                else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi == null && ngayve != null)
                    Session["TB"] = "Bạn đang tìm vé xe đi về ngày " + ngayve;
                else if (diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ ngày " + ngaydi + " - " + ngayve;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ " + diemdon + " ngày " + ngaydi + " - " + ngayve;
                else if (!diemdon.Equals("Chọn nơi xuất phát") && diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đi từ " + diemdon + " ngày " + ngaydi;
                else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve != null)
                    Session["TB"] = "Bạn đang tìm vé xe đến " + diemden + " ngày " + ngaydi + " - " + ngayve;
                else if (diemdon.Equals("Chọn nơi xuất phát") && !diemden.Equals("Chọn nơi đến") && ngaydi != null && ngayve == null)
                    Session["TB"] = "Bạn đang tìm vé xe đến " + diemden + " ngày " + ngaydi;
            }
            ViewBag.TotalPages = Math.Ceiling((double)listVeXe.Count() / itemsPerPage);
            ViewBag.CurrentPage = pageNumber;
            return View(listVeXe);
        }

        public ActionResult XemChiTietVeXe(int mave, string diemden)
        {
            var vexe = db.VEXEs.SingleOrDefault(t => t.MAVE == mave);
            var listvexe = db.VEXEs.Where(t => t.DIEMDEN == diemden).ToList();
            var listdanhgia = db.DANHGIAs.Where(t => t.MAVE == mave);
            VeXeViewModel vexeviewmodel = new VeXeViewModel
            {
                VeXe = vexe,
                ListVeXe = listvexe,
                ListDanhGia = listdanhgia
            };
            return View(vexeviewmodel);
        }

        public ActionResult DanhGia(DANHGIA dg, string tieude, string comment, int sao, int maVe, string diemDen)
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
            {
                Session["user"] = string.Empty;
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            var anh = "";
            var kh = db.KHACHHANGs.SingleOrDefault(t => t.TENKH == user);
            var vexe = db.VEXEs.SingleOrDefault(t => t.MAVE == maVe);
            var danhgia = db.DANHGIAs.SingleOrDefault(t => t.MAKH == kh.MAKH && t.MAVE == maVe);

            if (kh.GIOITINH == "Nam")
                anh = "review_2.jpg";
            else
                anh = "review_1.jpg";

            if (danhgia == null)
            {
                dg.MAKH = kh.MAKH;
                dg.MAVE = vexe.MAVE;
                dg.TIEUDE = tieude;
                dg.NOIDUNG = comment;
                dg.SOSAO = sao;
                dg.HINHANH = anh;
                dg.NGAYDG = DateTime.Now;
                db.DANHGIAs.InsertOnSubmit(dg);
            }
            else
                Session["TB"] = "Bạn đã đánh giá vé xe này";
            db.SubmitChanges();
            return RedirectToAction("XemChiTietVeXe", "VeXe", new { mave = maVe, diemden = diemDen });
        }

        public ActionResult DatVe(int mave)
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                return RedirectToAction("DangNhap", "NguoiDung");

            var gg = Session["giamgia"] as string;
            if (string.IsNullOrEmpty(gg))
                Session["giamgia"] = 0;

            var vexe = db.VEXEs.SingleOrDefault(t => t.MAVE.Equals(mave));
            var kh = db.KHACHHANGs.SingleOrDefault(k => k.TENKH.Equals(user));

            var giamgia = Convert.ToInt32(Session["giamgia"]);
            var tongtien = vexe.GIAVE - giamgia;
            Session["tongcong"] = tongtien;

            User_DatHang user_DatHang = new User_DatHang
            {
                VeXe = vexe,
                KhachHang = kh
            };
            return View(user_DatHang);
        }

        public ActionResult ThongBaoDatVe()
        {
            return View();
        }

        public ActionResult XacNhanDatVe(HOADON hd, CHITIETHOADON cthd, int mave, string ten, string diachi, string sdt, string ghe, string email, string ghichu)
        {
            var user = Session["user"] as string;
            if (string.IsNullOrEmpty(user))
                Session["user"] = string.Empty;

            var gg = Session["giamgia"] as string;
            if (string.IsNullOrEmpty(gg))
                Session["giamgia"] = 0;

            var magiamgia = Session["magiamgia"] as string;
            if (string.IsNullOrEmpty(gg))
            {
                Session["magiamgia"] = "Không có mã giảm giá";
                magiamgia = Session["magiamgia"] as string;
            }

            var giamgia = Convert.ToInt32(Session["giamgia"]);

            var kh = db.KHACHHANGs.SingleOrDefault(k => k.TENKH.Equals(user));
            var hoadon = db.HOADONs.Where(t => t.KHACHHANG.TENKH == user).ToList();
            var vexe = db.VEXEs.SingleOrDefault(t => t.MAVE == mave);

            var sohoadon = 1;

            if (hoadon.Count == 0)
            {
                hd.MAKH = kh.MAKH;
                hd.NGAYLAP = DateTime.Now;
                hd.SOHOADON = 1;
                hd.TENNGUOIDAT = ten;
                hd.DIACHI = diachi;
                hd.SDT = sdt;
                hd.EMAIL = email;
                hd.SOGHE = Convert.ToInt32(ghe);
                hd.THANHTIEN = vexe.GIAVE * Convert.ToInt32(ghe);
                hd.MAKHUYENMAI = magiamgia;
                hd.GHICHU = ghichu;
                hd.TRANGTHAI = false;
                db.HOADONs.InsertOnSubmit(hd);
                db.SubmitChanges();
            }
            else
            {
                var n = hoadon.Count;
                for (int i = 0; i < n; i++)
                {
                    if (hoadon[i].SOHOADON == sohoadon)
                    {
                        sohoadon = hoadon[i].SOHOADON + 1;
                    }
                    sohoadon = hoadon[i].SOHOADON + 1;
                }
                hd.MAKH = kh.MAKH;
                hd.NGAYLAP = DateTime.Now;
                hd.SOHOADON = sohoadon;
                hd.TENNGUOIDAT = ten;
                hd.DIACHI = diachi;
                hd.SDT = sdt;
                hd.EMAIL = email;
                hd.SOGHE = Convert.ToInt32(ghe);
                hd.THANHTIEN = vexe.GIAVE * Convert.ToInt32(ghe);
                hd.MAKHUYENMAI = magiamgia;
                hd.GHICHU = ghichu;
                hd.TRANGTHAI = false;
                db.HOADONs.InsertOnSubmit(hd);
                db.SubmitChanges();
            }
            var hoadon2 = db.HOADONs.SingleOrDefault(t => t.KHACHHANG.TENKH == user && t.SOHOADON == sohoadon);

            if (hoadon2 != null)
            {
                cthd.MAHD = hoadon2.MAHD;
                cthd.MAVE = mave;
                cthd.SOGHE = Convert.ToInt32(ghe);
                Session["thongbao"] = "Bạn đã đặt vé thành công!";
                db.CHITIETHOADONs.InsertOnSubmit(cthd);
                db.SubmitChanges();
            }
            else
            {
                Session["thongbao"] = "Đặt vé thất bại!";
            }
            db.SubmitChanges();
            return RedirectToAction("ThongBaoDatVe", "VeXe");
        }
    }
}