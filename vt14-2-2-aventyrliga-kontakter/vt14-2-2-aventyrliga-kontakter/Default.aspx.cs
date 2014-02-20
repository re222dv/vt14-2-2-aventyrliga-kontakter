﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vt14_2_2_aventyrliga_kontakter.Model;

namespace vt14_2_2_aventyrliga_kontakter {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            DataPager.SetPageProperties(1, 20, false);
        }

        public IEnumerable<Contact> ListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount) {
            try {
                return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
            } catch {
                ModelState.AddModelError(String.Empty, "Ett fel inträffade när kontakterna hämtades");
                totalRowCount = 0;
                return null;
            }
        }

        public void ListView_InsertItem() {
            var contact = new Contact();
            TryUpdateModel(contact);
            if (ModelState.IsValid) {
                try {
                    Service.SaveContact(contact);
                    Label.Text = "Kontakten lades till";
                    Correct.Visible = true;
                } catch {
                    ModelState.AddModelError(String.Empty, "Ett fel inträffade när kontakten skapades");
                }
            }
        }

        public void ListView_DeleteItem(int ContactID) {
            try {
                Service.DeleteContact(ContactID);
                Label.Text = "Kontakten togs bort";
                Correct.Visible = true;
            } catch {
                ModelState.AddModelError(String.Empty, String.Format("Ett fel inträffade när kontakten med id {0} togs bort", ContactID));
            }
        }

        public void ListView_UpdateItem(int ContactID) {
            var contact = Service.GetContact(ContactID);
            if (contact == null) {
                ModelState.AddModelError(String.Empty, String.Format("Kontakten med id {0} hittades inte", ContactID));
                return;
            }
            TryUpdateModel(contact);
            if (ModelState.IsValid) {
                try {
                    Service.SaveContact(contact);
                    Correct.Visible = true;
                } catch {
                    ModelState.AddModelError(String.Empty, String.Format("Ett fel inträffade när kontakten med id {0} uppdaterades", ContactID));
                }
            }
        }
    }
}