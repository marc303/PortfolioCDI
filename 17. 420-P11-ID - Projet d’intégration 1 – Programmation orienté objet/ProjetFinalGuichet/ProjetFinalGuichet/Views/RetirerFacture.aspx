<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetirerFacture.aspx.cs" Inherits="ProjetFinalGuichet.Views.RetirerFacture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supprimer une facture</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../css/facture.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <div class="wrapper">
        <form id="formFacture" runat="server" class="form-facture">
            <h5 class="form-signin-heading">Supprimer la facture suivante :</h5>
            <label>Liste des factures : </label>
            <br />
            <div class="form-group">
                <asp:DropDownList CssClass="form-control" ID="dropdownFactures" runat="server" require="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownFactures_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="btn-group">
                <div>
                    <asp:Button ID="btnConfirmer" Text="Confirmer la suppression" runat="server" CssClass="btn btn-primary" OnClick="btnConfirmer_Click" Enabled="false" />
                </div>
                <div>
                    <asp:Button ID="btnAnnuler" Text="Annuler la suppression/Retour" runat="server" CssClass="btn btn-primary " OnClick="btnAnnuler_Click" CausesValidation="false"/>
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
