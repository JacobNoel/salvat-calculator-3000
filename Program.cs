using System;

namespace GLO_GIF_SAPOUD_SALVAT_CALCULATOR_3000
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] noms = new string[] { "Jack", "Marc", "Rem", "Parc" };
            decimal[] coutSecParIng = new decimal[noms.Length];
            decimal[][] coutTotalParMethodeParIng = new decimal[Enum.GetValues(typeof(methodesDeCalcul)).Length][];
            decimal totalSec;
            decimal totalFinal;

            // Total par ing. avant taxes et shipping et tip
            int i = 0;
            foreach (string nom in noms)
            {
                int j = 1;
                decimal? coutArticle;

                while (true)
                {
                    Console.WriteLine("Veuillez ajouter le cout du " + j + "e article de " + nom + ". Appuyez sur enter si aucun autre n'existe.");
                    try
                    {
                        string result = Console.ReadLine();
                        if (string.IsNullOrEmpty(result))
                        {
                            break;
                        }
                        coutArticle = decimal.Parse(result);
                    }
                    catch
                    {
                        Console.WriteLine("Tu viendras me revoir quand tu sauras compter");
                        return;
                    }
                    coutSecParIng[i] += (decimal)coutArticle;
                    j++;
                }
                i++;
            }

            // Total avant taxes, shipping et tip
            Console.WriteLine("Veuillez inscrire le montant total sec.");
            try
            {
                totalSec = decimal.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Tu viendras me revoir quand tu sauras compter");
                return;
            }

            // Total total
            Console.WriteLine("Veuillez inscrire le montant total après taxes, shipping et tip.");
            try
            {
                totalFinal = decimal.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Tu viendras me revoir quand tu sauras compter");
                return;
            }

            // Boucle sur toutes les méthodes
            int k = 0;
            Array methodes = Enum.GetValues(typeof(methodesDeCalcul));
            foreach (methodesDeCalcul meth in methodes)
            {
                coutTotalParMethodeParIng[k] = new decimal[noms.Length];
                // TheoREM, qui constitue à ajouter le meme montant à chaque ing
                if (meth == methodesDeCalcul.TheoREM)
                {
                    decimal delta = totalFinal - totalSec;
                    decimal montantAAjouterAChaqueIng = delta / noms.Length;

                    for (i = 0; i < noms.Length; i++)
                    {
                        coutTotalParMethodeParIng[0][i] = coutSecParIng[i] + montantAAjouterAChaqueIng;
                    }
                }
                // JackFact, qui ajoute un montant proportionnel au coût du repas
                else if (meth == methodesDeCalcul.JackFact)
                {
                    for (i = 0; i < noms.Length; i++)
                    {
                        coutTotalParMethodeParIng[1][i] = (coutSecParIng[i] * totalFinal) / totalSec;
                    }
                }
                k++;
            }

            // Affichage
            string message = String.Format("{0,10}", "");
            foreach (string nom in noms)
            {
                message += String.Format("{0,-10}", nom);
            }
            message += "\n";

            i = 0;
            foreach (methodesDeCalcul meth in Enum.GetValues(typeof(methodesDeCalcul)))
            {
                message += String.Format("{0,-10}", meth);
                for (int j = 0; j < noms.Length; j++)
                {
                    message += String.Format("{0,-10}", coutTotalParMethodeParIng[i][j].ToString("0.00"));
                }
                message += "\n";
                i++;
            }
            Console.Write(message);
        }

        enum methodesDeCalcul
        {
            TheoREM,
            JackFact
        }
    }
}
