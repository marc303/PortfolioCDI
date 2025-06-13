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
class Paie {

    private final double TAUX_RRC = 0.0495;
    private final double TAUX_AE = 0.0198;
    private final double TAUX_COMMISSION = 0.015;
    
    private int num_semaine;
    private int idEmp;
    private double taux_horaire;
    private double nb_heures;
    private double total_ventes;
    private double nb_heuresSup;
    private double commission_vente;
    private double rrc;
    private double ae;
    private double salaire_brut;
    private double salaire_net;
    
    //Constructeur à cinq arguments (idEmp, numSemaine, nbHeures, taux_horaire, total_ventes) 
    //de l'objet Employe
    public Paie(int idEmp, int numSemaine, double nbHeures, double taux_horaire, double total_vente) {
        setIdEmp(idEmp);
        setNum_semaine(numSemaine);
        if (nbHeures > 44 && taux_horaire != 15.00) {
            setNb_heures(44);
            setNb_heuresSup(nbHeures-44);
        }
        else
            setNb_heures(nbHeures);
        
        setTaux_horaire(taux_horaire);
        setTotal_ventes(total_vente);
        
        setCommission_vente(calculerCommission(total_vente));
        
        setSalaire_brut(calculerSalaireBrut());
        
        setRrc(calculerRRC(this.salaire_brut));
        
        setAe(calculerAE(this.salaire_brut));
        
        setSalaire_net(calculerSalireNet());
    }
    
    //Obtient le numéro de semaine de l'objet Paie actuel
    public int getNumSemaine(){
        return num_semaine;
    }
    
    //Définit le numéro de semaine de l'objet Paie actuel
    public void setNum_semaine(int num_semaine) {
        this.num_semaine = num_semaine;
    }

    //Obtient l'ID de l'employé de l'objet Paie actuel
    public int getIdEmp() {
        return idEmp;
    }

    //Définit l'ID de l'employé de l'objet Paie actuel
    public void setIdEmp(int idEmp) {
        this.idEmp = idEmp;
    }

    //Obtient le taux horaire de l'objet Paie actuel
    public double getTaux_horaire() {
        return taux_horaire;
    }
    
    //Définit le taux horaire de l'objet Paie actuel
    public void setTaux_horaire(double taux_horaire) {
        this.taux_horaire = taux_horaire;
    }

    //Obtient le nombre d'heures travaillées(temps régulier) de l'objet Paie actuel
    public double getNb_heures() {
        return nb_heures;
    }
    
    //Définit le nombre d'heures travaillées(temps régulier) de l'objet Paie actuel
    public void setNb_heures(double nb_heures) {
        this.nb_heures = nb_heures;
    }
    
    //Obtient le total des ventes de l'objet Paie actuel
    public double getTotal_ventes() {
        return total_ventes;
    }
    
    //Définit le total des ventes de l'objet Paie actuel
    public void setTotal_ventes(double total_ventes) {
        this.total_ventes = total_ventes;
    }

    //Obtient la commission des ventes de l'objet Paie actuel
    public double getCommission_vente() {
        return commission_vente;
    }
    
    //Définit la commission des ventes de l'objet Paie actuel
    public void setCommission_vente(double commission_vente) {
        this.commission_vente = commission_vente;
    }
    
    //Obtient le nombre d'heures supplémentaires travaillées de l'objet Paie actuel
    public double getNb_heuresSup() {
        return nb_heuresSup;
    }

    //Définit le nombre d'heures supplémentaires travaillées de l'objet Paie actuel
    public void setNb_heuresSup(double nb_heuresSup) {
        this.nb_heuresSup = nb_heuresSup;
    }

    //Obtient la contribution RRC de l'objet Paie actuel
    public double getRrc() {
        return rrc;
    }

    //Définit la contribution RRC de l'objet Paie actuel
    public void setRrc(double rrc) {
        this.rrc = rrc;
    }
    
    //Obtient la contribution AE de l'objet Paie actuel
    public double getAe() {
        return ae;
    }

    //Définit la contribution AE de l'objet Paie actuel
    public void setAe(double ae) {
        this.ae = ae;
    }
    
    //Obtient le salaire brut de l'objet Paie actuel
    public double getSalaire_brut() {
        return salaire_brut;
    }

    //Définit le salaire brut de l'objet Paie actuel
    public void setSalaire_brut(double salaire_brut) {
        this.salaire_brut = salaire_brut;
    }
    
    //Obtient le salaire net de l'objet Paie actuel
    public double getSalaire_net() {
        return salaire_net;
    }

    //Définit le salaire net de l'objet Paie actuel
    public void setSalaire_net(double salaire_net) {
        this.salaire_net = salaire_net;
    }
    
    //Calcule la commission du total des ventes
    private double calculerCommission(double total_vente)
    {
        return total_vente * TAUX_COMMISSION;
    }
    
    //Calcule la contribution RRC du salaire
    private double calculerRRC(double salaire)
    {
        return salaire * TAUX_RRC;
    }
    
    //Calcule la contribution AE du salaire
     private double calculerAE(double salaire)
    {
        return salaire * TAUX_AE;
    }
     
    //Calcule le salaire brut de la paie 
    private double calculerSalaireBrut()
    {
        return (this.nb_heures*this.taux_horaire) + (this.nb_heuresSup*this.taux_horaire*1.5) 
                + this.commission_vente;
    }
    
    //Calcul salaire net après déduction
    private double calculerSalireNet()
    {
        return this.salaire_brut - this.rrc - this.ae;
    }
}
