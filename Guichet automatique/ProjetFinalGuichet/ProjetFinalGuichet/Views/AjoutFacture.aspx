<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjoutFacture.aspx.cs" Inherits="ProjetFinalGuichet.Views.AjoutFacture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ajout d'une facture</title>
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
            <h5 class="form-signin-heading">Ajout d'une facture :</h5>
            <label for="txtNom">Nom de la facture :</label>
            <div class="form-group">
                <asp:TextBox ID="txtNom" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="reqFieldNom" runat="server" ControlToValidate="txtNom" ErrorMessage="Le nom de la facture est un champ requis." ForeColor="Red" />
            </div>
            <label for="txtFournisseur">Nom du fournisseur :</label>
            <div class="form-group">
                <asp:TextBox ID="txtFournisseur" runat="server" CssClass="form-control" />
            </div>
            <div class="btn-group">
                <div>
                    <asp:Button ID="btnConfirmer" Text="Confirmer l'ajout d'une facture" runat="server" CssClass="btn btn-primary" OnClick="btnConfirmer_Click" />
                </div>
                <div>
                    <asp:Button ID="btnAnnuler" Text="Annuler l'ajout/Retour" runat="server" CssClass="btn btn-primary" OnClick="btnAnnuler_Click" CausesValidation="false" />
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
