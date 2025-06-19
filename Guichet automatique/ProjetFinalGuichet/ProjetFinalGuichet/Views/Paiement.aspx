<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Paiement.aspx.cs" Inherits="ProjetFinalGuichet.Views.Paiement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Paiement de factures</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../css/paiement.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <div class="wrapper">
        <form id="formPaiement" runat="server" class="form-paiement">
            <h5 class="form-signin-heading">Paiement de la facture :</h5>
            <label>Liste des factures : </label>
            <br />
            <div class="form-group">
                <asp:DropDownList CssClass="form-control" ID="dropdownFactures" runat="server" require="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownFactures_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <label for="inpMontant">Montant du paiement :</label>
            <br />
            <div class="form-group">
                <asp:TextBox ID="txtMontant" type="number" runat="server" class="form-control" min="0" value="0.00" step=".01"/> 
            </div>
            <div class="btn-group">
                <div>
                    <asp:Button ID="btnConfirmer" Text="Confirmer le paiement" runat="server" CssClass="btn btn-primary" OnClick="btnConfirmer_Click" Enabled="false" />
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