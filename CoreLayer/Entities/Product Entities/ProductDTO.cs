using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreTrainee_Month2.CoreLayer.Product_Entities
{
    public class ProductDTO
    {
        [Required(ErrorMessage="Поле имя обязательно")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Описание обязательно")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Цена обязательно")]
        [Range(0, double.MaxValue)]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; } = 0;

        
       // public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Продукт обязан иметь владельца")]
        [Range(1,int.MaxValue)]
        public int UserId { get; set; } = 0;
    }
}
