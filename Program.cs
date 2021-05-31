using System;

namespace REGEXP
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine("Для выхода, напишите exit ");
                Console.WriteLine("Для начала разбора, введите ссылку: ");
                //var str = new Razbor(Console.ReadLine());
                string str = Console.ReadLine();
                Console.WriteLine();
                switch (str)
                {
                    case "exit":
                        return;
                    default: 
                        var r = new Razbor(str);
                        r.Raz(str);
                        break;
                }
            }
        }
        
    }
}
