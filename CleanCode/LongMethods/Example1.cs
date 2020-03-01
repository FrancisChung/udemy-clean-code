using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FooFoo
{
    public class MemoryFileCreator
    {
        public System.IO.MemoryStream CreateMemoryFile()
        {
            MemoryStream ReturnStream = new MemoryStream();

            var dt = GetDataTable();

            StreamWriter sw = new StreamWriter(ReturnStream);

            WriteColumnNames(dt, sw);
            WriteRows(dt, sw);

            sw.Flush();
            sw.Close();

            return ReturnStream;
        }

        private static void WriteRows(DataTable dt, StreamWriter sw)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string str = String.Format("\"{0:c}\"", dr[i].ToString()).Replace("\r\n", " ");
                        sw.Write(str);
                    }
                    else
                    {
                        sw.Write("");
                    }

                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.WriteLine();
            }
        }

        private static void WriteColumnNames(DataTable dt, StreamWriter sw)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < dt.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.WriteLine();

        }

        private static DataTable GetDataTable()
        {
            string strConn = ConfigurationManager.ConnectionStrings["FooFooConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strConn);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [FooFoo] ORDER BY id ASC", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "FooFoo");
            DataTable dt = ds.Tables["FooFoo"];
            return dt;
        }
    }

    public partial class Download : System.Web.UI.Page
    {
        private readonly MemoryFileCreator _memoryFileCreator = new MemoryFileCreator();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClearResponse();

            SetCacheability();

            WriteContentToResponse(GetCSV());
        }

        private byte[] GetCSV()
        {
            System.IO.MemoryStream ms = _memoryFileCreator.CreateMemoryFile();
            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteArray;
        }

        private void WriteContentToResponse(byte[] byteArray)
        {
            Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
            Response.ContentType = "text/comma-separated-values";
            Response.AddHeader("Content-Disposition", "attachment; filename=FooFoo.csv");
            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.BinaryWrite(byteArray);
        }

        private void SetCacheability()
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
        }

        private void ClearResponse()
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
        }
    }
}