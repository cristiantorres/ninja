using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ninja.model.Entity;
using ninja.model.Mock;

namespace ninja.model.Manager {

    public class InvoiceManager {

        private InvoiceMock _mock;

        public InvoiceManager() {

            this._mock = InvoiceMock.GetInstance();

        }

        public IList<Invoice> GetAll() {

            return this._mock.GetAll();

        }

        public Invoice GetById(long id) {

            return this._mock.GetById(id);

        }

        public void Insert(Invoice item) {

            this._mock.Insert(item);

        }

        public void Delete(long id) {
            Invoice invoice = this.GetById(id);
            if (invoice != null)
                this._mock.DeleteDetail(id);
                this._mock.Delete(invoice);
            
        }
        public void Update(long id)
        {

            Invoice invoice = this.GetById(id);
            
            this._mock.Update(invoice);

        }
        public void Update(Invoice invoice)
        {

            this._mock.Update(invoice);

        }
        public Boolean Exists(long id) {

            return this._mock.Exists(id);

        }

        public void UpdateDetail(long id, IList<InvoiceDetail> detail) {

            /*
              Este método tiene que reemplazar todos los items del detalle de la factura
              por los recibidos por parámetro
             */

            #region codigo implementado

            Invoice invoiceToModify = this.GetById(id);
            invoiceToModify.DeleteDetails();
            foreach (InvoiceDetail invoicedetail in detail)
            {
                invoiceToModify.AddDetail(invoicedetail);
            }

            #endregion codigo implementado

        }


        /*Bora un detail especifico*/
        public void deleteDetail(long idInvoice, long idDetail)
        {
            Invoice invoice = this.GetById(idInvoice);
           // List<InvoiceDetail> details = invoice.GetDetail().ToList();

            invoice.GetDetail().ToList().RemoveAll((x) => x.Id == idDetail);
            //foreach (var item in details)
            //{
            //    if (item.Id==idDetail)
            //    {
            //        details.Remove(item);
            //        break;
            //    }
            //}
        }
    }

}