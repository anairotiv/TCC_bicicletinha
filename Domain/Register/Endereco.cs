namespace TirandoAsRodinhas.Domain.Register;

public class Endereco : Entity
{
    public string CEP { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }


    public Endereco (string cep, string logradouro, int numero, string complemento, string bairro, string cidade, string estado )
    {
        var contract = new Contract<Endereco>()
                .IsNotNullOrEmpty(cep, "Cep")
                .IsLowerOrEqualsThan(cep, 8, "Cep")
                .IsGreaterOrEqualsThan (cep, 8, "Cep");
        AddNotifications(contract);

        CEP = cep;
        Logradouro = logradouro; 
        Numero = numero;
        Complemento = complemento;
        Estado = estado;
        Bairro = bairro;    
        Cidade = cidade;
    }
}