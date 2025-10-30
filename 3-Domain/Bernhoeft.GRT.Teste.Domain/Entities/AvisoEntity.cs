using Bernhoeft.GRT.Teste.Domain.Entities;

namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;

public partial class AvisoEntity(string titulo, string mensagem) : BaseEntity
{
    public string Titulo { get; private set; } = titulo;
    public string Mensagem { get; private set; } = mensagem;

    public void Atualizar(string? titulo, string? mensagem)
    {
        if (titulo is not null)
            Titulo = titulo;
        if (mensagem is not null)
            Mensagem = mensagem;

        AtualizarDataAlteracao();

    }
}