﻿using ninja.model.Entity;
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
            List<InvoiceViewModels> invoiceVMList = new List<InvoiceViewModels>();
            foreach (var inv in invoiceList)
            {
                InvoiceViewModels objVM = new InvoiceViewModels();
                objVM.Id = inv.Id;
                objVM.Type = inv.Type;
                invoiceVMList.Add(objVM);

            }
            return View(invoiceVMList);
        }



        public ActionResult New()
        {
            return View();
        }


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
            InvoiceViewModels invoiceVM = new InvoiceViewModels();
            invoiceVM.Id = invoiceToModity.Id;
            invoiceVM.Type = invoiceToModity.Type;

            return View(invoiceVM);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Update(InvoiceViewModels invoice)
        {
            try
            {

                Invoice invoiceToModify = new Invoice { Id = invoice.Id, Type = invoice.Type };
                _InvoiceContext.Update(invoiceToModify);
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
            InvoiceViewModels invoiceVM = new InvoiceViewModels();
            invoiceVM.Type = invoiceToDelete.Type;
            invoiceVM.Id = invoiceToDelete.Id;

            return View(invoiceVM);
        }

  
        [HttpPost]
        public ActionResult Delete(InvoiceViewModels invoice)
        {
            try
            {
                if (invoice == null)
                    return View("Error");
                _InvoiceContext.Delete(invoice.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #region InvoiceDetails
        [HttpGet]
        public ActionResult Detail(long id)
        {
            Invoice invoice = _InvoiceContext.GetById(id);
 
            List<InvoiceDetailViewModels> invoiceDetailVMList = new List<InvoiceDetailViewModels>();
            foreach (var inv in invoice.GetDetail())
            {
                InvoiceDetailViewModels objVM = new InvoiceDetailViewModels();
                objVM.Id = inv.Id;
                objVM.InvoiceId = inv.InvoiceId;
                objVM.Amount = inv.Amount;
                objVM.Description = inv.Description;
                objVM.TotalPrice = inv.TotalPrice;
                objVM.TotalPriceWithTaxes = inv.TotalPriceWithTaxes;
                objVM.UnitPrice = inv.UnitPrice;
                invoiceDetailVMList.Add(objVM);

            }
            ViewBag.Results = invoiceDetailVMList.Count;
            ViewBag.Invoice = id;
            return View(invoiceDetailVMList);
        }

        [HttpGet]
        public ActionResult DeleteItem(long? idinvoice, long? iditem)
        {
            if (idinvoice == null || iditem == null)
                return View("Error");
            try
            {
                Invoice _invoice = _InvoiceContext.GetById((long)idinvoice);
                InvoiceDetailViewModels invoiceDetailVM = new InvoiceDetailViewModels();
                foreach (var item in _invoice.GetDetail())
                {
                    if (item.Id == iditem)
                    {
                        invoiceDetailVM.InvoiceId = item.InvoiceId;
                        invoiceDetailVM.Amount = item.Amount;
                        invoiceDetailVM.Description = item.Description;
                        invoiceDetailVM.TotalPrice = item.TotalPrice;
                        invoiceDetailVM.TotalPriceWithTaxes = item.TotalPriceWithTaxes;
                        invoiceDetailVM.UnitPrice = item.UnitPrice;
                    }
                }
                ViewBag.Invoice = idinvoice;
                return View(invoiceDetailVM);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteItem(InvoiceDetailViewModels invoiceDetailVM)
        {
            try
            {
                if (invoiceDetailVM == null)
                    return View("Error");

                _InvoiceContext.deleteDetail(invoiceDetailVM.InvoiceId, invoiceDetailVM.Id);
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult AddItem(long id)
        {
            Invoice invoice = _InvoiceContext.GetById(id);
            InvoiceViewModels invVM = new InvoiceViewModels
            {
                Id = invoice.Id,
                Type = invoice.Type
            };
            ViewBag.Invoice = invVM;
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(InvoiceDetailViewModels invoiceDetailVM)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string invoiceForDetail = Request.Form["invoice"]; 
                    List<InvoiceDetail> items = new List<InvoiceDetail>();
                    InvoiceDetail detail =  new InvoiceDetail
                    {
                        InvoiceId = long.Parse(invoiceForDetail),
                        Amount = invoiceDetailVM.Amount,
                        Description = invoiceDetailVM.Description,
                        UnitPrice = invoiceDetailVM.UnitPrice
                    };
                    _InvoiceContext.AddDetail(invoiceDetailVM.Id, detail);
                    return RedirectToAction("Index");

                }
                return View(invoiceDetailVM);
            }
            catch
            {
                return View();
            }

  
        }

        public ActionResult UpdateItem(long? idinvoice, long iditem)
        {
            try
            {
                if (idinvoice == null)
                    return View("Error");
                Invoice invoiceToModity = _InvoiceContext.GetById((long)idinvoice);
                InvoiceDetailViewModels invoiceDetailVM = new InvoiceDetailViewModels();
                foreach (var item in invoiceToModity.GetDetail())
                {
                    if (item.Id == iditem)
                    {
                        invoiceDetailVM.Id = item.Id;
                        invoiceDetailVM.InvoiceId = item.InvoiceId;
                        invoiceDetailVM.Amount = item.Amount;
                        invoiceDetailVM.Description = item.Description;
                        invoiceDetailVM.TotalPrice = item.TotalPrice;
                        invoiceDetailVM.TotalPriceWithTaxes = item.TotalPriceWithTaxes;
                        invoiceDetailVM.UnitPrice = item.UnitPrice;
                        break;
                    }
                }

                return View(invoiceDetailVM);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

 
        [HttpPost]
        public ActionResult UpdateItem(InvoiceDetailViewModels invoice)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    InvoiceDetail invoiceDetailToModify = new InvoiceDetail
                    {
                        InvoiceId = invoice.InvoiceId,
                        Amount = invoice.Amount,
                        Description = invoice.Description,
                        UnitPrice = invoice.UnitPrice
                    };
                    _InvoiceContext.UpdateItem(invoiceDetailToModify);
                }
              return RedirectToAction("Index");
                
            }
            catch
            {
                return View("Error");
            }
        }
        #endregion InvoiceDetails
    }
}