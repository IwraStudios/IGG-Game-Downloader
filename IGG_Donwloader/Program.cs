using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace IGG_Donwloader
{
    class Program
    {
        static void Main(string[] args)
        {
            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/x-rar-compressed");

            using (IWebDriver driver = new FirefoxDriver(profile))
            {
                Console.WriteLine("paste IGG page url and press enter");
                driver.Navigate().GoToUrl(Console.ReadLine());
                Thread.Sleep(1000);
                IWebElement txt =  driver.FindElement(By.XPath("//*[text()[contains(., 'Link Google Drive:')]]"));
                IWebElement parent = txt.FindElement(By.XPath(".."));
                IReadOnlyList<IWebElement> links = parent.FindElements(By.TagName("a"));
                Console.WriteLine(links.ToString());
                foreach(IWebElement link in links)
                {
                    link.Click();//GOTO
                    string a =driver.Title; //debug
                    Thread.Sleep(2000);
                    //URL extraction
                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    string raw = driver.Url;
                    string b = driver.Title;
                    if (!raw.Contains("docs"))
                    {
                        Console.WriteLine("docs not found");
                    }
                    int f = raw.IndexOf("docs");
                    string edited = raw.Substring(f);
                    //URL extraction
                    //Drive
                    driver.Navigate().GoToUrl("https://" + edited);
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("uc-download-link")).Click();
                    Thread.Sleep(2000);
                    //Drive
                    //INIT repeat
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                    //INIT repeat
                }
            }
        }
    }
}
