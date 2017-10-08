using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

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
                wdriver.Navigate().GoToUrl("http://www.phptravels.net/admin");

                // Maximizamos la ventana
                wdriver.Manage().Window.Maximize();

                // Buscamos el formulario de inicio de sesión
                var formularioInicioSesion = wdriver.FindElement(By.ClassName("form-signin"));

                // Buscamos la caja de texto del correo electrónico del usuario, por nombre (name)
                var cajaTextoUsuarioCorreoElectronico = formularioInicioSesion.FindElement(By.Name("email"));

                // Hacemos clic en la caja de texto, para seleccionarla
                cajaTextoUsuarioCorreoElectronico.Click();
                
                // Ingresamos el correo electrónico del usuario
                cajaTextoUsuarioCorreoElectronico.SendKeys("usuario@prueba.com");

                // Buscamos la caja de texto de la contraseña, por su ID
                var cajaTextoContrasena = formularioInicioSesion.FindElement(By.Name("password"));

                // Hacemos clic en la caja de texto, para seleccionarla
                cajaTextoContrasena.Click();

                // Ingresamos la contraseña
                cajaTextoContrasena.SendKeys("contasenamala");

                // Buscamos el boton dentro del formulario
                var botonInicioSesion = formularioInicioSesion.FindElement(By.CssSelector("button.btn[type='submit']"));

                // Presionamos el botón
                botonInicioSesion.Click();

                // Debido a que el proceso de inicio se sesión es asíncrono,
                // esperamos un tiempo prudencial a que este finalice
                // (En este link se describe una forma apropiada de hacer esto: https://stackoverflow.com/a/7312740/806975)
                Thread.Sleep(5000); // 5 segundos

                // Verificamos que se muestra el aviso de error
                Assert.IsTrue(ExisteElemento(wdriver, By.CssSelector("form.form-signin div.alert.alert-danger")));

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
