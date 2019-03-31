using mail_ui_tests.Helpers;
using mail_ui_tests.Pages;
using NUnit.Framework;
using System;

namespace mail_ui_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class AuthorizationTestsChrome : Settings
    {
        private AuthorizationPage authorizationPage;

        // Setup before each test.
        [SetUp]
        public void Before()
        {
            Console.WriteLine("Open browser - Chrome");

            SetBrowserChrome();
            authorizationPage = new AuthorizationPage(webDriver);

            authorizationPage.OpenAuthorizationPage();
        }

        // Tests for Success authorization.

        // Test a successful login, all fields. With different values.
        [Test]
        public void successAuthorizationTest([Values("@mail.ru", "@inbox.ru", "@list.ru", "@bk.ru")] string _domain)
        {
            authorizationPage.SetAuthorizationData(login, password, _domain);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetUserName(), (login + _domain).ToLower(), "A different user name.");
        }

        // Verify successful authorization without selecting a domain name from drop-down list.
        [Test]
        public void successAuthorizationWithoutDomainTest()
        {
            authorizationPage.SetAuthorizationData(login + domain, password);
            authorizationPage.ClickLogin();
            
            Assert.AreEqual(authorizationPage.GetUserName(), (login + domain).ToLower(), "A different user name.");
        }


        // Tests for Success authorization.

        // Failed authorization check without data.
        [Test]
        public void failedAuthorizationWithoutData()
        {
            authorizationPage.ClickLogin();
            Assert.AreEqual(authorizationPage.GetValidationError(), "Введите имя ящика и пароль");
        }

        // Check unsuccessful login, without a login.
        [Test]
        public void failedAuthorizationWithoutLogin()
        {
            authorizationPage.SetLogin(login);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetValidationError(), "Введите пароль");
        }

        // Check unsuccessful login, without a password.
        [Test]
        public void failedAuthorizationWithoutPassword()
        {
            authorizationPage.SetPassword(password);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetValidationError(), "Введите имя ящика");
        }

        // Authorization check with incorrect password.
        [Test]
        public void failedAuthorizationWithIncorrectPassword()
        {
            authorizationPage.SetAuthorizationData(login, incorrectPassword, domain);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetValidationError(), "Неверное имя или пароль");
        }

        // Authorization check with incorrect login.
        [Test]
        public void failedAuthorizationWithIncorrectLogin()
        {
            authorizationPage.SetAuthorizationData(incorrectLogin, password, domain);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetValidationError(), "Неверное имя или пароль");
        }

        // Verification of authorization without taking into account the character case.
        [Test]
        public void failedAuthorizationWithoutSymbolsRegistrTest()
        {
            authorizationPage.SetAuthorizationData(login, password.ToLower(), domain);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetValidationError(), "Неверное имя или пароль");
        }

        // Verification of authorization with an invalid domain name.
        [Test]
        public void failedAuthorizationWithIncorrectDomain()
        {
            authorizationPage.SetAuthorizationData(login + incorrectDomain, password);
            authorizationPage.ClickLogin();

            Assert.AreEqual(authorizationPage.GetDomainError(), "Уточните почту для входа");
        }

        // Verification of password input in Russian.
        [Test]
        public void failedAuthorizationWithRussianPassword()
        {
            authorizationPage.SetPassword(russianPassword);
            Assert.AreEqual(authorizationPage.GetValidationError(), "Смените раскладку");
        }


        // Setup after each test.
        [TearDown]
        public void After()
        {
            Console.WriteLine("Close browser - Chrome");
            CloseBrowser();
        }
    }
}
