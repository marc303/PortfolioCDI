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
public class Joueur {

    //Constructeur de Joueur à 2 arguments (nom, pointage)
    public Joueur(String nom, int pointage)
    {
        this.nom = nom;
        this.pointage = pointage;
    }
    
    /**
     * @return the nom
     */
    public String getNom() {
        return nom;
    }

    /**
     * @param nom the nom to set
     */
    public void setNom(String nom) {
        this.nom = nom;
    }

    /**
     * @return the pointage
     */
    public int getPointage() {
        return pointage;
    }

    /**
     * @param pointage the pointage to set
     */
    public void setPointage(int pointage) {
        this.pointage = pointage;
    }
    
    private String nom;
    private int pointage;
}
