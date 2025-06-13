<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormLocation.aspx.cs" Inherits="MultiLocations.FormLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Formulaire de location</title>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE-edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen"/>
    <link href="../css/location.css" rel="stylesheet"/>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <form runat="server" class="grid">
        <h2 class="header">Hello World!</h2>
        <div class="first-content">
            <div>
                <label for="dropdownLocation" class="default-label jc-fs">Liste de locations</label>
                <asp:DropDownList CssClass="form-control default-input" ID="dropdownLocation" runat="server" OnSelectedIndexChanged="dropdownLocation_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList> 
            </div>
        </div>
        <h3 class="header">Informations du contrat</h3>
        <div class="display-contents">
            <label for="dropdownNIV" class="default-label">NIV du véhicule :</label>
            <asp:DropDownList CssClass="form-control default-input" ID="dropdownNIV" runat="server" required="true" AppendDataBoundItems="true">
                </asp:DropDownList>
        </div>
        <div class="display-contents">
            <label for="txtValeur" class="default-label">Valeur courante du véhicule (en $):</label>
            <asp:TextBox ID="txtValeur" runat="server" CssClass="form-control default-input number" placeholder="0.00" required="true"/>
        </div>
        <div class="display-contents">
            <label for="txtKiloDebut" class="default-label">Kilométrage au début de la location :</label>
            <asp:TextBox ID="txtKiloDebut" runat="server" CssClass="form-control default-input" TextMode="Number" placeholder="0" required="true"/>
        </div>
        <div class="display-contents">
            <label for="txtKiloFin" class="default-label">Kilométrage à la fin de la location :</label>
            <asp:TextBox ID="txtKiloFin" runat="server" CssClass="form-control default-input" TextMode="Number" placeholder="0"/>
        </div>
        <div class="display-contents">
            <label for="chkNeuf" class="default-label">Est-ce un véhicule neuf? :</label>
            <asp:CheckBox ID="chkNeuf" CssClass="form-control default-input" runat="server" required="true"/>
        </div>
        <div class="display-contents">
            <label for="dropdownClient" class="default-label">Nom du client :</label>
            <asp:DropDownList CssClass="form-control default-input" ID="dropdownClient" runat="server" required="true" AppendDataBoundItems="true">
                </asp:DropDownList>
        </div>
        <h3 class="header">Termes de location :</h3>
        <div class="display-contents">
            <label for="iptStartDate" class="default-label">Date de début du contrat : </label>
            <input type="date" id="iptStartDate" class="form-control default-input" name="contract-start" runat="server" required/>
        </div>
        <div class="display-contents">
            <label for="ipEndDate" class="default-label">Date de fin du contrat : </label>
            <input type="date" id="iptEndDate" class="form-control default-input" name="contract-end" runat="server"/>
        </div>
        <div class="display-contents">
            <label for="ipt1stPayment" class="default-label">Date du premier paiement : </label>
            <input type="date" id="ipt1stPayment" class="form-control default-input" name="first-payment" runat="server" required/>
        </div>
        <div class="display-contents">
            <label for="txtPaiement" class="default-label">Paiement mensuel (en $):</label>
            <asp:TextBox ID="txtPaiement" runat="server" CssClass="form-control default-input number" placeholder="0.00" required="true" />
        </div>
        <div class="display-contents">
            <label for="txtNbPaiement" class="default-label">Nombre de paiements :</label>
            <asp:TextBox ID="txtNbPaiement" runat="server" CssClass="form-control default-input" placeholder="0" TextMode="Number" required="true"/>
        </div>
         <div class="display-contents">
            <label for="txtSurprime" class="default-label">Taux de surprime (en $):</label>
            <asp:TextBox ID="txtSurprime" runat="server" CssClass="form-control default-input number" placeholder="0.00" required="true"/>
        </div>
        <div class="display-contents">
            <label for="txtKiloPermis" class="default-label">Kilométrage permis :</label>
            <asp:TextBox ID="txtKiloPermis" runat="server" CssClass="form-control default-input" placeholder="0" TextMode="Number" required="true"/>
        </div>
        <div class="display-contents">
            <label for="txtNbAnnees" class="default-label">Durée de la location en année :</label>
            <asp:TextBox ID="txtNbAnnees" runat="server" CssClass="form-control default-input" placeholder="0" TextMode="Number" required="true"/>
        </div>
        <div class="footer">
            <asp:Button ID="btnNouveau" runat="server" Text="Nouveau" CssClass="btn btn-primary" OnClick="btnNouveau_Click" UseSubmitBehavior="false" AutoPostBack="false"/>
            <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" CssClass="btn btn-primary" OnClick="btnEnregistrer_Click" Enabled="false"/>
            <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" CssClass="btn btn-primary" OnClick="btnAnnuler_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnLogout" runat="server" Text="Se déconnecter" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnLogout_Click"/>
        </div>
    </form>
</body>
</html>
