<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transfert.aspx.cs" Inherits="ProjetFinalGuichet.Views.Transfert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfert</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../css/transfert.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <div class="wrapper">
        <form id="formTransfert" runat="server" class="form-transfert">
            <h5 class="form-signin-heading">Transfert du compte <%: typeCompte.Nom %> <%: compte.Nom %> vers:</h5>
            <label>Compte destinataire : </label>
            <br />
            <div class="form-group">
                <asp:DropDownList CssClass="form-control" ID="dropdownComptes" runat="server" require="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownComptes_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <label for="inpMontant">Montant du transfert :</label>
            <br />
            <div class="form-group">
                <asp:TextBox ID="txtMontant" type="number" runat="server" class="form-control" min="0" value="0.00" step=".01"/> 
            </div>
            <div class="btn-group">
                <div>
                    <asp:Button ID="btnConfirmer" Text="Confirmer le transfert" runat="server" CssClass="btn btn-primary" OnClick="btnConfirmer_Click" Enabled="false" />
                </div>
                <div>
                    <asp:Button ID="btnAnnuler" Text="Annuler la transaction/Retour" runat="server" CssClass="btn btn-primary " OnClick="btnAnnuler_Click" CausesValidation="false" />
                </div>
            </div>
        <div id="dvMessageErreur" runat="server" visible="false" class="alert alert-danger">
            <strong>Erreur!</strong>
            <asp:Label ID="lblMessageErreur" runat="server" />
        </div>
        <div id="dvMessageSucces" runat="server" visible="false" class="alert alert-success">
            <strong>Succès!</strong>
            <asp:Label ID="lblMessageSucces" runat="server" />
        </div>
        </form>
    </div>
</body>
</html>
