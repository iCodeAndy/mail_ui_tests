using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace mail_ui_tests.Helpers
{
    class Settings
    {
        public static IWebDriver webDriver;

        // Positive data for tests.
        protected static string login = "seleniumLoginGM";
        protected static string password = "seleniumPasswordGM";
        protected static string domain = "@mail.ru";

        // Negative data for tests.
        protected static string incorrectLogin = "404seleniumLoginGM";
        protected static string incorrectPassword = "NOTseleniumPasswordGM";
        protected static string incorrectDomain = "@NOTmail.ru";
        protected static string russianPassword = "Пароль";   

        // Browser settings.
        protected static void SetBrowserChrome()
        {
            ChromeOptions options = new ChromeOptions();

            // Configure the page load strategy. Normal - Wait until the page is fully loaded.
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AddArguments("start-maximized");

            webDriver = new ChromeDriver(options);

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        // Close all browser Windows. Not only window is currently in focus.
        protected static void CloseBrowser()
        {
            if(webDriver != null)
            {
                webDriver.Quit();
                webDriver = null;
            }
        }
    }
}
