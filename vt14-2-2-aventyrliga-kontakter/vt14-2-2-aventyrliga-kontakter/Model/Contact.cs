using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vt14_2_2_aventyrliga_kontakter.Model {
    public class Contact : AbstractModel {
        public int ContactID {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public String FirstName {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public String LastName {
            get;
            set;
        }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public String EmailAddress {
            get;
            set;
        }
    }
}