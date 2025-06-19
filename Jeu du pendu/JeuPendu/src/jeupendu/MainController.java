/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jeupendu;

import Exceptions.*;
import java.io.IOException;
import java.net.URL;
import java.util.*;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.MenuItem;
import javafx.scene.control.TextField;
import javafx.scene.shape.Circle;
import javafx.scene.shape.Line;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;
import javax.swing.JOptionPane;

/**
 *
 * @author Marc
 */
public class MainController implements Initializable {

    private Joueur joueur;
    private Partie partie;
    private Stage stage;
    private Scene scene;
    private Parent root;
    private ArrayList<Button> btnList = new ArrayList<>();
    
    @FXML
    private MenuItem quitterMenu;
    
    @FXML
    private Circle penduTete;

    @FXML
    private Line penduCorps;

    @FXML
    private Line penduJambe1;

    @FXML
    private Line penduBras1;

    @FXML
    private Line penduBras2;

    @FXML
    private Line penduJambe2;
    
    @FXML
    private TextField txtJoueur;
    
    @FXML
    private TextField txtScore;
    
    @FXML
    private TextField txtMot;
    
    //Le bouton affiche une fenêtre pour la création d'un joueur s'il n'existe pas
    //Sinon, on remet le pointage du joueur à 0 et on démarre une nouvelle partie
    @FXML
    void btnDebuterJeu(ActionEvent event) throws IOException {
            if (joueur == null)
                openSecondaryWindow(event);
            else
            {
                joueur.setPointage(0);
                prochainePartie();
            }
    }
    
    @FXML
    void btnLettreSelect(ActionEvent event) {
        
        //Ajoute le bouton sélectionné à une liste pour les réinitialiser
        //à leur état d'origine plus tard
        Button button = (Button)((Node)event.getSource());
        btnList.add(button);
        
        //Obtient la lettre du bouton
        String lettre = button.getText();
        
        //Essaie de vérifier si la partie existe
        try {
            partie = Pendu.verifierPartie(partie);
        } 
        //Attrape l'exception et affiche un message si la partie n'existe pas
        catch (PartieNullException e) {
            JOptionPane.showMessageDialog(null, e.messageErreur, "Erreur", JOptionPane.INFORMATION_MESSAGE);
        }

        //Si le mot contient la lettre, toutes les occurences de celle-ci sont révélées
        if (partie.contientLettre(lettre)) {
            char ch = lettre.charAt(0);
            partie.revelerLettre(ch);
        }
        
        //Sinon, c'est une erreur et on affiche une partie du bonhomme pendu
        else
        {
            int nbErr = partie.getNbErreurs();
            partie.setNbErreurs(++nbErr);
            afficherPendu(nbErr);
        }
        
        //Désactive le bouton sélectionné
        button.setDisable(true);
        updateInfo();
        
        //Si le mot est complétement révé;é ou atteinte du maximum d'erreurs
        //Déclenchement d'une prochaine partie
        if(partie.verifierFinPartie() || partie.getNbErreurs() == 6)
            prochainePartie();
    }
 
    //Méthode pour démarrer la fenêtre secondaire pour la saisie du nom du Joueur
    public void openSecondaryWindow(ActionEvent event)
    {
        try {
              FXMLLoader loader = new FXMLLoader();
              root = loader.load(getClass().getResource("Second.fxml").openStream());
              stage = (Stage)((Node)event.getSource()).getScene().getWindow();
              scene = new Scene(root);
              stage.setScene(scene);
              stage.setTitle("Sélection nom");
              
              //Gère la fermeture prématurée de la seconde fenêtre
              stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
                  @Override
                  public void handle(WindowEvent winEvent){
                     SecondController sec = new SecondController();
                      try {
                          sec.openMainWindow();
                      } catch (IOException ex) {
                          ex.printStackTrace();
                      }
                  }
              });
              
              stage.show();
        } catch (IOException ex) {
             ex.printStackTrace();
        } 
    }
    
    //Méthode affichant les informations du joueur et de la partie
    public void afficherInfo(Partie partie)
    {
        this.partie = partie;
        this.joueur = partie.getJoueur();
        
        txtJoueur.setText(joueur.getNom());
        txtScore.setText(String.valueOf(joueur.getPointage()));
        txtMot.setText(partie.cacherMot());
    }
    
    //Méthode visant à mettre jour le pointage et le mot de la partie
    public void updateInfo()
    {
        txtScore.setText(String.valueOf(joueur.getPointage()));
        txtMot.setText(partie.getFormatCache().toString());
    }
    
    //Méthode pour déclencher la prochaine partie
    public void prochainePartie()
    {
        //Boucle réactivant les boutons désactivés et remet à zéro la liste
        for (Button button : btnList) {
            button.setDisable(false);
        }
        btnList.removeAll(btnList);
        
        cacherPendu();
         
        partie = Pendu.creerPartie(joueur);
        partie.cacherMot();
        updateInfo();
    }
    
    //Méthode affichant une partie du pendu selon le nombre d'erreur
    public void afficherPendu(int nbErr)
    {
        switch(nbErr) {
            case 1:
                penduTete.setVisible(true);
                break;
            case 2:
                penduCorps.setVisible(true);
                break;
            case 3:
                penduBras1.setVisible(true);
                break;
            case 4:
                penduBras2.setVisible(true);
                break;
            case 5:
                penduJambe1.setVisible(true);
                break;
            case 6:
                penduJambe2.setVisible(true);
                break;
            default:
                 break;
        }
    }
    
    //Méthdoe cachant les parties du pendu
    public void cacherPendu()
    {
        penduTete.setVisible(false);
        penduCorps.setVisible(false);
        penduBras1.setVisible(false);
        penduBras2.setVisible(false);
        penduJambe1.setVisible(false);
        penduJambe2.setVisible(false);
    }

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        quitterMenu.setOnAction((ActionEvent t) -> {System.exit(0);});
    }    
    
}
