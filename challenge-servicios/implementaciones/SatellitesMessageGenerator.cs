﻿using challenge_servicios.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace challenge_servicios.implementaciones
{
    /// <summary>
    /// Responsabilidad: Formar un mensaje realizando un merge entre los mensajes obtenidos por los satélites
    /// </summary>
    public class SatellitesMessageGenerator : MessageGenerator
    {
        StrategyStringMerger _merger;
        public SatellitesMessageGenerator(StrategyStringMerger merger)
        {
            this._merger = merger;
        }

        /// <summary>
        /// Devuelve un mensaje que forma mediante el merge de los mensajes recibidos por los satélites.
        /// Se permite un desfasaje de los mensajes de una posición a izquierda o derecha. 
        /// Se contempla que el contenido de una de las posiciones del arreglo del mensaje se pierda, es decir llegue vacío; 
        /// pero no se contempla que se pierda un índice del arreglo del mensaje, por lo que se devuelve un mensaje del tamaño del menor de los arreglos recibidos.
        /// La diferencia del tamaño de los arreglos recibidos no puede ser superior a uno.
        /// </summary>
        /// <param name="messages">Mensajes recibidos por cada satélite. Cada mensaje corresponde a un arreglo de palabras.</param>
        /// <returns>Mensaje que se forma a partir de los mensajes recibidos.</returns>
        /// <exception cref="System.ArgumentException">Si no se recibe la misma cantidad de mensajes que de satélites disponibles</exception>
        /// <exception cref="System.ArgumentException">Si no se recibe la misma cantidad de mensajes que de satélites disponibles</exception>
        /// <exception cref="System.ArgumentNullException">Si la entrada es nula</exception>
        public string GetMessage(params string[][] messages)
        {
            if (messages == null)
            {
                throw new ArgumentNullException();
            }
            if (messages.Length != 3)
            {
                throw new ArgumentException();   
            }
            string[] result = Solve(messages);
            return string.Join(" ", result);
        }

        public string[] Solve(params string[][] messages)
        {
            string[][] solutions = Solutions(messages);

            return BestSolution(solutions);
            
        }

        private string[] BestSolution(string[][] solutions)
        {
            string[] solution = solutions[0];
            string[] auxSolution;
            int? repetitions;
            Dictionary<string, int> stringsRepetitions = solution.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            repetitions = stringsRepetitions.Count(x => !x.Key.Equals(""));
            Dictionary<string, int> auxStringsRepetitions;
            for (int i = 1; i < solutions.Length; i++)
            {
                auxSolution = solutions[i];
                auxStringsRepetitions = auxSolution.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
                if (auxStringsRepetitions.Count(x => !x.Key.Equals("")) > repetitions)
                {
                    repetitions = auxStringsRepetitions.Count(x => !x.Key.Equals(""));
                    solution = auxSolution;
                }
            }
            return solution;
        }

        public string[][] Solutions(params string[][] messages)
        {
            string[] result = messages[0];
            
            int length = messages.Min(x => x.Length);
            var pepe = algo(messages[0], messages[1], length);
            var result2 = new List<string[]>();
            foreach (var lala in pepe)
            {
                result2.AddRange(algo(lala, messages[2], length));
            }
                        
            return result2.ToArray();
        }


        public string[][] algo(string[] a, string[] b, int length)
        {

            var result = new List<string []>();
            for (int i = 0; i <= a.Length - length; i++)
            {
                for (int j = 0; j <= b.Length - length; j++)
                {
                    result.Add(ArrayStringMerge(a.Skip(i).Take(length).ToArray(), b.Skip(j).Take(length).ToArray()));
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayMessageOne"></param>
        /// <param name="arrayMessageTwo"></param>
        /// <returns></returns>
        public string[] ArrayStringMerge(string[] arrayMessageOne, string[] arrayMessageTwo, int index1, int index2, int length)
        {
            
            return ArrayStringMerge(arrayMessageOne.Skip(index1).Take(length).ToArray(), arrayMessageTwo.Skip(index2).Take(length).ToArray());
            
        }

        public string[] ArrayStringMerge(string[] arrayMessageOne, string[] arrayMessageTwo)
        {
            var result = new List<string>();

            for (int index = 0; index < arrayMessageOne.Length; index++)
            {
                if (arrayMessageOne[index].Equals(arrayMessageTwo[index]))
                    result.Add(arrayMessageOne[index]);
                else if (string.IsNullOrEmpty(arrayMessageOne[index]))
                    result.Add(arrayMessageTwo[index]);
                else if (string.IsNullOrEmpty(arrayMessageTwo[index]))
                    result.Add(arrayMessageOne[index]);
                else
                    result.Add(string.Empty);
            }

            return result.ToArray();
        }

        public string[] ArrayStringMerger2(string[] arrayMessageOne, string[] arrayMessageTwo)
        {
            var result = new List<string>();

            if (arrayMessageOne.Length != arrayMessageTwo.Length)
                throw new ArgumentException("La cantidad de emisores no corresponde con la esperada.");

            int indexOne, indexTwo, cantCoincidencias = 0, auxcantCoincidencias;
            int indexOneResult;
            int indexTwoResult;
            for (int i = 0; i <= 1; i++)
            {
                indexOne = i;
                for (int j = 0; j <= 1; j++)
                {
                    indexTwo = j;
                    auxcantCoincidencias = ContarCoincidenciasNoVacias(arrayMessageOne, arrayMessageTwo, indexOne, indexTwo);
                    if (auxcantCoincidencias > cantCoincidencias)
                    {
                        indexOneResult = indexOne;
                        indexTwoResult = indexTwo;
                        cantCoincidencias = auxcantCoincidencias;
                    }
                }

            }

            return result.ToArray();
        }

        public string[] ArrayStringMergeFromIndex(string[] arrayMessageOne, string[] arrayMessageTwo, int indexOne, int indexTwo)
        {
            var result = new List<string>();

            return result.ToArray();
        }

        public int CoincidenciasConDesfasaje(string[] arrayMessageOne, string[] arrayMessageTwo)
        {            
            int indexOne = 0, indexTwo = 0, result = 0, auxResult;
            int indexOneResult = 0; 
            int indexTwoResult = 0;
            for (int i = 0; i <= 1; i++)
            {
                auxResult = ContarCoincidenciasNoVacias(arrayMessageOne, arrayMessageTwo, indexOne, indexTwo);
                if (auxResult > result)
                {
                    indexOneResult = indexOne;
                    indexTwoResult = indexTwo;
                    result = auxResult;
                }
            }
            

            return result;
        }

        private int ContarCoincidenciasNoVacias(string[] arrayMessageOne, string[] arrayMessageTwo, int indexOne, int indexTwo)
        {
            if (arrayMessageOne.Length == indexOne || arrayMessageTwo.Length == indexTwo)
                return 0;

            return (arrayMessageOne[indexOne].Equals(arrayMessageTwo[indexTwo]) && !string.IsNullOrEmpty(arrayMessageOne[indexOne]) ? 1 : 0) 
                + ContarCoincidenciasNoVacias(arrayMessageOne, arrayMessageTwo, indexOne + 1, indexTwo + 1);
        }
    }
}
