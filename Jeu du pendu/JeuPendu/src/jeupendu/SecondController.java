/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jeupendu;

import Exceptions.*;
import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import javax.swing.JOptionPane;

/**
 * FXML Controller class
 *
 * @author Marc
 */
public class SecondController implements Initializable {

    private Joueur joueur;
    private Partie partie;
    private Scene scene;
    private Stage stage;
    private Parent root;
    
    @FXML
    private TextField txtNom;

    //Bouton pour envoyer le joueur et la première partie à la fenêtre principale
    @FXML
    void btnEnvoyerJoueur(ActionEvent event) throws IOException{
            String nom = "";
        
            //Essaie sur la saisie n'étant pas vide ou invalide
            try {
                nom = Pendu.verifierNomVide(txtNom.getText());
                Pendu.verifierNomAlpha(nom);
                
                //Création du joueur et de la partie
                joueur = Pendu.creerJoueur(nom);
                partie = Pendu.creerPartie(joueur);
                
                //Ferme la deuxième fenêtre et réouvre la première
                closeSecondWindow(event);
                openMainWindow();
            }
            //Attrape l'exception et affiche un message si la saisie est vide
            catch (EmptyStringException e) {
                JOptionPane.showMessageDialog(null, e.messageErreur, "Erreur", JOptionPane.INFORMATION_MESSAGE);
            }
            //Attrape l'exception et affiche un message si la saisie est invalide
            catch (NotAlphabetException e) {
                JOptionPane.showMessageDialog(null, e.messageErreur, "Erreur", JOptionPane.INFORMATION_MESSAGE);
            }
            
    }
    
    //Méthode pour la réouverture de la fenêtre principale du jeu
    public void openMainWindow() throws IOException
    {
            FXMLLoader loader = new FXMLLoader();
            root = loader.load(getClass().getResource("Main.fxml").openStream());
            
            //Si la partie existe, afficher les infos de celle-ci
            if (partie != null) {
                MainController mc = loader.getController();
                mc.afficherInfo(partie);
            }
            
            stage = new Stage();
            scene = new Scene(root);
            stage.setScene(scene);
            stage.setTitle("Jeu du Pendu");
            stage.show();
    }
    
    //Méthode pour la fermeture de la seconde fenêtre après une saisie valide
    public void closeSecondWindow(ActionEvent event) throws IOException
    {
        FXMLLoader loader = new FXMLLoader();
        root = loader.load(getClass().getResource("Second.fxml").openStream());
        stage = (Stage)((Node)event.getSource()).getScene().getWindow();
        scene = new Scene(root);
        stage.setScene(scene);
        stage.close();
    }
    
     /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    
}
