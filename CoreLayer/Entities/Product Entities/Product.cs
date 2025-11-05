namespace PreTrainee_Month2.CoreLayer.Product_Entities
{
    public class Product
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string  Description { get; set; }=string.Empty;
        public decimal Price { get; set; } = 0;
        public DateTime DateOfCreation { get; set; } = new DateTime(0, 0, 0);


        public int UserId { get; set; } = 0;        
        
        //для связи в EF
        public User Owner { get; set; }
    }
}
