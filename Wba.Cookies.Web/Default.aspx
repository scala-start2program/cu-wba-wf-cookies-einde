<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Wba.Cookies.Web.Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cookies</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="card" style="margin-top: 100px;">
                <div class="card-header bg-dark text-light">
                    <h2>Mijn gegevens</h2>
                </div>
                <div class="card-body">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" style="width: 150px;">Naam : </span>
                        </div>
                        <asp:TextBox ID="txtNaam" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" style="width: 150px;">Geboortedatum : </span>
                        </div>
                        <asp:TextBox ID="txtGeboortedatum" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" style="width: 150px;">Taal : </span>
                        </div>
                        <asp:DropDownList ID="cmbTaal" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Nederlands" Value="NL"></asp:ListItem>
                            <asp:ListItem Text="Frans" Value="FR"></asp:ListItem>
                            <asp:ListItem Text="Engels" Value="EN"></asp:ListItem>
                            <asp:ListItem Text="Duits" Value="DU"></asp:ListItem>
                            <asp:ListItem Text="Italiaans" Value="IT"></asp:ListItem>
                            <asp:ListItem Text="Spaans" Value="SP"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="card-footer bg-light">
                    <div class="btn-group">
                        <asp:LinkButton ID="lnkSave" runat="server"
                            CssClass="btn btn-success" OnClick="lnkSave_Click">
                            Save cookies
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server"
                            CssClass="btn btn-danger" OnClick="lnkDelete_Click">
                            Delete cookies
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkRefresh" runat="server" 
                            CssClass="btn btn-warning" OnClick="lnkRefresh_Click">
                            Reload page
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
