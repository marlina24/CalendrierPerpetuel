using System;
using System.Globalization;

namespace CalendrierPerpetuel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int annee;

            // Saisie sécurisée de l'année
            while (true)
            {
                Console.Write("Saisir une année (ex : 2025) : ");
                string saisie = Console.ReadLine() ?? "";

                if (int.TryParse(saisie, out annee)
                    && annee >= 1900 && annee <= 2100)
                    break;

                Console.WriteLine("Année invalide. Recommence.");
            }

            var cultureFr = CultureInfo.GetCultureInfo("fr-FR");

            // ===== SAISONS =====
            var saisons =
                CalculateurCalendrier.CalculerDatesDebutsSaisons(annee);

            Console.WriteLine($"\nDates des débuts de saisons de l'année {annee}");
            Console.WriteLine(new string('-', 45));
            Console.WriteLine($"- printemps : {saisons.Item1.ToString("ddd dd MMM", cultureFr)}");
            Console.WriteLine($"- été       : {saisons.Item2.ToString("ddd dd MMM", cultureFr)}");
            Console.WriteLine($"- automne   : {saisons.Item3.ToString("ddd dd MMM", cultureFr)}");
            Console.WriteLine($"- hiver     : {saisons.Item4.ToString("ddd dd MMM", cultureFr)}");

            // ===== JOURS FÉRIÉS =====
            var joursFeries =
                CalculateurCalendrier.CalculerJoursFeriesFrancais(annee);

            Console.WriteLine($"\nJours fériés de l'année {annee}");
            Console.WriteLine(new string('-', 45));

            foreach (var jf in joursFeries)
                Console.WriteLine($"- {jf.date.ToString("ddd dd MMM", cultureFr)} : {jf.libelle}");

            // ===== CHANGEMENTS D’HEURES =====
            var changements =
                CalculateurCalendrier.CalculerChangementsHeures(annee);

            Console.WriteLine($"\nChangements d'heures de l'année {annee}");
            Console.WriteLine(new string('-', 45));
            Console.WriteLine($"- Heure d'été   : {changements.ete.ToString("ddd dd MMM HH:mm zzz", cultureFr)}");
            Console.WriteLine($"- Heure d'hiver : {changements.hiver.ToString("ddd dd MMM HH:mm zzz", cultureFr)}");

            // ===== ANNIVERSAIRE (ÉTAPE 6) =====
            DateOnly dateAnniv;
            while (true)
            {
                Console.Write("\nSaisir votre date d'anniversaire (JJ/MM) : ");
                string saisie = Console.ReadLine() ?? "";

                if (DateOnly.TryParseExact(saisie, "dd/MM",
                    cultureFr, DateTimeStyles.None, out dateAnniv))
                    break;

                Console.WriteLine("Date invalide. Recommence.");
            }

            dateAnniv = new DateOnly(annee, dateAnniv.Month, dateAnniv.Day);
            Console.WriteLine(
                $"\nEn {annee}, votre anniversaire sera un {dateAnniv.ToString("dddd", cultureFr)}.");

            Console.WriteLine("\nAppuyez sur Entrée pour quitter...");
            Console.ReadLine();
        }
    }
}
