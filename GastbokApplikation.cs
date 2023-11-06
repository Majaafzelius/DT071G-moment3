using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public static class GastbokApplikation
{
    private static List<GastbokInlagg> gastbokInlaggList = new List<GastbokInlagg>();
    private static string filnamn = "gastbok.json";

    public static void Run()
    {
        LaddaGastbok();

        while (true)
        {
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
                    SparaGastbok();
                    return;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
        }
    }

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

    private static void TaBortInlagg()
    {
        if (gastbokInlaggList.Count > 0)
        {
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

    private static void SparaGastbok()
    {
        string json = JsonConvert.SerializeObject(gastbokInlaggList, Formatting.Indented);
        File.WriteAllText(filnamn, json);
        Console.WriteLine("Gästboken har sparats.");
    }

    private static void LaddaGastbok()
    {
        if (File.Exists(filnamn))
        {
            string json = File.ReadAllText(filnamn);
            gastbokInlaggList = JsonConvert.DeserializeObject<List<GastbokInlagg>>(json);
        }
    }
}
