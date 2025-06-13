<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MultiLocations.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login MultiLocations</title>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE-edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen"/>
    <link href="../css/login.css" rel="stylesheet"/>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <main class="form-signin">
        <form id="form1" runat="server">
            <div>
                <h2 class="form-signin-heading">Connexion Multi-Locations</h2>
                    <label for="txtUsername">Nom d'utilisateur</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Entrer nom d'utilisateur" required="true" />
                        <br />
                    <label for="txtPassword">Mot de passe</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Entrer mot de passe" required="true" />
                        <br />
                    <asp:Button ID="btnLogin" Text="Se connecter" runat="server" Class="btn btn-primary" OnClick="btnLogin_Click" />
                        <br />
                        <br />
                    <div id="dvMessage" runat="server" visible="false" class="alert alert-danger">
                        <strong>Erreur!</strong>
                        <asp:Label ID="lblMessage" runat="server" />
                    </div>
            </div>
        </form>
    </main>
</body>
</html>
