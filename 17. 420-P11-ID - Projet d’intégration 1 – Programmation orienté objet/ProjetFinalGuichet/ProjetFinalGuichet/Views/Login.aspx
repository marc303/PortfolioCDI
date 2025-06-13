<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjetFinalGuichet.Views.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Guichet</title>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE-edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen"/>
    <link href="../css/login.css" rel="stylesheet"/>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
    <body>
        <div class="wrapper">
            <form id="form1" runat="server" class="form-signin">
                <h2 class="form-signin-heading">Connexion Guichet</h2>
                    <label for="txtCodeClient">Code client:</label>
                    <br />
                <div class="form-group">
                    <asp:TextBox class="form-control" id="txtCodeClient" placeholder="Saisissez votre code client" maxlength="6" runat="server" required="true"/>
                    <div class="alert_placeholder">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCodeClient" ErrorMessage="Seulement un code client à 6 chiffres est valide." ControlToValidate="txtCodeClient" 
                            runat="server" ValidationExpression="^\d{6}$" CssClass="alert alert-danger"/>
                    </div>
                </div>
                    <label for="txtNIP">Numéro d'identification personnel (NIP): </label>
                <br />
                <div class="form-group">
                    <asp:TextBox class="form-control" id="txtNIP" TextMode="Password" placeholder="Numéro d'identification personnel (NIP)" maxlength="4" runat="server" required="true"/>
                    <div class="alert_placeholder">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorNIP" ErrorMessage="Seulement un NIP à 4 chiffres est valide." ControlToValidate="txtNIP" 
                    runat="server" ValidationExpression="^\d{4}$" CssClass="alert alert-danger"/>
                    </div>
                </div>
                <asp:Button id="btnLogin" Text="Se connecter" runat="server" CssClass="btn btn-primary margin" OnClick="btnLogin_Click"/>
                <div id="dvMessageCodeClient" runat="server" visible="false" class="alert alert-danger margin">
                    <strong>Erreur!</strong>
                    <asp:Label ID="lblMessageCodeClient" runat="server" />
                </div>
                <div id="dvMessageNIP" runat="server" visible="false" class="alert alert-danger margin">
                    <strong>Erreur!</strong>
                    <asp:Label ID="lblMessageNIP" runat="server" />
                </div>
                <div id="dvTentative" runat="server" visible="false" class="alert alert-danger margin">
                    <strong>Erreur!</strong>
                    <asp:Label ID="lblTentative" runat="server" />
                </div>
            </form>
        </div>
    </body>
</html>
