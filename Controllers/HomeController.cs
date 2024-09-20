using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using recursividad.Models;

namespace recursividad.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    //Cada uno de estos IActionResult devuelven la pagina creada en cshtlm en cuestion
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Factorial()
    {
        return View();
    }
    public IActionResult Cambio()
    {
        return View();
    }

    public IActionResult Fibonacci()
    {
        return View();
    }

    public IActionResult MCD()
    {
        return View();
    }

    public IActionResult Hanoi()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    //Esta funcion espera ser llamada por la funcion calcularFactorial()
    [HttpGet]
    public IActionResult calcularFactorial(int number)
    {
        //se vuelve a comprobar por si las dudas si el dato es negativo.
        if(number < 0){
            return BadRequest("No se puede calcular la factorial de un numero negativo");
        }
        //Si la factorial es 0, siempre sera 1.
        else if(number == 0){
            return BadRequest("La factorial del numero es 1");
        }
        //Almaceno el resultado del metodo "calcularFacto()" en un entero para poder imprimirlo.
        int result = calcularFacto(number);
        return Ok($"La factorial del numero es {calcularFacto(number)}");
    }
    //logica que saca la factorial. EL dato se regresa como entero.
    public int calcularFacto(int number)
    {
        //Cuando el numero llegue a 0, la factorial dejara de calcular. Esto evita que el resultado se vuelva 0.
        if(number <= 1)
        {
            return 1;
        }
        //Llamada recursiva con la que el numero se va reduciendo hasta 0.
        return number*calcularFacto(number-1);

    }
    [HttpGet]
    public IActionResult fibonacc()
    {
        //Esta variable sin definir, almacena TODOS los numeros guardados en la lista "fibona"
        var fibonacciSequence = fibona(0, 1, 0, new List<int>());
        //El resultado solicita TODOS los datos de la lista y los imprime por partes.
        return Ok($"El resultado de la secuencia es: {string.Join(", ", fibonacciSequence)}");
    }
    /*Recibe 3 datos iniciales, que son los primeros numeros de la sucesion.
      Los siguientes se guardan en la lista "numbers"
    */
    public List<int> fibona (int a, int b, int c, List<int>numbers){
        //Esto es para evitar un bucle infinito.
        if (c <= 1000)
        {
            /*A es el primer numero. B el segundo.
              C es el siguiente numero de la sucesion,
              el cual se almacena de nuevo en A para calcular 
              el siguiente numero al sumar A y B.
            */
            c = a + b;
            b = a;
            a = c;
            //aca se guarda cada numero que se avanza en al sucesion.
            numbers.Add(a);
            //Se repite el proceso de forma recursiva
            return fibona(a,b,c,numbers);
        }
        else
        {
            //Alcanzado el limite, TODOS los numeros calculados se devuelven en forma de lista.
            return numbers;
        }
    }
    [HttpGet]
    //Se llama a la funcion con los datos para imprimir el resultado del metodo CMcd.
    public IActionResult calcularMcd(int num1, int num2)
    {
        return Ok($"El MCD de los numeros es: {CMcd(num1,num2)}");
    }
    //Recibe los datos de IActionResult calcularMcd()
    public int CMcd(int num1, int num2)
    {
        int num3 = 0;
        //En caso de que el segundo numero sea mayor que el primero, se invierten
        //Esto para hacer mas facil el proceso
        if(num2>num1)
        {
            num3 = num1;
            num1 = num2;
            num2 = num3;
        }
        /*
        Aca primero se comprueba si el modular de ambos no equivale a cero,
        pues si equivale a 0, el numero 2 es el resultado del MCD.

        En caso de que no, el numero 3 almacena el dato del numero 2
        (El cual esta destinado a reducirse) y el numero 2 se convierte en
        el resultado del modulo de num1 y num2.
        Por ultimo, numero 1 obtiene el valor del numero 3 para poder
        avanzar en el algoritmo y resolverlo.
        
        Para mas info, Investigar el algoritmo de Euclides.
        Si, incluso yo no entendi esta cosa al inicio XD.
        */
        if((num1%num2) != 0)
        {
            num3 = num2;
            num2 = num1%num2;
            num1 = num3;
            return CMcd(num1, num2);
        }
        return num2;
    }
    [HttpGet]
    //Esta funcion llama al metodo para obtener el precio e imprime resultados.
    public IActionResult calcularCambio(double precio, double pago)
    {
        //Esto sirve para obtener el cambio a regresar.
        double dinero = pago - precio;
        int b100 = 0, b50 = 0, b20 = 0, m10 = 0, m5 = 0, m1 = 0, c50 = 0, c20 = 0, c1 = 0;
        return Ok($"El cambio es de {dinero} pesos\n" + Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1));
    }
    // En esta funcion se da el lujo de guardar todo el texto de una vez
    static string Cambio(double dinero, int b100, int b50, int b20, int m10, int m5, int m1, int c50, int c20, int c1)
    {
        //Con el cambio obtenido, se va calculando de a poco la forma en la que se devolera.

            dinero = Math.Round(dinero, 2); // Redondear para evitar problemas de precisión

            /*
            Si la cantidad de cambio es superior a la denominacion de cualquier bilete o moneda
            se resta el cambio y se cuenta la cantidad de veces que se tuvo que usar el bille o moneda.
            El proceso se repite de forma recursiva cada que se cumple la condicional de cualquier if.
            */
            if (dinero >= 100)
            {
                dinero -= 100;
                b100++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 50)
            {
                dinero -= 50;
                b50++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 20)
            {
                dinero -= 20;
                b20++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 10)
            {
                dinero -= 10;
                m10++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 5)
            {
                dinero -= 5;
                m5++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 1)
            {
                dinero -= 1;
                m1++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 0.50)
            {
                dinero -= 0.50;
                c50++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 0.20)
            {
                dinero -= 0.20;
                c20++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else if (dinero >= 0.01)
            {
                dinero -= 0.01;
                c1++;
                return Cambio(dinero, b100, b50, b20, m10, m5, m1, c50, c20, c1);
            }
            else
            {
                //El resultado se devuelve ya listo
                string resu = "El cambio se devuelve en: \n" +
                b100 + " billetes de 100 pesos\n" +
                b50 + " billetes de 50 pesos\n" +
                b20 + " billetes de 20 pesos\n" +
                m10 + " monedas de 10 pesos\n" +
                m5 + " monedas de 5 pesos\n" +
                m1 + " monedas de 1 peso\n" +
                c50 + " monedas de 50 centavos\n" +
                c20 + " monedas de 20 centavos\n" +
                c1 + " monedas de 1 centavo\n";
                return resu;
            }
    }
    [HttpGet]
    public IActionResult resolver(int discos)
    {
        /*
        Se establecen los nombres de los pilares (1 - A, 2 - B, 3 - B)
        Para que sea posible entender facilmente la solucion.
        */
        char origen = 'A';
        char destino = 'C';
        char auxiliar = 'B';
        //la lista se almacena en una variable global
        var reso = ResolverHanoi(discos, origen, destino, auxiliar, new List<string>());
        //El resultado de se imprimiendo poco a poco llamando todos los string
        return Ok($"El proceso a seguir es: \n {string.Join(",\n ", reso)}");


    }

     public List<string> ResolverHanoi(int discos, char origen, char destino, char auxiliar, List<string>proceso)
    {
        // Caso base: Si solo hay un disco, se mueve directamente
        if (discos == 1)
        {
            proceso.Add($"Mueve el disco 1 de {origen} a {destino}");
            return proceso;
        }

        // Paso 1: Mueve los n-1 discos de la torre origen a la torre auxiliar
        ResolverHanoi(discos - 1, origen, auxiliar, destino,proceso);

        // Paso 2: Mueve el disco n de la torre origen a la torre destino
        proceso.Add($"Mueve el disco {discos} de {origen} a {destino}");

        // Paso 3: Mueve los n-1 discos de la torre auxiliar a la torre destino
        ResolverHanoi(discos - 1, auxiliar, destino, origen,proceso);
        return proceso;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
