using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MuhendisSozluk.User
{
    public partial class Yazar : System.Web.UI.Page
    {
        String con = connectionStrings.bedir;
        static String name2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                object user = Session["username"];
                if (user != null)
                {
                    btn_default_profile.Text = "makamım";
                    btn_default_loginout.Text = "çıkış yap";

                }
                else
                {
                    btn_default_profile.Text = "kayıt ol";
                    btn_default_loginout.Text = "giriş yap";
                }

                if (RouteData.Values["name"] != null)
                {
                    name2 = (RouteData.Values["name"].ToString());
                    fill();
                    fillWriter();
                }
                else Response.Redirect("~/yazar/antiochus");


            }
        }

        protected void fill()
        {
            DataSet ds_title = loadSolKanat();
            title_repeater.DataSource = ds_title;
            title_repeater.DataBind();

            DataSet ds = fillWritersEntries(name2);
            writer_entry_repeater.DataSource = ds;
            writer_entry_repeater.DataBind();

        }

        private DataSet loadSolKanat()
        {
            SqlConnection con2 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select Top 25 Name from TITLE where Visible='True' order by LastUpdate asc", con2);
            // da.SelectCommand.Parameters.AddWithValue(@"name", title);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        private DataSet fillWritersEntries(String name)
        {
            SqlConnection con1 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select Top 10 * from ENTRY where WriterID = (select ID from WRITER where Url=@name) and Visible='True'", con1);
            da.SelectCommand.Parameters.AddWithValue(@"name", name);

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void fillWriter()
        {
            String name;
            String dep;
            String sen;
            double rating = 0;
            int Entry = 0;
            SqlConnection c1 = new SqlConnection(con);
            SqlCommand cmd1 = c1.CreateCommand();
            cmd1.CommandText = "select ID, Name, SeniorityID, DepartmentID, Rating from WRITER Where Url=@url ";
            cmd1.Parameters.AddWithValue(@"url", name2);
            c1.Open();
            
            var rdr = cmd1.ExecuteReader();
            if (rdr.Read()){ 
                name = rdr.GetString(1);
                sen = getSeniorityByID(rdr.GetInt32(2));
                dep = getDepartmentByID(rdr.GetInt32(3));
                rating = rdr.GetDouble(4);
                Entry = getEntryCount(rdr.GetInt32(0));

            }
            else
            {
                name = "Sorgu hatası";
                dep = "Sorgu hatası";
                sen = "Sorgu hatası";
               
            }
            c1.Close();

            lbl_writer_department.Text = dep;
            lbl_writer_name.Text = name;
            lbl_writer_seniority.Text = sen;
            lbl_writer_rating.Text = rating.ToString();
            lbl_writer_entries.Text = Entry.ToString();


        }

        public String getSeniorityByID(int id)
        {
            String result = "";
            SqlConnection c2 = new SqlConnection(con);
            SqlCommand cmd2 = c2.CreateCommand();
            cmd2.CommandText = "select Name from SENIORITY Where ID=@id ";
            cmd2.Parameters.AddWithValue(@"id", id);
            c2.Open();
            
                var rdr2 = cmd2.ExecuteReader();
            if (rdr2.Read()) { 
                result = rdr2.GetString(0);
            }
            else
            {
                result = "HATA";
            }
            c2.Close();
            return result;
        }

        String getDepartmentByID(int id)
        {
            String result = null;
            SqlConnection con2 = new SqlConnection(connectionStrings.bedir);
            var cmd2 = con2.CreateCommand();
            cmd2.CommandText = "select Name from DEPARTMENT where ID = @id";
            cmd2.Parameters.AddWithValue(@"id", id);
            con2.Open();
            var rdr2 = cmd2.ExecuteReader();
            if (rdr2.Read())
            {
                result = rdr2.GetString(0);
            }
            else
            {
                result = "oda bulunamadı!";
            }
            con2.Close();
            return result;
        }

        int getEntryCount(int id)
        {
            int result = 0;
            SqlConnection con3 = new SqlConnection(connectionStrings.bedir);
            var cmd3 = con3.CreateCommand();
            cmd3.CommandText = "select count(ID) from ENTRY where WriterID = @id";
            cmd3.Parameters.AddWithValue(@"id", id);
            con3.Open();
            var rdr3 = cmd3.ExecuteReader();
            if (rdr3.Read())
            {
                result = rdr3.GetInt32(0);
            }
            else
            {
                result = 0;
            }
            con3.Close();
            return result;
        }

        protected void btn_user_search_Click(object sender, EventArgs e)
        {
            String key = txt_user_search.Text;


            if (key.StartsWith("#"))
            {
                searchEntry(key.Substring(1));
            }
            else if (key.StartsWith("@"))
            {
                searchWriter(key.Substring(1));
            }
            else
            {
                searchTitle(key);
            }
        }
        public void searchEntry(String key)
        {
            String url = "~/entry/" + key;
            SqlConnection con = new SqlConnection(connectionStrings.bedir);
            int number = Int32.Parse(key);
            var cmd = con.CreateCommand();
            cmd.CommandText = "select ID from ENTRY where ID = @number";
            cmd.Parameters.AddWithValue(@"number", number);
            con.Open();
            var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                Response.Redirect(url);
            }

            else
            {
                lbl_user_search.Text = "bu entry imha edildi ya da hiç yazılmadı!";
            }
            con.Close();
        }

        protected void searchWriter(String key)
        {
            String url = "~/yazar/" + Helper.SEOUrl(key);

            SqlConnection con = new SqlConnection(connectionStrings.bedir);
            var cmd = con.CreateCommand();
            cmd.CommandText = "select ID from WRITER where Url = @number";
            cmd.Parameters.AddWithValue(@"number", key);
            con.Open();
            var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                Response.Redirect(url);
            }

            else
            {
                lbl_user_search.Text = "bu yazar imha edildi ya da hiç var olmadı!";
            }
            con.Close();
        }
        protected void searchTitle(String key)
        {

            SqlConnection con2 = new SqlConnection(connectionStrings.bedir);
            String name = Helper.SEOUrl(key);
            String url = "~/" + name;
            var cmd = con2.CreateCommand();

            cmd.CommandText = "select ID from TITLE where Url = @name";
            cmd.Parameters.AddWithValue(@"name", name);

            con2.Open();
            var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                Response.Redirect(url);
            }

            else
            {

                if (Session["username"] != null && getSeniority(Session["username"].ToString())!=1)
                {
                    btn_new_title.Visible = true;
                    lbl_user_search.Text = "bu başlık imha edildi ya da hiç açılmadı.";
                }
                else
                {
                    lbl_user_search.Text = "bu başlık imha edildi ya da hiç açılmadı.";
                }
            }
            con2.Close();
        }

        protected void btn_default_profile_Click(object sender, EventArgs e)
        {
            object user = Session["username"];
            if (user == null)
            {
                Response.Redirect("/User/Signup.aspx");
            }
            else
            {
                if (getSeniority(user.ToString()) == 4)
                {
                    Response.Redirect("/Admin/Home.aspx");
                }
                Response.Redirect("/User/MyProfile.aspx");
            }
        }

        protected int getSeniority(String name)
        {
            int result = 0;
            SqlConnection c1 = new SqlConnection(con);
            SqlCommand cmd1 = c1.CreateCommand();
            cmd1.CommandText = "select SeniorityID from WRITER where Name=@name";
            cmd1.Parameters.AddWithValue(@"name", name);
            c1.Open();
            var rdr = cmd1.ExecuteReader();
            if (rdr.Read())
            {
                result = rdr.GetInt32(0);
            }
            else result = -1;
            c1.Close();
            return result;
        }

        protected void btn_default_loginout_Click(object sender, EventArgs e)
        {
            object user = Session["username"];
            if (user == null)
            {
                Response.Redirect("/User/Login.aspx");
            }
            else
            {
                Session.Remove(user.ToString());
                user = null;
                Response.Redirect("/default.aspx");
            }

        }

    }

}