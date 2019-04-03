using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MuhendisSozluk.Department;


namespace MuhendisSozluk.Department
{
    public partial class Oda : System.Web.UI.Page
    {
        static String con = connectionStrings.bedir;
        static String name = "";
        static int head = 0;

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
                fill();
                if (RouteData.Values["dep"] != null)
                {
                    name = RouteData.Values["dep"].ToString();
                    loadInfo(name);
                }
                else
                {
                    Response.Redirect("/oda/bilgisayar-muhendisleri-odasi");

                }

            }
        }

        public void loadInfo(String name)
        {
            int ID = getDepartmentID(getDepartment(name));
            double rakim=0.0;
            String headname = getWriter(head);
            int entry = 0;
            int nufus=0;

            SqlConnection c1 = new SqlConnection(con);
            SqlCommand cmd1 = c1.CreateCommand();
            cmd1.CommandText = "select avg(Rating) from WRITER where DepartmentID=@id";
            cmd1.Parameters.AddWithValue(@"id", ID);
            c1.Open();
            var reader = cmd1.ExecuteReader();
            if (reader.Read()) rakim = reader.GetDouble(0);
            c1.Close();

            SqlConnection c2 = new SqlConnection(con);
            SqlCommand cmd2 = c2.CreateCommand();
            cmd2.CommandText = "select count(ENTRY.ID) from ENTRY inner join WRITER on ENTRY.WriterID=WRITER.ID and WRITER.DepartmentID=@id";
            cmd2.Parameters.AddWithValue(@"id", ID);
            c2.Open();
            var reader2 = cmd2.ExecuteReader();
            if (reader2.Read()) entry = reader2.GetInt32(0);
            c2.Close();

            SqlConnection c3 = new SqlConnection(con);
            SqlCommand cmd3 = c3.CreateCommand();
            cmd3.CommandText = "select count(ID) from WRITER where DepartmentID=@id";
            cmd3.Parameters.AddWithValue(@"id", ID);
            c3.Open();
            var reader3 = cmd3.ExecuteReader();
            if (reader3.Read()) nufus = reader3.GetInt32(0);
            c3.Close();


            lbl_adi.InnerText = getDepartment(name);
            lbl_baskan.InnerText = headname;
            lbl_nufus.InnerText = nufus.ToString();
            lbl_rakim.InnerText = rakim.ToString();
            lbl_entry.InnerText = entry.ToString();

        }
        public String getTitleName(String url)
        {
            String result = null;
            SqlConnection con3 = new SqlConnection(connectionStrings.bedir);
            var cmd3 = con3.CreateCommand();
            cmd3.CommandText = "select Name from TITLE where Url = @url";
            cmd3.Parameters.AddWithValue(@"url", url);
            con3.Open();
            var rdr3 = cmd3.ExecuteReader();
            if (rdr3.Read())
            {
                result = rdr3.GetString(0);
            }
            else
            {
                result = "başlık bulunamadı!";
            }
            con3.Close();
            return result;
        }
        protected void fill()
        {
            DataSet ds_title = loadSolKanat();
            title_repeater.DataSource = ds_title;
            title_repeater.DataBind();

           
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

        private DataSet loadSolKanat()
        {
            SqlConnection con2 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select Top 25 Name from TITLE where Visible='True' order by LastUpdate asc", con2);
            // da.SelectCommand.Parameters.AddWithValue(@"name", title);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

       
        private DataSet GetData(String url)
        {
            SqlConnection con1 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select * from ENTRY where TitleID=(select ID from TITLE where Url= @url)", con1);
            da.SelectCommand.Parameters.AddWithValue(@"url", url);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        private DataSet GetOneEntry(int number)
        {
            SqlConnection con1 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select * from ENTRY where ID=@number", con1);
            da.SelectCommand.Parameters.AddWithValue(@"number", number);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
       
        protected int getWriterID(String writerName)
        {
            int result;
            var connection = new SqlConnection(connectionStrings.bedir);
            var command = connection.CreateCommand();
            command.CommandText = "select ID from WRITER where Name = @name";
            command.Parameters.AddWithValue("@name", writerName);

            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                result = reader.GetInt32(0);
            }
            else
            {
                result = 0;
            }
            connection.Close();
            return result;
        }
        protected int getTitleID(String titleName)
        {
            int result;
            var connection = new SqlConnection(connectionStrings.bedir);
            var command = connection.CreateCommand();
            command.CommandText = "select ID, IsActive from TITLE where Name=@title";
            command.Parameters.AddWithValue("@title", titleName);

            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetBoolean(1))
                {
                    result = reader.GetInt32(0);
                }
                else
                {
                    result = -1;
                }
            }
            else
            {
                result = 0;
            }
            connection.Close();
            return result;

        }

        protected void btn_left_title_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            String title = Helper.SEOUrl(btn.Text);
            Response.Redirect(title);
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
            String url = "~/entry/" + Helper.SEOUrl(key);
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
                if (Session["username"] != null && getSeniority(Session["username"].ToString()) != 1)
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
        String boolEvetHayir(Boolean x)
        {
            if (x) return "evet";
            return "hayır";
        }
        String getWriter(int id)
        {
            String result = null;
            SqlConnection con2 = new SqlConnection(connectionStrings.bedir);
            var cmd2 = con2.CreateCommand();
            cmd2.CommandText = "select Name from WRITER where ID = @id";
            cmd2.Parameters.AddWithValue(@"id", id);
            con2.Open();
            var rdr2 = cmd2.ExecuteReader();
            if (rdr2.Read())
            {
                result = rdr2.GetString(0);
            }
            else
            {
                result = "yazar bulunamadı!";
            }
            con2.Close();
            return result;
        }
        String getDepartment(String url)
        {
            String result = null;
            SqlConnection con2 = new SqlConnection(connectionStrings.bedir);
            var cmd2 = con2.CreateCommand();
            cmd2.CommandText = "select Name, Head from DEPARTMENT where Url = @id";
            cmd2.Parameters.AddWithValue(@"id", url);
            con2.Open();
            var rdr2 = cmd2.ExecuteReader();
            if (rdr2.Read())
            {
                result = rdr2.GetString(0);
                head = rdr2.GetInt32(1);
            }
            else
            {
                result = "oda bulunamadı!";
            }
            con2.Close();
            return result;
        }
        String getTitle(int id)
        {
            String result = null;
            SqlConnection con3 = new SqlConnection(connectionStrings.bedir);
            var cmd3 = con3.CreateCommand();
            cmd3.CommandText = "select Name from TITLE where ID = @id";
            cmd3.Parameters.AddWithValue(@"id", id);
            con3.Open();
            var rdr3 = cmd3.ExecuteReader();
            if (rdr3.Read())
            {
                result = rdr3.GetString(0);
            }
            else
            {
                result = "başlık bulunamadı!";
            }
            con3.Close();
            return result;
        }

        protected void btn_new_title_Click(object sender, EventArgs e)
        {
            String name = txt_user_search.Text;
            String url = Helper.SEOUrl(txt_user_search.Text);
            DateTime date = DateTime.Now;
            int writer_id = getWriterID(Session["username"].ToString());
            int dep_id = getDepartmentID(Session["username"].ToString());
            SqlConnection con1 = new SqlConnection(con);
            SqlCommand cmd1 = con1.CreateCommand();
            cmd1.CommandText = "insert into TITLE (Name, Date, LastUpdate, Visible, IsActive, Useful, Useless, Url, WriterID, DepartmentID) values (@name, @date, @lastupdate, 1, 1, 0, 0, @url, @wid, @did)";
            cmd1.Parameters.AddWithValue(@"name", name);
            cmd1.Parameters.AddWithValue(@"date", date);
            cmd1.Parameters.AddWithValue(@"lastupdate", date);
            cmd1.Parameters.AddWithValue(@"url", url);
            cmd1.Parameters.AddWithValue(@"wid", writer_id);
            cmd1.Parameters.AddWithValue(@"did", dep_id);
            con1.Open();
            var exe = cmd1.ExecuteNonQuery();
            if (exe > 0)
            {
                Response.Redirect("~/" + url);
            }
            else lbl_user_search.Text = "başlık tam açılamadı. sıkışmış olmalı";

        }
        protected int getDepartmentID(String username)
        {
            int result = 0;
            SqlConnection c1 = new SqlConnection(con);
            SqlCommand cmd1 = c1.CreateCommand();
            cmd1.CommandText = "select ID from DEPARTMENT where Name=@name";
            cmd1.Parameters.AddWithValue(@"name", username);
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

    }//master.cs

}