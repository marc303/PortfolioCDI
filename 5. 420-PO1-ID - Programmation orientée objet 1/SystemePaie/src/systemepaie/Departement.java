/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package systemepaie;
import java.util.*;

/**
 *
 * @author Marc
 */
class Departement {
    
    private int id;
    private String nom;
    private String type;
    private double taux_horaire;
    private ArrayList<Employe> mesEmployes = new ArrayList<>();
    
    //Constructeur à quatre arguments (id, nom, type, taux_horaire) 
    //de l'objet Departement 
    public Departement(int id, String nom, String type, double taux_horaire){
//        this.id = id;
//        this.nom = nom;
//        this.taux_horaire = taux_horaire;
          setDepartementID(id);
          setDepartementNom(nom);
          setTaux_Horaire(taux_horaire);
          setType(type);
    }
    
    //Obtient l'id de l'objet Departement actuel
    public int getDepartementID(){
        return id;
    }
    
    //Obtient le nom de l'objet Departement actuel
    public String getDepartementNom(){
        return nom;
    }
    
    //Obtient le taux horaire de l'objet Departement actuel
    public double getTaux_Horaire(){
        return taux_horaire;
    }
    
    //Obtient le type de l'objet Departement actuel
    public String getType() {
        return type;
    }

    //Définit le type de l'objet Departement actuel
    public void setType(String type) {
        this.type = type;
    }

    
    //Définit l'id de l'objet Departement actuel
    public void setDepartementID(int id){
        this.id = id;
    }
    
    //Définit le nom de l'objet Departement actuel
    public void setDepartementNom(String nom){
        this.nom = nom;
    }
    
    //Définit le taux horaire de l'objet Departement actuel
    public void setTaux_Horaire(double taux_horaire){
       this.taux_horaire = taux_horaire;
    }
    
    //Obtient la liste de de l'Objet département actuel
    public ArrayList<Employe> getListEmployes(){
        return mesEmployes;
    }
    
    //Définit la liste d<employés de l'objet Departement actuel
    public void setListEmployes(ArrayList<Employe> employes)
    {
        this.mesEmployes = employes;
    }
    
    //Ajoute l'objet Employe reçu dans la liste d'employés 
    //du département correspondant
    public void setEmploye(Employe emp){
        this.mesEmployes.add(emp);
    }
    
    @Override
    public String toString(){
        
        String obj = this.id + " - " + this.nom + " (" + this.type + ")";
    
        return obj;
    }
    
}
