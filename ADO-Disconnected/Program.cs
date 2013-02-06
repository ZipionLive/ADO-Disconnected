//fork de Therence
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace ADO_Disconnected
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable cave = new DataTable("Cave"); // création de la table "Cave" (vide pour l'instant)

            DataColumn id = new DataColumn("ID");
            id.DataType = typeof(int);
            id.AutoIncrement = true; // active l'autoincrémentation de la colonne
            id.AutoIncrementSeed = 1; // valeur de départ pour l'autoincrémentation
            id.AutoIncrementStep = 1; // pas pour l'autoincrémentation
            cave.Columns.Add(id);
            cave.PrimaryKey = new DataColumn[] { id }; // désignation de la colonne "ID" comme clé primaire de la table

            DataColumn vin = new DataColumn("Vin"); // création d'une colonne "Vin"
            vin.DataType = typeof(string); // le type de données est string par défaut, cette ligne est donc optionelle
            vin.Unique = true; // détermine si les valeurs de la colonnes doivent être uniques (false par défaut)
            vin.AllowDBNull = false; // détermine si la colonne accepte les valeurs NULL (true par défaut)
            vin.Caption = "Vin"; // nom que portera la colonne dans la représentation graphique (par défaut, c'est le nom de la colonne spécifié lors de la déclaration)
            cave.Columns.Add(vin); // la colonne "Vin" est ajoutée à la table "Cave"

            DataColumn annee = new DataColumn("Annee", typeof(int)); // on peut utiliser le constructeur à 2 paramètres pour déclarer une nouvelle colonne tout en spécifiant son type
            annee.AllowDBNull = false;
            annee.Caption = "Année";
            cave.Columns.Add(annee);

            DataColumn marque = new DataColumn("Marque");
            marque.MaxLength = 35;  // détermine la taille maximale dans le cas d'un string (-1 par défaut, càd illimité)
            marque.AllowDBNull = false;
            cave.Columns.Add(marque);

            // la colonne suivante est une colonne dérivée des colonnes "Marque" et "Année"
            DataColumn marqueEtAnnee = new DataColumn("MarqueEtAnnee");
            marqueEtAnnee.MaxLength = 40;
            marqueEtAnnee.Expression = "Annee + ' ' + Marque"; // la propriété "Expression" permet de concaténer les valeurs de plusieurs colonnes
            marqueEtAnnee.Caption = "Marque et Année";
            cave.Columns.Add(marqueEtAnnee);

            // remplissage de la table
            DataRow newCave = cave.NewRow(); // création de la ligne à insérer
            newCave["Vin"] = "Beaujolais";
            newCave["Marque"] = "Grand Cru";
            newCave["Annee"] = 1982;
            cave.Rows.Add(newCave); // ajout de la ligne à la table "Cave"

            cave.LoadDataRow(new object[] { null, "Bourgogne", 2012, "Prix 2012" }, true); // une autre méthode d'ajout de lignes
            cave.LoadDataRow(new object[] { null, "Saint-Emilion", 1983, "Cuvée Prestige" }, true);
            cave.LoadDataRow(new object[] { null, "Pommard", 1959, "Clos Blanc" }, true);

            printTable(cave);
        }

        static void printTable(DataTable table)
        {
            foreach (DataRow r in table.Rows)
            {
                Console.WriteLine(string.Format("row : {0} | {1} | {2} | {3} || {4}", r["ID"], r["Vin"], r["Annee"], r["Marque"], r["MarqueEtAnnee"]));
            }
        }
    }
}
