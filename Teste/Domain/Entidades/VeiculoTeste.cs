using Minimal_Api.Dominio.Entidades;

namespace Teste.Domain.Entidades;

[TestClass]
public class VeiculoTeste
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange
        var veiculo = new Veiculo();

        //Act
        veiculo.Id = 1;
        veiculo.Modelo = "Fit";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2010;
        
        //Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Fit", veiculo.Modelo);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2010, veiculo.Ano);
    }
}