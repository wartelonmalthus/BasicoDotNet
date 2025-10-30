using Bernhoeft.GRT.Teste.Domain.Models.Common;

namespace Bernhoeft.GRT.Teste.Domain.Models.Aviso;

public class AvisoFilter : PaginacaoOrdenacao
{
    public string? Titulo { get; set; }
    public string? Mensagem { get; set; }
    public bool? Ativo { get; set; }
    public DateTime? DataCriacaoInicio { get; set; }
    public DateTime? DataCriacaoFim { get; set; }
}
