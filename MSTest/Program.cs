using System;
using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            Actions actions = new Actions(driver);
            driver.Url = "https://www.sogeti.com/";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@id='CookieConsent']/div[1]/div/div[2]/div[2]/button[1]")).Click();
            WebElement serviceOption = (WebElement)driver.FindElement(By.XPath("/html/body/div[1]/header/div[2]/nav/ul/li[3]/div[1]"));
            actions.MoveToElement(serviceOption).Perform();
            IList<IWebElement> all = driver.FindElements(By.CssSelector("ul.level1 li"));
            String[] allText = new String[all.Count];
            int i = 0;
            foreach (IWebElement element in all)
            {
                allText[i++] = element.Text;
                if (element.Text == "Automation")
                {
                    String attr = element.Text;
                    driver.FindElement(By.LinkText(attr)).Click();
                    break;
                }
            }
            String PageTitle = driver.FindElement(By.XPath("/html/head/title")).GetAttribute("innerHTML");
            String AutomationText = driver.FindElement(By.XPath("/html/body/div[1]/main/div[3]/div/div[1]/div[2]/div")).GetAttribute("innerHTML");
            if (PageTitle.Contains("Automation")  && AutomationText.Contains("AUTOMATION"))
            {
                Console.WriteLine("The Automation screen along with AUTOMATION text is displayed!");
            }
            try
            {
                actions.MoveToElement(serviceOption).Perform();
            }
            catch (StaleElementReferenceException e)
            {
                Actions newActions = new Actions(driver);
                serviceOption = (WebElement)driver.FindElement(By.XPath("/html/body/div[1]/header/div[2]/nav/ul/li[3]/div[1]"));
                newActions.MoveToElement(serviceOption).Perform();
            }
            WebElement serviceColor = (WebElement)driver.FindElement(By.XPath("/html/body/div[1]/header/div[2]/nav/ul/li[3]/div[2]/ul/li[4]/a"));
            WebElement automationColor = (WebElement)driver.FindElement(By.XPath("/html/body/div[1]/header/div[2]/nav/ul/li[3]/div[1]/span"));
            String rgbFormatofServiceColor = serviceColor.GetCssValue("color");
            String rgbFormatofAutomationColor = automationColor.GetCssValue("color");
            if(rgbFormatofAutomationColor.Contains(rgbFormatofServiceColor))
            {
                Console.WriteLine("Services and Automation are Selected");
            }
        }
    }
}
