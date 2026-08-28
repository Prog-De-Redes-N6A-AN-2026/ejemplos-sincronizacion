using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    public abstract class TareaAbstracta
    {
        private Thread? thread;

        public void Ejecutar()
        {
            thread = new Thread(this.Procesamiento);
            thread.Start();
        }

        public void EsperarQueTermine()
        {
            if (thread != null)
                thread.Join();
        }


        protected abstract void Procesamiento();
    }
    public class TareaConcreta : TareaAbstracta
    {
        protected override void Procesamiento()
        {
            Console.WriteLine("[Procesamiento] Empezando tarea concreta!");
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[Procesamiento] Ejecutando iteracion {i} de la tarea");
            }
            Console.WriteLine("[Procesamiento] Finalizando tarea concreta!");
        }
    }

    internal class Ejemplo7
    {
        public static void Ejemplo()
        {
            TareaConcreta tarea = new TareaConcreta();
            Console.WriteLine("[Ejemplo] Empezando tarea!");
            tarea.Ejecutar();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[Ejemplo] Ejecutando codigo en main {i}");
            }

            tarea.EsperarQueTermine();
            Console.WriteLine("[Ejemplo] Fin");
        }
    }
}
