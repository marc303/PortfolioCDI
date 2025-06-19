/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jeupendu;

import Exceptions.*;
import java.util.Random;

/**
 *
 * @author Marc
 */
public class Pendu {
    
    //Tableau de mots pour le jeu du pendu
    private static final String[] mots = {"appel","entonnoir","rotule","offre",
        "polluer","photo","forme","plan","acrobate","lourdeau"};
    
    //Méthode vérifiant si le nom est vide, si oui on lance l'exception correspondate
    //Sinon, retourne le nom
    public static String verifierNomVide(String nom) throws EmptyStringException
    {
        String temp = null;
        temp = nom;
        
        if("".equals(temp))
            throw new EmptyStringException("Vous devez saisir un nom.");
        
        return temp;
    }
    
    //Méthode vérifiant si le nom ne contient que des lettres
    //Si oui, on lance l'exception correspondante. Sinon, on retounre le nom
    public static String verifierNomAlpha(String nom) throws NotAlphabetException
    {
        String regex = "^[a-zA-Z]+$";
        
        if (!nom.matches(regex))
            throw new NotAlphabetException("Saisie invalide! "
                    + "Le nom ne doit contenir que des lettres");
        
        return nom;
    }
    
    //Méthode vérifiant si la partie existe. Si oui, on lance l'exception corresapondate
    //Si on retourne la partie en question
    public static Partie verifierPartie(Partie partie) throws PartieNullException
    {
        Partie temp = null;
        temp = partie;
        
        if (temp == null)
            throw new PartieNullException("La partie n'a pas commencé");
    
        return temp;
    }
    
    //Méthode choissisant le mot à deviner
    public static String choisirMot()
    {
        Random rand = new Random();
        int i = rand.nextInt(9);
        
        return mots[i];
    }
    
    //Méthode pour créer un joueur. Retourne Joueur
    public static Joueur creerJoueur(String nom)
    {
        Joueur joueur = new Joueur(nom, 0);
        return joueur;
    }
    
    //Méthode pour créer une partie. Retourne Partie
    public static Partie creerPartie(Joueur joueur)
    {
        String motCache = choisirMot();
        Partie partie = new Partie(joueur, motCache, 0);
        return partie;
    }
}
