using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace WebAppBU.Views
{
    public class BusinessUnitUrlsController : Controller
    {
       
        // GET: BusinessUnitUrls
        public ActionResult Index()
        {           
            DataTable dt = ReadFromSql();
            if (dt!=null)
            {
                List<DataRow> list = dt.AsEnumerable().ToList();
            }
            return View(dt);
        }

        private DataTable ReadFromSql()
        {
            string stConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlCon = new SqlConnection(stConnectionString);
                //string queryString = " select burl.* from businessunit bu join businessuniturl burl on bu.businessunitid = burl.businessunitid where bu.isactive = 1";
                string queryString = "select burl.BusinessUnitID,burl.Url as Client,'https://'+ burl.Url + '.brandmuscle.net' [Url] "
                    +"from businessunit bu join businessuniturl burl on bu.businessunitid = burl.businessunitid where bu.isactive = 1";
                using (SqlConnection connection = sqlCon)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, connection);
                    adapter.Fill(ds);
                    
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                DataTable mytable = new DataTable();
                mytable.Columns.Add("Error", typeof(string));

                DataRow dr = mytable.NewRow(); 
                dr["Error"] = ex.ToString(); 
                mytable.Rows.Add(dr); 

                return mytable;
            }
           // return ds.Tables[0];
        }

       
    }
}
