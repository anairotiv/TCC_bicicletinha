using TirandoAsRodinhas.Domain.Register;
using TirandoAsRodinhas.Infra.Data;

namespace TirandoAsRodinhas.Endpoints.Parceiros;

public class ParceirosPost
{
    public static string Template => "/cadastrar";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(ParceirosRequest parceirosRequest, ApplicationDbContext context)
    {

        /* try {

             if (!category.IsValid) {
                 return Results.ValidationProblem(category.Notifications.ConvertToProbelmDetails());
             }

         }
         catch (Exception) {

             context.
         } */


        var endereco = new Endereco(parceirosRequest.Cep, parceirosRequest.Logradouro, parceirosRequest.Numero,
                                    parceirosRequest.Complemento, parceirosRequest.Bairro, parceirosRequest.Cidade,
                                    parceirosRequest.Estado);
        if (!endereco.IsValid) 
        {
            return Results.ValidationProblem(endereco.Notifications.ConvertToProblemDetails());
        }


        var contato = new Contato(parceirosRequest.TelCelular, parceirosRequest.TelFixo, parceirosRequest.Email);

        if (!contato.IsValid) 
        {
            return Results.ValidationProblem(contato.Notifications.ConvertToProblemDetails());
        }    

        var parceiro = new Parceiro (parceirosRequest.EmpresaID){
            EnderecoId = endereco.Id,
            ContatoId = contato.Id,
        };

        if (parceirosRequest.Tipo == "PESSOA FISICA") {
            var pessoaFisica = new PessoaFisica(parceirosRequest.CPF_CNPJ, parceirosRequest.Nome_Razao) {
                ParceiroId = parceiro.Id
            };

            if (!pessoaFisica.IsValid) 
            {

                return Results.ValidationProblem(pessoaFisica.Notifications.ConvertToProblemDetails());
            }

            await context.PessoasFisicas.AddAsync(pessoaFisica);

            return Results.Created($"/pessoaFisica/{pessoaFisica.Id}", pessoaFisica.Id);

        }

        if (parceirosRequest.Tipo == "PESSOA JURIDICA") {
            var pessoaJuridica = new PessoaJuridica (parceirosRequest.CPF_CNPJ, parceirosRequest.Nome_Razao) {
                ParceiroId = parceiro.Id
            };
            if (!pessoaJuridica.IsValid) {

                return Results.ValidationProblem(pessoaJuridica.Notifications.ConvertToProblemDetails());
            }

            await context.PessoaJuridicas.AddAsync(pessoaJuridica);

            return Results.Created($"/pessoaFisica/{pessoaJuridica.Id}", pessoaJuridica.Id);

        }

        context.Enderecos.Add(endereco);
        context.Contatos.Add(contato);
        context.Parceiros.Add(parceiro);
        context.SaveChanges();

        return Results.Created($"/cadastrar/{parceiro.Id}", parceiro.Id);
    }
}
