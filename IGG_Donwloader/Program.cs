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

            //Disable Ads
            //Whitelist
            profile.SetPreference("network.proxy.no_proxies_on", "localhost,127.0.0.1,igg-games.com,igg-games.co,docs.google.com,googleusercontent.com");

            //Rules
            profile.SetPreference("network.proxy.backup.ftp", "0.0.0.0");
            profile.SetPreference("network.proxy.backup.ftp_port", 1);
            profile.SetPreference("network.proxy.backup.socks", "0.0.0.0");
            profile.SetPreference("network.proxy.backup.socks_port", 1);
            profile.SetPreference("network.proxy.backup.ssl", "0.0.0.0");
            profile.SetPreference("network.proxy.backup.ssl_port", 1);
            profile.SetPreference("network.proxy.ftp", "0.0.0.0");
            profile.SetPreference("network.proxy.ftp_port", 1);
            profile.SetPreference("network.proxy.http", "0.0.0.0");
            profile.SetPreference("network.proxy.http_port", 1);
            profile.SetPreference("network.proxy.socks", "0.0.0.0");
            profile.SetPreference("network.proxy.socks_port", 1);
            profile.SetPreference("network.proxy.ssl", "0.0.0.0");
            profile.SetPreference("network.proxy.ssl_port", 1);
            profile.SetPreference("network.proxy.type", 1);
            profile.SetPreference("network.proxy.share_proxy_settings", true);

            //Disable Ads



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
                    Thread.Sleep(3500);
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
