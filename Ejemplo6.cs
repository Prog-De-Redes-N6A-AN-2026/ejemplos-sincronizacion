using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    internal class Ejemplo6
    {
        static Thread[] threads = new Thread[10];
        static Semaphore sem = new Semaphore(3, 3); // parametros: (initialCount,maximumCount)
        //static Semaphore sem = new Semaphore(0, 3);

        static void Ejecutar()
        {
            Console.WriteLine("[Ejecutar] {0} esta esperando en la fila", Thread.CurrentThread.Name);
            sem.WaitOne();
            Console.WriteLine("[Ejecutar] {0} entra a la region critica!", Thread.CurrentThread.Name);
            Thread.Sleep(300);
            Console.WriteLine("[Ejecutar] {0} sale de la region critica", Thread.CurrentThread.Name);
            sem.Release();
        }

        public static void Ejemplo()
        {
            for (int i = 0; i < 10; i++)
            {
                // Creamos 10 hilos que ejecutan la misma funcion
                threads[i] = new Thread(Ejecutar);
                threads[i].Name = "hilo_" + i;
                threads[i].Start();
            }
            Console.ReadLine();
        }
    }
}
