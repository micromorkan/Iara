using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static int[] numeros = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
        private static string[] palavras = { "abracadabra", "allottee", "assessee" };

        static void Main(string[] args)
        {
            VerificarArrayImpar(numeros);
            RemoverLetrasDuplicadas(palavras);
        }

        static void VerificarArrayImpar(int[] array)
        {
            var somentePares = array.ToList().Where(x => x % 2 == 0);

            if (somentePares.Count() > 0)
            {
                Console.WriteLine("Infelizmente a sequencia contem números pares.");
                Console.WriteLine("Os seguintes números da sequencia são pares: ");
                Console.WriteLine(string.Join(",", somentePares.Select(x => x.ToString()).ToArray()));
            }
            else
            {
                Console.WriteLine("A lista é composta somente por números ímpares!");
                Console.WriteLine(string.Join(",", array.ToList().Select(x => x.ToString()).ToArray()));
            }
        }

        static void RemoverLetrasDuplicadas(string[] array)
        {
            string[] novoArray = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                string resultString = string.Empty;

                for (int x = 0; x < array[i].Length; x++)
                {
                    if (resultString.Length == 0)
                    {
                        resultString += array[i][x];
                    }
                    else if (resultString.Length != 0 && resultString.Substring(resultString.Length - 1) != array[i][x].ToString())
                    {
                        resultString += array[i][x];
                    }
                }

                novoArray[i] = resultString;
            }

            Console.WriteLine("");
            Console.WriteLine(string.Join(",", novoArray.ToList().Select(x => x.ToString()).ToArray()));
        }
    }
}
