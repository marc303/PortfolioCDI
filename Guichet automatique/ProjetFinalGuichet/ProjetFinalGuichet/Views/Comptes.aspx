<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Comptes.aspx.cs" Inherits="ProjetFinalGuichet.Views.Comptes" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comptes client</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../css/comptes.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-3.7.0.min.js"></script>
</head>
<body>
    <form runat="server">
        <header>
            <div class="row">
                <div id="left" class="block">
                </div>
                <div id="center" class="block">
                    <h2 class="form-signin-heading">Bienvenue, <%: user.nomComplet %></h2>
                    <br />
                    <h3 class="form-signin-heading">Voici vos comptes :</h3>
                </div>
                <div id="right" class="block">
                    <asp:Button ID="btnLogout" runat="server" Text="Se Déconnecter" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnLogout_Click" />
                </div>
            </div>
            <hr />
        </header>
        <main>
            <div runat="server" id="dvComptes">
                <asp:Repeater runat="server" ID="repeatCompte" ItemType="ProjetFinalGuichet.Classes.Compte">
                    <ItemTemplate>
                        <div class="card w-50 center">
                            <div class="card-body">
                                <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem, "Nom") %> - <%# compte.getTypeCompte((int)DataBinder.Eval(Container.DataItem, "Type")).Nom %></h5>
                                <p class="card-text">Solde du compte : <%# DataBinder.Eval(Container.DataItem, "Solde") %>$</p>
                                <asp:Button Text="Opérer" runat="server" CssClass="btn btn-primary" CommandName='<%# DataBinder.Eval(Container.DataItem, "Nom") %>' OnCommand="btnCompte_Command" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </main>
    </form>
</body>
</html>
