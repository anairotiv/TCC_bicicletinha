namespace TirandoAsRodinhas.Domain.Register;

public class PessoaJuridica : Entity
{
    public string CNPJ { get; set; }
    public string RazaoSocial { get; set; }
    public Guid ParceiroId { get; set; }
    public Parceiro Parceiro { get; set; }


    public PessoaJuridica(string razaosocial, string cnpj)
    {
        var contract = new Contract<PessoaJuridica>()
           .IsNotNullOrEmpty(razaosocial, "razaosocial")
           .IsNotNullOrEmpty(cnpj, "Cnpj");

        AddNotifications(contract);

        RazaoSocial = razaosocial;
        CNPJ = cnpj;
    }
}

 
