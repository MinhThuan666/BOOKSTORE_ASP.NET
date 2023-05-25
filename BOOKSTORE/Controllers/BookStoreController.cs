using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOOKSTORE.Models;

using PagedList;
using PagedList.Mvc;
namespace BOOKSTORE.Controllers
{
    public class BookStoreController : Controller
    {
        //tao doi tuong de chua du lieu tu model1 dataQLBansach da tao:
        QLBansachEntities1 data = new QLBansachEntities1();
        // ham lay n quyen sach moi
        private List<SACH> Lasachmoi(int count)
        {
            //sap xep sach theo ngay cap nhat, sau do lay top @count
            return data.SACHes .OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();   
        }
        public ActionResult Index(int ? page)
        {
            // tao bien quy dinh so sp tren moi trang 
            int pageSize=4;
            // tao bien so trang
            int pageNum = (page ?? 1);
            
            // lay top 5 album ban chay nhat
            var sachmoi = Lasachmoi(15);
            return View(sachmoi.ToPagedList(pageNum, pageSize));
        }



        /*//Tạo 1 đối tượng chứa toàn bộ CSDL từ dbQLBansach
        QLBansachEntities data = new QLBansachEntities();

        private List<SACH> Laysachmoi(int count) =>
            //Sắp xếp giảm dần theo Ngaycapnhat, lấy count đống dấu
            data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        public ActionResult Index()
        {
            //Lấy 8 quyển sách mới nhất
            var sachmoi = Laysachmoi(8);
            return View(sachmoi);
        }*/

        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        public ActionResult Nhaxuatban()
        {
            var chude = from cd in data.NHAXUATBANs select cd;
            return PartialView(chude);
        }

        public ActionResult SPTheochude (int id)
        {
            var sach = from s in data.SACHes where s.MaCD==id select s;
            return View(sach);
        }

        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }

        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}