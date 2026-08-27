using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    class LockBDD
    {
        public void GuardarDatos(string texto, uint id)
        {
            lock (this)
            {
                Console.WriteLine($"[LockBDD.GuardarDatos] Ejecutando (hilo {id})");
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(25);
                    Console.Write(texto);
                }
                Console.WriteLine($"\n[LockBDD.GuardarDatos] Terminó (hilo {id})");
            }
        }
    }

    internal class Ejemplo3
    {
        public static LockBDD bd = new LockBDD();

        static void MetodoHiloDeTrabajo(uint id)
        {
            Console.WriteLine($"[MetodoHiloDeTrabajo] Empezando hilo de trabajo secundario {id}");
            Console.WriteLine($"[MetodoHiloDeTrabajo] Llamando BDD.GuardarDatos en hilo de trabajo secundario {id}");
            string texto = (id % 2 == 0) ? "o" : "x";
            bd.GuardarDatos(texto, id);
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
