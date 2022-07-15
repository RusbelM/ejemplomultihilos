using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;

namespace Ejemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Ejemplo de programación multhilo:
            El ejemplo consiste en acceder a un json que contiene los datos de los docentes del itla.
            El programa contiene de 3 funciones:
            - GetDocentes() para obtener los docentes
            - ShowNameDocentesWithEmail() para mostrar los docentes con sus emails
            - ShowNameDocentesWithArea() para mostrar los docentes con sus areas
            */

            //Luego creamos dos hilos, le pasamos las funciones y luego iniciamos dichos hilos.
            Thread showNameWithEmail = new(new ThreadStart(ShowNameDocentesWithEmail));
            Thread showNameWithArea = new(new ThreadStart(ShowNameDocentesWithArea));
            showNameWithEmail.Start();
            showNameWithArea.Start();
        }

        public static void ShowNameDocentesWithEmail()
        {
            try
            {
                GetDocentes().ForEach(doc => {
                    Console.WriteLine($"Docente + email: {doc.profesor} | {doc.correoInstitucional}");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowNameDocentesWithArea()
        {
            try
            {
                GetDocentes().ForEach(doc => {
                    Console.WriteLine($"Docente + área: {doc.profesor} | {doc.area}");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static List<DocenteModel> GetDocentes()
        {
            List<DocenteModel> docentes = new();
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                StreamReader streamReader = new($"{path}/Docentes.json");
                string jsonString = streamReader.ReadToEnd();
                docentes = JsonConvert.DeserializeObject<List<DocenteModel>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return docentes;
        }

    }
}
