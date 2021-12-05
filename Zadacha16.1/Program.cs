using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;
using System.IO;

namespace Zadacha16._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product();
            Product product2 = new Product();
            Product product3 = new Product();
            Product product4 = new Product();
            Product product5 = new Product();
            Product[] products = new Product[] { product1, product2, product3, product4, product5 };

            int productNumber = 0;

            foreach (Product product in products)
            {
                productNumber += 1;
                Console.Write("Введите код продукта номер {0}: ", productNumber);
                product.ProductCode = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите имя продукта номер {0}: ", productNumber);
                product.ProductName = Console.ReadLine();
                Console.Write("Введите цену продукта номер {0}: ", productNumber);
                product.ProductPrice = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine();
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonStringWrite = JsonSerializer.Serialize(products, options);

            string path0 = "Json";
            if (!Directory.Exists(path0))
            {
                Directory.CreateDirectory(path0);
            }
            string path = "Json/Products.json";
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(jsonStringWrite);
            }
            Console.WriteLine("Нажмите любую клавишу на клавиатуре для определения наиболее дорогого товара.");
            Console.ReadKey();

            //Далее приведен код программы второй задачи.
            string jsonStringRead;
            using (StreamReader sr = new StreamReader(path))
            {
                jsonStringRead = sr.ReadToEnd();
            }

            Product[] productsRead = JsonSerializer.Deserialize<Product[]>(jsonStringRead);

            double[] arrayPrices = new double[] { productsRead[0].ProductPrice, productsRead[1].ProductPrice, productsRead[2].ProductPrice, productsRead[3].ProductPrice, productsRead[4].ProductPrice };
            double maxPrice = arrayPrices[0];
            int productReadNumber = 0;
            for (int i = 0; i < 5; i++)
            {
                if (arrayPrices[i] > maxPrice)
                {
                    maxPrice = arrayPrices[i];
                    productReadNumber = i;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Товар \"{0}\" с ценой {1:N} является наиболее дорогим (или одним из наиболее дорогих).", productsRead[productReadNumber].ProductName, maxPrice);
            Console.ReadKey();
        }
    }
    class Product
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
