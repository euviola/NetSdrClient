using NetArchTest.Rules;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetSdrClientAppTests
{
    public class ArchitectureTests
    {
        [Test]
        public void App_Should_Not_Depend_On_EchoServer()
        {
            var result = Types.InAssembly(typeof(NetSdrClientApp.NetSdrClient).Assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp")
                .ShouldNot()
                .HaveDependencyOn("EchoServer")
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void Messages_Should_Not_Depend_On_Networking()
        {
            // Arrange
            var result = Types.InAssembly(typeof(NetSdrClientApp.Messages.NetSdrMessageHelper).Assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp.Messages")
                .ShouldNot()
                .HaveDependencyOn("NetSdrClientApp.Networking")
                .GetResult();

            // Assert
            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void Networking_Should_Not_Depend_On_Messages()
        {
            // Arrange
            var result = Types.InAssembly(typeof(NetSdrClientApp.Networking.ITcpClient).Assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp.Networking")
                .ShouldNot()
                .HaveDependencyOn("NetSdrClientApp.Messages")
                .GetResult();

            // Assert
            Assert.That(result.IsSuccessful, Is.True);
        }

         // НОВЕ ПРАВИЛО 1
        [Test]
        public void Networking_Should_Not_Depend_On_UI()
        {
            var result = Types.InCurrentDomain()
                .That()
                .ResideInNamespace("NetSdrClientApp.Networking")
                .ShouldNot()
                .HaveDependencyOn("NetSdrClientApp.UI")
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        // НОВЕ ПРАВИЛО 2
        [Test]
        public void Messages_Should_Not_Depend_On_EchoServer()
        {
            var result = Types.InAssembly(typeof(NetSdrClientApp.Messages.NetSdrMessageHelper).Assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp.Messages")
                .ShouldNot()
                .HaveDependencyOn("EchoServer")
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        // НОВЕ ПРАВИЛО 3
        [Test]
        public void App_Should_Not_Depend_On_Messages()
        {
            var result = Types.InAssembly(typeof(NetSdrClientApp.NetSdrClient).Assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp")
                .ShouldNot()
                .HaveDependencyOn("NetSdrClientApp.Messages")
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }
        
    }
}