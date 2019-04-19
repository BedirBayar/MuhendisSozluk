<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" ValidateRequest="false" Inherits="MuhendisSozluk._default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title id="title" runat="server"></title>
    <link href="~/default.css" rel="stylesheet" />

</head>
<body>
         <form id="form1" runat="server">
           


        <div class="top">
            <div class="logo" style="align-content:center; text-align:center;">
               <a href="muhendis-sozluk" style="line-height:30px; font-family:Consolas; font-size:x-large; color:#555656; text-decoration:none;"> mühendis<br /> sözlük<br />v0.7.0 
                   <%--<img src="/Pictures/mainlogo.png" style="width:100%; height:100%;"/>--%></a>
            </div>

            <div class="search">
                <div style="width: 100%; height: 30%; border-top:10px; text-align: center">
                    <asp:TextBox ID="txt_user_search" runat="server" placeholder="başlık, #entry ya da @yazar" Width="250px" Height="20px" BorderStyle="None"></asp:TextBox>
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

                    <asp:Button ID="btn_default_profile" runat="server" BorderStyle="None" BackColor="#555656" ForeColor="White" Width="49%" Height="100%" Font-Size="Large" Text="Kayıt Ol" OnClick="btn_default_profile_Click" />
                    <asp:Button ID="btn_default_loginout" runat="server" BorderStyle="None" BackColor="#555656" ForeColor="White" Width="49%" Height="100%" Font-Size="Large" Text="Giriş Yap" OnClick="btn_default_loginout_Click" />


                </div>
                <div class="department_info" style="text-align:center"><asp:Label ID="lbl_department_info" runat="server" ForeColor="#555656"></asp:Label></div>
                <div class="rating_info" style="text-align:center"> <asp:Label ID="lbl_rating" runat="server" ForeColor="#555656"></asp:Label></div>
            </div>


        </div>
       <%-- <div class="categories">
        </div>--%>
        <div class="solkanat">

            <asp:Repeater ID="title_repeater" runat="server">
                <ItemTemplate>
                    <div style="width: 100%; height:auto; border-bottom: 1px dashed gray; padding-top: 10px; float: left; background-color: #aaaaaa">
                        <%--<asp:LinkButton ID="btn_left_title" Font-Underline="false" OnClick="btn_left_title_Click" CssClass="btn_left_title" runat="server" Width="100%"  ForeColor="White" ></asp:LinkButton>--%>
                        <a href='<%#string.Format("{0}", MuhendisSozluk.Helper.SEOUrl(Eval("Name").ToString()))%>' style="color: black; font-size:large; font-family:Consolas; text-decoration: none; padding-left: 5px;"><%#Eval("Name")%></a>
                        <%--<asp:Label ID="lbl_title_entry_today" runat="server" Width="19%" ForeColor="#d9d9d9" Text='<% #Eval("Today") %>'></asp:Label> --%>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <div class=" entries">
           
            <div class="title_name" style="width: 100%; height: 30px; float: left; text-align:center; background-color:#cedcdd; line-height: 30px;">
                <asp:Label ID="lbl_default_title_name" runat="server" Font-Bold="true" Text="mühendis sözlük"></asp:Label>
            </div>
              <asp:ScriptManager ID="script_entries" runat="server" ></asp:ScriptManager>
                  <asp:UpdatePanel ID="update_entries" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="btn_entry_send" EventName="Click" />
                      </Triggers>
                      <ContentTemplate>
                             <div class="page_selector" style=" width:100%; height:20px; float:left; line-height:20px; text-align:center">
                                 <asp:Button ID="btn_previous_page" runat="server" Text="<< önceki" OnClick="btn_previous_page_Click"></asp:Button>
                                 <asp:Label ID="lbl_pagenumber" runat="server" Text="butonları deneyebilirsiniz."></asp:Label>
                                 <asp:Button ID="btn_next_page" runat="server" Text="sonraki >>" OnClick="btn_next_page_Click"></asp:Button>
                             </div>
                <asp:Repeater ID="entry_repeater" runat="server">
                    <ItemTemplate>
                        <div class="cover" style="width: 100%; float: right; margin-top: 20px; height:auto; background-color: #d9d9d9">
                            <div class="entry" style="width: 100%; height: auto; padding: 5px">
                                <p id="lbl_rpt_entry_content" runat="server" style="font-family:Consolas;"><%#Eval("Contents")%> </p>

                            </div>
                            <div class="stats" style="width: 100%; height: 20px; margin-bottom: 5px; margin-top: 5px;">
                                <asp:Button ID="entry_like" runat="server" Visible="false" Width="5%" Height="100%" ForeColor="Green" BackColor="Transparent" Text="+" Font-Size="Large" BorderStyle="None" Font-Bold="true" />
                                <asp:Button ID="entry_dislike" runat="server" Visible="false" Width="5%" Height="100%" ForeColor="Red" BackColor="Transparent" Font-Size="Large" BorderStyle="None" Text="-" Font-Bold="true" />
                                <asp:Button ID="entry_fav" runat="server" Visible="false" Width="5%" Height="100%" ForeColor="Blue" BackColor="Transparent" Font-Size="Large" BorderStyle="None" Text="#" Font-Bold="true" />
                                <asp:Label ID="entry_rating" runat="server" Visible="false" Width="15%" Height="100%" ForeColor="Gray" BackColor="Transparent" BorderStyle="None" Text='<% #Eval("Rating") %>' />
                                <asp:Label ID="entry_date" runat="server" Width="30%" Height="100%" ForeColor="Gray" BackColor="Transparent" BorderStyle="None" Text='<% #Eval("Date") %>' />
                                 <a href='<%#"../yazar/"+MuhendisSozluk.Helper.SEOUrl(Eval("WriterName").ToString()) %>' style="text-decoration:none; color:#555555"><%#Eval("WriterName") %></a>
                                <asp:Button ID="entry_complain" runat="server" Visible="false" Width="6%" Height="100%" ForeColor="Orange" BackColor="Transparent" Font-Size="Large" BorderStyle="None" Text="!?" Font-Bold="true" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
           </ContentTemplate>
                  </asp:UpdatePanel>
           
            
            <%--  <asp:TextBox Class="txt_write_title" ID="txt_write_title" runat="server" MaxLength="100" TextMode="MultiLine" Width="99%" ></asp:TextBox>
                <div class="entry_send_button">
                    <asp:Button ID="btn_title_send" runat="server" BorderStyle="None" Text=" aç" Width="100px" Height="100%" BackColor="#ffffe6" OnClick="btn_title_send_Click"/>
                    <asp:Label ID="lbl_title_send" runat="server"></asp:Label>
                </div>--%>
            <asp:UpdatePanel ID="panelbkz" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">

                    <ContentTemplate>
                 
            <div class="bkz" style="margin-top: 15px">
            
                        <asp:TextBox ID="txt_bkz" placeholder="(bkz: )" runat="server" Width="90%" BorderStyle="None" Height="20px"></asp:TextBox>

                        <asp:Button ID="btn_bkz" runat="server" Width="50px" Height="20px" Text="ekle" OnClick="btn_bkz_Click" />

                       <div style="margin-top:10px"> <asp:TextBox ID="div_write_entry" runat="server" placeholder="giriş yaptıysanız entry girebilirsiniz." Font-Names="Consolas" Width="99%" Height="200px" TextMode="MultiLine"></asp:TextBox></div>
                        <%--  <div contenteditable="true" id="div_write_entry" runat="server"  style="width: 98%; margin-top: 10px; height: 50px; padding: 5px; margin-left: auto; margin-right: auto; background-color: white">--%>

                    <%--</div>--%>
                  
                
            </div>
              </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_bkz" EventName="Click" />
                    </Triggers>

                </asp:UpdatePanel>




            <%--                <asp:TextBox Class="txt_write_entry" ID="txt_write_entry" runat="server" MaxLength="100" TextMode="MultiLine"  placeholder="entry girebilmek için giriş yapınız." Width="99%" ></asp:TextBox>
            --%>
            <br />
            <div class="entry_send_button">

                <asp:Button ID="btn_entry_send" runat="server" BorderStyle="None" Text="gönder" Font-Size="Large" Width="100px" Height="100%" OnClick="btn_entry_send_Click" BackColor="#aaaaaa" />

                <asp:Label ID="lbl_entrysend_status" runat="server"></asp:Label>
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

