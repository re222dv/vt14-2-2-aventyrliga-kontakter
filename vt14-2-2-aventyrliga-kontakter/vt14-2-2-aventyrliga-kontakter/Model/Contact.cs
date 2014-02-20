using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vt14_2_2_aventyrliga_kontakter.Model {
    public class Contact {
        public int ContactID {
            get;
            set;
        }

        public String FirstName {
            get;
            set;
        }

        public String LastName {
            get;
            set;
        }

        public String EmailAddress {
            get;
            set;
        }
    }
}