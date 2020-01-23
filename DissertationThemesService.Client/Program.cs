using DissertationThemes.ServiceApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DissertationThemesService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ChannelFactory<IDissertationThemesService>("WordClient"))
            {
                IDissertationThemesService proxy = client.CreateChannel();
                while (true)
                {
                    Console.WriteLine("Klient spusteny!");
                    if (Console.ReadLine() == ".") break;
                    //var ret = proxy.GenerateDocx(1500);
                    //proxy.GetStudyPrograms();
                    //proxy.GetThemeYears();
                    //proxy.GetStudyPrograms();
                    proxy.GetThemes(2016, 61);
                    Console.ReadLine();
                    //proxy.GetThemes(2016, 61);
                    //                    string buff = "";
                    //                    foreach (var b in ret)
                    //                    {
                    //                        buff += b.ToString();
                    //                    }
                    //                    Console.WriteLine("{0}", buff);
                }
            }
        }
    }
}
