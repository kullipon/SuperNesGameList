using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperNesGameList
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"D:\Jeux\_Super_Nes_FULLSET\Complete Snes Rom Set\";
                IEnumerable<string> filenames = null;
                List<string> GoodList = new List<string>();
                List<string> BadList = new List<string>();

                filenames = Directory.EnumerateFiles(path);   
                             
                
                foreach (var item in filenames)
                {

                   var fileName =  Path.GetFileName(item);
                   //On va séparer le nom de fichier du nom du répertoire                       
                   var res = Regex.Split(item, @"(.*Set)\\(.*\.smc)");

                    if (res.Count() > 1)
                    {
                        //Tri sur le format du string avec les Regex
                        Match match = Regex.Match(fileName, @"^.{1}[^S]{1}.*(\[!\]\.smc$)|(^.{1}[^S]{1}.*\(J\) \(V1\.0\)\.smc$)|(^.{1}[^S]{1}.*\(J\)\.smc$)|(^.{1}[^S]{1}.*\(U\)\.smc$)|(^.{1}[^S]{1}.*\(E\)\.smc$)|(^.{1}[^S]{1}.*\(F\)\.smc$)|(^.{1}[^S]{1}.*\(F\) \(V1\.0\)\.smc$)");
                        
                        //Si on rentre pas dans les conditions
                        if (!match.Success)
                        {
                            using (StreamWriter strW = new StreamWriter("SuperNesBadGameList.txt",true))
                            {
                                strW.WriteLine(fileName);
                            }
                            BadList.Add(fileName);
                        }
                        //Sinon C'est le format recherché
                        else
                        {
                            using (StreamWriter strW = new StreamWriter("SuperNesGameList.txt", true))
                            {
                                strW.WriteLine(match.Value);
                            }
                            GoodList.Add(match.Value);                            
                        }
                    }                      
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur:" + e.Message);
            }        
        }
    }
}
