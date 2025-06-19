<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solde.aspx.cs" Inherits="ProjetFinalGuichet.Views.Solde" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solde et transactions du compte</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../css/transactions.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <form runat="server">
        <header class="header">
            <div class="row">
                <div id="left" class="block">
                    <asp:Button ID="btnRetour" runat="server" Text="Retour" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnRetour_Click" />
                </div>
                <div id="center" class="block">
                    <h3 class="form-signin-heading">Compte <%: typeCompte.Nom %> - <%: compte.Nom %> </h3>
                    <br />
                    <h4 class="form-signin-heading">Solde : <%: compte.Solde %>$</h4>
                    <br />
                    <h5 class="form-signin-heading">Liste des transactions pour ce compte :</h5>
                </div>
                <div id="right" class="block">
                    <asp:Button ID="btnLogout" runat="server" Text="Se Déconnecter" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnLogout_Click" />
                </div>
            </div>
            <hr />
        </header>
        <main>
            <div id="dvTransactions" runat="server">
                <div class="card-deck">
                    <asp:Repeater runat="server" ID="repeatTransaction" ItemType="ProjetFinalGuichet.Classes.Transactions">
                        <ItemTemplate>
                            <div class="card spacing">
                                <div class="card-body">
                                    <h5 class="card-title"><%# transaction.getTypeTransaction((int)DataBinder.Eval(Container.DataItem, "Type")).Nom %></h5>
                                    <div class="row">
                                        <div class="block left_card">
                                            <p class="card-text">Du compte : <%# DataBinder.Eval(Container.DataItem, "CompteDe") %></p>
                                        </div>
                                        <div class="block right_card">
                                            <p class="card-text">Vers le compte : <%# DataBinder.Eval(Container.DataItem, "CompteVers") %></p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="block left_card">
                                            <p class="card-text">Numéro de la facture : <%# DataBinder.Eval(Container.DataItem, "NumeroFacture") %></p>
                                        </div>
                                        <div class="block right_card">
                                            <p class="card-text">Montant : <%# DataBinder.Eval(Container.DataItem, "Montant") %>$</p>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="block left_card">
                                            <p class="card-text"><small class="text-muted"><%# DataBinder.Eval(Container.DataItem, "Description") %></small></p>
                                        </div>
                                        <div class="block right_card">
                                            <p class="card-text"><small class="text-muted"><%# DataBinder.Eval(Container.DataItem, "DateTransaction") %></small></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
