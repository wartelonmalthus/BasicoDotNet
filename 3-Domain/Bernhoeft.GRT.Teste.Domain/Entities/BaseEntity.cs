using System.Xml.Serialization;

namespace Bernhoeft.GRT.Teste.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public DateTime DataCriacao { get; private set; } = DateTime.Now;
    public DateTime? DataAlteracao { get;  private set; }
    public bool Ativo { get; private set; } = true;

    public void DelecaoLogica()
    {
        Ativo = false;
        AtualizarDataAlteracao();
    }

    public void AtualizarDataAlteracao()
    {
        DataAlteracao = DateTime.Now;

    }
}