using ninja.model.Entity;
using ninja.model.Manager;
using ninja.Models;
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
            List<InvoiceViewModels> invoiceVM = new List<InvoiceViewModels>();
             foreach (var inv in invoiceList)
            {
                InvoiceViewModels objVM = new InvoiceViewModels();
                objVM.Id = inv.Id;
                objVM.Type = inv.Type;

            }
            return View(invoiceList);
        }

    
        // GET: Invoice/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult New(InvoiceViewModels invoice)
        {
            try
            {

                if (this.ModelState.IsValid)
                {
                    Invoice _invoiceToAdd = new Invoice();
                    _invoiceToAdd.Id = invoice.Id;
                    _invoiceToAdd.Type = invoice.Type;
                    _InvoiceContext.Insert(_invoiceToAdd);
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
        public ActionResult Update(InvoiceViewModels invoice)
        {
            try
            {

                //Verificar la modificacion de una invoice
               // _InvoiceContext.Delete(invoice.Id);
               // _InvoiceContext.Insert(invoice);
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
        public ActionResult Delete(InvoiceViewModels invoice)
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
