using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PruebaSelenium
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, ]
        public void InicioSesion_ConCredencialesIncorrectas_MuestraAviso()
        {
            // Inicializamos la instancia del driver de Selenium para el navegador Google Chrome
            using (IWebDriver wdriver = new ChromeDriver())
            {
                // Abrimos la página web de inicio se sesión
                wdriver.Navigate().GoToUrl("https://courses.ultimateqa.com/users/sign_in");

                // Maximizamos la ventana
                wdriver.Manage().Window.Maximize();

                // Buscamos la caja de texto del correo electrónico del usuario, por su ID
                var cajaTextoUsuarioCorreoElectronico = wdriver.FindElement(By.Id("user_email"));

                // Hacemos clic en la caja de texto, para seleccionarla
                cajaTextoUsuarioCorreoElectronico.Click();
                
                // Ingresamos el correo electrónico del usuario
                cajaTextoUsuarioCorreoElectronico.SendKeys("usuario@prueba.com");

                // Buscamos la caja de texto de la contraseña, por su ID
                var cajaTextoContrasena = wdriver.FindElement(By.Id("user_password"));

                // Hacemos clic en la caja de texto, para seleccionarla
                cajaTextoContrasena.Click();

                // Ingresamos la contraseña
                cajaTextoContrasena.SendKeys("1234");

                // Buscamos y presionamos el botón de inicio de sesión
                wdriver.FindElement(By.Id("btn-signin")).Click();

                // Verificamos que se muestra el aviso de error
                Assert.IsTrue(ExisteElemento(wdriver, By.Id("notifications-error")));

                // Cerramos el driver, y sus ventanas asociadas
                wdriver.Quit();
            }
        }

        /// <summary>
        /// Verifica si un elemento existe en la página web.
        /// </summary>
        /// <param name="wdriver">
        /// Instancia de Web Driver.
        /// </param>
        /// <param name="buscarPor">
        /// Mecanismo de búsqueda.
        /// </param>
        /// <returns>
        /// True si el elemento se encuentra en la página, y false en caso contrario.
        /// </returns>
        private static bool ExisteElemento(IWebDriver wdriver, By buscarPor)
        {
            try
            {
                wdriver.FindElement(buscarPor);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
