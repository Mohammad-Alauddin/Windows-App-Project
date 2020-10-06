using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinStoreProject
{

   
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SuperShopList.ItemsSource = GetSuperShopList();
        }


        public ObservableCollection<SuperShopInfo> GetSuperShopList()
        {
            const string GetSuperShopQuery = @"SELECT [SuperShopId]
      ,[SuperShopName]
      ,[SuperShopSize]
      ,[SuperShopLocation]
      ,[SuperShopContact]
       FROM [dbo].[SuperShop]";

            var SuperShopInfos = new ObservableCollection<SuperShopInfo>();
            try
            {

                using (SqlConnection conn = new SqlConnection(@"Data Source=LAB02-PC04\SQLEXPRESS;Initial Catalog=SuperShopDb;user id=jaha ; password=123456;"))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetSuperShopQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var SuperShopInfo = new SuperShopInfo();
                                    SuperShopInfo.SuperShopId = reader.GetInt32(0);
                                    SuperShopInfo.SuperShopName = reader.GetString(1);
                                    SuperShopInfo.SuperShopSize = reader.GetString(2);
                                    SuperShopInfo.SuperShopLocation = reader.GetString(3);
                                    SuperShopInfo.SuperShopContact = reader.GetString(4);
                                 
                                    SuperShopInfos.Add(SuperShopInfo);
                                }
                            }
                        }
                    }
                }
                return SuperShopInfos;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //   string connectionString = "Data Source = LAB02-PC04\SQLEXPRESS; Initial Catalog= SuperShopDb; Integrated Security=True;" providerName = "System.Data.SqlClient"
            //
            SqlConnection con = new SqlConnection(@"Data Source=LAB02-PC04\SQLEXPRESS; Initial Catalog=SuperShopDb; user id=jaha ; password=123456;");
            // SqlConnection con = new SqlConnection(@"Server= LAB02-PC04\SQLEXPRESS; Database= SuperShopDb; Integrated Security=SSPI;");



            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }catch(Exception ex)
            {
                string exList = ex.Message;
            }


           
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[SuperShop]([SuperShopId],[SuperShopName],[SuperShopSize],[SuperShopLocation],[SuperShopContact])VALUES( "+this.SuperShopId.Text+" , '"+this.SuperShopName.Text+"', '"+this.SuperShopSize.Text+"','"+this.SuperShopLocation.Text+"','"+this.SuperShopContact.Text+"')", con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            SuperShopList.ItemsSource = GetSuperShopList();

        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAB02-PC04\SQLEXPRESS; Initial Catalog=SuperShopDb; user id=jaha ; password=123456;");
            // SqlConnection con = new SqlConnection(@"Server= LAB02-PC04\SQLEXPRESS; Database= SuperShopDb; Integrated Security=SSPI;");


            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();


            string updateString = "UPDATE [dbo].[SuperShop] SET [SuperShopName] = '"+this.SuperShopName.Text+"' ,[SuperShopSize] = '"+this.SuperShopSize.Text+"' ,[SuperShopLocation] = '"+this.SuperShopLocation.Text+"'  ,[SuperShopContact] ='"+this.SuperShopContact.Text+"' WHERE  SuperShopId = '"+this.SuperShopId.Text+"' ";
            //" 

            SqlCommand cmd = new SqlCommand(updateString, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            SuperShopList.ItemsSource = GetSuperShopList();


        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAB02-PC04\SQLEXPRESS; Initial Catalog=SuperShopDb; user id=jaha ; password=123456;");
            // SqlConnection con = new SqlConnection(@"Server= LAB02-PC04\SQLEXPRESS; Database= SuperShopDb  ; Integrated Security=SSPI;");


            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();


            SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[SuperShop] WHERE SuperShopId = '"+this.SuperShopId.Text+"'", con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            SuperShopList.ItemsSource = GetSuperShopList();
        }

        private void SuperShopList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
