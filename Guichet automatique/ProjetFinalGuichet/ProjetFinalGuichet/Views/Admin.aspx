<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ProjetFinalGuichet.Views.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opérations administateur</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style class="anchorjs"></style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href="../css/admin.css" rel="stylesheet" />
    <script defer src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</head>
<body>
    <form id="formAdmin" runat="server">
        <header>
            <div class="row">
                <div class="col-2">
                </div>
                <div class="col-8">
                    <h2 class="form-signin-heading" style="text-align:center">Opérations administrateur</h2>
                </div>
                <div class="col-2">
                     <asp:Button ID="btnLogout" runat="server" Text="Se Déconnecter" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnLogout_Click" />
                </div>
            </div>
        </header>
        <hr />
        <div class="accordion" id="accordionOperations">
            <div class="card">
                <div class="card-header" id="headingOne">
                    <h5 class="mb-0">
                        <%--<asp:Button Text="Créer un client" runat="server" CssClass="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCreateClient" aria-expanded="true" aria-controls="collapseCreateClient"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCreateClient" aria-expanded="true" aria-controls="collapseCreateClient">
                            Créer un client
                        </button>
                    </h5>
                </div>

                <div id="collapseCreateClient" class="collapse" aria-labelledby="headingOne" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="creerClientPanel" DefaultButton="btnCreerClient" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="txtPrenom">Prénom</label>
                                    <asp:TextBox CssClass="form-control" ID="txtPrenom" placeholder="Saissisez le prénom du client" runat="server" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="creerClient" ControlToValidate="txtPrenom" ErrorMessage="Le prénom est requis" CssClass="alert alert-warning" />
                                </div>
                                <div class="col">
                                    <label for="txtNom">Nom</label>
                                    <asp:TextBox CssClass="form-control" ID="txtNom" placeholder="Saissisez le nom du client" runat="server" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="creerClient" ControlToValidate="txtNom" ErrorMessage="Le nom est requis" CssClass="alert alert-warning" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col">
                                    <label for="txtEmail">Adresse courriel</label>
                                    <asp:TextBox CssClass="form-control" ID="txtEmail" placeholder="Saissisez une adresse courriel valide" runat="server" TextMode="Email" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="creerClient" ControlToValidate="txtEmail" ErrorMessage="Le courriel est requis" CssClass="alert alert-warning" />
                                </div>
                                <div class="col">
                                    <label for="txtTelephone">Téléphone</label>
                                    <asp:TextBox CssClass="form-control" ID="txtTelephone" placeholder="Saissiez un numéro de téléphone valide" runat="server" TextMode="Phone" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ValidationGroup="creerClient" ControlToValidate="txtTelephone" ErrorMessage="Le téléphone est requis" CssClass="alert alert-warning" />
                                    <asp:RegularExpressionValidator ID="revPhone" ErrorMessage="Numéro de téléphone invalide" ControlToValidate="txtTelephone" runat="server" ValidationExpression="^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$" CssClass="alert alert-danger" Display="Dynamic" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col">
                                    <label for="txtCodeClient">Code client</label>
                                    <asp:TextBox CssClass="form-control" ID="txtCodeClient" placeholder="Saisissez un code client valide" runat="server" TextMode="Number" MaxLength="6" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ValidationGroup="creerClient" ControlToValidate="txtCodeClient" ErrorMessage="Le code client est requis" CssClass="alert alert-warning" />
                                    <asp:RegularExpressionValidator ID="revCodeClient" ErrorMessage="Seulement un code client à 6 chiffres est valide." ControlToValidate="txtCodeClient" runat="server" ValidationExpression="^\d{6}$" CssClass="alert alert-danger" Display="Dynamic" />
                                </div>
                                <div class="col">
                                    <label for="txtNIP">Numéro d'identification personnel (NIP)</label>
                                    <asp:TextBox CssClass="form-control" ID="txtNIP" placeholder="Saissiez un numéro d'identification personnel (NIP)" runat="server" MaxLength="4" TextMode="Password" />
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ValidationGroup="creerClient" ControlToValidate="txtNIP" ErrorMessage="Le NIP est requis" CssClass="alert alert-warning" />
                                    <asp:RegularExpressionValidator ID="revNIP" ErrorMessage="Seulement un NIP à 4 chiffres est valide." ControlToValidate="txtNIP" runat="server" ValidationExpression="^\d{4}$" CssClass="alert alert-danger" Display="Dynamic" />
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <div class="form-check">
                                    <asp:CheckBox CssClass="form-check-input" ID="chkBloque" runat="server" ValidationGroup="creerClient" />
                                    <label class="form-check-label" for="chkBloque">
                                        Bloquer le client lors de la création
                                    </label>
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="btnCreerClient" Text="Créer le client" runat="server" CssClass="btn btn-primary" ValidationGroup="creerClient" OnClick="btnCreerClient_Click" CausesValidation="true" />
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header" id="headingTwo">
                    <h5 class="mb-0">
                        <%-- <asp:Button runat="server" ID="linkCreerCompte" CssClass="btn btn-link" type="button" Text="Créer compte" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte" UseSubmitBehavior="false" AutoPostBack="true" OnClick="linkCreerCompte_Click"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte">
                            Créer un compte
                        </button>
                    </h5>
                </div>

                <div id="collapseCreateCompte" class="<%= state %>" aria-labelledby="headingTwo" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="creerComptePanel" DefaultButton="btnCreerCompte" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="dropdownClients">Client</label>
                                    <asp:DropDownList CssClass="form-control" ID="dropdownClients" runat="server" required="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownClients_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ValidationGroup="creerCompte" ControlToValidate="dropdownClients" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col">
                                    <label for="dropdownTypeCompte">Type de compte</label>
                                    <asp:DropDownList CssClass="form-control" ID="dropdownTypeCompte" runat="server" required="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownTypeCompte_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ValidationGroup="creerCompte" ControlToValidate="dropdownTypeCompte" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col">
                                    <label for="txtSolde">Solde du compte</label>
                                    <asp:TextBox ID="txtSolde" type="number" runat="server" CssClass="form-control" min="0" value="0.00" step=".01" Enabled="false" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ValidationGroup="creerCompte" ControlToValidate="txtSolde" />
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="btnCreerCompte" Text="Créer le compte" runat="server" CssClass="btn btn-primary" ValidationGroup="creerCompte" OnClick="btnCreerCompte_Click" Enabled="false" CausesValidation="true" />
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header" id="headingThree">
                    <h5 class="mb-0">
                        <%-- <asp:Button runat="server" ID="linkCreerCompte" CssClass="btn btn-link" type="button" Text="Créer compte" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte" UseSubmitBehavior="false" AutoPostBack="true" OnClick="linkCreerCompte_Click"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseShowTransactions" aria-expanded="true" aria-controls="collapseShowTransactions">
                            Afficher transactions par compte
                        </button>
                    </h5>
                </div>

                <div id="collapseShowTransactions" class="<%= state2 %>" aria-labelledby="headingThree" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="showTransactionsPanel" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="dropdownComptes">Compte</label>
                                    <asp:DropDownList CssClass="form-control" ID="dropdownComptes" runat="server" required="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownComptes_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ValidationGroup="showTransactions" ControlToValidate="dropdownComptes" />
                                </div>
                            </div>
                            <br />
                            <%--<asp:Button ID="btnShowTransactions" Text="Affihcer les transactions" runat="server" CssClass="btn btn-primary" ValidationGroup="showTransactions" OnClick="btnShowTransactions_Click" Enabled="false" CausesValidation="true"/>--%>
                        </asp:Panel>
                    </div>
                    <asp:Repeater runat="server" ID="repeatTransaction" ItemType="ProjetFinalGuichet.Classes.Transactions" Visible="false">
                        <ItemTemplate>
                            <div class="card-body">
                                <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem, "typeTransaction.Nom") %></h5>
                                <div class="row">
                                    <div class="col">
                                        <p class="card-text">Du compte : <%# DataBinder.Eval(Container.DataItem, "CompteDe") %></p>
                                    </div>
                                    <div class="col">
                                        <p class="card-text">Vers le compte : <%# DataBinder.Eval(Container.DataItem, "CompteVers") %></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="card-text">Numéro de la facture : <%# DataBinder.Eval(Container.DataItem, "NumeroFacture") %></p>
                                    </div>
                                    <div class="col">
                                        <p class="card-text">Montant : <%# DataBinder.Eval(Container.DataItem, "Montant") %>$</p>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col">
                                        <p class="card-text"><small class="text-muted"><%# DataBinder.Eval(Container.DataItem, "Description") %></small></p>
                                    </div>
                                    <div class="col">
                                        <p class="card-text"><small class="text-muted"><%# DataBinder.Eval(Container.DataItem, "DateTransaction") %></small></p>
                                    </div>
                                </div>
                            </div>
                            <hr />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="card">
                <div class="card-header" id="headingFour">
                    <h5 class="mb-0">
                        <%-- <asp:Button runat="server" ID="linkCreerCompte" CssClass="btn btn-link" type="button" Text="Créer compte" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte" UseSubmitBehavior="false" AutoPostBack="true" OnClick="linkCreerCompte_Click"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseBlockClient" aria-expanded="true" aria-controls="collapseBlockClient">
                            Bloquer ou débloquer un client
                        </button>
                    </h5>
                </div>

                <div id="collapseBlockClient" class="<%= state3 %>" aria-labelledby="headingFour" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="blockClientPanel" DefaultButton="btnBlock" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="dropdownClients2">Client</label>
                                    <asp:DropDownList CssClass="form-control" ID="dropdownClients2" runat="server" required="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownClients2_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ValidationGroup="blockClient" ControlToValidate="dropdownClients2" />
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="btnBlock" Text="Bloquer" runat="server" CssClass="btn btn-primary" ValidationGroup="blockClient" OnClick="btnBlock_Click" Enabled="false" CausesValidation="true" />
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header" id="headingFive">
                    <h5 class="mb-0">
                        <%-- <asp:Button runat="server" ID="linkCreerCompte" CssClass="btn btn-link" type="button" Text="Créer compte" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte" UseSubmitBehavior="false" AutoPostBack="true" OnClick="linkCreerCompte_Click"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseModifierGuichet" aria-expanded="true" aria-controls="collapseModifierGuichet">
                            Modifier le guichet
                        </button>
                    </h5>
                </div>

                <div id="collapseModifierGuichet" class="collapse" aria-labelledby="headingFive" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="modifierGuichetPanel" DefaultButton="btnModGuichet" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="txtMontantGuichet">Montant à ajouter au guichet</label>
                                    <asp:TextBox ID="txtMontantGuichet" type="number" runat="server" CssClass="form-control" min="0" value="0" step="10" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ValidationGroup="modGuichet" ControlToValidate="txtMontantGuichet" />
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <div class="form-check">
                                    <asp:CheckBox CssClass="form-check-input" ID="chkFermerGuichet" runat="server" ValidationGroup="modGuichet" />
                                    <label class="form-check-label" for="chkFermerGuichet">
                                        Cochez pour fermer le guichet/Décochez pour l'ouvrir
                                    </label>
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="btnModGuichet" Text="Modifer le guichet" runat="server" CssClass="btn btn-primary" ValidationGroup="modGuichet" OnClick="btnModGuichet_Click" CausesValidation="true" />
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header" id="headingSix">
                    <h5 class="mb-0">
                        <%-- <asp:Button runat="server" ID="linkCreerCompte" CssClass="btn btn-link" type="button" Text="Créer compte" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte" UseSubmitBehavior="false" AutoPostBack="true" OnClick="linkCreerCompte_Click"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapsePreleveHypo" aria-expanded="true" aria-controls="collapsePreleveHypo">
                            Prélever un montant d'un compte hypothécaire
                        </button>
                    </h5>
                </div>

                <div id="collapsePreleveHypo" class="<%= state4 %>" aria-labelledby="headingSix" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="preleveHypoPanel" DefaultButton="btnPrelevement" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="dropdownComptesHypo">Compte hypothécaire</label>
                                    <asp:DropDownList CssClass="form-control" ID="dropdownComptesHypo" runat="server" required="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdownComptesHypo_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ValidationGroup="prevHypo" ControlToValidate="dropdownComptesHypo" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col">
                                    <label for="txtMontantPrelevement">Montant à prélever</label>
                                    <asp:TextBox ID="txtMontantPrelevement" type="number" runat="server" CssClass="form-control" min="0" value="0.00" step=".01" Enabled="false" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic" ValidationGroup="prevHypo" ControlToValidate="txtMontantPrelevement" />
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="btnPrelevement" Text="Confirmer le prélèvement" runat="server" CssClass="btn btn-primary" ValidationGroup="prevHypo" OnClick="btnPrelevement_Click" Enabled="false" CausesValidation="true" />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header" id="headingSeven">
                    <h5 class="mb-0">
                        <%-- <asp:Button runat="server" ID="linkCreerCompte" CssClass="btn btn-link" type="button" Text="Créer compte" data-bs-toggle="collapse" data-bs-target="#collapseCreateCompte" aria-expanded="true" aria-controls="collapseCreateCompte" UseSubmitBehavior="false" AutoPostBack="true" OnClick="linkCreerCompte_Click"/>--%>
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#collapseAutres" aria-expanded="true" aria-controls="collapseAutres">
                            Autres
                        </button>
                    </h5>
                </div>

                <div id="collapseAutres" class="collapse" aria-labelledby="headingSeven" data-bs-parent="#accordionOperations">
                    <div class="card-body">
                        <asp:Panel ID="payerInteretPanel" DefaultButton="btnPayerInteret" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="btnPayerInteret">Payer de l'intérêt à tous les comptes épargnes de 1% : </label>
                                    <asp:Button ID="btnPayerInteret" Text="Payer de l'intérêt" runat="server" CssClass="btn btn-primary" ValidationGroup="payerInteret" OnClick="btnPayerInteret_Click" CausesValidation="true" />
                                </div>

                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="augmenterCreditPanel" DefaultButton="btnAugmenterCredit" runat="server">
                            <div class="row">
                                <div class="col">
                                    <label for="btnAugmenterCredit">Augmenter le solde de toutes les marges de crédit de 5% : </label>
                                    <asp:Button ID="btnAugmenterCredit" Text="Augmenter le crédit" runat="server" CssClass="btn btn-primary" ValidationGroup="augmenterCredit" OnClick="btnAugmenterCredit_Click" CausesValidation="true" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
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
</body>
</html>
