using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ChiTietHDController : Controller
    {
        private CS4PEntities db = new CS4PEntities();

        // GET: /ChiTietHD/
        public PartialViewResult Index()
        {
            if (Session["CTHoaDon"] == null)
                Session["CTHoaDon"] = new List<ChiTietHD>();
            return PartialView(Session["CTHoaDon"]);
        }

        // GET: /ChiTietHD/Details/5
        public int DonGiaBan(int SP_id)
        {
            return db.BangSanPhams.Find(SP_id).GiaBan;
        }

        // GET: /ChiTietHD/Create
        public PartialViewResult Create()
        {
            ViewBag.SP_id = new SelectList(db.BangSanPhams, "id", "TenSP");
            var model = new ChiTietHD();
            model.HD_id = 0;
            model.SoLuong = 1;
            return PartialView(model);
        }

        // POST: /ChiTietHD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(ChiTietHD model)
        {
            if (ModelState.IsValid)
            {
                model.id = Environment.TickCount;
                model.BangSanPham = db.BangSanPhams.Find(model.SP_id);
                var CTHoaDon = Session["CTHoaDon"] as List<ChiTietHD>;
                if (CTHoaDon == null)
                    CTHoaDon = new List<ChiTietHD>();
                CTHoaDon.Add(model);
                Session["CTHoaDon"] = CTHoaDon;
                return RedirectToAction("Create", "BangHoaDon");
            }
            
            ViewBag.SP_id = new SelectList(db.BangSanPhams, "id", "TenSP", model.SP_id);
            return View("Create", model);
        }

        // GET: /ChiTietHD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHD chitiethd = db.ChiTietHDs.Find(id);
            if (chitiethd == null)
            {
                return HttpNotFound();
            }
            ViewBag.HD_id = new SelectList(db.BangHoaDons, "id", "TenKhachHang", chitiethd.HD_id);
            ViewBag.SP_id = new SelectList(db.BangSanPhams, "id", "MaSP", chitiethd.SP_id);
            return View(chitiethd);
        }

        // POST: /ChiTietHD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,HD_id,SP_id,DonGiaBan,SoLuong")] ChiTietHD chitiethd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitiethd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HD_id = new SelectList(db.BangHoaDons, "id", "TenKhachHang", chitiethd.HD_id);
            ViewBag.SP_id = new SelectList(db.BangSanPhams, "id", "MaSP", chitiethd.SP_id);
            return View(chitiethd);
        }

        // GET: /ChiTietHD/Delete/5
        public ActionResult Delete(int id)
        {
            var CTHoaDon = Session["CTHoaDon"] as List<ChiTietHD>;
            CTHoaDon = CTHoaDon.Except(CTHoaDon.Where(c => c.id == id)).ToList();
            Session["CTHoaDon"] = CTHoaDon;
            return RedirectToAction("Create", "BangHoaDon");
        }

        // POST: /ChiTietHD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHD chitiethd = db.ChiTietHDs.Find(id);
            db.ChiTietHDs.Remove(chitiethd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
