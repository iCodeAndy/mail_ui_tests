using mail_ui_tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mail_ui_tests.Pages
{
    class AuthorizationPage
    {
        private IWebDriver webDriver;
        
        // URL current page.
        private string urlAuthorization = "https://mail.ru/";

        // Constructor to initialize the current page.
        public AuthorizationPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        // Initializes the WebElements of the current page.
        private IWebElement etLogin => webDriver.FindElement(By.Id("mailbox:login"));
        private IWebElement etPassword => webDriver.FindElement(By.Id("mailbox:password"));
        private IWebElement ddlDomain => webDriver.FindElement(By.Id("mailbox:domain"));
        private IWebElement btnLogin => webDriver.FindElement(By.Id("mailbox:submit"));
        private IWebElement btnSignIn => webDriver.FindElement(By.Id("PH_authLink"));
        private IWebElement msgError => webDriver.FindElement(By.Id("mailbox:error"));

        // Initializes the WebElements of the another page.
        private IWebElement msgBadDomain => webDriver.FindElement(By.ClassName("login-page__external_head"));
        private IWebElement lblUserEmail => webDriver.FindElement(By.Id("PH_user-email"));


        // Open the page - Authorization in the browser.
        public void OpenAuthorizationPage()
        {
            webDriver.Navigate().GoToUrl(urlAuthorization);
            Utils.sleep(5);
        }

        // Set the data for authorization.
        public void SetAuthorizationData(string login, string password)
        {
            etLogin.SendKeys(login);
            etPassword.SendKeys(password);
        }

        // Overloading for method SetAuthorizationData.
        public void SetAuthorizationData(string login, string password, string domain)
        {
            SetLogin(login);
            SetPassword(password);
            SelectDomain(domain);
        }

        // Set data in the login field.
        public void SetLogin(string login)
        {
            etLogin.SendKeys(login);
        }

        // Set data in the password field.
        public void SetPassword(string password)
        {
            etPassword.SendKeys(password);
        }

        // Select a domain from the drop-down list.
        public void SelectDomain(string domain)
        {
            new SelectElement(ddlDomain).SelectByText(domain);
        }

        // Press the Sign-In button.
        public void ClickLogin()
        {
            btnLogin.Click();
            Utils.sleep(10);    
        }

        // Get error text if data has not been validated.
        public string GetValidationError()
        {
            return msgError.Text;
        }


        // Methods for other pages.

        // Get error text if domain is not specified correctly.
        public string GetDomainError()
        {
            return msgBadDomain.Text;
        }

        // Get the name of the user's mailbox.
        public string GetUserName()
        {
            return lblUserEmail.Text;
        }
    }
}
