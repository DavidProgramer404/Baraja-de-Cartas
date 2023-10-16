
public class Carta
{
    public string Nombre { get; set; }
    public string Pinta { get; set; }
    public int Valor { get; set; }

    public Carta(string nombre, string pinta, int valor)
    {
        Nombre = nombre;
        Pinta = pinta;
        Valor = valor;
    }

    public void Print()
    {
        Console.WriteLine($"Nombre: {Nombre}, Pinta: {Pinta}, Valor: {Valor}");
    }
}

public class Mazo
{
    public List<Carta> Cartas { get; private set; } = new List<Carta>();

    public Mazo()
    {
        string[] pintas = { "Tréboles", "Picas", "Corazones", "Diamantes" };

        foreach (string pinta in pintas)
        {
            for (int i = 1; i <= 13; i++)
            {
                Cartas.Add(new Carta(ObtenerNombre(i),pinta,i));
            }
        }
    }

    private string ObtenerNombre(int valor)
    {
        switch (valor)
        {
            case 1: return "As";
            case 11: return "J";
            case 12: return "Reina";
            case 13: return "Rey";
            default: return valor.ToString();
        }
    }

    public void Barajar()
    {
        Random rng = new Random();
        int n = Cartas.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Carta value = Cartas[k];
            Cartas[k] = Cartas[n];
            Cartas[n] = value;
        }
    }

    public Carta Repartir()
    {
        if (Cartas.Count > 0)
        {
            Carta cartaRepartida = Cartas[Cartas.Count - 1];
            Cartas.RemoveAt(Cartas.Count - 1);
            return cartaRepartida;
        }
        else
        {
            Console.WriteLine("No quedan cartas en el mazo.");
            return null;
        }
    }

    public void Reiniciar()
    {
        List<Carta> cartasTemporales = new List<Carta>(Cartas);
        Cartas.Clear();
        Cartas.AddRange(cartasTemporales);
    }

    public class Jugador
    {
        public string Nombre { get; set; }
        public List<Carta> Mano { get; private set; } = new List<Carta>();

        public Jugador(string nombre)
        {
            Nombre = nombre;
        }

        public Carta Robar(Mazo mazo)
        {
            Carta cartaRobada = mazo.Repartir();
            if (cartaRobada != null)
            {
                Mano.Add(cartaRobada);
            }
            return cartaRobada;
        }

        public Carta Descartar(int indice)
        {
            if (indice >= 0 && indice < Mano.Count)
            {
                Carta cartaDescartada = Mano[indice];
                Mano.RemoveAt(indice);
                return cartaDescartada;
            }
            else
            {
                Console.WriteLine("Índice inválido. No se pudo descartar la carta.");
                return null;
            }
        }

        public void MostrarMano()
        {
            Console.WriteLine($"Mano de {Nombre}:");
            foreach (Carta carta in Mano)
            {
                carta.Print();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // Instanciar una Carta
            Carta miCarta = new Carta("As", "Corazones", 1);
            miCarta.Print();

            // Instanciar un Mazo e imprimir las cartas
            Mazo mazo = new Mazo();
            foreach (Carta carta in mazo.Cartas)
            {
                carta.Print();
            }

            // Repartir una carta y mostrar el mazo después
            Carta cartaRepartida = mazo.Repartir();
            if (cartaRepartida != null)
            {
                cartaRepartida.Print();
            }
            else
            {
                Console.WriteLine("No quedan cartas en el mazo.");
            }

            // Reiniciar y barajar el mazo
            mazo.Reiniciar();
            mazo.Barajar();

            // Crear un Jugador y robar tres cartas
            Jugador jugador = new Jugador("Jugador1");
            for (int i = 0; i < 3; i++)
            {
                Carta cartaRobada = jugador.Robar(mazo);
                if (cartaRobada != null)
                {
                    cartaRobada.Print();
                }
                else
                {
                    Console.WriteLine("No quedan cartas en el mazo.");
                }
            }

            // Mostrar la mano del jugador y descartar una carta
            jugador.MostrarMano();
        }
    }
}
