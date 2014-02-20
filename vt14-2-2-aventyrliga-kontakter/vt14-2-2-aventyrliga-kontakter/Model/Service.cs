using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vt14_2_2_aventyrliga_kontakter.Model.DAL;

namespace vt14_2_2_aventyrliga_kontakter.Model {
    public class Service {

        public static void DeleteContact(Contact contact) {
            DeleteContact(contact.ContactID);
        }

        public static void DeleteContact(int contactId) {
            ContactDAL.DeleteContact(contactId);
        }

        public static Contact GetContact(int contactId) {
            return ContactDAL.GetContactById(contactId);
        }

        public static IEnumerable<Contact> GetContacts() {
            return ContactDAL.GetContacts();
        }

        public static IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount) {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public static void SaveContact(Contact contact) {
            if (contact.ContactID == 0) {
                ContactDAL.InsertContact(contact);
            } else {
                ContactDAL.UpdateContact(contact);
            }
        }
    }
}