using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Lab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            
            List<Laptop> laptops = parser.MainParser();
            parser.PrintNotes(laptops);

            using (var db = new LaptopContext())
            {
                db.Laptops.AddRange(laptops);
                db.SaveChanges();
            }

            Console.WriteLine("Данные сохранены в базу данных!");
        }
    }
}
