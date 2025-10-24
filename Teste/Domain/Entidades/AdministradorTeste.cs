using Minimal_Api.Dominio.Entidades;

namespace Teste.Domain.Entidades;

[TestClass]
public class AdministradorTeste
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange
        var adm = new Administrador();

        //Act
        adm.Id = 1;
        adm.Email = "administrador@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";
        
        //Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("administrador@teste.com", adm.Email);
        Assert.AreEqual("teste", adm.Senha);
        Assert.AreEqual("Adm", adm.Perfil);
    }
}