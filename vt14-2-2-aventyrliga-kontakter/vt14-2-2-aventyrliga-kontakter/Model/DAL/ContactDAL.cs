using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace vt14_2_2_aventyrliga_kontakter.Model.DAL {
    public class ContactDAL : DALBase {

        public static void DeleteContact(int contactId) {
            using (SqlConnection conn = CreateConnection()) {
                try {
                    SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                } catch {
                    throw new ApplicationException("An error occured while removing the contact from the database.");
                }
            }
        }

        public static Contact GetContactById(int contactId) {
            using (SqlConnection conn = CreateConnection()) {
                try {
                    SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ContactID", contactId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader()) {

                        if (reader.Read()) {
                            var contactIDIndex = reader.GetOrdinal("ContactID");
                            var firstNameIndex = reader.GetOrdinal("FirstName");
                            var lastNameIndex = reader.GetOrdinal("LastName");
                            var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                            return new Contact {
                                ContactID = reader.GetInt32(contactIDIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            };
                        }
                    }

                    return null;
                } catch {
                    throw new ApplicationException("An error occured while getting the contact from the database.");
                }
            }
        }

        public static IEnumerable<Contact> GetContacts() {
            using (var conn = CreateConnection()) {
                try {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader()) {
                        var contactIDIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read()) {
                            contacts.Add(new Contact {
                                ContactID = reader.GetInt32(contactIDIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }
                    }

                    contacts.TrimExcess();

                    return contacts;
                } catch {
                    throw new ApplicationException("An error occured while getting contacts from the database.");
                }
            }
        }

        public static IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount) {
            using (var conn = CreateConnection()) {
                try {
                    var contacts = new List<Contact>(maximumRows);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;

                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader()) {
                        var contactIDIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read()) {
                            contacts.Add(new Contact {
                                ContactID = reader.GetInt32(contactIDIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }

                    }

                    totalRowCount = (int) cmd.Parameters["@RecordCount"].Value;

                    contacts.TrimExcess();

                    return contacts;
                } catch {
                    throw new ApplicationException("An error occured while getting contacts from the database.");
                }
            }
        }

        public static void InsertContact(Contact contact) {
            using (SqlConnection conn = CreateConnection()) {
                try {
                    SqlCommand cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    contact.ContactID = (int) cmd.Parameters["@ContactID"].Value;
                } catch {
                    throw new ApplicationException("An error occured while adding the contact to the database.");
                }
            }
        }

        public static void UpdateContact(Contact contact) {
            using (SqlConnection conn = CreateConnection()) {
                try {
                    SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactID;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;


                    conn.Open();

                    cmd.ExecuteNonQuery();
                } catch {
                    throw new ApplicationException("An error occured while updating the contact in the database.");
                }
            }
        }
    }
}