using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    public delegate void TerminarTareaDelegate(TareaAbstractaCallback tareaAbstracta);
    
    public abstract class TareaAbstractaCallback
    {
        private Thread? thread;
        private TerminarTareaDelegate? callback;
        protected int resultado;

        public int Resultado
        {
            get
            {
                return resultado;
            }
        }

        public void Ejecutar(TerminarTareaDelegate callback)
        {
            this.callback = callback;
            thread = new Thread(this.Procesar);
            thread.Start();
        }

        private void Procesar()
        {
            this.Procesamiento();
            if (callback != null)
                callback(this);
        }

        protected abstract void Procesamiento();
    }

    public class TareaConcretaCallback : TareaAbstractaCallback
    {
        protected override void Procesamiento()
        {
            Console.WriteLine("[Procesamiento] Empezando tarea concreta!");
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[Procesamiento] Ejecutando iteracion {i} de la tarea");
                resultado += i;
            }
            Console.WriteLine("[Procesamiento] Finalizando tarea concreta!");
        }
    }

    internal class Ejemplo8
    {
        public static void Ejemplo()
        {
            Console.WriteLine("[Ejemplo] Empezando tarea!");

            TareaConcretaCallback tareaConcretaCallback = new TareaConcretaCallback();

            tareaConcretaCallback.Ejecutar(ImprimirQueTerminoTarea);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[Ejemplo] Ejecutando codigo en main {i}");
            }
        }

        static void ImprimirQueTerminoTarea(TareaAbstractaCallback tarea)
        {
            Console.WriteLine($"[ImprimirQueTerminoTarea] Fin, resultado: {tarea.Resultado}");
        }
    }
}
