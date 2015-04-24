using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GWSApp.DAL;
using GWSApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GWSApp.Controllers
{
    public class InvoicesController : Controller
    {
        private GWSContext db = new GWSContext();

        // GET: Invoices
        [HttpGet]
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (Request.QueryString != null)
            {
                // confirmation for PDF creation
                if (Request.QueryString["pdf"] == "pdf")
                {
                    ViewBag.Message = "<p>PDF Created Successfully.</p>";
                }

                // error message for PDF creation
                if (Request.QueryString["pdf"] == "error")
                {
                    ViewBag.Message = "<p>Error - Could not create PDF.</p>";
                }

                // confirmation for mailer
                if (Request.QueryString["mail"] == "mail")
                {
                    ViewBag.Message = "<p>Invoice Sent Successfully.</p>";
                }

                // error message for mailer
                if (Request.QueryString["pdf"] == "error")
                {
                    ViewBag.Message = "<p>Error - Failed to Email Invoice.</p>";
                }
            }

            var invoices = from m in db.Invoices.Include(i => i.Customer) select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = invoices.Where(s => s.Customer.LastName.Equals(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    invoices = invoices.OrderByDescending(s => s.Customer.LastName);
                    break;
                default:
                    invoices = invoices.OrderBy(s => s.Customer.LastName);
                    break;
            }

            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,Year,QtyRateA,QtyRateB,QtyRateC,QtyRateD,QtyRateE,SubtotalA,SubtotalB,SubtotalC,SubtotalD,SubtotalE,Total,Arrears,GrandTotal,AmountPaid")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", invoice.CustomerID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,Year,QtyRateA,QtyRateB,QtyRateC,QtyRateD,QtyRateE,SubtotalA,SubtotalB,SubtotalC,SubtotalD,SubtotalE,Total,Arrears,GrandTotal,AmountPaid")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "LastName", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Invoices/ExportPdf/id
       public ActionResult ExportPdf(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            bool pdfSuccess = MakePdfInvoice(invoice);

            if (pdfSuccess == true)
            {
                return RedirectToAction("Index", new { pdf = "pdf" });
            }
            else
            {
                return RedirectToAction("Index", new { pdf = "error" });
            }

        }


        public async Task<ActionResult> Mail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            
            Customer customer = db.Customers.Find(invoice.CustomerID);
       
            var body = "<p>Email From: Meter Matters </p><p>Message: Dear " + customer.FirstName +", here is your invoice for " + invoice.Year + ".</p><p></p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(customer.Email));   
            message.From = new MailAddress("metermatters@gmail.com");  
            message.Subject = "GWS Invoice Mailer";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;
            try
            {
                message.Attachments.Add(new Attachment(HttpContext.Server.MapPath("~/pdf/" + +customer.ID + customer.FullName + " Invoice " + invoice.Year + ".pdf")));
                
            }
            catch
            {
                //darn, there was an error, try creating invoice
                try
                {
                    bool pdfSuccess = MakePdfInvoice(invoice);
                    message.Attachments.Add(new Attachment(HttpContext.Server.MapPath("~/pdf/" + +customer.ID + customer.FullName + " Invoice " + invoice.Year + ".pdf")));
                }
                catch
                {
                    //dammit, looks like we can't send the invoice
                    return RedirectToAction("Index", new { mail = "error" });
                }
            }
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "metermatters@gmail.com",  
                        Password = "Imameter"  
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);

                    // create a new note to record that email was sent
                    Note newNote = new Note();
                    newNote.CustomerID = customer.ID;
                    newNote.NoteText = "Invoice was issued to " + customer.FullName + " by email at " + DateTime.Now + ".";
                    db.Notes.Add(newNote);
                    db.SaveChanges();
                }


                return RedirectToAction("Index", new { mail = "mail" });
        }

        // invoice pdf generator, called from pdf and mail actions
        public bool MakePdfInvoice(Invoice invoice)
        {

            Customer customer = db.Customers.Find(invoice.CustomerID);
            RatesObject rates = (RatesObject)HttpContext.Application["Rates"];

            try
            {
                Document doc = new Document(iTextSharp.text.PageSize.A4, 50, 50, 25, 25);
                var path = AppDomain.CurrentDomain.BaseDirectory + "pdf/" + customer.ID + customer.FullName + " Invoice " + invoice.Year + ".pdf";
                var output = new FileStream(path, FileMode.Create);
                PdfWriter wri = PdfWriter.GetInstance(doc, output);

                doc.Open();

                var titleFont = FontFactory.GetFont("Cambria", 16, Font.BOLD);
                var subTitleFont = FontFactory.GetFont("Cambria", 12, Font.BOLD);
                var boldTableFont = FontFactory.GetFont("Cambria", 10, Font.BOLD);
                var endingMessageFont = FontFactory.GetFont("Cambria", 9, Font.ITALIC);
                var bodyFont = FontFactory.GetFont("Cambria", 11, Font.NORMAL);
                var tableFont = FontFactory.GetFont("CAmbria", 10, Font.NORMAL);


                doc.Add(new Paragraph("Lavey-Billis Co-Op Water Group Society", bodyFont));
                doc.Add(Chunk.NEWLINE);

                var invheader = new Paragraph("Invoice " + invoice.Year, titleFont);
                invheader.Alignment = Element.ALIGN_RIGHT;
                doc.Add(invheader);
                var invno = new Paragraph("Invoice Number: " + customer.InvoiceNumber, bodyFont);
                invno.Alignment = Element.ALIGN_RIGHT;
                doc.Add(invno);
                var date = new Paragraph("Date: " + String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now), bodyFont);
                date.Alignment = Element.ALIGN_RIGHT;
                doc.Add(date);


                doc.Add(new Paragraph("Phone", bodyFont));
                doc.Add(new Paragraph("S. McDermott 087-2368653", bodyFont));
                doc.Add(new Paragraph("T. Owens 087-9881967", bodyFont));
                doc.Add(new Paragraph("P. McMahon 086-8138286", bodyFont));
                doc.Add(new Paragraph("S. Smith", bodyFont));
                doc.Add(Chunk.NEWLINE);

                PdfPTable invisible = new PdfPTable(2); //invisible table for formatting
                invisible.TotalWidth = 500f;
                invisible.LockedWidth = true;
                invisible.DefaultCell.BorderWidth = 0;
                invisible.AddCell(new Phrase("To:", boldTableFont));
                PdfPCell invisCell = new PdfPCell(new Phrase("Subject: Water Rates for " + invoice.Year, boldTableFont));
                invisCell.BorderWidth = 0;
                invisCell.HorizontalAlignment = 2;
                invisible.AddCell(invisCell);
                doc.Add(invisible);

                doc.Add(new Paragraph(customer.FullName, tableFont));
                doc.Add(new Paragraph(customer.Address1, tableFont));
                doc.Add(new Paragraph(customer.Address2, tableFont));
                doc.Add(new Paragraph(customer.Address3, tableFont));
                doc.Add(new Paragraph(customer.Address4, tableFont));

                doc.Add(Chunk.NEWLINE);

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 1;
                table.AddCell(new Phrase("Rate Type", boldTableFont));
                table.AddCell(new Phrase("Cubic Metres", boldTableFont));
                table.AddCell(new Phrase("€Euro", boldTableFont));
                table.AddCell(new Phrase("Rate A", tableFont));
                table.AddCell(new Phrase("0-" + rates.BandA.ToString(), tableFont));
                table.AddCell(new Phrase(rates.RateA.ToString(), tableFont));
                table.AddCell(new Phrase("Rate B", tableFont));
                int lowerLimitB = rates.BandA + 1;
                table.AddCell(new Phrase(lowerLimitB.ToString() + "-" + rates.BandB.ToString(), tableFont));
                table.AddCell(new Phrase(rates.RateB.ToString(), tableFont));
                table.AddCell(new Phrase("Rate C", tableFont));
                int lowerLimitC = rates.BandB + 1;
                table.AddCell(new Phrase(lowerLimitC.ToString() + "-" + rates.BandC.ToString(), tableFont));
                table.AddCell(new Phrase(rates.RateC.ToString(), tableFont));
                table.AddCell(new Phrase("Rate D", tableFont));
                int lowerLimitD = rates.BandC + 1;
                table.AddCell(new Phrase(lowerLimitD.ToString() + "-" + rates.BandD.ToString(), tableFont));
                table.AddCell(new Phrase(rates.RateD.ToString(), tableFont));
                table.AddCell(new Phrase("Rate E", tableFont));
                int lowerLimitE = rates.BandD + 1;
                table.AddCell(new Phrase(lowerLimitE.ToString() + "+", tableFont));
                table.AddCell(new Phrase(rates.RateE.ToString(), tableFont));


                doc.Add(table);
                doc.Add(Chunk.NEWLINE);

                PdfPTable table2 = new PdfPTable(4);
                table2.SpacingBefore = 10;
                table2.SpacingAfter = 10;
                table2.TotalWidth = 500f;
                table2.LockedWidth = true;
                table2.AddCell(new Phrase("Description", boldTableFont));
                table2.AddCell(new Phrase("Quantity", boldTableFont));
                table2.AddCell(new Phrase("Unit Price", boldTableFont));
                table2.AddCell(new Phrase("Total", boldTableFont));
                table2.AddCell(new Phrase("Water Rate A", tableFont));
                table2.AddCell(new Phrase(invoice.QtyRateA.ToString(), tableFont));
                table2.AddCell(new Phrase(rates.RateA.ToString(), tableFont));
                table2.AddCell(new Phrase(invoice.SubtotalA.ToString(), tableFont));
                table2.AddCell(new Phrase("Water Rate B", tableFont));
                table2.AddCell(new Phrase(invoice.QtyRateB.ToString(), tableFont));
                table2.AddCell(new Phrase(rates.RateB.ToString(), tableFont));
                table2.AddCell(new Phrase(invoice.SubtotalB.ToString(), tableFont));
                table2.AddCell(new Phrase("Water Rate C", tableFont));
                table2.AddCell(new Phrase(invoice.QtyRateC.ToString(), tableFont));
                table2.AddCell(new Phrase(rates.RateC.ToString(), tableFont));
                table2.AddCell(new Phrase(invoice.SubtotalC.ToString(), tableFont));
                table2.AddCell(new Phrase("Water Rate D", tableFont));
                table2.AddCell(new Phrase(invoice.QtyRateD.ToString(), tableFont));
                table2.AddCell(new Phrase(rates.RateD.ToString(), tableFont));
                table2.AddCell(new Phrase(invoice.SubtotalD.ToString(), tableFont));
                table2.AddCell(new Phrase("Water Rate E", tableFont));
                table2.AddCell(new Phrase(invoice.QtyRateE.ToString(), tableFont));
                table2.AddCell(new Phrase(rates.RateE.ToString(), tableFont));
                table2.AddCell(new Phrase(invoice.SubtotalE.ToString(), tableFont));
                PdfPCell cell1 = new PdfPCell();
                cell1.BorderWidth = 0;
                PdfPCell cell2 = new PdfPCell();
                cell2.BorderWidth = 0;
                PdfPCell cell3 = new PdfPCell(new Phrase("Total:", boldTableFont));
                cell3.BorderWidth = 1;
                PdfPCell cell4 = new PdfPCell(new Phrase(invoice.Total.ToString(), tableFont));
                cell4.BorderWidth = 1;
                PdfPCell cell5 = new PdfPCell();
                cell5.BorderWidth = 0;
                PdfPCell cell6 = new PdfPCell();
                cell6.BorderWidth = 0;
                PdfPCell cell7 = new PdfPCell(new Phrase("Arrears:", boldTableFont));
                cell7.BorderWidth = 1;
                PdfPCell cell8 = new PdfPCell(new Phrase(invoice.Arrears.ToString(), tableFont));
                cell8.BorderWidth = 1;
                PdfPCell cell9 = new PdfPCell();
                cell9.BorderWidth = 0;
                PdfPCell cell10 = new PdfPCell();
                cell10.BorderWidth = 0;
                PdfPCell cell11 = new PdfPCell(new Phrase("Amount Due:", boldTableFont));
                cell11.BorderWidth = 1;
                PdfPCell cell12 = new PdfPCell(new Phrase(invoice.GrandTotal.ToString(), tableFont));
                cell12.BorderWidth = 1;
                table2.AddCell(cell1);
                table2.AddCell(cell2);
                table2.AddCell(cell3);
                table2.AddCell(cell4);
                table2.AddCell(cell5);
                table2.AddCell(cell6);
                table2.AddCell(cell7);
                table2.AddCell(cell8);
                table2.AddCell(cell9);
                table2.AddCell(cell10);
                table2.AddCell(cell11);
                table2.AddCell(cell12);

                doc.Add(table2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Payment due within 30 days from invoice date.", endingMessageFont));
                doc.Add(new Paragraph("Please use Giro Enclosed only.", endingMessageFont));
                doc.Add(new Paragraph("Management reserve right to withdraw supply for non payment.", endingMessageFont));
                doc.Add(new Paragraph("1 Cubic Metre = 220 Gallons", endingMessageFont));

                doc.Close();
                return (true);
            }
            catch
            {
                //pdf generation failed
                return (false);
            }
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
