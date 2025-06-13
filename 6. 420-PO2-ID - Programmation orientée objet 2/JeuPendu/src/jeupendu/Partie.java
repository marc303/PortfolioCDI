/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jeupendu;

/**
 *
 * @author Marc
 */
public class Partie {

    private Joueur joueur;
    private String motCache;
    private StringBuilder formatCache;
    private int nbErreurs;
    private int nbPoints;
    
    //Constructeur de Partie à 3 arguments (joueur, motCache, nbErreurs)
    public Partie(Joueur joueur, String motCache, int nbErreurs)
    {
        this.joueur = joueur;
        this.motCache = motCache;
        this.nbErreurs = nbErreurs;
    }
    
    /**
     * @return the joueur
     */
    public Joueur getJoueur() {
        return joueur;
    }

    /**
     * @param joueur the joueur to set
     */
    public void setJoueur(Joueur joueur) {
        this.joueur = joueur;
    }

    /**
     * @return the motCache
     */
    public String getMotCache() {
        return motCache;
    }

    /**
     * @param motCache the motCache to set
     */
    public void setMotCache(String motCache) {
        this.motCache = motCache;
    }
    
    /**
     * @return the nbErreurs
     */
    public int getNbErreurs() {
        return nbErreurs;
    }

    /**
     * @param nbErreurs the nbErreurs to set
     */
    public void setNbErreurs(int nbErreurs) {
        this.nbErreurs = nbErreurs;
    }
    
    /**
     * @return the formatCache
     */
    public StringBuilder getFormatCache() {
        return formatCache;
    }

    //Méthode masquant le mot à deviner. Retourne la String correspondante.
    public String cacherMot()
    {
        String mot = this.motCache;
        formatCache = new StringBuilder();
        
        for (int i = 0; i < mot.length(); i++) {
            formatCache.append('*');
        }
        
        return formatCache.toString();
    }
    
    //Vérifie si le mot conttient la lettre en argument
    //Si oui, renvoie de true. Sinon, false
    public boolean contientLettre(String lettre)
    {
        if (this.motCache.contains(lettre)) {
            return true;
        }
        else
            return false;
    }
    
    //Méthode revelant toutes les occurences de la lettre en argument
    //Pour chaque occurence, augmentation du score par 1.
    //Si le mot n'a plus de '*'. Autrement dit, il est complètement révélé,
    //on ajoute 5 points au score
    public void revelerLettre(char lettre)
    {
        String mot = this.motCache;
        StringBuilder format = this.formatCache;
        nbPoints = joueur.getPointage();
        
        for (int i = 0; i < format.length(); i++) {
            if (mot.charAt(i) == lettre) {
                format.setCharAt(i, lettre);
                nbPoints++;
            }
        }
        if (!format.toString().contains("*")) {
            nbPoints+=5;
        }
        
        this.joueur.setPointage(nbPoints);
    }
    
    //Vérifie si la partie est finie en comparant la mot cache au format affiché
    //Si oui, la partie est finie, on retourne true. Sinon, renvoie false 
    //et la partie continue
    public boolean verifierFinPartie()
    {
        if (this.formatCache.toString().equals(this.motCache))
            return true;
        else
            return false;
    }
}
