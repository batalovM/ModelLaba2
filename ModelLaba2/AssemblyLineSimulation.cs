using System;
using System.Collections.Generic;

namespace ModelLaba2;

// Основной класс для моделирования поточной линии
public class AssemblyLineSimulation
{
    private List<Workstation> workstations; // Список рабочих мест
    private List<Product> delayedProducts; // Список отложенных изделий
    private double currentTime; // Текущее время моделирования
    private Random random; // Генератор случайных чисел

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public AssemblyLineSimulation()
    {
        // Инициализация рабочих мест
        workstations = new List<Workstation>
        {
            new() { Queue = new Queue<Product>(), ProcessingTime = 1.25 },
            new() { Queue = new Queue<Product>(), ProcessingTime = 0.5 }
        };

        delayedProducts = new List<Product>();
        currentTime = 0;
        random = new Random();
    }
    // Метод для запуска моделирования с заданным количеством изделий
    public void Simulate(int numProducts)
    {
        for (int i = 0; i < numProducts; i++)
        {
            // Генерация времени поступления и времени обработки для нового изделия
            double arrivalTime = currentTime + ExponentialRandom(0.4);
            double processingTime = random.NextDouble() < 0.5 ? 1.25 : 0.5;

            Product product = new Product { Id = i, ArrivalTime = arrivalTime, ProcessingTime = processingTime };
            currentTime = arrivalTime;

            bool isDelayed = ProcessProduct(product); // Обработка нового изделия
            if (isDelayed)
            {
                delayedProducts.Add(product);
            }
        }

        // Расчет статистики и вывод результатов моделирования
        double totalProcessingTime = workstations[0].Queue.Count * workstations[0].ProcessingTime +
                                     workstations[1].Queue.Count * workstations[1].ProcessingTime;
        double processingTimePerProduct = totalProcessingTime / numProducts;
        double probabilityDelayed = (double)delayedProducts.Count / numProducts;

        Console.WriteLine($"Загрузка рабочих мест: {totalProcessingTime}");
        Console.WriteLine($"Время обработки изделия на поточной линии: {processingTimePerProduct}");
        Console.WriteLine($"Вероятность попадания изделий в отложенные: {probabilityDelayed}");
    }
    // Метод для обработки нового изделия
    private bool ProcessProduct(Product product)
    {
        if (workstations[0].Queue.Count + workstations[1].Queue.Count >= 8)
        {
            return true;
        }

        if (workstations[1].Queue.Count >= 2)
        {
            workstations[0].IsBlocked = true;
            return true;
        }

        if (workstations[0].IsBlocked)
        {
            return true;
        }

        workstations[0].Queue.Enqueue(product);
        if (!workstations[0].IsBlocked)
        {
            ProcessWorkstation(workstations[0]);
        }

        return false;
    }
    // Метод для обработки рабочего места
    private void ProcessWorkstation(Workstation workstation)
    {
        if (workstation.Queue.Count > 0)
        {
            Product product = workstation.Queue.Dequeue();
            double processingTime = product.ProcessingTime;
            currentTime += processingTime;

            if (workstation == workstations[0] && workstations[1].Queue.Count < 2)
            {
                ProcessWorkstation(workstations[1]);
                workstation.IsBlocked = false;
            }
        }
    }
    // Метод для генерации случайных чисел по экспоненциальному распределению
    private double ExponentialRandom(double lambda)
    {
        return -Math.Log(1 - random.NextDouble()) / lambda;
    }
}
