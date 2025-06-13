package systemepaie;

import java.util.*;

public class SystemePaie {
    
    public static void main(String[] args) {
        String menu = GestionPaie.afficheMenu();
        boolean quitte = false;
        Scanner clavier = new Scanner(System.in);
        GestionPaie.creationDepartement();
        
        do {            
            System.out.println(menu);
            System.out.print("Sélectionnez une option entre 1 et 7 : ");
            
            String option = clavier.next();
            
            switch(option){
                case "1" :
                    GestionPaie.ajouterEmploye();
                    pause();
                    break;
                case "2" :
                    GestionPaie.ajouterPaie();
                    pause();
                    break;
                case "3" :
                    GestionPaie.afficherRRC();
                    pause();
                    break;
                case "4" :
                    GestionPaie.afficherAE();
                    pause();
                    break;
                case "5" :
                    GestionPaie.afficheEmp_Fixe();
                    break;
                case "6" :
                    GestionPaie.afficheEmp_Com();
                    pause();
                    break;
                case "7" :
                    quitte = true;
                    break;
                default :
                    System.out.println("\n*** Entrez une valeur entre 1 et 7 ***");
                    pause();
                    break;
            }
            
        } while (!quitte);
                
    }
    
    private static void pause(){
        System.out.println("\nAppuyez sur ENTRÉE pour continuer...");
        try {
            System.in.read();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    
}
