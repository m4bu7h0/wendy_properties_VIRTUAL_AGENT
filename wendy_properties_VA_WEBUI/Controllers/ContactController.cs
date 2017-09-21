using System.Linq;
using System.Web.Mvc;
using wendy_properties_VA_DOMAIN.business;
using wendy_properties_VA_WEBUI.Models;

namespace wendy_properties_VA_WEBUI.Controllers {
    public class ContactController : Controller {
        // GET: Contact
        public ActionResult AddContact() {

            var contact = new BLContact();

            var contactVM = new ContactVM {

                Agents =
                    new SelectList(contact.GetAgents().Values)
            };

            return View(contactVM);
        }

        [HttpPost]
        public ActionResult AddContact(ContactVM contact) {

            var contactBL = new BLContact();

            contactBL.AddContact(contact.FirstName,
                contact.LastName, contact.EmailAddress,
                contact.Agent, contact.Message);

            return RedirectToAction("Index", "Home");
        }
    }
}