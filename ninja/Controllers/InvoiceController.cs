using ninja.model.Entity;
using ninja.model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.Controllers
{
    public class InvoiceController : Controller
    {

        private InvoiceManager _InvoiceContext = new InvoiceManager();
        // GET: Invoice
        public ActionResult Index()
        {
            IList<Invoice> invoiceList = _InvoiceContext.GetAll();
            return View(invoiceList);
        }

    
        // GET: Invoice/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult New(Invoice invoice)
        {
            try
            {

                if (this.ModelState.IsValid)
                {
                    _InvoiceContext.Insert(invoice);
                    return RedirectToAction("Index");
                }
                return View(invoice);
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Update(int? id)
        {
            if (id == null)
                  return View("Error");
            Invoice invoiceToModity = _InvoiceContext.GetById((long)id);
            return View(invoiceToModity);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Update(Invoice invoice)
        {
            try
            {
                _InvoiceContext.Delete(invoice.Id);
                _InvoiceContext.Insert(invoice);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return View("Error");
            Invoice invoiceToDelete = _InvoiceContext.GetById((long)id);
            return View(invoiceToDelete);
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(Invoice invoice)
        {
            try
            {
                if(invoice == null)
                    return View("Error");
                _InvoiceContext.Delete(invoice.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Invoice/Details
        public ActionResult Detail(long id)
        {
             
            Invoice invoice = _InvoiceContext.GetById(id);
  
            return View(invoice.GetDetail());
        }

    }
}
