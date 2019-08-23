using System.Drawing;
using System.Threading;
using PollyPOC.Core;

namespace PollyPOC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Colorful.Console.WriteLine("------------------------------------------");
            Colorful.Console.WriteLine("               Retry Example              ");
            Colorful.Console.WriteLine("------------------------------------------");

            Examples.RetryPolicyExample();

            Colorful.Console.WriteLine("------------------------------------------");
            Colorful.Console.WriteLine("         Retry and Wait Example           ");
            Colorful.Console.WriteLine("------------------------------------------");


            Examples.RetryAndWaitPolicyExample();

            Colorful.Console.WriteLine("------------------------------------------");
            Colorful.Console.WriteLine("             Fallback Example             ");
            Colorful.Console.WriteLine("------------------------------------------");

            Examples.FallbackPolicyExample();

            Colorful.Console.WriteLine("------------------------------------------");
            Colorful.Console.WriteLine("             Policy Wrap Example          ");
            Colorful.Console.WriteLine("------------------------------------------");


            Examples.WrapPoliciesExample();

            Colorful.Console.WriteLine("------------------------------------------");
            Colorful.Console.WriteLine("          Circuit Breaker Example          ");
            Colorful.Console.WriteLine("------------------------------------------");

            for (var i = 0; i < 3; i++)
                Examples.CircuitBreakerExample();

            Colorful.Console.WriteLine("Waiting some seconds until the circuit is opened again...", Color.Aqua);
            Thread.Sleep(20000);

            Colorful.Console.WriteLine("Retrying... circuit should be open now.", Color.Aqua);
            Examples.CircuitBreakerExample(false);

            Colorful.Console.WriteLine("------------------------------------------");
            Colorful.Console.WriteLine("               Cache Example             ");
            Colorful.Console.WriteLine("------------------------------------------");

            for (var i = 0; i < 3; i++)
                Examples.CacheExample();

            Colorful.Console.WriteLine("Waiting some seconds until the cache expires", Color.Aqua);
            Thread.Sleep(20000);

            Colorful.Console.WriteLine("Retrying... cache should be expired now.", Color.Aqua);
            Examples.CacheExample();
        }

        
    }
}