<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Operations.aspx.cs" Inherits="ProjetFinalGuichet.Views.Operations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opérations Compte</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="../css/operations.css" rel="stylesheet" />
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
                    <h3 class="form-signin-heading">Compte <%: typeCompte.Nom %> - <%: compte.Nom %></h3>
                    <br />
                    <h4 class="form-signin-heading">Liste des opérations pour ce compte :</h4>
                </div>
                <div id="right" class="block">
                    <asp:Button ID="btnLogout" runat="server" Text="Se Déconnecter" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnLogout_Click" />
                </div>
            </div>

                    <hr />
        </header>
        <main>
            <div id="dvOperations" runat="server">
                <div class="card-deck">
                    <asp:Repeater runat="server" ID="repeatOperation" ItemType="ProjetFinalGuichet.Classes.Operation">
                        <ItemTemplate>
                            <div class="card spacing">
                                <div class="card-body">
                                    <h5 class="card-title"><%# transactions.getTypeTransaction((int)DataBinder.Eval(Container.DataItem, "typeTransaction")).Nom %></h5>
                                    <p class="card-text"><%# getDescription((int)DataBinder.Eval(Container.DataItem, "typeTransaction")) %></p>
                                    <asp:Button Text='<%# getButtonText((int)DataBinder.Eval(Container.DataItem, "typeTransaction")) %>' runat="server" CssClass="btn btn-primary" CommandName='<%#DataBinder.Eval(Container.DataItem,"typeTransaction" ) %>' OnCommand="btnOperation_Command" />
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
