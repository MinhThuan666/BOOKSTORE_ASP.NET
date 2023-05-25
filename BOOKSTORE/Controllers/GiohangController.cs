using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOOKSTORE.Models;

namespace BOOKSTORE.Controllers
{
    public class GiohangController : Controller
    {
        // tao doi tuong data chua duw lieu tu model dbBansach da tao:
        QLBansachEntities1 data = new QLBansachEntities1();
        //lay gio hang

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"]=lstGiohang;
            }
            return lstGiohang;
        }

        // THEM VAO GIO? HANG`
        public ActionResult ThemGiohang(int iMasach, string strURL)
        {
            //lay ra Session gio hang
            List<Giohang> lstGiohang = Laygiohang();
            //kiem tra sach nay ton tai trong Session["Giohang"] chua?
            Giohang sanpham = lstGiohang.Find(n => n.iMasach == iMasach);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMasach);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }

        }

        //Tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }

        //tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }

        //Xay dung trang Gio hang
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        //Tao partial view de hien thi thong tin gio hang
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        // Xóa giỏ hàng
        public ActionResult XoaGiohang(int iMaSP)
        {
            //lấy giỏ hàng từ Session
            List<Giohang> lstGiohang = Laygiohang();
            // Kiểm tra sách đã có trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            // nếu tồn tại cho sửa số lượng
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMasach == iMaSP);
                return RedirectToAction("Giohang");
            }    
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            return RedirectToAction("Giohang");
        }

        //cập nhật giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            //Lay giỏ hàng từ Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiểm tra sách đã có trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            // nếu tồn tại cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        // Hiển thị View DatHang de cap nhat cac don hàng
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            var Ngaygiao = String.Format("{0:MM/dd/yyyy }", collection["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(Ngaygiao);
            ddh.Trinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            data.DONDATHANGs.Add(ddh);
            data.SaveChanges();
            //Theem chi tiet don hang
            foreach (var item in gh)
            {
                CTDATHANG ctdh = new CTDATHANG();
                ctdh.SoDH = ddh.SoDH;
                ctdh.SoDH = item.iMasach;
                ctdh.Soluong = item.iSoluong;
                ctdh.Dongia = (decimal)item.dDongia;
                data.CTDATHANGs.Add(ctdh);
            }
            Session["Giohang"] = null;
            data.SaveChanges();
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Thongtindathang()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}