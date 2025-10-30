namespace Bernhoeft.GRT.Teste.Domain.Models.Common;

public abstract class PaginacaoOrdenacao
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string OrderBy { get; set; } = "DataCriacao";
    public bool Descending { get; set; } = true;
}
