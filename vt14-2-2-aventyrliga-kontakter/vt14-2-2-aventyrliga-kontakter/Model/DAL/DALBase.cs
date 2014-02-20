using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace vt14_2_2_aventyrliga_kontakter.Model.DAL {
    public abstract class DALBase {
        private static String _connectionString;

        static DALBase() {
            _connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        protected static SqlConnection CreateConnection() {
            return new SqlConnection(_connectionString);
        }
    }
}