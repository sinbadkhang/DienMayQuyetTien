using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BangHoaDonController : Controller
    {
        private CS4PEntities db = new CS4PEntities();

        // GET: /BangHoaDon/
        public ActionResult Index()
        {
            return View(db.BangHoaDons.ToList());
        }

        // GET: /BangHoaDon/Create
        public ActionResult Create()
        {
            return View(Session["BangHoaDon"]);
        }

        // POST: /BangHoaDon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BangHoaDon model)
        {
            if (ModelState.IsValid)
            {
                Session["BangHoaDon"] = model;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2()
        {
            using (var scope = new TransactionScope())
            try
            {
                var bangHoaDon = Session["BangHoaDon"] as BangHoaDon;
                var CTHoaDon = Session["CTHoaDon"] as List<ChiTietHD>;
                
                db.BangHoaDons.Add(bangHoaDon);
                db.SaveChanges();

                foreach (var chiTiet in CTHoaDon)
                {
                    chiTiet.HD_id = bangHoaDon.id;
                    chiTiet.BangSanPham = null;
                    db.ChiTietHDs.Add(chiTiet);
                }
                db.SaveChanges();
                scope.Complete();

                Session["BangHoaDon"] = null;
                Session["CTHoaDon"] = null;
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View("Create");
        }

        // GET: /BangHoaDon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangHoaDon banghoadon = db.BangHoaDons.Find(id);
            if (banghoadon == null)
            {
                return HttpNotFound();
            }
            return View(banghoadon);
        }

        // POST: /BangHoaDon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,TenKhachHang,SoDienThoai,DiaChi,GhiChu")] BangHoaDon banghoadon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banghoadon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banghoadon);
        }

        // GET: /BangHoaDon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangHoaDon banghoadon = db.BangHoaDons.Find(id);
            if (banghoadon == null)
            {
                return HttpNotFound();
            }
            return View(banghoadon);
        }

        // POST: /BangHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BangHoaDon banghoadon = db.BangHoaDons.Find(id);
            db.BangHoaDons.Remove(banghoadon);
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
