using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBS_IT_DEV_RISK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fileIn = Path.Combine(path, "Portfolios.txt");
            string fileOut = Path.Combine(path, "Categorized.txt");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("UBS – IT DEV RISK ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(string.Format("Arquivo de Entrada: {0}", fileIn));

            if (File.Exists(fileIn))
            {
                FileIO file = new FileIO();
                Stopwatch stopwatch = new Stopwatch();

                Console.WriteLine("Lendo o arquivo...");
                try
                {
                    stopwatch.Start();
                    file.Read(fileIn);
                    stopwatch.Stop();

                    Console.WriteLine(string.Format("Leitura concluída ({0}ms).", stopwatch.ElapsedMilliseconds));
                    Console.WriteLine(string.Format("Data de Refência: {0}", file.DateRef.ToString("MM/dd/yyyy")));
                    Console.WriteLine(string.Format("Número de Registros: {0}", file.NumberLines.ToString()));


                    Console.WriteLine("");
                    Console.WriteLine("");


                    Console.WriteLine(string.Format("Arquivo de Saída: {0}", fileOut));
                    Console.WriteLine("Gravando o arquivo...");
                    stopwatch.Restart();
                    file.Save(fileOut);
                    stopwatch.Stop();
                    Console.WriteLine(string.Format("Gravação concluída ({0}ms).", stopwatch.ElapsedMilliseconds));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Arquivo de Portifólios não encontrado.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Pressione ENTER para sair.");
            Console.ReadLine();
        }


    }
}
