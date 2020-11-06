using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progmet_inlämning_01
{
    class Program
    {

        class Person
        {
            public string namn, adress, telefon, mail;

            public Person(string n, string a, string t, string m)
            {
                namn = n; adress = a; telefon = t; mail = m;
            }

            public string Print(bool edit)
            {
                if (edit == true)
                {
                    return namn + "-" + adress + "-" + telefon + "-" + mail;
                }

                else
                {
                    Console.WriteLine(namn + adress + telefon + mail);
                }

                return namn + "-" + adress + "-" + telefon + "-" + mail;
            }

        }

        static void Main(string[] args)
        {
            string[] telbok;
            string textdokument;
            List<Person> folk = new List<Person>();
            string svar;
            string[] k = new string[4];

            Console.WriteLine("skriv in filnamn");
            textdokument = Console.ReadLine();

            //puts file content to string array, source: https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/
            telbok = File.ReadAllLines(textdokument);

            //asigns file content to person list
            for (int i = 0; i < telbok.Length; i++)
            {
                k = telbok[i].Split('-');
                folk.Add(new Person(k[0], k[1], k[2], k[3]));
            }

            do
            {
                Console.WriteLine();
                Console.WriteLine("skriv in en av valen");
                Console.WriteLine("1: lägg till kontakt");
                Console.WriteLine("2: ta bort kontakt");
                Console.WriteLine("3: visa kontaktlista");
                Console.WriteLine("4: redigera kontakt");
                
                svar = Console.ReadLine();
                svar.ToLower();
                
                Console.WriteLine();

                if (svar == "lägg till kontakt" || svar == "1") // adds contact to list
                {
                    string tel;
                    string namn;
                    string adress;
                    string mail;

                    Console.Write("namn: ");
                    namn = Console.ReadLine();

                    Console.Write("adress: ");
                    adress = Console.ReadLine();

                    Console.Write("telefon nummer: ");
                    tel = Console.ReadLine();

                    Console.Write("mail: ");
                    mail = Console.ReadLine();

                    namn = namn + "-" + adress + "-" + tel + "-" + mail;

                    //saves changes to file, source: https://www.c-sharpcorner.com/article/csharp-streamwriter-example/
                    using (StreamWriter outputFile = new StreamWriter(textdokument, true))
                    {
                        outputFile.WriteLine(namn);
                    }

                    folk.Add(new Person(namn, adress, tel, mail)); 

                    Console.WriteLine(namn + " tillagt");
                }

                else if (svar == "visa kontaktlista" || svar == "3")
                {
                    for (int i = 0; i < folk.Count; i++)
                    {
                        Console.Write(i + 1 + ": "); folk[i].Print(false); // writes out list of contacts
                    }
                }

                else if (svar == "ta bort kontakt" || svar == "2") //removes contact from list
                {
                    Console.WriteLine("skriv index av vem som ska bort"); 
                    int num = int.Parse(Console.ReadLine());
                    folk.RemoveAt(num - 1);

                    using (StreamWriter outputFile = new StreamWriter(textdokument))
                    {
                        for (int i = 0; i < folk.Count; i++)
                        {
                            outputFile.WriteLine(folk[i].Print(true)); // saves changes to file
                        }
                    }
                }

                else if(svar == "redigera kontakt" || svar == "4") // edits contactinfo
                {
                    Console.WriteLine();
                    Console.WriteLine("kontakt index");
                    int p = int.Parse(Console.ReadLine())-1;

                    Console.WriteLine();
                    Console.WriteLine("vilken informaion vill du ändra?");
                    Console.WriteLine("1: namn");
                    Console.WriteLine("2: adress");
                    Console.WriteLine("3: telefon");
                    Console.WriteLine("4: mail");
                    string info = Console.ReadLine();
                    info.ToLower();

                    if(info == "namn" ||info == "1")
                    {
                        Console.Write("nytt namn: ");
                        folk[p].namn = Console.ReadLine();
                    }

                    else if(info == "adress" || svar == "2")
                    {
                        Console.Write("ny adress: ");
                        folk[p].adress = Console.ReadLine();
                    }

                    else if(info == "telefon" || info == "3")
                    {
                        Console.Write("nytt telefon nummer: ");
                        folk[p].telefon = Console.ReadLine();
                    }

                    else if(info == "mail" || info == "4")
                    {
                        Console.Write("nytt mail: ");
                        folk[p].mail = Console.ReadLine();
                    }

                    else
                    {
                        Console.WriteLine("okänd kommand");
                    }

                    using (StreamWriter outputFile = new StreamWriter(textdokument))
                    {
                        for (int i = 0; i < folk.Count; i++)
                        {
                            outputFile.WriteLine(folk[i].Print(true)); // saves changes to file
                        }
                    }

                    Console.WriteLine("info redigerad");
                }
                else
                {
                    Console.WriteLine("okänd kommando, försök igen");
                    Console.WriteLine();
                }

            }
            while (svar != "sluta");//stops program

            Console.WriteLine("adjö");

            Console.ReadKey();
        }
    }
}
