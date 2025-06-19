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
class Employe {
    
    private int idEmp;
    private String prenom;
    private String nom;
    private int idDep;
    private ArrayList<Paie> mesPaies = new ArrayList<Paie>();
    
    //Constructeur à quatre arguments (idEmp, prenom, nom, idDep) 
    //de l'objet Employe 
    public Employe(int idEmp, String prenom, String nom, int idDep) {
          setEmployeID(idEmp);
          setPrenom(prenom);
          setNom(nom);
          setDepartement(idDep);
    }
    
    //Obtient l'id de l'employé de l'objet Employe actuel
    public int getEmployeID(){
        return idEmp;
    }
    
    //Obtient le prénom de l'employé de l'objet Employe actuel
    public String getPrenom(){
        return prenom;
    }
    
    //Obtient le nom de l'employé de l'objet Employe actuel
    public String getNom(){
        return nom;
    }
    
    //Obtient l'id du département de l'objet Employe actuel
    public int getDepartementID(){
        return idDep;
    }
    
    //Définit l'id de l'employé de l'objet Employe actuel
    public void setEmployeID(int idEmp){
        this.idEmp = idEmp;
    }
    
    //Définit le prénom de l'employé de l'objet Employe actuel
    public void setPrenom(String prenom){
        this.prenom = prenom;
    }
    
    //Définit le nom de l'employé de l'objet Employe actuel
    public void setNom(String nom){
        this.nom = nom;
    }
    
    //Définit l'id du département de l'objet Employe actuel
    public void setDepartement(int idDep){
        this.idDep = idDep;
    }
    
    //Obtient la liste des paies de l'objet Employe actuel
    public ArrayList<Paie> getMesPaies() {
        return mesPaies;
    }

    //Définit la liste des paies de l'objet Employe actuel
    public void setMesPaies(ArrayList<Paie> mesPaies) {
        this.mesPaies = mesPaies;
    }
    
    //Vérifie si une paie existe déjà pour une semaine donnée
    public boolean paieExiste(int num_semaine){
        
        //Si la liste des paies n'est pas vide
        if (!mesPaies.isEmpty()) {
            
            //Boucle la liste des paies
            for (Paie paie : getMesPaies()) {
                
                //Si la semaine spécifiée a une paie
                if (paie.getNumSemaine() == num_semaine) {
                    return true;
                }
            }
        }
        
        return false;
    }
    
    //Ajout d'une paie dans Employe actuel 
    public void ajouterPaie(Paie paie){
        this.getMesPaies().add(paie);
    }
    
    @Override
    public String toString(){
        
        String obj = "L'employé " + this.prenom + " " + this.nom 
                     + " avec l'ID " + this.idEmp;
    
        return obj;
    }
}
