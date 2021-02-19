using challenge_servicios.servicios;
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
            int length = messages.Min(x => x.Length);
            var resultsMergeM1M2 = MatrixStringMerge(messages[0], messages[1], length);
            var resultMergeFinal = new List<string[]>();
            foreach (var resultMergeM1M2 in resultsMergeM1M2)
            {
                resultMergeFinal.AddRange(MatrixStringMerge(resultMergeM1M2, messages[2], length));
            }
                        
            return resultMergeFinal.ToArray();
        }


        public string[][] MatrixStringMerge(string[] message1, string[] message2, int length)
        {

            var result = new List<string []>();
            for (int i = 0; i <= message1.Length - length; i++)
            {
                for (int j = 0; j <= message2.Length - length; j++)
                {
                    result.Add(ArrayStringMerge(message1.Skip(i).Take(length).ToArray(), message2.Skip(j).Take(length).ToArray()));
                }
            }
            return result.ToArray();
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
    }
}
