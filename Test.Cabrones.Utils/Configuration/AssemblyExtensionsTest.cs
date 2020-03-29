using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Cabrones.Utils.Configuration
{
    public class AssemblyExtensionsTest
    {
        [Fact]
        public void deve_retornar_o_FileInfo_de_um_assembly()
        {
            // Arrange, Given

            var assembly = Assembly.GetEntryAssembly() ?? throw new NullReferenceException();
            var caminhoEsperado = new FileInfo(assembly.Location).FullName;
            
            // Act, When

            var caminhoObtido = assembly.FileInfo().FullName;

            // Assert, Then

            caminhoObtido.Should().Be(caminhoEsperado);
        }
        
        [Fact]
        public void deve_retornar_o_DirectoryInfo_de_um_assembly()
        {
            // Arrange, Given

            var assembly = Assembly.GetEntryAssembly() ?? throw new NullReferenceException();
            var caminhoEsperado = new FileInfo(assembly.Location).Directory?.FullName;
            
            // Act, When

            var caminhoObtido = assembly.DirectoryInfo().FullName;

            // Assert, Then

            caminhoObtido.Should().Be(caminhoEsperado);
        }
    }
}