using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<MobileDevice> devices = new List<MobileDevice>();

        while (true)
        {
            Console.WriteLine("\nВаш выбор:");
            Console.WriteLine("1 - Ввод мобильного устройства");
            Console.WriteLine("2 - Ввод смартфона");
            Console.WriteLine("3 - Ввод электронной книги");
            Console.WriteLine("4 - Вывод всех устройств");
            Console.WriteLine("5 - Изменить параметры устройства");
            Console.WriteLine("0 - Выход");

            int choice = InputFunc.ReadInt("Введите номер пункта: ", 0, 5);

            switch (choice)
            {
                case 1:
                    {
                        string model = InputFunc.ReadString("Введите модель: ");
                        int weight = InputFunc.ReadInt("Введите вес (г): ", 1, 2000);
                        int price = InputFunc.ReadInt("Введите цену (руб): ", 100, 1000000);

                        devices.Add(new MobileDevice(model, weight, price));
                        break;
                    }
                case 2:
                    {
                        string model = InputFunc.ReadString("Введите модель: ");
                        int weight = InputFunc.ReadInt("Введите вес (г): ", 1, 2000);
                        int price = InputFunc.ReadInt("Введите цену (руб): ", 100, 1000000);
                        int screenSize = InputFunc.ReadInt("Введите размер экрана (дюймы): ", 1, 10);

                        int techChoice = InputFunc.ReadInt("Технология экрана (0-Undefined, 1-LCD, 2-OLED, 3-AMOLED): ", 0, 3);
                        TechOfScreen screenTech = (TechOfScreen)techChoice;

                        devices.Add(new Smartphone(screenSize, screenTech, model, weight, price));
                        break;
                    }
                case 3:
                    {
                        string model = InputFunc.ReadString("Введите модель: ");
                        int weight = InputFunc.ReadInt("Введите вес (г): ", 1, 2000);
                        int price = InputFunc.ReadInt("Введите цену (руб): ", 100, 1000000);
                        int storage = InputFunc.ReadInt("Введите объем памяти (МБ): ", 100, 1000000);

                        int hasTouch = InputFunc.ReadInt("Есть ли сенсорный экран? (1 - Да, 0 - Нет): ", 0, 1);

                        devices.Add(new EBook(storage, hasTouch == 1, model, weight, price));
                        break;
                    }
                case 4:
                    {
                        if (devices.Count == 0)
                        {
                            Console.WriteLine("Список устройств пуст!");
                            break;
                        }

                        Console.WriteLine("\nСписок устройств:");
                        for (int i = 0; i < devices.Count; i++)
                        {
                            Console.WriteLine($"Устройство #{i + 1}:");
                            devices[i].Print();
                            Console.WriteLine("---------------------");
                        }
                        break;
                    }
                case 5:
                    {
                        if (devices.Count == 0)
                        {
                            Console.WriteLine("Список устройств пуст!");
                            break;
                        }

                        for (int i = 0; i < devices.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} - {devices[i].Model}");
                        }

                        int index = InputFunc.ReadInt("Выберите номер устройства для изменения: ", 1, devices.Count) - 1;

                        string newModel = InputFunc.ReadString("Введите новую модель: ");
                        int newWeight = InputFunc.ReadInt("Введите новый вес (г): ", 1, 2000);
                        int newPrice = InputFunc.ReadInt("Введите новую цену (руб): ", 100, 1000000);

                        devices[index].ChangeModel(newModel);
                        devices[index].ChangeWeight(newWeight);
                        devices[index].ChangePrice(newPrice);

                        Console.WriteLine("Параметры устройства успешно обновлены!");
                        break;
                    }
                case 0:
                    return;
            }
        }
    }
}
