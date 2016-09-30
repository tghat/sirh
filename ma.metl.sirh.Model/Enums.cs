using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ma.metl.sirh.Model
{

    public enum Etat
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("En cours")]
        EnCours,

        [Description("Validé")]
        Valide,

        [Description("Rejeté")]
        Rejete
    }

    public enum EtatQuota
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("En cours")]
        EnCours,

        [Description("Envoyé pour Visa")]
        Envoye,

        [Description("Visé")]
        Vise
    }

    public enum EtatCandidature
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("Envoyée")]
        Envoyee,

        [Description("Signée")]
        Signee,

        [Description("Rejetée")]
        Rejetee,

        [Description("Validée")]
        Validee,
    }

    public enum EtatDetailAVC
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("En cours")]
        EnCours,

        [Description("Validé")]
        Valide,

        [Description("Vérifié")]
        Verifie,

        [Description("Retenu")]
        Retenu,

        [Description("Non Retenu")]
        NonRetenu,
    }

    public enum TypeFlux
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("Avancement au choix")]
        AC,

        [Description("Avancement sur examen")]
        AE,
    }

    public enum EtatPromotion
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("Admis")]
        Admis,

        [Description("Non Admis")]
        Nadmis,
    }

    public enum Resultats
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("Résultats de l'écrit")]
        ResEcrit,

        [Description("Résultats de l'oral")]
        ResOral,

        [Description("Résultats définitifs")]
        ResDef,
    }

    public enum EtatPublication
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("Active")]
        Active,

        [Description("Inactive")]
        Inactive
    }

    public enum TypeAvancement
    {

        [Description("Au choix")]
        AuChoix,

        [Description("Sur examen")]
        SurExamen
    }

    public enum TypePublication
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description("Programe des examens")]
        ProgExam,

        [Description(" Liste des promouvables")]
        ListPromouvable,

        [Description(" Résultats de l'ecrit")]
        ResEcrit,

        [Description(" Résultats de l'oral")]
        ResOral,

        [Description(" Résultats définitifs")]
        ResDef
    }

    public enum TypePublicationAVC
    {
        [Description("Sélectionnez")]
        Selectionnez,

        [Description(" Liste des promouvables")]
        ListPromouvable,

        [Description(" Résultats")]
        Resultats,

    }

    public enum Annee
    {
     

        [Description("2000")]
        A0 = 2000,

        [Description("2001")]
        A1 = 2001,

        [Description("2002")]
        A2 = 2002,

        [Description("2003")]
        A3 = 2003,

        [Description("2004")]
        A4 = 2004,

        [Description("2005")]
        A5 = 2005,

        [Description("2006")]
        A6 = 2006,

        [Description("2007")]
        A7 = 2007,

        [Description("2008")]
        A8 = 2008,

        [Description("2009")]
        A9 = 2009,

        [Description("2010")]
        A10 = 2010,

        [Description("2011")]
        A11 = 2011,

        [Description("2012")]
        A12 = 2012,

        [Description("2013")]
        A13 = 2013,

        [Description("2014")]
        A14 = 2014,

        [Description("2015")]
        A15 = 2015,

        [Description("2016")]
        A16 = 2016,

        [Description("2017")]
        A17 = 2017,

        [Description("2018")]
        A18 = 2018,

        [Description("2019")]
        A19 = 2019,

        [Description("2020")]
        A20 = 2020,

        [Description("2021")]
        A21 = 2021,

        [Description("2022")]
        A22 = 2022,

        [Description("2023")]
        A23 = 2023,

        [Description("2024")]
        A24 = 2024,

        [Description("2025")]
        A25 = 2025,

        [Description("2026")]
        A26 = 2026,

        [Description("2027")]
        A27 = 2027,

        [Description("2028")]
        A28 = 2028,

        [Description("2029")]
        A29 = 2029,

        [Description("2030")]
        A30 = 2030,
    }

    public enum Heure
    {
        [Description("01")]
        H1 = 01,
        [Description("02")]
        H2 = 02,
        [Description("03")]
        H3 = 03,
        [Description("04")]
        H4 = 04,
        [Description("05")]
        H5 = 05,
        [Description("06")]
        H6 = 06,
        [Description("07")]
        H7 = 07,
        [Description("08")]
        H8 = 08,
        [Description("09")]
        H9 = 09,
        [Description("10")]
        H10 = 10,
        [Description("11")]
        H11 = 11,
        [Description("12")]
        H12 = 12,
    }

    public enum Minute
    {
        [Description("05")]
        M1 = 05,
        [Description("10")]
        M2 = 10,
        [Description("15")]
        M3 = 15,
        [Description("20")]
        M4 = 20,
        [Description("25")]
        M5 = 25,
        [Description("30")]
        M6 = 30,
        [Description("35")]
        M7 = 35,
        [Description("40")]
        M8 = 40,
        [Description("45")]
        M9 = 45,
        [Description("50")]
        M10 = 50,
        [Description("55")]
        M11 = 55,
        [Description("00")]
        M12 = 00,
    }

    public enum ReunionStatus
    {
        [Description("#01DF3A:Validée")] // green
        Valide = 0,
        [Description("#FF8000:Reportée")] // orange
        Reporte,
        [Description("#FF0000:Annulée")] // red
        Annule

    }

    public static class Enums
    {
        /// Get all values
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// Get all the names
        public static IEnumerable<T> GetNames<T>()
        {
            return Enum.GetNames(typeof(T)).Cast<T>();
        }

        /// Get the name for the enum value
        public static string GetName<T>(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        /// Get the underlying value for the Enum string
        public static int GetValue<T>(string enumString)
        {
            return (int)Enum.Parse(typeof(T), enumString.Trim());
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static string GetEnumName<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).ToList();

            foreach (var n in name)
            {
                var field = type.GetField(n);
                var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var descr = ((DescriptionAttribute)customAttribute[0]).Description.Substring(((DescriptionAttribute)customAttribute[0]).Description.ToString().IndexOf(":") + 1);
                if (descr.Equals(value))
                    return n;
            }

            return null;
        }


    }


}