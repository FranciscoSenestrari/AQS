using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MensajesController : Controller
    {
        private ZardMessageEntities db = new ZardMessageEntities();
        private DateTime tiempo = DateTime.Today;
        // GET: Mensajes
        public ActionResult Index()
        {
            var mensajes = db.Mensajes.Include(m => m.Contexto).Include(m => m.Usuario);
            return View(mensajes.ToList());
        }

        // GET: Mensajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensaje mensaje = db.Mensajes.Find(id);
            if (mensaje == null)
            {
                return HttpNotFound();
            }
            return View(mensaje);
        }

        // GET: Mensajes/Create
        public ActionResult Create()
        {
            ViewBag.id_ctx = new SelectList(db.Contextoes, "idContexto", "nomb");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre");
            return View();
        }

        // POST: Mensajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create([Bind(Include = "id_mensaje,fecha,titulo,mensaje1,id_usuario,id_ctx")] Mensaje mensaje)
        {
            if (ModelState.IsValid)
            {
                db.Mensajes.Add(mensaje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ctx = new SelectList(db.Contextoes, "idContexto", "nomb", mensaje.id_ctx);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", mensaje.id_usuario);
            return View(mensaje);
        }

        // GET: Mensajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensaje mensaje = db.Mensajes.Find(id);
            if (mensaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_ctx = new SelectList(db.Contextoes, "idContexto", "nomb", mensaje.id_ctx);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", mensaje.id_usuario);
            return View(mensaje);
        }

        // POST: Mensajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mensaje,fecha,titulo,mensaje1,id_usuario,id_ctx")] Mensaje mensaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ctx = new SelectList(db.Contextoes, "idContexto", "nomb", mensaje.id_ctx);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", mensaje.id_usuario);
            return View(mensaje);
        }

        // GET: Mensajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensaje mensaje = db.Mensajes.Find(id);
            if (mensaje == null)
            {
                return HttpNotFound();
            }
            return View(mensaje);
        }

        // POST: Mensajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensaje mensaje = db.Mensajes.Find(id);
            db.Mensajes.Remove(mensaje);
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
