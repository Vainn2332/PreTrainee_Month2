using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreTrainee_Month2.CoreLayer.Entities.Product_Entities
{
    public class ProductPutDTO
    {
        [Required(ErrorMessage = "Поле имя обязательно")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Описание обязательно")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Цена обязательно")]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
    }
}
