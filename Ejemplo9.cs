using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    public delegate void TerminarTareaEvento(TareaAbstractaEvento tareaAbstracta);

    public abstract class TareaAbstractaEvento
    {
        private Thread? thread;
        public event TerminarTareaEvento? AlTerminarTarea;
        protected int resultado;

        public int Resultado
        {
            get
            {
                return resultado;
            }
        }

        public void Ejecutar()
        {
            thread = new Thread(this.Procesar);
            thread.Start();
        }
        
        private void Procesar()
        {
            this.Procesamiento();
            if (AlTerminarTarea != null)
                AlTerminarTarea(this);
        }

        protected abstract void Procesamiento();
    }

    public class TareaConcretaEvento : TareaAbstractaEvento
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

    internal class Ejemplo9
    {
        public static void Ejemplo()
        {
            Console.WriteLine("[Ejemplo] Empezando tarea!");

            TareaConcretaEvento tareaConcreta = new TareaConcretaEvento();

            tareaConcreta.Ejecutar();

            tareaConcreta.AlTerminarTarea += new TerminarTareaEvento(ImprimirQueTerminoTarea);
            tareaConcreta.AlTerminarTarea += new TerminarTareaEvento(ImprimirQueTerminoTarea);
            tareaConcreta.AlTerminarTarea += new TerminarTareaEvento(ImprimirQueTerminoTarea);
            tareaConcreta.AlTerminarTarea += new TerminarTareaEvento(ImprimirQueTerminoTarea);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"[Ejemplo] Ejecutando codigo en main {i}");
            }
        }

        static void ImprimirQueTerminoTarea(TareaAbstractaEvento tarea)
        {
            Console.WriteLine($"[ImprimirQueTerminoTarea] Fin, resultado: {tarea.Resultado}");
        }
    }
}
