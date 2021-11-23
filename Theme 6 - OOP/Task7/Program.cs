using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Random random = new Random();
            Store store = new Store();
            Queue<Shopper> shopperPool = new Queue<Shopper>();
            int shopperCount = 5;

            for (int i = 0; i < shopperCount; i++)
                shopperPool.Enqueue(new Shopper(random.Next(100, 1000), store.GetProducts(random.Next(3, 10), random)));

            for(int i = 1; shopperPool.Count > 0; i++)
            {
                Shopper currentShopper = shopperPool.Dequeue();
                Console.WriteLine($"Покупатель {i} кошелёк: {currentShopper.Money} руб.");
                currentShopper.ShowCart();
                currentShopper.BuyProducts(random);
                currentShopper.ShowBoughtProducts();
                Console.WriteLine("============================================");
                
            }
            Console.Read();
        }
    }

    class Shopper
    {
        private List<Product> _productsInCart;
        private List<Product> _boughtProducts;
        private int _money;

        public int Money => _money;

        public Shopper(int money, List<Product> productsInCart)
        {
            _money = money;
            _productsInCart = productsInCart;
            _boughtProducts = null;
        }

        public void BuyProducts(Random random)
        {
            int orderAmount = ScoreOrderAmount();
            Console.WriteLine($"Сумма заказа: {orderAmount} руб.");

            while (orderAmount > _money)
            {
                Product surplusProduct = _productsInCart[random.Next(_productsInCart.Count)];
                orderAmount -= surplusProduct.Price;
                Console.WriteLine($"Недостаточно средств. {surplusProduct.Name} убран.");
                Console.WriteLine($"Сумма заказа: {orderAmount} руб.");
                _productsInCart.Remove(surplusProduct);
            }

            _money -= orderAmount;
            _boughtProducts = _productsInCart;
            _productsInCart = null;
        }
        
        public void ShowCart()
        {
            ShowContainer(_productsInCart, "Корзина");
        }

        public void ShowBoughtProducts()
        {
            ShowContainer(_boughtProducts, "Купленные продукты");
        }

        private int ScoreOrderAmount()
        {
            int orderAmount = 0;
            _productsInCart.ForEach(product => orderAmount += product.Price);
            
            return orderAmount;
        }

        private void ShowContainer(List<Product> productList, string containerName)
        {
            string letter = containerName + ": \n";

            if(productList.Count > 0)
                productList.ForEach(product => letter += product.ToString() + "\n");
            else
                letter += "Пусто \n";

            Console.Write(letter);
        }
    }

    class Store
    {
        private Product[] _allProducts;

        public Store()
        {
            _allProducts = new Product[]
           {
                new Product("Колбаса", 300),
                new Product("Сыр", 100),
                new Product("Хлеб", 30),
                new Product("Курица", 150),
                new Product("Шашлык", 500),
                new Product("Кетчуп", 50),
                new Product("Майонез", 50),
                new Product("Чипсы", 70),
                new Product("Роллтон", 20),
                new Product("Макароны", 100),
                new Product("Овощи", 60),
                new Product("Гречка", 40)
           };
        }

        public List<Product> GetProducts(int productCount, Random random)
        {
            List<Product> exportProducts = new List<Product>();

            for(int i = 0; i < productCount; i++)
            {
                exportProducts.Add(_allProducts[random.Next(_allProducts.Length)]);
            }
            return exportProducts;
        }
    }
   
    class Product
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return Name + " " + Price + " руб.";
        }
    }
}