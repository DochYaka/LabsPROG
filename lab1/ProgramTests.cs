using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Лаба_1.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void CreateMobileDevice_ValidInput_CreatesDeviceCorrectly()
        {
            // Arrange
            var input = new StringReader("TestModel\n150\n25000");
            Console.SetIn(input);

            // Act
            var device = new MobileDevice(
                InputFunc.ReadString("Введите модель: "),
                InputFunc.ReadInt("Введите вес (г): ", 1, 2000),
                InputFunc.ReadInt("Введите цену (руб): ", 100, 1000000)
            );

            // Assert
            Assert.Equal("TestModel", device.Model);
            Assert.Equal(150, device.Weight);
            Assert.Equal(25000, device.Price);

            // Cleanup
            Console.SetIn(Console.In);
        }

        [Fact]
        public void CreateSmartphone_ValidInput_CreatesSmartphoneCorrectly()
        {
            // Arrange
            var input = new StringReader("6\n2\nGalaxy\n180\n50000"); // Правильный порядок: ScreenSize, ScreenTech, Model, Weight, Price
            Console.SetIn(input);

            // Act
            var smartphone = new Smartphone(
                InputFunc.ReadInt("Введите размер экрана (дюймы): ", 1, 10),
                (TechOfScreen)InputFunc.ReadInt("Технология экрана (0-Undefined, 1-LCD, 2-OLED, 3-AMOLED): ", 0, 3),
                InputFunc.ReadString("Введите модель: "),
                InputFunc.ReadInt("Введите вес (г): ", 1, 2000),
                InputFunc.ReadInt("Введите цену (руб): ", 100, 1000000)
            );

            // Assert
            Assert.Equal("Galaxy", smartphone.Model);
            Assert.Equal(6, smartphone.ScreenSize);
            Assert.Equal(TechOfScreen.OLED, smartphone.ScreenTech);

            // Cleanup
            Console.SetIn(Console.In);
        }

        [Fact]
        public void DeviceList_AddMultipleDevices_StoresCorrectly()
        {
            // Arrange
            var devices = new List<MobileDevice>();

            var mobileDevice = new MobileDevice("Basic", 100, 5000);
            var smartphone = new Smartphone(6, TechOfScreen.OLED, "Smart", 150, 25000);
            var ebook = new EBook(2000, true, "Ebook", 200, 15000);

            // Act
            devices.Add(mobileDevice);
            devices.Add(smartphone);
            devices.Add(ebook);

            // Assert
            Assert.Equal(3, devices.Count);
            Assert.IsType<MobileDevice>(devices[0]);
            Assert.IsType<Smartphone>(devices[1]);
            Assert.IsType<EBook>(devices[2]);
        }

        [Fact]
        public void PrintAllDevices_OutputsAllInformation()
        {
            // Arrange
            var devices = new List<MobileDevice>
            {
                new MobileDevice("Device1", 100, 5000),
                new Smartphone(5, TechOfScreen.LCD, "Phone1", 150, 15000),
                new EBook(1000, false, "Book1", 200, 8000)
            };

            var outputWriter = new StringWriter();
            Console.SetOut(outputWriter);

            // Act
            foreach (var device in devices)
            {
                device.Print();
                Console.WriteLine("---------------------");
            }

            var result = outputWriter.ToString();

            // Assert
            Assert.Contains("Device1", result);
            Assert.Contains("Phone1", result);
            Assert.Contains("Book1", result);
            Assert.Contains("Тип экрана: LCD", result);
            Assert.Contains("Объем памяти: 1000 МБ", result);

            // Cleanup
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}