using System.ComponentModel.DataAnnotations;

namespace CRUD.Domain.DTOs
{
    public class ItemDto
    {

        [Required(ErrorMessage = "O ID é obrigatório para atualização.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID deve ser maior que zero.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do item é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do item não pode exceder 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço do item é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }
    }
}