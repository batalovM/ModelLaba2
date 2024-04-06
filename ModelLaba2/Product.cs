namespace ModelLaba2;

// Класс для представления изделия
public class Product
{
    public int Id { get; set; } // Идентификатор изделия
    public double ArrivalTime { get; set; } // Время поступления на линию
    public double ProcessingTime { get; set; } // Время обработки изделия
}