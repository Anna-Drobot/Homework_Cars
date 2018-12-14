using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9
{
    public static class RandomExtension
    {
        public static string RandomString(this Random rand, int lenght)
        {
            List<char> tmp = new List<char>();
            for (int i = 0; i < lenght; i++)
            {
                tmp.Add(Convert.ToChar(rand.Next(65, 91)));
            }
            return new string(tmp.ToArray());
        }
    }
    enum COLORS { RED = 1, ORANGE, YELLOW, GREEN, BLUE, PURPLE, PINK, WHITE, GREY, SILVER, BLACK }
    class Car
    { 
        public string Mark { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public Car()
        {
            Random rand = new Random();
            Mark = rand.RandomString(6);
            Model = rand.RandomString(6);
            Price = rand.Next(1_000, 15_001);
            COLORS color = (COLORS)rand.Next(1, 12);
            string[] tmp = color.ToString().Split('.');
            Color = tmp[tmp.Length - 1];
        }
        public override string ToString()
        {
            return $"{Mark} {Model} ${Price} ({Color})";
        }
    }
    class CarCollection
    {
        List<Car> cars;
        public CarCollection()
        {
            cars = new List<Car>();
        }
        public void AddCar()
        {
            cars.Add(new Car());
        }
        public void DelCar(int index = 0)
        {
            if (index >= cars.Count)
                index %= cars.Count;
            cars.RemoveAt(index);
        }
        public void PrintCars(List<Car> tmp = null)
        {
            if (tmp == null)
                tmp = cars;
            foreach (var item in tmp)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// Prints all cars with price higher than minPrice
        /// </summary>
        /// <param name="minPrice">
        /// Minimum Price
        /// </param>
        /// <exception cref="decimal">ArgumentException</exception>
        public void SelectByMinPrice(decimal minPrice = 10_000m)
        {
            List<Car> tmp = new List<Car>();
            foreach (Car car in cars)
            {
                if (car.Price > minPrice)
                    tmp.Add(car);
            }
            PrintCars(tmp);
        }
        /// <summary>
        /// Prints all cars with some value of a color
        /// </summary>
        /// <param name="color">
        /// Name of a color in capital letters
        /// All possible colors:
        /// RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE, PINK, WHITE, GREY, SILVER, BLACK
        /// </param>
        /// <exception cref="string">ArgumentException</exception>
        public void SelectByColor(string color = "RED")
        {
            List<Car> tmp = new List<Car>();
            foreach(Car car in cars)
            {
                if (car.Color == color)
                    tmp.Add(car);
            }
            PrintCars(tmp);
        }
        /// <summary>
        /// Prints all cars with some value of a price and mark
        /// </summary>
        /// <param name="mark">
        /// Name of mark
        /// </param>
        /// <param name="price">
        /// Price
        /// </param>
        /// <exception cref="string">ArgumentException</exception>
        /// <exception cref="int">ArgumentException</exception>
        public void SelectByMarkAndPrice (string mark, decimal price)
        {
            List<Car> tmp = new List<Car>();
            foreach (Car car in cars)
            {
                if (car.Mark == mark && car.Price == price)
                    tmp.Add(car);
            }
            PrintCars(tmp);
        }
        /// <summary>
        /// Gets the price of all cars
        /// </summary>
        /// <returns>Price of all cars (decimal)</returns>
        public decimal TotalPrice()
        {
            decimal total = 0;
            foreach (var item in cars)
            {
                total += item.Price;
            }
            Console.WriteLine("Total price: " + total);
            return total;
        }
        /// <summary>
        /// Gets an amount of cars of some color
        /// </summary>
        /// <param name="color">
        /// Name of a color in capital letters
        /// All possible colors:
        /// RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE, PINK, WHITE, GREY, SILVER, BLACK
        /// </param>
        /// <exception cref="string">ArgumentException</exception>
        /// <returns>Amount of cars of some color (int)</returns>
        public int CountByColor(string color = "RED")
        {
            int count = 0;
            foreach (var item in cars)
            {
                if (item.Color == color)
                    count++;
            }
            Console.WriteLine($"Number of {color.ToLower()} cars: {count}");
            return count;
        }
        /// <summary>
        /// Prints all cars, which price is lower that maxPrice
        /// </summary>
        /// <param name="maxPrice">
        /// Upper boundary of car price
        /// </param>
        /// <exception cref="int">ArgumentException</exception>
        public void SelectCheap(decimal maxPrice = 5_000m)
        {
            foreach (var item in cars)
            {
                if(item.Price < maxPrice)
                    Console.WriteLine($"{item.Mark} {item.Model}");
            }
        }
        /// <summary>
        /// Prints all cars, which price is higher than minPrice and lower than maxPrice
        /// Can calculate amount of cars with specified colors
        /// </summary>
        /// <param name="minPrice">
        /// Minimum pricie
        /// </param>
        /// <param name="maxPrice">
        /// Maximum price
        /// </param>
        /// <param name="color1">
        /// Color of car
        /// </param>
        /// <param name="color2">
        /// Color of car
        /// </param>
        /// <returns>Amount of cars, which color is equal to color1 and color2 (Tuple<int, int>)</returns>
        public Tuple<int, int> SelectByPriceDiapazon(decimal minPrice, decimal maxPrice, string color1 = "RED", string color2 = "BLACK")
        {
            int count1 = 0, count2 = 0;
            List<Car> tmp = new List<Car>();
            foreach (var item in cars)
            {
                if (item.Price >= minPrice && item.Price <= maxPrice)
                    tmp.Add(item);
                if (item.Color == color1)
                    count1++;
                else if (item.Color == color2)
                    count2++;
            }
            PrintCars(tmp);
            Console.WriteLine($"Number of {color1.ToLower()} cars: {count1}");
            Console.WriteLine($"Number of {color2.ToLower()} cars: {count2}");
            return new Tuple<int, int>(count1, count2);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int action;
            CarCollection cars = new CarCollection();
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Add a car");
                Console.WriteLine("2 - Delete a car");
                Console.WriteLine("3 - Print all cars");
                Console.WriteLine("4 - Print all cars with price higher than...");
                Console.WriteLine("5 - Print all cars colored with...");
                Console.WriteLine("6 - Print all cars with specified mark and price");
                Console.WriteLine("7 - Get price of all cars");
                Console.WriteLine("8 - Get the number of cars colored with...");
                Console.WriteLine("9 - Print all cars with price lower than...");
                Console.WriteLine("10 - Print all cars in price diapazon and get the number of cars colored with 2 specified colors");
                Console.WriteLine("11 - Exit");
                action = int.Parse(Console.ReadLine());
                if (action == 1)
                    cars.AddCar();
                else if (action == 2)
                {
                    Console.WriteLine("1 - delete first car");
                    Console.WriteLine("2 - delete other car");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                        cars.DelCar();
                    else
                    {
                        int index;
                        Console.WriteLine("Index:");
                        index = int.Parse(Console.ReadLine());
                        cars.DelCar(index);
                    }
                }
                else if (action == 3)
                    cars.PrintCars();
                else if(action == 4)
                {
                    Console.WriteLine("1 - $10000");
                    Console.WriteLine("2 - other price");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                        cars.SelectByMinPrice();
                    else
                    {
                        decimal price;
                        Console.WriteLine("Price:");
                        price = decimal.Parse(Console.ReadLine());
                        cars.SelectByMinPrice(price);
                    }
                }
                else if(action == 5)
                {
                    Console.WriteLine("1 - red");
                    Console.WriteLine("2 - other color");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                        cars.SelectByColor();
                    else
                    {
                        int colorID;
                        Console.WriteLine("Colors:");
                        for(int i = 1; i <= 11; i++)
                            Console.WriteLine($"{i} - {(COLORS)i}");
                        colorID = int.Parse(Console.ReadLine());
                        string[] tmp = ((COLORS)colorID).ToString().Split('.');
                        cars.SelectByColor(tmp[tmp.Length - 1]);
                    }
                }
                else if(action == 6)
                {
                    string mark;
                    decimal price;
                    Console.WriteLine("Mark:");
                    mark = Console.ReadLine();
                    Console.WriteLine("Price:");
                    price = decimal.Parse(Console.ReadLine());
                    cars.SelectByMarkAndPrice(mark, price);
                }
                else if(action == 7)
                {
                    cars.TotalPrice();
                }
                else if(action == 8)
                {
                    Console.WriteLine("1 - red");
                    Console.WriteLine("2 - other color");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                        cars.CountByColor();
                    else
                    {
                        int colorID;
                        Console.WriteLine("Colors:");
                        for (int i = 1; i <= 11; i++)
                            Console.WriteLine($"{i} - {(COLORS)i}");
                        colorID = int.Parse(Console.ReadLine());
                        string[] tmp = ((COLORS)colorID).ToString().Split('.');
                        cars.CountByColor(tmp[tmp.Length - 1]);
                    }
                }
                else if(action == 9)
                {
                    Console.WriteLine("1 - $5000");
                    Console.WriteLine("2 - other price");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                        cars.SelectCheap();
                    else
                    {
                        decimal price;
                        Console.WriteLine("Price:");
                        price = decimal.Parse(Console.ReadLine());
                        cars.SelectCheap(price);
                    }
                }
                else if(action == 10)
                {
                    decimal price1, price2;
                    int colorID1, colorID2;
                    Console.WriteLine("Min price:");
                    price1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Max price:");
                    price2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("1 - default colors: red and black");
                    Console.WriteLine("2 - other colors");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                        cars.SelectByPriceDiapazon(price1, price2);
                    else
                    {
                        Console.WriteLine("Colors:");
                        for (int i = 1; i <= 11; i++)
                            Console.WriteLine($"{i} - {(COLORS)i}");
                        Console.WriteLine("First color:");
                        colorID1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Second color:");
                        colorID2 = int.Parse(Console.ReadLine());
                        string[] tmp1 = ((COLORS)colorID1).ToString().Split('.');
                        string[] tmp2 = ((COLORS)colorID2).ToString().Split('.');
                        cars.SelectByPriceDiapazon(price1, price2, tmp1[tmp1.Length - 1], tmp2[tmp2.Length - 1]);
                    }
                }
                Console.ReadLine();
            } while (action != 11);
        }
    }
}
