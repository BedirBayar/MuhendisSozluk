<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oda.aspx.cs" Inherits="MuhendisSozluk.Department.Oda" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title id="title" runat="server"></title>
    <link href="~/default.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script_entries" runat="server"></asp:ScriptManager>


        <div class="top">
            <div class="logo">
            </div>

            <div class="search">
                <div style="width: 100%; height: 30%; text-align: center">
                    <asp:TextBox ID="txt_user_search" runat="server" placeholder="başlık, #entry ya da @yazar" Width="250px" Height="20px" BorderStyle="Solid"></asp:TextBox>
                    <asp:Button ID="btn_user_search" runat="server" Width="70px" Height="25px" Text="ara" Font-Size="Large" BorderStyle="None" OnClick="btn_user_search_Click" />

                </div>
                <div style="width: 100%; height: 20%;">
                    <asp:Label ID="lbl_user_search" runat="server"> </asp:Label>
                </div>
                <div style="width: 100%; height: 49%; line-height: 45px;">
                    <asp:Button ID="btn_new_title" runat="server" Visible="false" Text="başlığı aç" OnClick="btn_new_title_Click" />
                </div>
            </div>
            <div class="profile">
                <div class="buttons">

                    <asp:Button ID="btn_default_profile" runat="server" BorderStyle="None" BackColor="#ffffe6" Width="49%" Height="100%" Font-Size="Large" Text="Kayıt Ol" OnClick="btn_default_profile_Click" />
                    <asp:Button ID="btn_default_loginout" runat="server" BorderStyle="None" BackColor="#ffffe6" Width="49%" Height="100%" Font-Size="Large" Text="Giriş Yap" OnClick="btn_default_loginout_Click" />


                </div>
                <div class="department_info"></div>
                <div class="rating_info"></div>
            </div>


        </div>
        <div class="categories">
        </div>
        <div class="solkanat">

            <asp:Repeater ID="title_repeater" runat="server">
                <ItemTemplate>
                    <div style="width: 100%; height: 30px; border-bottom: 1px dashed gray; padding-top: 10px; float: left; background-color: #777777">
                        <%--<asp:LinkButton ID="btn_left_title" Font-Underline="false" OnClick="btn_left_title_Click" CssClass="btn_left_title" runat="server" Width="100%"  ForeColor="White" ></asp:LinkButton>--%>
                        <a href='<%#string.Format("{0}", MuhendisSozluk.Helper.SEOUrl(Eval("Name").ToString()))%>' style="color: #9e3232; text-decoration: none; padding-left: 5px;"><%#Eval("Name")%></a>
                        <%--<asp:Label ID="lbl_title_entry_today" runat="server" Width="19%" ForeColor="#d9d9d9" Text='<% #Eval("Today") %>'></asp:Label> --%>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <div class="entries">
        <div class="oda">
            <div class="oda_adi" style="width:100%; align-content:center; text-align:center; float:left; height:50px"><label id="lbl_adi" runat="server" style="width:100%; line-height:50px; font-size:large"></label></div>
            <table id="oda_table" style="width:100%; padding:20px;">
                <tr><td style ="width:49%; height:30px;">başkan</td><td style ="width:49%; height:30px;"><label id="lbl_baskan" runat="server"></label></td></tr>
                <tr><td style ="width:49%; height:30px;">nüfus</td><td style ="width:49%; height:30px;"><label id="lbl_nufus" runat="server"></label></td></tr>
                <tr><td style ="width:49%; height:30px;">rakım</td><td style ="width:49%; height:30px;"><label id="lbl_rakim" runat="server"></label></td></tr>
                <tr><td style ="width:49%; height:30px;">entry sayısı</td><td style ="width:49%; height:30px;"><label id="lbl_entry" runat="server"></label></td></tr>
            </table>
        </div>


        <div class="footer">
            <h4 style="margin-top: 30px;">Antiochus Software 2015 </h4>
            <p>Her Hakkı Saklıdır. </p>
        </div>
          </div>
        <div class="sagkanat">
        </div>





    </form>

</body>
</html>
