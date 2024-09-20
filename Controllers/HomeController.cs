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

    [HttpGet]
    public IActionResult calcularFactorial(int number)
    {
        if(number < 0){
            return BadRequest("No se puede calcular la factorial de un numero negativo");
        }
        else if(number == 0){
            return BadRequest("La factorial del numero es 1");
        }
        int result = calcularFacto(number);
        return Ok($"La factorial del numero es {calcularFacto(number)}");
    }

    public int calcularFacto(int number)
    {
        if(number <= 1)
        {
            return 1;
        }
        return number*calcularFacto(number-1);

    }
    [HttpGet]
    public IActionResult fibonacc()
    {
        var fibonacciSequence = fibona(0, 1, 0, new List<int>());
        return Ok($"El resultado de la secuencia es: {string.Join(", ", fibonacciSequence)}");
    }
    public List<int> fibona (int a, int b, int c, List<int>numbers){
        if (c <= 1000)
        {
            c = a + b;
            b = a;
            a = c;
            numbers.Add(a);
            return fibona(a,b,c,numbers);
        }
        else
        {
            return numbers;
        }
    }
    [HttpGet]
    public IActionResult calcularMcd(int num1, int num2)
    {
        return Ok($"El MCD de los numeros es: {CMcd(num1,num2)}");
    }

    public int CMcd(int num1, int num2)
    {
        int num3 = 0;
        if(num2>num1)
        {
            num3 = num1;
            num1 = num2;
            num2 = num3;
        }

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
    public IActionResult calcularCambio(double precio, double pago)
    {
        double dinero = pago - precio;
        return Ok($"El cambio es de {dinero} pesos\n" + Cambio(dinero));
    }
    static string Cambio(double dinero)
    {
        int b100 = 0, b50 = 0, b20 = 0, m10 = 0, m5 = 0, m1 = 0, c50 = 0, c20 = 0, c1 = 0;

        while (dinero > 0)
        {
            dinero = Math.Round(dinero, 2); // Redondear para evitar problemas de precisión

            if (dinero >= 100)
            {
                dinero -= 100;
                b100++;
            }
            else if (dinero >= 50)
            {
                dinero -= 50;
                b50++;
            }
            else if (dinero >= 20)
            {
                dinero -= 20;
                b20++;
            }
            else if (dinero >= 10)
            {
                dinero -= 10;
                m10++;
            }
            else if (dinero >= 5)
            {
                dinero -= 5;
                m5++;
            }
            else if (dinero >= 1)
            {
                dinero -= 1;
                m1++;
            }
            else if (dinero >= 0.50)
            {
                dinero -= 0.50;
                c50++;
            }
            else if (dinero >= 0.20)
            {
                dinero -= 0.20;
                c20++;
            }
            else if (dinero >= 0.01)
            {
                dinero -= 0.01;
                c1++;
            }
        }

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
    [HttpGet]
    public IActionResult resolver(int discos)
    {
        
        char origen = 'A';
        char destino = 'C';
        char auxiliar = 'B';
        var reso = ResolverHanoi(discos, origen, destino, auxiliar, new List<string>());
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
