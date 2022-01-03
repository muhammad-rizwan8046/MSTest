using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
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
            var anInstanceofMyClassProgram = new Program();
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
            //String AutomationText = driver.FindElement(By.XPath("/html/body/div[1]/main/div[3]/div/div[1]/div[2]/div")).GetAttribute("innerHTML");
            String AutomationText = driver.FindElement(By.ClassName("case-item-box")).Text;
            Assert.AreEqual("Automation", PageTitle,"Automation text is not Visible");
            Assert.AreEqual("AUTOMATION", AutomationText,"Automation page is not Displayed");
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
            anInstanceofMyClassProgram.RgbaToHex(rgbFormatofServiceColor);
            anInstanceofMyClassProgram.RgbaToHex(rgbFormatofAutomationColor);
        }
        public void RgbaToHex(String RgbaValue)
        {
            string[] colorvalue1 = RgbaValue.Split('(');
            string[] colorvalue2 = colorvalue1[1].Split(')');
            string colorvalue = colorvalue2[0].ToString();
            string[] ColorCodeRGBValue = colorvalue.Split(',');
            Color myColor = Color.FromArgb(Convert.ToInt32(ColorCodeRGBValue[0]), Convert.ToInt32(ColorCodeRGBValue[1]), Convert.ToInt32(ColorCodeRGBValue[2]));
            string hexValue = "#"+myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
            Assert.AreEqual(hexValue, "#FF304C", "Color codes are not equal");
        }
    }
}
