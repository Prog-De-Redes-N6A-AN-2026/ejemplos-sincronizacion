using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    class MonitorBDD
    {
        public void GuardarDatos(string texto, uint id)
        {
            Monitor.Enter(this);
            Console.WriteLine($"[MonitorBDD.GuardarDatos] Ejecutando (hilo {id})");
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(25);
                Console.Write(texto);
            }
            Console.WriteLine($"\n[MonitorBDD.GuardarDatos] Terminó (hilo {id})");
            Monitor.Exit(this);
        }

        public void GuardarDatosException(string texto, uint id)
        {
            Monitor.Enter(this);
            Console.WriteLine($"[MonitorBDD.GuardarDatos] Ejecutando (hilo {id})");

            // Ocurre un error antes de liberar el lock con Monitor.Exit.
            throw new Exception("ERROR!");
            
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(25);
                Console.Write(texto);
            }
            Console.WriteLine($"\n[MonitorBDD.GuardarDatos] Terminó (hilo {id})");
            Monitor.Exit(this);
        }

        public void GuardarDatosExceptionSolved(string texto, uint id)
        {
            Monitor.Enter(this);
            try
            {
                Console.WriteLine($"[MonitorBDD.GuardarDatos] Ejecutando (hilo {id})");

                // Ocurre un error antes de terminar el trabajo.
                // Al tener Monitor.Exit en el bloque finally, se libera indistintamente.
                throw new Exception("ERROR!");

                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(25);
                    Console.Write(texto);
                }
                Console.WriteLine($"\n[MonitorBDD.GuardarDatos] Terminó (hilo {id})");
            }
            finally
            {
                Monitor.Exit(this);
            }
        }
    }

    internal class Ejemplo2
    {
        public static MonitorBDD bd = new MonitorBDD();

        static void MetodoHiloDeTrabajo(uint id)
        {
            Console.WriteLine($"[MetodoHiloDeTrabajo] Empezando hilo de trabajo secundario {id}");
            Console.WriteLine($"[MetodoHiloDeTrabajo] Llamando BDD.GuardarDatos en hilo de trabajo secundario {id}");
            string texto = (id % 2 == 0) ? "o" : "x";
            bd.GuardarDatos(texto, id);
            Console.WriteLine($"[MetodoHiloDeTrabajo] Hilo de trabajo secundario {id} terminó");
        }

        static void MetodoHiloDeTrabajoException(uint id)
        {
            Console.WriteLine($"[MetodoHiloDeTrabajo] Empezando hilo de trabajo secundario {id}");
            Console.WriteLine($"[MetodoHiloDeTrabajo] Llamando BDD.GuardarDatos en hilo de trabajo secundario {id}");
            string texto = (id % 2 == 0) ? "o" : "x";
            try
            {
                bd.GuardarDatosException(texto, id);
            }
            catch (Exception)
            {
            }
            Console.WriteLine($"[MetodoHiloDeTrabajo] Hilo de trabajo secundario {id} terminó");
        }

        static void MetodoHiloDeTrabajoExceptionSolved(uint id)
        {
            Console.WriteLine($"[MetodoHiloDeTrabajo] Empezando hilo de trabajo secundario {id}");
            Console.WriteLine($"[MetodoHiloDeTrabajo] Llamando BDD.GuardarDatos en hilo de trabajo secundario {id}");
            string texto = (id % 2 == 0) ? "o" : "x";
            try
            {
                bd.GuardarDatosExceptionSolved(texto, id);
            }
            catch (Exception)
            {
            }
            Console.WriteLine($"[MetodoHiloDeTrabajo] Hilo de trabajo secundario {id} terminó");
        }

        public static void Ejemplo()
        {
            Console.WriteLine("[Ejemplo] Creando hilos de trabajo secundarios");

            Thread t1 = new Thread(() => MetodoHiloDeTrabajo(1));
            Thread t2 = new Thread(() => MetodoHiloDeTrabajo(2));

            t1.Start();
            t2.Start();
        }
    }
}
