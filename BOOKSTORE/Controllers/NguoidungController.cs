using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOOKSTORE.Models;

namespace BOOKSTORE.Controllers
{
    public class NguoidungController : Controller
    {
        QLBansachEntities1 data = new QLBansachEntities1();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]*/

        /*[ValidateAntiForgeryToken]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {

                //them đữ liệu vào csdl
                data.KHACHHANGs.Add(kh);
                //luu vao csdl
                data.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]

        public ActionResult DangNhap(FormCollection collection)
        {
            //String Tendn = collection["TenDN"].ToString();
            //String MatKhau = collection["Matkhau"].ToString();
            var Tendn = collection["TenDN"].ToString();
            var MatKhau = collection["Matkhau"].ToString();


            if (String.IsNullOrEmpty(Tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(MatKhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.FirstOrDefault(n => n.Taikhoan == Tendn && n.Matkhau == MatKhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "BookStore");

                }
                else
                {
                    ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không chính xác!";

                }
                return View();
            }
            return View();

        }*/


        public ActionResult Dangky()
        {
            return View();
        }
        //POST: Hàm đăng ký (post) nhập liệu từ trang Dangky và thức hiện việc thao tác dữ liệu
        [HttpPost]
        
        public ActionResult Dangky(FormCollection collection,KHACHHANG kh)
        {
            //gán các giá trị người dùng nhập liệu cho các biến
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }    
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }    
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }    
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }    
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập điện thoại";
            }  
            else
            {
                // gán giá trị cho đối tượng gọi mới (kh)
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;   
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai; 
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                data.KHACHHANGs.Add(kh);   
                data.SaveChanges();
                return RedirectToAction("Dangnhap");

            }
            return this.Dangky();
        }

        public ActionResult Dangnhap(FormCollection collection)
        {
            // gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên người dùng";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // gán giá trị cho đối tượng được tạo mới (kh)
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    //ViewBag.Thongbao = " chúc mừng đăng nhập thành công"
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "BookStore");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }  
            return View();
        }
    }
}