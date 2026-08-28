using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    internal class Ejemplo10
    {
        public static void Ejemplo()
        {
            Console.WriteLine("[Ejemplo] Empezando hilo de trabajo de segundo plano!");

            BackgroundWorker bw = new BackgroundWorker();
            // Esto soluciona que termine el worker sin hacer ReadLine
            // AutoResetEvent done = new AutoResetEvent(false);

            bw.DoWork += MetodoDeTrabajo;

            bw.RunWorkerCompleted += AlTerminar;
            // Esto soluciona que termine el worker sin hacer ReadLine
            // bw.RunWorkerCompleted += (s, e) => done.Set();

            bw.RunWorkerAsync();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[Ejemplo] Ejecutando codigo en main {i}");
            }

            // Esto soluciona que termine el worker sin hacer ReadLine
            // Cuando ocurre .Set, continua y el evento vuelve a fals
            // done.WaitOne();

            Console.ReadLine();
        }

        static void MetodoDeTrabajo(object? sender, DoWorkEventArgs e)
        {
            Console.WriteLine("[MetodoDeTrabajo] Empezo a trabajar!");
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[MetodoDeTrabajo] Ejecutando iteracion {i}");
            }
        }

        static void AlTerminar(object? sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("[AlTerminar] Termino de trabajar!");
        }
    }
}
