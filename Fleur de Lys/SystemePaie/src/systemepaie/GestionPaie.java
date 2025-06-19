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
class GestionPaie {
    
      private static ArrayList<Employe> listeEmployes = new ArrayList<Employe>();
      private static ArrayList<Departement> mesDep = new ArrayList<Departement>(); 
    
      private static Scanner clavier = new Scanner(System.in);
      private static boolean existe;
      
    //Affichage du menu des options  
    static String afficheMenu(){
    
        String menu = "\n****************************************************************************" +
                      "\n* 1. Ajouter un employé                                                    *" +
                      "\n* 2. Ajouter une paie                                                      *" + 
                      "\n* 3. Afficher le total des contributions au Régime de Retraite du Canada   *" +
                      "\n* 4. Afficher le total des contributions à l'assurance emploi              *" +
                      "\n* 5. Liste des employés à taux fixe                                        *" +
                      "\n* 6. Liste des employés à commission                                       *" + 
                      "\n* 7. Quitter                                                               *" + 
                      "\n****************************************************************************\n";
        
        return menu;
    }
    
    //Créer les différents départements
    static void creationDepartement(){
        Departement dep1 = new Departement(1, "Restaurant", "Fixe", 8.50);
        Departement dep2 = new Departement(2, "Maintenance", "Fixe", 12.50);
        Departement dep3 = new Departement(3, "Commis/Paysagistes", "Fixe", 15.75);
        Departement dep4 = new Departement(4, "Ventes", "Commission", 15.00);
        
        mesDep.add(dep1);
        mesDep.add(dep2);
        mesDep.add(dep3);
        mesDep.add(dep4);
    }
    
    //Crée et ajoute un employé dans un département
    static void ajouterEmploye(){
        existe = true;
        int idEmp;
        int idDep;
        String prenom;
        String nom;
        Departement departement;
        
        //Boucle tant que l'ID de l'employé saisi existe
        do {            
          System.out.print("\nSaissisez l'ID de l'employé à ajouter : ");
          idEmp = clavier.nextInt();
            if (employeExiste(idEmp)) {
                System.out.println("\n*** L'employé avec l'identifiant saisi existe déjà. ***");
            }
            else
                existe = false;
        } while (existe);
        
        //Saisi du prénom de l'employé à ajouter
        System.out.print("Saissisez le prénom de l'employé à ajouter : ");
        prenom = clavier.next();
        
        //Saisi du nom de l'employé à ajouter
        System.out.print("Saissisez le nom de l'employé à ajouter : ");
        nom = clavier.next();
        
        
        System.out.println(afficheListeDepartement());
        
        //Boucle tant que le département saisi n'existe pas
        do {            
            System.out.print("\nSaissisez le département de l'employé à ajouter : ");
            idDep = clavier.nextInt();
            if (!departementExiste(idDep)) {
                System.out.println("\n*** Le département choisi n'existe pas. "
                        + "Saissisez une valeur entre 1 et 4. ***\n" + afficheListeDepartement());
            }
            else
                existe = true;
        } while (!existe);
        
        //Création d'un employé
        Employe employe = new Employe(idEmp, prenom, nom, idDep);
        
        //Obtient le département selon son ID
        departement = getDepartementByID(idDep);
        
        //Ajout de l'employé dans son département
        departement.setEmploye(employe);
        
        //Ajout de l'employé dans la liste globale
        listeEmployes.add(employe);
        
        //Message avertissant que l'employé a bien été créé et ajouté correctement
        System.out.println("\n*** " + employe.toString() + " a bien été ajouté dans le "
                + "département " + departement.getDepartementNom() + ". ***");
    }
    
    //Crée et ajoute une paie dans la liste de paie d'un employé
    static void ajouterPaie(){
        existe = false;
        int idEmp;
        int num_semaine;
        double nb_heures;
        double ventes = 0;
        Departement departement;
        Employe employe;
        
        //Boucle tant que l'ID saisi n'existe pas
        do {            
          System.out.print("\nSaissisez l'ID de l'employé dont vous voulez ajouter une paie : ");
          idEmp = clavier.nextInt();
            if (!employeExiste(idEmp)) {
                System.out.println("\n*** Vous ne pouvez pas ajouter une paie à un employé non existant. ***");
            }
            else
                existe = true;
        } while (!existe);
        
        //Obtient l'employé correspondant à l'ID saisi
        employe = getEmployeByID(idEmp);
        
        //Boucle tant que la semaine saisi a une paie pour l'employé correspondant
        do {            
            System.out.print("\nSaissisez un numéro de semaine entre 1 et 52 pour la paie hebdomadaire : ");
            num_semaine = clavier.nextInt();
            if (employe.paieExiste(num_semaine)) {
                System.out.println("\n*** Une seule paie par semaine! ***");
            }
            else
                existe = false;
        } while (existe);
        
        //Saisi du nombre d'heures travaillées par l'employé durant la semaine
        System.out.print("Saissisez le nombre d'heures travaillées par l'employé durant la semaine " + num_semaine + " : ");
        nb_heures = clavier.nextDouble();
        
        //Obtient le département selon son ID
        departement = getDepartementByID(employe.getDepartementID());
        
        //Vérifie si le département de l'employé est celui des Ventes
        //Si oui, demande de saisi du total des ventes
        if (employe.getDepartementID() == 4) {
            System.out.print("Saissisez le total des ventes effectuées par l'employé durant la semaine " + num_semaine + " : ");
            ventes = clavier.nextDouble();
        }
        
        //Création de la paie
        Paie paie = new Paie(idEmp, num_semaine, nb_heures, departement.getTaux_Horaire(), ventes);
        
        //Ajout de la Paie dans la liste de paie de l'employé correspondant
        employe.ajouterPaie(paie);
        
        //Message avertissant que la paie a bien été créé et ajouté correctement
        System.out.println("\n*** La paie a bien été ajouté pour " + employe.toString() +". ***");
        
    }
    
    //Affiche le total des contributions d'un employé au Régime de Retraite du Canada
    static void afficherRRC(){
        existe = false;
        int idEmp;
        double total_rrc;
        Employe emp;
        
        //Boucle tant que l'ID saisi n'existe pas
        do {            
          System.out.print("\nSaissisez l'ID de l'employé dont vous voulez consulter"
                  + " le total des contriburions au Régime de Retraite du Canada : ");
          idEmp = clavier.nextInt();
            if (!employeExiste(idEmp)) {
                System.out.println("\n*** Vous ne pouvez pas consulter les contributions d'un employé non existant. ***");
            }
            else
                existe = true;
        } while (!existe);
        
        //Obtient l'employé correspondant à l'ID saisi
        emp = getEmployeByID(idEmp);
        
         //Si l'employé a au moins un paie   
         if (!emp.getMesPaies().isEmpty()) {
            
            //Appel de la méthode pour calculer le total des contributions
            total_rrc = calculerRRC(emp);
        
            //Affichage du résultat
            System.out.println("\n*** " + emp.toString() + " a contribué un total de "
                    + String.format("%.2f", total_rrc) + " $ au Régime de Retraite du Canada. ***");
         }
         //Sinon message avertissant que l'employé n'a aucune paie
        else
            System.out.println("\n*** " + emp.toString() + " n'a aucune paie donc sa "
                    + "contribution au Régime de Retraite du Canada est de 0 $ ***");
        
    }
    
    //Affiche le total des contributions d'un employé à l'assurance emploi
    static void afficherAE(){
        existe = false;
        int idEmp;
        double total_ae;
        Employe emp;
        
        //Boucle tant que l'ID saisi n'existe pas
        do {            
          System.out.print("\nSaissisez l'ID de l'employé dont vous voulez consulter"
                  + " le total des contriburions à l'assurance emploi : ");
          idEmp = clavier.nextInt();
            if (!employeExiste(idEmp)) {
                System.out.println("\n*** Vous ne pouvez pas consulter les contributions d'un employé non existant. ***");
            }
            else
                existe = true;
        } while (!existe);
        
        //Obtient l'employé correspondant à l'ID saisi
        emp = getEmployeByID(idEmp);
        
        //Si l'employé a au moins un paie
        if (!emp.getMesPaies().isEmpty()) {
            
            //Appel de la méthode pour calculer le total des contributions
            total_ae = calculerAE(emp);
        
            //Affichage du résultat
            System.out.println("\n*** " + emp.toString() + " a contribué un total de "
                + String.format("%.2f", total_ae) + " $ à l'assurance emploi. ***");
        }
        //Sinon message avertissant que l'employé n'a aucune paie
        else
            System.out.println("\n*** " + emp.toString() + " n'a aucune paie donc sa "
                    + "contribution à l'assurance emploi est de 0 $ ***");
        
    
    }
   
    
    //Affiche la liste des employés à commission
    static void afficheEmp_Com(){
        
        //Appel de la méthode pour obtenir le ou les départements à Commission
        //et en obtient une liste
        ArrayList<Departement> deps = getDepsByType("Commission");
        
        //Boucle parcourant tous les départements de type Commission
        for (Departement dep : deps) {
            
            //Obtient la liste des employé selon le département courant
            ArrayList<Employe> emps = dep.getListEmployes();
            
            //Si la liste d'employé n'est pas vide
            if(!emps.isEmpty())
            {
                double total_ventes = 0;
                
                //Boucle parcourant la liste d'employés du département
                for (Employe emp : emps) {
                    
                    //Appel de la méthode pour calculer le total des ventes d'un employé
                    //et affiche le nom, l'ID et le total des ventes de ce dernier
                    total_ventes = calculerVentes(emp);
                    System.out.println("\n" + emp.toString() + " du département " + dep.getDepartementNom() 
                            +  " a vendu pour un total de " + String.format("%.2f", total_ventes) + " $");
                }
            }
            //Sinon affiche un message
            else
                System.out.println("\n*** Il n'y a aucun employé dans le département " +
                        dep.getDepartementNom() + " de type " + dep.getType() + " ***");
            
        }
    }
    
    //Affiche la liste des employés à taux fixe
    static void afficheEmp_Fixe(){
        
        //Appel de la méthode pour obtenir le ou les départements à taux Fixe
        //et en obtient une liste
        ArrayList<Departement> deps = getDepsByType("Fixe");
        
        //Boucle parcourant tous les départements de type Fixe
        for (Departement dep : deps) {
            
            //Obtient la liste des employé selon le département courant
            ArrayList<Employe> emps = dep.getListEmployes();
            
            //Si la liste d'employé n'est pas vide
            if(!emps.isEmpty())
            {
                double total_heuresSup = 0;
                
                //Boucle parcourant la liste d'employés du département
                for (Employe emp : emps) {
                    
                    //Appel de la méthode pour calculer le total des ventes d'un employé
                    //et affiche le nom, l'ID et le total des heures supplémentaires de ce dernier
                    total_heuresSup = calculerHeuresSup(emp);
                    System.out.println("\n" + emp.toString() + " du département " + dep.getDepartementNom() + 
                            " a travaillé pour un total de " + String.format("%.2f", total_heuresSup) 
                            + " heures supplémentaires");
                }
            }
            //Sinon affiche un message
            else
                System.out.println("\n*** Il n'y a aucun employé dans le département " +
                        dep.getDepartementNom() + " de type " + dep.getType() + " ***");
            
        }
    }
    
    //Calcul le total les contributions RRC d'un employé spécifié
    private static double calculerRRC(Employe emp){
        
        //Obtient la liste des paies d'un employé
        ArrayList<Paie> paies = emp.getMesPaies();
        
        double total_rrc = 0;
       
            //Boucle parcourant la liste des paies d'un employé pour 
            //calculer le total des contributions RRC
            for (Paie paie : paies) {
                total_rrc += paie.getRrc();
            }  
    
        
        return total_rrc;
    }
    
    //Calcul le total les contributions AE d'un employé spécifié
    private static double calculerAE(Employe emp){
        
        //Obtient la liste des paies d'un employé
        ArrayList<Paie> paies = emp.getMesPaies();
        
        double total_ae = 0;
        
        //Boucle parcourant la liste des paies d'un employé pour 
        //calculer le total des contributions AE
        for (Paie paie : paies) {
            total_ae += paie.getAe();
        }
        
        return total_ae;
    }
    
    //Calcul le total des ventes d'un employé spécifié
    private static double calculerVentes(Employe emp)
    {
        //Obtient la liste des paies d'un employé
        ArrayList<Paie> paies = emp.getMesPaies();
        
        double total_vente = 0;
        
        //Boucle parcourant la liste des paies d'un employé pour 
        //calculer le total des ventes
        for (Paie paie : paies) {
            total_vente += paie.getTotal_ventes();
        }
        
        return total_vente;
    }
    
    //Calcul le total des heures supplémentaires d'un employé spécifié
    private static double calculerHeuresSup(Employe emp)
    {
        //Obtient la liste des paies d'un employé
        ArrayList<Paie> paies = emp.getMesPaies();
        
        double total_heuresSup = 0;
        
        //Boucle parcourant la liste des paies d'un employé pour 
        //calculer le total des heures supplémentaires
        for (Paie paie : paies) {
            total_heuresSup += paie.getNb_heuresSup();
        }
        
        return total_heuresSup;
    }
    
    //Renvoie la liste des département selon un type spécifié
    private static ArrayList<Departement> getDepsByType(String type)
    {
        ArrayList<Departement> deps = new ArrayList<Departement>();
     
        //Boucle parcourant la liste globale des départements
        for (Departement dep : mesDep) {
            //Si le département correspond au type spécifié
            if (dep.getType().equalsIgnoreCase(type)) {
                deps.add(dep);
            }
        }
        
        return deps;
    }
    
    //affiche la liste des départements à titre informatif
    private static String afficheListeDepartement()
    {
        String listeDep = "";
        
        listeDep += "\nListe des départements : \n";
        
        //Boucle les différents département pour afficher leur informations
        for (Departement dep : mesDep) {
            listeDep += "\n" + dep.toString();
        }
        
        return listeDep;
    }
    
    //Vérifie si l'employé existe déjà avec son ID
    private static boolean employeExiste(int idEmp){
       
        //Si la liste globale des empployés n'est pas vide
        if (!listeEmployes.isEmpty()) {
            
            //Boucle la liste d'employé globale
            for (Employe emp : listeEmployes) {
                
                //Si l'employé avec l'ID spécifié existe
                if (emp.getEmployeID() == idEmp) {
                    return true;
                }
            }
        }
        return false;
    }
    
    //Renvoie un objet Employe selon son ID
    private static Employe getEmployeByID(int idEmp){
        
        //Boucle la liste d'employé globale
        for (Employe emp : listeEmployes) {
            
                //Si l'employé avec l'ID spécifié est trouvé
                if (emp.getEmployeID() == idEmp) {
                    return emp;
                }
        }
        return null;
    }
    
    //Vérifie si le département est existant grâce à son ID
    private static boolean departementExiste(int idDep){
    
        //Boucle la liste de départements globale
            for (Departement dep : mesDep) {
                
                //Si le département avec l'ID spécifié existe
                if (dep.getDepartementID() == idDep) {
                    return true;
                }
            }
       
        return false;
    }
    
    //Renvoie un objet Departement selon son ID
    private static Departement getDepartementByID(int idDep){
        
        //Boucle la liste de départements globale
        for (Departement dep : mesDep) {
            
                //Si le département avec l'ID spécifié est trouvé
                if (dep.getDepartementID() == idDep) {
                    return dep;
                }
            }
        return null;
    }
   
}
