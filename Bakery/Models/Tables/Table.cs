
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private ICollection<IBakedFood> FoodOrders;
        private ICollection<IDrink> DrinkOrders;
        private int tableNumber;
        private int capacity;
        private int numberOfPeople;
        private decimal pricePerPerson;
        private bool isReserved;
        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            FoodOrders = new List<IBakedFood>();
            DrinkOrders = new List<IDrink>();
            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
            this.isReserved = false;
        }
        public int TableNumber
        {
            get { return this.tableNumber; }
            private set
            {
                this.tableNumber = value;
            }
        }

        public int Capacity
        {
            get { return this.capacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                this.capacity = value;
            }
        }
        public int NumberOfPeople
        {
            get { return this.numberOfPeople; }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }
                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson
        {
            get { return this.pricePerPerson; }
            private set
            {
                pricePerPerson = value;
            }
        }

        public bool IsReserved
        {
            get { return isReserved; }
        }

        public decimal Price
        {
            get { return pricePerPerson * NumberOfPeople; }
        }

        public void Clear()
        {
            //Removes all of the ordered drinks and food and finally frees the table and sets the count of people to 0.
            FoodOrders = new List<IBakedFood>();
            DrinkOrders = new List<IDrink>();
            isReserved = false;
            this.NumberOfPeople = 0;
        }

        public decimal GetBill()
        {

            decimal totalPrice = 0;
            foreach (var food in FoodOrders)
            {
                totalPrice += food.Price;
            }
            foreach (var drink in DrinkOrders)
            {
                totalPrice += drink.Price;
            }
            totalPrice += this.Price;
            return totalPrice;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Table: {this.TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Price per Person: {PricePerPerson}");

            return sb.ToString().Trim();
        }

        public void OrderDrink(IDrink drink)
        {
            DrinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            FoodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            isReserved = true;
            NumberOfPeople = numberOfPeople;
        }
    }
}
