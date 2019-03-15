<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MuhendisSozluk.WebForm1"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="panel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" >
                <ContentTemplate>
                   <asp:TextBox ID="txt" runat="server"></asp:TextBox> <br />
                    <asp:Button ID="btn" runat="server" Text="Ekle" OnClick="btn_Click"/> <br />
                    <asp:Label ID="lbl" runat="server"></asp:Label>
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn" EventName="Click"/>
                </Triggers>
            
            </asp:UpdatePanel>

    </form>
</body>
</html>
