using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;



namespace SignalRChat.Pages
{
    public class scoresModel : PageModel
    {
        public void OnGet()
        {
           
            BindGridView();

        }


        private void BindGridView()
        {
            // Get the connection string from Web.config.  
            // When we use Using statement,  
            // we don't need to explicitly dispose the object in the code,  
            // the using statement takes care of it. 
            string connectionString = "datasource=192.168.0.94;port=3306;username=apppruebas;password=Capeluam209173$$_;database=test;";
            string strSelectCmd = "SELECT Dorsal,E1,E2,E3,E4,(e1+e2+e3+e4-COALESCE(GREATEST(E1,E2,E3,E4))-COALESCE(least(E1,E2,E3,E4)))/2 as E,A1,A2,A3,A4,(a1+a2+a3+a4-COALESCE(GREATEST(a1,a2,a3,a4))-COALESCE(least(a1,a2,a3,a4)))/2 as A,D,P, D+((a1+a2+a3+a4-COALESCE(GREATEST(a1,a2,a3,a4))-COALESCE(least(a1,a2,a3,a4)))/2)+((e1+e2+e3+e4-COALESCE(GREATEST(E1,E2,E3,E4))-COALESCE(least(E1,E2,E3,E4))))-P as SCORE FROM SCORE";

            {
                // Create a DataSet object. 
                DataSet dsScore = new DataSet();


                // Create a SELECT query. 


                // Create a SqlDataAdapter object 
                // SqlDataAdapter represents a set of data commands and a  
                // database connection that are used to fill the DataSet and  
                // update a SQL Server database.  
                MySqlDataAdapter da = new MySqlDataAdapter(strSelectCmd, connectionString);

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                // Open the connection 
                databaseConnection.Open();


                // Fill the DataTable named "Person" in DataSet with the rows 
                // returned by the query.new n 
                da.Fill(dsScore);


                // Get the DataView from Person DataTable. 
                DataView dvScore = dsScore.Tables[0].DefaultView;


                // Set the sort column and sort order. 
                // dvPerson.Sort = ViewState["SortExpression"].ToString();


                // Bind the GridView control. 
                //
                //gvScore.DataSource = dvScore;
                //gvScore.DataBind();
            }
        }
    }
}