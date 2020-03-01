using System.Configuration;
using System.Data;
using System.Data.SqlClient;

static internal class TableReader
{
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