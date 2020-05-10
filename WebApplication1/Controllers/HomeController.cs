using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetEdit(string id)
        {
           if (id == "ROB")
            {
                return PartialView("ROBEditPartial");
            }
            if (id == "Gocart")
            {
                return PartialView("GocartEditPartial");
            }
            return PartialView("ROBEditPartial");

        }

        public ActionResult Test()
        {
  
            return PartialView("ReportViewPartial");

        }






        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }



        public ActionResult SavePayments(string PeopleId)
        {
            string VCNumber = "Zone7-" + RandomString(6, false);
            string status = "Not Active";

            List<string> p = PeopleId.Split(',').ToList();
            try
            {

                VoucherDto Tosave = new VoucherDto();

                Tosave.NamesId = p[0];
                Tosave. Email = p[1];
                Tosave. Contact = p[2];
                Tosave. NoOfRides = p[3];
                Tosave.NoOfPeople = p[4];
                Tosave.AgeGroup = p[5];
                Tosave.OccasionId = p[6];
                Tosave.OccasionDate = p[7];
                Tosave.Category = p[8];
                Tosave.VoucherNo = VCNumber;
                Tosave.Status = status;
                string DatePath = "VC"+RandomString(6, false) + ".pdf";

                string Path = Server.MapPath("~/Uploads/" + DatePath);
                ViewBag.PDFVoucher = DatePath;
                var rep = new XtraReport1();
                rep.NamesId.Value = p[0];
                rep.Email.Value = p[1];
                rep.Contact.Value = p[2];
                rep.NoOfRides.Value = p[3];
                rep.NoOfPeople.Value = p[4];
                rep.AgeGroup.Value = p[5];
                rep.OccasionId.Value = p[6];
               // Tosave.OccasionDate = p[7];
                rep.ValidFrom.Value = DateTime.Today.ToShortDateString();
                rep.ValidTo.Value = DateTime.Today.AddDays(90).ToShortDateString();
                rep.VoucherNo.Value = VCNumber;
                rep.Status.Value = status;
                rep.Category.Value = p[8];
                rep.ExportToPdf(Path);



                try
                {

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("b.mutheiwana@gmail.com");
                    mail.IsBodyHtml = true;
                    mail.To.Add(Tosave.Email);
                    mail.Subject = VCNumber;
                    mail.Body = "mail with attachment";

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(Path);
                    mail.Attachments.Add(attachment);


                    SmtpServer.Port = 587;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("b.mutheiwana@gmail.com", "Nekhavhambe27");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.ToString();
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Unable to Create.Error : " + ex;
            }

            return PartialView("ReportViewPartial");

        }
    }
}