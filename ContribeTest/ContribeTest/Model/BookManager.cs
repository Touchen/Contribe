using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContribeTest.Model
{
    class BookManager
    {

        private static BookManager _BookManager;
        private List<Book> _Books;
        private List<Book> _Orders;
        private decimal _FinalPrice;

        private BookManager()
        {
            _Books = new List<Book>();
            _Orders = new List<Book>();
            GetBooks(string.Empty);
        }

        /// <summary>
        /// Singleton pattern used. Because only one BookManager should exist
        /// </summary>
        /// <returns> the instance of BookManager</returns>
        public static BookManager GetInstance()
        {
            if(_BookManager == null)
            {
                _BookManager = new BookManager();
            }
            return _BookManager;
        }

        /// <summary>
        /// Adds a book from Books(which is the from the BooksViewList) to the OrderList. 
        /// </summary>
        /// <param name="aBookIndex"></param>
        public void Add(int aBookIndex)
        {
            _Orders.Add(_Books[aBookIndex]);
        }

        /// <summary>
        /// Removes a book from the OrderList at the specific Index 
        /// </summary>
        /// <param name="aOrderIndex"></param>
        public void Remove(int aOrderIndex)
        {
            _Orders.RemoveAt(aOrderIndex);
        }

        /// <summary>
        /// Returns the Size of the OrderList
        /// </summary>
        /// <returns></returns>
        public int GetSizeOfOrderList()
        {
            return _Orders.Count;
        }

        /// <summary>
        /// Returns the TotalPrice, Note this is not hte FinalPrice
        /// </summary>
        /// <returns></returns>
        public decimal GetTotPrice()
        {
            decimal totPrice = 0;
            for (int i = 0; i < _Orders.Count; i++)
            {
                totPrice += _Orders[i].GetPrice();
            }
            return totPrice;
        }

        /// <summary>
        /// Asks the Connector to get the String from the specific URL. The string will then be Converted to a list. 
        /// Later on it Checks if the List contains any title or author with a text similar to aSearch
        /// </summary>
        /// <param name="search"></param>
        public void GetBooks(string aSearch)
        {
           
            string booksJSON = Connector.GetInstance().GetStringFromServer("http://www.contribe.se/arbetsprov-net/books.json");

            var list = JObject.Parse(booksJSON)["books"].Select(el=> new{ title = (string)el["title"], author = (string)el["author"], price = (decimal)el["price"], inStock = (int)el["inStock"] }).ToList();

            _Books.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].title.Contains(aSearch) || list[i].author.Contains(aSearch))
                {
                    Book book = new Book(list[i].title, list[i].author, list[i].price, list[i].inStock);
                    _Books.Add(book);
                }
            }

        }

        /// <summary>
        /// Returns a ObservableCollection to for the ViewModel to be able to show it to the Screen.
        /// Asks the connector for the List of books and checks if all of the books are still in stock. I have to check this once more, because it might have been 
        /// changed during the time between putting them into the OrderList and when they have been ordered. This is also the reason why OrderList have been seperated
        /// To save time and power, this loop also put together the Finalprice
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GetReceipt()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();
            string booksJSON = Connector.GetInstance().GetStringFromServer("http://www.contribe.se/arbetsprov-net/books.json");
            _FinalPrice = 0;

            var list = JObject.Parse(booksJSON)["books"].Select(el => new { title = (string)el["title"], author = (string)el["author"], price = (decimal)el["price"], inStock = (int)el["inStock"] }).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < _Orders.Count; j++)
                {
                    if (list[i].title == _Orders[j].GetTitle() && list[i].author == _Orders[j].GetAuthor())
                    {
                        if (list[i].inStock <= 0)
                        {
                            collection.Add(_Orders[j].GetTitle() + ", " + _Orders[j].GetAuthor() + " is out of stock");
                        }
                        else
                        {
                            _FinalPrice += list[i].price;
                            collection.Add(_Orders[j].GetTitle() + ", " + _Orders[j].GetAuthor() + " Order have been placed");
                        }
                    }
                }
            }
            return collection;
        }

        
        /// <summary>
        /// Returns the FinalPrice of all the products
        /// </summary>
        /// <returns></returns>
        public decimal GetFinalPrice()
        {
            return _FinalPrice;
        }

        /// <summary>
        /// Adds a readable string into the ObservableCollection from the books in _Books.
        /// 
        /// </summary>
        /// <returns>Returns a ObservableCollection for the ViewModel to be able to show it to the screen</returns>
        public ObservableCollection<string> GetBookList()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();

            for (int i = 0; i < _Books.Count; i++)
            {
                collection.Add(_Books[i].ToString());   
            }
            return collection;
        }

        /// <summary>
        /// Adds a readable string into the ObservableCollection from the books in _Orders.
        /// </summary>
        /// <returns>>Returns a ObservableCollection for the ViewModel to be able to show it to the screen</returns>
        public ObservableCollection<string> GetOrderList()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();

            for (int i = 0; i < _Orders.Count; i++)
            {
                collection.Add(_Orders[i].GetTitle());
            }
            return collection;
        }

    }
}
