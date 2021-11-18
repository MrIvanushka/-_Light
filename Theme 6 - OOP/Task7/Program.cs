using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Random random = new Random();
            Store store = new Store(random);
            Queue<Shopper> shopperPool = new Queue<Shopper>();
            int shopperCount = 5;

            for (int i = 0; i < shopperCount; i++)
                shopperPool.Enqueue(new Shopper(random.Next(100, 1000), store.GetProducts(random.Next(3, 10))));

            for(int i = 1; shopperPool.Count > 0; i++)
            {
                Shopper currentShopper = shopperPool.Dequeue();
                Console.WriteLine($"Покупатель {i} кошелёк: {currentShopper.Money} руб.");
                currentShopper.ShowCart();
                currentShopper.BuyProducts();
                currentShopper.ShowBoughtProducts();
                Console.WriteLine("============================================");
            }
        }
    }

    class Shopper
    {
        private List<Product> _productsInCart;
        private List<Product> _boughtProducts;
        private int _money;
        private Random _random;

        public int Money => _money;

        public Shopper(int money, List<Product> productsInCart)
        {
            Init(money, productsInCart);
            _random = new Random();
        }

        public Shopper(int money, List<Product> productsInCart, Random random)
        {
            Init(money, productsInCart);
            _random = random;
        }

        private void Init(int money, List<Product> productsInCart)
        {
            _money = money;
            _productsInCart = productsInCart;
            _boughtProducts = null;
        }

        public void BuyProducts()
        {
            int orderAmount = ScoreOrderAmount();
            Console.WriteLine($"Сумма заказа: {orderAmount} руб.");

            while (orderAmount > _money)
            {
                Product surplusProduct = _productsInCart[_random.Next(_productsInCart.Count)];
                orderAmount -= surplusProduct.Price;
                Console.WriteLine($"Недостаточно средств. {surplusProduct.Name} убран.");
                Console.WriteLine($"Сумма заказа: {orderAmount} руб.");
                _productsInCart.Remove(surplusProduct);
            }

            _boughtProducts = _productsInCart;
            _productsInCart = null;
        }

        private int ScoreOrderAmount()
        {
            int orderAmount = 0;
            _productsInCart.ForEach(product => orderAmount += product.Price);
            
            return orderAmount;
        }

        public void ShowCart()
        {
            ShowContainer(_productsInCart, "Корзина");
        }

        public void ShowBoughtProducts()
        {
            ShowContainer(_boughtProducts, "Купленные продукты");
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
        private Random _random;

        public Store()
        {
            InitProducts();
            _random = new Random();
        }

        public Store(Random random)
        {
            InitProducts();
            _random = random;
        }

        private void InitProducts()
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

        public List<Product> GetProducts(int productCount)
        {
            List<Product> exportProducts = new List<Product>();

            for(int i = 0; i < productCount; i++)
            {
                exportProducts.Add(_allProducts[_random.Next(_allProducts.Length)]);
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
            return Name.ToString() + " " + Price.ToString() + " руб.";
        }
    }
}