using System.ComponentModel.DataAnnotations;

namespace processos.Model;

public class Processo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o número do processo.")]
    [StringLength(200, ErrorMessage = "O número deve ter no máximo 200 caracteres.")]
    public string Numero { get; set; } = string.Empty;

    // Mantido DateTime? como na aula 08/09. A coluna data_pro aceita NULL.
    public DateTime? Data { get; set; }

    [Required(ErrorMessage = "Informe o interessado.")]
    [StringLength(200)]
    public string Interessado { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o assunto.")]
    [StringLength(200)]
    public string Assunto { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione a situação.")]
    public string Situacao { get; set; } = "Aberto";
}
