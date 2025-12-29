using System;

namespace CalendrierPerpetuel
{
    internal static class CalculateurCalendrier
    {
        // ===============================
        // CALCUL DE LA DATE DE PÂQUES
        // ===============================
        public static DateOnly CalculerDatePaques(int annee)
        {
            int a = annee % 19;
            int b = annee / 100;
            int c = annee % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;

            int mois = (h + l - 7 * m + 114) / 31;
            int jour = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateOnly(annee, mois, jour);
        }

        // ===============================
        // DÉBUT DES SAISONS
        // ===============================
        public static (DateOnly, DateOnly, DateOnly, DateOnly)
            CalculerDatesDebutsSaisons(int annee)
        {
            return (
                new DateOnly(annee, 3, 20),
                new DateOnly(annee, 6, 21),
                new DateOnly(annee, 9, 22),
                new DateOnly(annee, 12, 21)
            );
        }

        // ===============================
        // JOURS FÉRIÉS FRANÇAIS
        // ===============================
        public static (DateOnly date, string libelle)[]
            CalculerJoursFeriesFrancais(int annee)
        {
            DateOnly paques = CalculerDatePaques(annee);

            return new (DateOnly, string)[]
            {
                (new DateOnly(annee, 1, 1), "Jour de l'an"),
                (paques, "Pâques"),
                (paques.AddDays(1), "Lundi de Pâques"),
                (new DateOnly(annee, 5, 1), "Fête du travail"),
                (new DateOnly(annee, 5, 8), "Armistice 1945"),
                (paques.AddDays(39), "Ascension"),
                (paques.AddDays(49), "Pentecôte"),
                (paques.AddDays(50), "Lundi de Pentecôte"),
                (new DateOnly(annee, 7, 14), "Fête nationale"),
                (new DateOnly(annee, 8, 15), "Assomption"),
                (new DateOnly(annee, 11, 1), "Toussaint"),
                (new DateOnly(annee, 11, 11), "Armistice 1918"),
                (new DateOnly(annee, 12, 25), "Noël")
            };
        }

        // ===============================
        // CHANGEMENTS D’HEURES
        // ===============================
        public static (DateTimeOffset ete, DateTimeOffset hiver)
            CalculerChangementsHeures(int annee)
        {
            DateTime dernierDimancheMars = new DateTime(annee, 3, 31);
            while (dernierDimancheMars.DayOfWeek != DayOfWeek.Sunday)
                dernierDimancheMars = dernierDimancheMars.AddDays(-1);

            DateTimeOffset heureEte =
                new DateTimeOffset(dernierDimancheMars.AddHours(2),
                new TimeSpan(1, 0, 0));

            DateTime dernierDimancheOctobre = new DateTime(annee, 10, 31);
            while (dernierDimancheOctobre.DayOfWeek != DayOfWeek.Sunday)
                dernierDimancheOctobre = dernierDimancheOctobre.AddDays(-1);

            DateTimeOffset heureHiver =
                new DateTimeOffset(dernierDimancheOctobre.AddHours(3),
                new TimeSpan(2, 0, 0));

            return (heureEte, heureHiver);
        }
    }
}
