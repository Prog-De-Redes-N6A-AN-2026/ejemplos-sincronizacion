using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ejemplo Adaptado de https://www.c-sharpcorner.com/UploadFile/1d42da/wait-and-pulse-method-in-threading-C-Sharp/

namespace Synchronization
{
    internal class PingPong
    {
        public void Ping(bool ejecutando)
        {
            lock (this)
            {
                if (!ejecutando)
                {
                    // Se detiene
                    Monitor.Pulse(this); // Notificar a cualquier hilo que esté esperando
                    return;
                }
                Console.WriteLine("Ping ");
                Monitor.Pulse(this); // Dejar que Pong ejecute
                Monitor.Wait(this); // Esperando a que termine Pong
            }
        }

        public void Pong(bool ejecutando)
        {
            lock (this)
            {
                if (!ejecutando)
                {
                    // Se detiene
                    Monitor.Pulse(this); // Notificar a cualquier hilo que esté esperando
                    return;
                }
                Console.WriteLine("Pong ");
                Monitor.Pulse(this); // Dejar que Ping ejecute
                Monitor.Wait(this); // Esperando a que termine Ping
            }
        }
    }

    internal class MiThread
    {
        public Thread thread;
        PingPong pingPongObject;

        // Construimos un nuevo hilo
        public MiThread(string nombre, PingPong pp)
        {
            thread = new Thread(this.Ejecutar);
            pingPongObject = pp;
            thread.Name = nombre;
            thread.Start();
        }

        // Comienza ejecucion de nuevo hilo
        void Ejecutar()
        {
            if (thread.Name == "Ping")
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(100);
                    pingPongObject.Ping(true);
                }
                pingPongObject.Ping(false);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(100);
                    pingPongObject.Pong(true);
                }
                pingPongObject.Pong(false);
            }
        }
    }
 
    internal class Ejemplo4
    {
        public static void Ejemplo()
        {
            Console.WriteLine("[Ejemplo] Se deja caer la pelota...");

            PingPong pp = new PingPong();

            MiThread t1 = new MiThread("Ping", pp);
            MiThread t2 = new MiThread("Pong", pp);

            t1.thread.Join();
            t2.thread.Join();

            Console.WriteLine("[Ejemplo] La pelota deja de rebotar");
            Console.Read();
        }
    }
}
