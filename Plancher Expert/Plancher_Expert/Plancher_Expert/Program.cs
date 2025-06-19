namespace Plancher_Expert
{
    internal class Program
    {

        //Déclaration et initialisation des tableaux publics
        public static string[] types = new string[5] { "Tapis commercial", "Tapis de qualité", "Plancher de bois franc", "Plancher flottant", "Céramique" };
        public static decimal[,] frais = new decimal[5, 2] { { 1.29m, 2.00m }, { 3.99m, 2.25m }, { 3.49m, 3.25m }, { 1.99m, 2.25m }, { 1.49m, 3.25m } };

        //Déclaration et initialisation des variables publiques
        public const decimal taux_taxe = 0.15m;
        public const string format_dollar = "0.00 $";
        public static decimal cout_materiaux, cout_main_doeuvre, taxe_materiaux, taxe_main_doeuvre, grand_total, superficie = 0;
        public static int type_plancher = -1;

        static void Main(string[] args)
        {
            //Déclaration et initialisation des variables locales
          
            bool longueur_valide, largeur_valide, plancher_valide = false;

            //Boucle pour vérifier les dimensions du plancer pour calculer sa superficie
            while (superficie == 0)
            {
                Console.WriteLine();
                Console.Write(" Donnez-moi la longueur en pied de votre plancher : ");
                string entree_longueur = Console.ReadLine();
                longueur_valide = decimal.TryParse(entree_longueur, out decimal longueur_plancher);

                Console.WriteLine();
                Console.Write(" Donnez-moi la largeur en pied de votre plancher : ");
                string entree_largeur = Console.ReadLine();
                largeur_valide = decimal.TryParse(entree_largeur, out decimal largeur_plancher);

                //Vérifie si les valeurs de la longueur et de la largeur sont valides pour calculer la superficie du plancher
                if (longueur_valide && largeur_valide)
                {
                    //Vérifie si les valeurs saisies sont plus grandes que 0 avant de faire le calcul
                    if (longueur_plancher > 0 && largeur_plancher > 0)
                    {
                        superficie = longueur_plancher * largeur_plancher;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(@" *** Les dimensions doivent être une valeur entière ou décimale supérieure à 0 ***");
                        Console.WriteLine();
                        Console.WriteLine("------------------------------------------------------------------------");
                    }
                }
                //Si non, affiche une message d'erreur selon quelle(s) dimension(s) est invalide
                else
                {
                    MessageDimension(longueur_valide, largeur_valide);
                }
            }

            //Boucle vérifiant la validité du type de plancher
            while (!plancher_valide)
            {
                Console.WriteLine();

                //Saisie par l'utilisateur du type de plancher
                AfficherTypes();
                Console.WriteLine();

                Console.Write(" Choissisez un type de couvre-plancher pour l'installation : ");
                string entree = Console.ReadLine();

                //Vérifie si l'entrée de l'utilisateur est un type de plancher valide
                plancher_valide = int.TryParse(entree, out type_plancher);

                //Si le type de plancher est valide,
                if (plancher_valide)
                {
                    //Nous vérifions si le type de plancher se situe entre 0 et 4 inclusivement
                    //Si oui, il reste valide. Si non, il devient invalide
                    plancher_valide = type_plancher >= 0 && type_plancher < 5;
                }

                //Si le type de plancher est invalide, un message s'affiche
                if (!plancher_valide)
                {
                    Console.WriteLine();
                    Console.WriteLine(" *** Vous devez saisir une valeur entre 0 et 4 inclusivement. ***");
                }
            }

            //Calcul les coûts, les taxes et le grand total selon le type de plancher choisi
            switch (type_plancher)
            {
                case 0:
                    CalculerCoutEtTaxe(0);
                    break;
                case 1:
                    CalculerCoutEtTaxe(1);
                    break;
                case 2:
                    CalculerCoutEtTaxe(2);
                    break;
                case 3:
                    CalculerCoutEtTaxe(3);
                    break;
                case 4:
                    CalculerCoutEtTaxe(4);
                    break;
            }

            Console.WriteLine();

            //Affichage des informations sur les frais d'installation
            AfficherInfos();

            Console.ReadKey();
        }

        //Affiche les choix de types de couvre-planchers
        private static void AfficherTypes()
        {
            Console.WriteLine("\t******Type de couvre-plancher******");
            Console.WriteLine("\t*                                 *");
            Console.WriteLine("\t* 0 - Tapis commercial            *");
            Console.WriteLine("\t* 1 - Tapis de qualité            *");
            Console.WriteLine("\t* 2 - Plancher de bois franc      *");
            Console.WriteLine("\t* 3 - Plancher flottant           *");
            Console.WriteLine("\t* 4 - Céramique                   *");
            Console.WriteLine("\t*                                 *");
            Console.WriteLine("\t***********************************");
        }

        //Affiche les informations sur les frais de l'installation
        private static void AfficherInfos()
        {

            Console.WriteLine(@$" Les frais pour le type de couvre-plancher ""{ types[type_plancher] }"" recouvrant une superficie de {superficie} pieds carrés sont :");
            Console.WriteLine();
            Console.WriteLine($" Le coût des matériaux est de : {cout_materiaux.ToString(format_dollar)}");
            Console.WriteLine($" Le coût de la main-d'oeuvre est de : {cout_main_doeuvre.ToString(format_dollar)}");
            Console.WriteLine($" Les taxes des matériaux sont de : {taxe_materiaux.ToString(format_dollar)}");
            Console.WriteLine($" Les taxes de la main-d'oeuvre sont de : {taxe_main_doeuvre.ToString(format_dollar)}");
            Console.WriteLine();
            Console.WriteLine($" Pour un grand total de : {grand_total.ToString(format_dollar)}");
        }
        //Affiche un ou plusieur(s) message(s) selon les arguments longueur et largeur
        public static void MessageDimension(bool longueur, bool largeur)
        {
            if (!longueur)
            {
                Console.WriteLine();
                Console.WriteLine(@" *** La longueur déterminée n'est pas une valeur entière ou décimale. ***");
            }
            if (!largeur)
            {
                Console.WriteLine();
                Console.WriteLine(@" *** La largeur déterminée n'est pas une valeur entière ou décimale. ***");
            }
        }

        //Calcul les coûts, les taxes et le grand total des frais d'installation
        public static void CalculerCoutEtTaxe(int rangee)
        {
            cout_materiaux = superficie * frais[rangee, 0];
            cout_main_doeuvre = superficie * frais[rangee, 1];
            taxe_materiaux = cout_materiaux * taux_taxe;
            taxe_main_doeuvre = cout_main_doeuvre * taux_taxe;
            grand_total = cout_materiaux + cout_main_doeuvre + taxe_materiaux + taxe_main_doeuvre;
        }

    }
}