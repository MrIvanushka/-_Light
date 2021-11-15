using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleApp thisApp = new ConsoleApp();

            do
            {
                thisApp.Update();
            }
            while (thisApp.enable = true);
        }
    }

    class ConsoleApp
    {
        public bool enable = true;
        private Seller _seller;
        private Shopper _shopper;

        public ConsoleApp()
        {
            _seller = new Seller("George", new Product[]{new Product("C#Light", 30), new Product("UnityJunior", 105), new Product("Particles", 30), new Product("File", 30) });
            _shopper = new Shopper(GetVariable("Insert name: "), GetInt("Insert your money: "));
        }

        public void Update()
        {
            string command = GetVariable($"<{_shopper.Name}({_shopper.Money}$)> ");

            try
            {
                switch (command)
                {
                    case "Buy":
                        _shopper.BuyProduct(_seller, GetVariable("What product doo you want? "));
                        Console.WriteLine("The product was bought.");
                        break;
                    case "ShowMyInventory":
                        Console.Write(_shopper.ShowInventory());
                        break;
                    case "ShowProductList":
                        Console.Write(_seller.ShowInventory());
                        break;
                    case "Exit":
                        enable = false;
                        break;
                    case "Help":
                        Console.WriteLine("Buy - buy the product \n" +
                            "ShowMyInventory - show bought products \n" +
                            "ShowProductList -show seller's products \n" +
                            "Exit - exit");
                        break;
                    default:
                        throw new System.Exception("Unknown command. Write Help to see all commands.");
                        break;

                }
            }
            catch (System.Exception exception)
            {
                PrintErrorMessage(exception.Message);
            }

            
        }

        private int GetInt(string message)
        {
            return Convert.ToInt32(GetVariable(message));
        }

        private string GetVariable(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    abstract class Character
    {
        public string Name { get; private set; }

        private List<Product> _inventory = new List<Product>();

        public Character() : this("Username")
        { }

        public Character(string name)
        {
            Name = name;
        }

        protected void GetProduct(Product product)
        {
            _inventory.Add(product);
        }

        protected void GiveProduct(Product product)
        {
            _inventory.Remove(product);
        }

        public Product FindProduct(string productName)
        {
            Product foundProduct = null;

            _inventory.ForEach(product =>
            {
                if (product.Name == productName)
                    foundProduct = product;
            });

            if (foundProduct == null)
                throw new Exception($"{Name} doesn't contain the {productName}.");

            return foundProduct;
        }

        public string ShowInventory()
        {
            if (_inventory.Count == 0)
                throw new Exception("Character inventory is empty.");

            string letter = "";
            _inventory.ForEach(product => letter += product.ToString() + "\n");
            return letter;
        }
    }

    class Seller : Character
    {
        public Seller(string name, Product[] products) : base(name)
        {
            foreach(var product in products)
            {
                GetProduct(product);
            }
        }

        public Product SellProduct(string productName, ref int shopperMoney)
        {
            Product product = FindProduct(productName);

            if (shopperMoney < product.Price)
                throw new Exception($"Not enough money to buy {productName}.");

            shopperMoney -= product.Price;
            GiveProduct(product);
            return product;
        }

        public override string ToString()
        {
            return $"Seller {Name}; products: \n" + ShowInventory();
        }
    }

    class Shopper : Character
    {
        private int _money;
        public int Money => _money;

        public Shopper(string name, int money) : base(name)
        {
            _money = money;
        }

        public void BuyProduct(Seller seller, string productName)
        {
            GetProduct(seller.SellProduct(productName, ref _money));
        }

        public override string ToString()
        {
            return $"Shopper {Name}; + cash: {_money}; inventory: \n" + ShowInventory();
        }
    }

    class Product
    {
        public readonly string Name;
        public readonly int Price;

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name}, price: {Price}$";
        }
    }
}