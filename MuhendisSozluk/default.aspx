    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MuhendisSozluk._default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title id="title" runat="server"></title>
    <link href="~/default.css" rel="stylesheet" />

</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>

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
        <div class=" entries">
            <div class="title_name" style="width: 100%; height: 30px; float: left; text-align: center; line-height: 30px;">
                <asp:Label ID="lbl_default_title_name" runat="server" Font-Bold="true" Text="başlık"></asp:Label>
            </div>

            <div style="width: 100%; height: auto">
                <asp:Repeater ID="entry_repeater" runat="server">
                    <ItemTemplate>
                        <div class="cover" style="width: 100%; float: right; margin-top: 20px; background-color: #d9d9d9">
                            <div class="entry" style="width: 100%; height: auto; padding: 5px">
                                <p id="lbl_rpt_entry_content" runat="server"><%#Eval("Contents")%> </p>

                            </div>
                            <div class="stats" style="width: 100%; height: 20px; margin-bottom: 5px; margin-top: 20px;">
                                <asp:Button ID="entry_like" runat="server" Width="5%" Height="100%" ForeColor="Green" BackColor="Transparent" Text="+" Font-Size="Large" BorderStyle="None" Font-Bold="true" />
                                <asp:Button ID="entry_dislike" runat="server" Width="5%" Height="100%" ForeColor="Red" BackColor="Transparent" Font-Size="Large" BorderStyle="None" Text="-" Font-Bold="true" />
                                <asp:Button ID="entry_fav" runat="server" Width="5%" Height="100%" ForeColor="Blue" BackColor="Transparent" Font-Size="Large" BorderStyle="None" Text="#" Font-Bold="true" />
                                <asp:Label ID="entry_rating" runat="server" Width="15%" Height="100%" ForeColor="Gray" BackColor="Transparent" BorderStyle="None" Text='<% #Eval("Rating") %>' />
                                <asp:Label ID="entry_date" runat="server" Width="24%" Height="100%" ForeColor="Gray" BackColor="Transparent" BorderStyle="None" Text='<% #Eval("Date") %>' />
                                <asp:Label ID="entry_writer" runat="server" Width="34%" Height="100%" ForeColor="Gray" BackColor="Transparent" BorderStyle="None" Text='<% #Eval("WriterName") %>' />
                                <asp:Button ID="entry_complain" runat="server" Width="6%" Height="100%" ForeColor="Orange" BackColor="Transparent" Font-Size="Large" BorderStyle="None" Text="!?" Font-Bold="true" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <%--  <asp:TextBox Class="txt_write_title" ID="txt_write_title" runat="server" MaxLength="100" TextMode="MultiLine" Width="99%" ></asp:TextBox>
                <div class="entry_send_button">
                    <asp:Button ID="btn_title_send" runat="server" BorderStyle="None" Text="başlık aç" Width="100px" Height="100%" BackColor="#ffffe6" OnClick="btn_title_send_Click"/>
                    <asp:Label ID="lbl_title_send" runat="server"></asp:Label>
                </div>--%> 
            <div class="bkz" style="margin-top: 15px">
            <asp:UpdatePanel ID="panelbkz" runat="server">
                <ContentTemplate>
                        <asp:TextBox ID="txt_bkz" placeholder="(bkz: )" runat="server" Width="70%" BorderStyle="None" Height="20px"></asp:TextBox>

                        <asp:Button ID="btn_bkz" runat="server" Width="50px" Height="20px" Text="ekle" OnClick="btn_bkz_Click" />

                        <asp:TextBox ID="div_write_entry" runat="server" Width="95%" TextMode="MultiLine"></asp:TextBox>
                  

                </ContentTemplate>
               <%-- <Triggers>
                    <asp:asyncPostBackTrigger ControlID="btn_bkz" EventName="Click"/>
                </Triggers>--%>
            </asp:UpdatePanel>
          </div>
                   <%-- <div contenteditable="true"   runat="server"  style="width: 98%; margin-top: 10px; height: 50px; padding: 5px; margin-left: auto; margin-right: auto; background-color: white">
                    </div>--%>
                   
        


            <%--                <asp:TextBox Class="txt_write_entry" ID="txt_write_entry" runat="server" MaxLength="100" TextMode="MultiLine"  placeholder="entry girebilmek için giriş yapınız." Width="99%" ></asp:TextBox>
            --%>
            <div class="entry_send_button">

                <asp:Button ID="btn_entry_send" runat="server" BorderStyle="None" Text="gönder" Width="100px" Height="100%" OnClick="btn_entry_send_Click" BackColor="#ffffe6" />

                <asp:Label ID="lbl_entrysend_status" runat="server"></asp:Label>
            </div>


            <div class="footer">
                <h4 style="margin-top: 30px;">Antiochus Software 2015 </h4>
                <p>Her Hakkı Saklıdır. </p>
            </div>
        </div>



    </form>
   
</body>
</html>

