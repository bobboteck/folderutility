using System;
using System.IO;

namespace folderutility
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                Console.WriteLine("Analisi in corso ...");

                RiorganizzaDati(args[0]);

                Console.WriteLine("Operazione completata.");
            }
            else
            {
                Console.WriteLine("Attenzione per poter eseguire il tool è necessario indicare un percorso");
                Console.WriteLine("Es.: dotnet folderutility.dll -- /home/me/Scrivania/fotofolder/");
                Console.WriteLine(@"     dotnet folderutility.dll -- C:\users\me\descktop\fotofolder/");
            }
        }

        private static void RiorganizzaDati(string cartellaBase, bool soloVerifica=false)
        {
            string[] folders = Directory.GetDirectories(cartellaBase);

            foreach(string folder in folders)
            {
                string folderName = Path.GetFileName(folder);
                Console.Write(">{0} - ", folderName);

                string[] files = Directory.GetFiles(folder);

                foreach(string file in files)
                {
                    string fileExtension = Path.GetExtension(file);
                    if(fileExtension.ToLower() == ".avi" || fileExtension.ToLower() == ".mov")
                    {
                        if(!soloVerifica)
                        {
                            File.Delete(file);
                        }
                        Console.Write("({0})", Path.GetFileName(file));
                    }
                }

                // Verifica se il nome della cartella contiene degli undescore
                if(folderName.Contains("_"))
                {
                    string newfolderName = folderName.Replace("_", "");
                    if(!soloVerifica)
                    {
                        Directory.Move(folder, cartellaBase + newfolderName);
                    }
                    Console.Write(" - {0}\n", newfolderName);
                }
                else
                {
                    Console.Write(" - {0}\n", folderName);
                }
            }
        }
    }
}
