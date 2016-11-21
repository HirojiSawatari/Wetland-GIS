using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace 湿地GIS
{
    class C_conn
    {
        string s_conn;
        OleDbConnection c_my_conn;

        public OleDbConnection get_conn()
        {
            s_conn = global::湿地GIS.Properties.Settings.Default.LNNU_GISConnectionString;
            c_my_conn = new OleDbConnection(s_conn);
            c_my_conn.Open();
            return c_my_conn;
        }
    }
}
