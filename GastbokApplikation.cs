using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

// Programmets huvudklass
public static class GastbokApplikation
{
    // Lista för att lagra gästboksinlägg
    private static List<GastbokInlagg> gastbokInlaggList = new List<GastbokInlagg>();
    // Filnamn för att spara och ladda gästboken
    private static string filnamn = "gastbok.json";

// Metod för att köra gästbokapplikationen
    public static void Run()
    {
        // Ladda gästboken från filen vid programstart
        LaddaGastbok();

        while (true)
        {
            // Meny
            Console.WriteLine("Välkommen till gästboken!");
            Console.WriteLine("1. Lägg till inlägg");
            Console.WriteLine("2. Ta bort inlägg");
            Console.WriteLine("3. Visa alla inlägg");
            Console.WriteLine("4. Avsluta");

            Console.Write("Välj ett alternativ (1-4): ");
            string val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    LaggTillInlagg();
                    break;

                case "2":
                    TaBortInlagg();
                    break;

                case "3":
                    VisaAllaInlagg();
                    break;

                case "4":
                    // Spara Gästboken i json-filen och avsluta programmet
                    SparaGastbok();
                    return;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            // Rensa konsolfönstret för nästa iteration
            Console.Clear();
        }
    }

    // Metod för att lägga till ett gästboksinlägg
    private static void LaggTillInlagg()
    {
        Console.Write("Ange ägare till inlägget: ");
        string agare = Console.ReadLine();

        Console.Write("Ange texten för inlägget: ");
        string text = Console.ReadLine();

        if (!string.IsNullOrEmpty(agare) && !string.IsNullOrEmpty(text))
        {
            GastbokInlagg inlagg = new GastbokInlagg { Agare = agare, Text = text };
            gastbokInlaggList.Add(inlagg);
            Console.WriteLine("Inlägget har lagts till.");
        }
        else
        {
            Console.WriteLine("Fel: Både ägare och text måste anges.");
        }
    }

    // Metod för att ta bort ett gästboksinlägg
    private static void TaBortInlagg()
    {
        if (gastbokInlaggList.Count > 0)
        {
            // Visa alla inlägg för användaren att välja från
            VisaAllaInlagg();
            Console.Write("Ange index på inlägget att ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < gastbokInlaggList.Count)
            {
                gastbokInlaggList.RemoveAt(index);
                Console.WriteLine("Inlägget har tagits bort.");
            }
            else
            {
                Console.WriteLine("Ogiltigt index. Försök igen.");
            }
        }
        else
        {
            Console.WriteLine("Gästboken är tom. Inga inlägg att ta bort.");
        }
    }

    // Metod för att visa alla gästboksinlägg
    private static void VisaAllaInlagg()
    {
        if (gastbokInlaggList.Count > 0)
        {
            Console.WriteLine("Alla inlägg i gästboken:");
            for (int i = 0; i < gastbokInlaggList.Count; i++)
            {
                Console.WriteLine($"{i}. Ägare: {gastbokInlaggList[i].Agare}, Text: {gastbokInlaggList[i].Text}");
            }
        }
        else
        {
            Console.WriteLine("Gästboken är tom. Inga inlägg att visa.");
        }
    }

    // Metod för att spara gästboken i en JSON-fil
    private static void SparaGastbok()
    {
        string json = JsonConvert.SerializeObject(gastbokInlaggList, Formatting.Indented);
        File.WriteAllText(filnamn, json);
        Console.WriteLine("Gästboken har sparats.");
    }

    // Metod för att ladda gästboken från en JSON-fil
    private static void LaddaGastbok()
    {
        if (File.Exists(filnamn))
        {
            string json = File.ReadAllText(filnamn);
            gastbokInlaggList = JsonConvert.DeserializeObject<List<GastbokInlagg>>(json);
        }
    }
}
