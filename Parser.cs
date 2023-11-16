using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;

namespace Lab6
{
    public class Parser
    {
        public List<Laptop> MainParser()
        {
            //getting path to the chromedriver
            var chromeDriverService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var chromeOptions = new ChromeOptions();

            IWebDriver driver = new ChromeDriver(chromeDriverService);

            driver.Navigate().GoToUrl("https://www.mvideo.ru/noutbuki-planshety-komputery-8/noutbuki-118?from=under_search");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            List<Laptop> laptops;

            Console.WriteLine("Парсинг начался!");
 
            // getting all laptops items
            var laptopsList = driver.FindElements(By.CssSelector(".product-cards-layout__item"));
            RunJs(ref driver);

            laptops = ParseLaptops(laptopsList);

            driver.Close();
            Console.WriteLine("Парсинг закончился!\n");
            
            return laptops;
        }

        public List<Laptop> ParseLaptops(ReadOnlyCollection<IWebElement> laptopsList)
        {
            List<Laptop> laptops = new List<Laptop>();

            foreach (var item in laptopsList)
            {
                Laptop laptop = new Laptop();

                var feutureList = item.FindElements(By.CssSelector(".product-feature-list__value"));
                var rawCost = item.FindElement(By.CssSelector(".price__main-value")).Text;

                laptop.Title = item.FindElement(By.CssSelector(".product-title__text")).Text;

                if (laptop.Title.Contains("Apple"))
                {
                    laptop.Resolution = feutureList[0].Text;
                    laptop.CPU = feutureList[2].Text;
                    laptop.RAM = Convert.ToInt32(feutureList[4].Text);
                    laptop.GPU = "Apple Graphics";
                    laptop.SSD = feutureList[6].Text;
                    laptop.Cost = Convert.ToInt32(rawCost.Remove(rawCost.Length - 1).Trim().Replace(" ", ""));
                    laptops.Add(laptop);
                }
                else
                {
                    laptop.Resolution = feutureList[0].Text;
                    laptop.CPU = feutureList[2].Text;
                    laptop.RAM = Convert.ToInt32(feutureList[4].Text);
                    laptop.GPU = feutureList[6].Text;
                    laptop.SSD = feutureList[8].Text;
                    laptop.Cost = Convert.ToInt32(rawCost.Remove(rawCost.Length - 1).Trim().Replace(" ", ""));
                    laptops.Add(laptop);
                }
            }

            return laptops;
        }

        public void PrintNotes(List<Laptop> laptops)
        {
            foreach (var item in laptops)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void RunJs(ref IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
            @"function scrollToBottom() {
            var scrollHeight = document.documentElement.scrollHeight || document.body.scrollHeight;
            var scrollStep = scrollHeight / 10;
            function smoothScroll() {
                if (window.scrollY < scrollHeight) {
                    window.scrollBy(0, scrollStep);
                    setTimeout(smoothScroll, 10);
                }
            }
            smoothScroll();
            }
            scrollToBottom();");
        }
    }
}
