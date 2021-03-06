﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MuhendisSozluk.Title;


namespace MuhendisSozluk
{
    public partial class _default : System.Web.UI.Page
    {
        static String con = connectionStrings.bedir;
        static String title2 = "muhendis-sozluk";
        static int entries=1;
        static int pageindex = 1;
        static int maxpages = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                object user = Session["username"];
                if (user != null)
                {
                    btn_default_profile.Text = "makamım";
                    btn_default_loginout.Text = "çıkış yap";
                    lbl_department_info.Text = getDepartment(getDepartmentID(user.ToString()));
                    lbl_rating.Text = getWriterRating(user.ToString()).ToString();
                }
                else
                {
                    btn_default_profile.Text = "kayıt ol";
                    btn_default_loginout.Text = "giriş yap";
                }
                fill();

                if (RouteData.Values["title"] != null)
                {
                    title2 = RouteData.Values["title"].ToString();
                    setEntries(getTitleID(getTitleName(title2)));
                    loadEntries(title2);
                    lbl_default_title_name.Text = getTitleName(title2);

                }
                else
                {
                    Response.Redirect("/muhendis-sozluk");

                }
            }
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

        public void loadEntries(String url)
        {
           
            DataSet ds = GetData(url);
            entry_repeater.DataSource = ds;
            entry_repeater.DataBind();
           
        }
        public void setEntries(int title)
        {
            int result = 0;
            SqlConnection c1 = new SqlConnection(con);
            SqlCommand cmd1 = c1.CreateCommand();
            cmd1.CommandText = "select count (ID) from ENTRY where TitleID=@id";
            cmd1.Parameters.AddWithValue(@"id", title);
            c1.Open();
            var rdr = cmd1.ExecuteReader();
            if (rdr.Read())
            {
                result = rdr.GetInt32(0);
            }
            else result = -1;
            c1.Close();
            entries = result;
            maxpages = entries / 10 + 1;
            lbl_pagenumber.Text = "1 of  " + maxpages.ToString();
        }
        public void loadOneEntry(int number)
        {
            DataSet ds = GetOneEntry(number);
            entry_repeater.DataSource = ds;
            entry_repeater.DataBind();
        }
        private DataSet GetData(String url)
        {
            int id = getTitleID(getTitleName(url));
            int start = 10 * (pageindex - 1);
            int stop = 10 * pageindex;
            SqlConnection con1 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select * from 
                (select Row_Number() over 
                 (order by ID) as RowIndex, * from ENTRY where TitleID=@id) as Sub
                 Where Sub.RowIndex > @start and Sub.RowIndex <= @stop", con1);
            da.SelectCommand.Parameters.AddWithValue(@"id", id);
            da.SelectCommand.Parameters.AddWithValue(@"start", start);
            da.SelectCommand.Parameters.AddWithValue(@"stop", stop);
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
        private DataSet fillEntriesFirst()
        {
            SqlConnection con1 = new SqlConnection(connectionStrings.bedir);
            SqlDataAdapter da = new SqlDataAdapter(@"select * from ENTRY where TitleID = (select top 1 ID from TITLE order by LastUpdate asc) and Visible='True'", con1);

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        protected void btn_entry_send_Click(object sender, EventArgs e)
        {

            object user = Session["username"];
            if (user != null)
            {
                // String a = div_write_entry.InnerText;
                // String date12 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DateTime date = DateTime.UtcNow;
                String content = div_write_entry.Text.ToString().ToLower();
                int writerid = getWriterID(user.ToString());
                int titleid = getTitleID(lbl_default_title_name.Text);
                String url = Helper.SEOUrl(getTitle(titleid));

                if (titleid != -1 && content!=null)
                {
                    var connect = new SqlConnection(con);
                    var cmd = connect.CreateCommand();
                    cmd.CommandText = "insert into ENTRY (Date, Contents, WriterID, WriterName, TitleID, TitleName, Visible, FavCount, LikeCount, DislCount) values (@date, @content, @writerid, @writername, @titleid, @titlename, 'True', 0, 0, 0)";
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@writerid", writerid);
                    cmd.Parameters.AddWithValue("@writername", user.ToString());
                    cmd.Parameters.AddWithValue("@titleid", titleid);
                    cmd.Parameters.AddWithValue("@titlename", lbl_default_title_name.Text);
                   

                    connect.Open();
                    try
                    {
                        var reader = cmd.ExecuteNonQuery();
                        TitleLayer.setTitleUpdate(titleid);
                        div_write_entry.Text = "";
                        Response.Redirect(url);


                    }
                    catch (Exception ex)
                    {
                        lbl_entrysend_status.Text = ex.Message.ToString();
                    }
                    connect.Close();
                }
                else
                {
                    lbl_entrysend_status.Text = "entry boş ya da bu başlığa entry girişi durduruldu.";
                }
            }
            else
            {
                Response.Redirect("/User/Login.aspx");
            }
        }

        protected String validateBkz(String bkz)
        {

            String url = "";
            SqlConnection con1 = new SqlConnection(con);
            SqlCommand cmd = con1.CreateCommand();
            cmd.CommandText = "select Url from TITLE where Name=@name";
            cmd.Parameters.AddWithValue(@"name", bkz);
            con1.Open();
            var rdr = cmd.ExecuteReader();
            if (rdr.Read())
                url = rdr.GetString(0);
            else
                bkz = "böyle bir başlık yok.";
            con1.Close();

            return "(bkz: " + "<a href=" + url + " style=" + "text-decoration:none" + "> " + bkz + "</a>)";


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
        protected double getWriterRating(String writerName)
        {
            double result;
            var connection = new SqlConnection(connectionStrings.bedir);
            var command = connection.CreateCommand();
            command.CommandText = "select Rating from WRITER where Name = @name";
            command.Parameters.AddWithValue("@name", writerName);

            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                result = reader.GetDouble(0);
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
            String title = btn.Text;
            loadEntries(title);
            lbl_default_title_name.Text = title;
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
        String boolEvetHayir(Boolean x) {    return x ? "Evet" : "Hayır";  }

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
        String getDepartment(int id)
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
            String name = txt_user_search.Text.ToLower();
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
            else lbl_user_search.Text = "başlık tam açılamadı.";

        }
        protected int getDepartmentID(String username)
        {
            int result = 0;
            SqlConnection c1 = new SqlConnection(con);
            SqlCommand cmd1 = c1.CreateCommand();
            cmd1.CommandText = "select DepartmentID from WRITER where Name=@name";
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

        protected void btn_bkz_Click(object sender, EventArgs e)
        {
            String bkz = txt_bkz.Text;
            String url = "";
            url += Helper.SEOUrl(bkz);
            //SqlConnection con1 = new SqlConnection(con);
            //SqlCommand cmd = con1.CreateCommand();
            //cmd.CommandText = "select Url from TITLE where Name=@name";
            //cmd.Parameters.AddWithValue(@"name", bkz);
            //con1.Open();
            //var rdr = cmd.ExecuteReader();
            //if (rdr.Read())
            //    url = rdr.GetString(0);
            //else
            //    bkz = "böyle bir başlık yok.";

            //con1.Close();

            div_write_entry.Text += "(bkz: " + "<a href=" + url + " style=" + "text-decoration:none" + "> " + bkz + "</a>)";

        }

        protected void btn_previous_page_Click(object sender, EventArgs e)
        {
            if (pageindex > 1)
            {
                pageindex--;
                if (pageindex == 1) btn_previous_page.Visible = false;
                btn_next_page.Visible = true;
                lbl_pagenumber.Text = pageindex.ToString() + " of " + maxpages.ToString(); 
                loadEntries(title2);
            }
        }

        protected void btn_next_page_Click(object sender, EventArgs e)
        {
            if (pageindex < maxpages)
            {
                pageindex++;
                if (pageindex == maxpages) btn_next_page.Visible = false;
                btn_previous_page.Visible = true;
                lbl_pagenumber.Text = pageindex.ToString() + " of " + maxpages.ToString();
                loadEntries(title2);
            }
        }
    }//master.cs

}