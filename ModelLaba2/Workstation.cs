using System.Collections.Generic;

namespace ModelLaba2;

// Класс для представления рабочего места
public class Workstation
{
    public Queue<Product> Queue { get; set; } // Очередь изделий на рабочем месте
    public double ProcessingTime { get; set; } // Время обработки на рабочем месте
    public bool IsBlocked { get; set; } // Флаг блокировки рабочего места
}