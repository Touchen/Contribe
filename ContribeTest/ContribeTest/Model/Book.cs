using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContribeTest.Model
{
    class Book
    {
        private string _Title;
        private string _Author;
        private decimal _Price;
        private int _InStock;

        /// <summary>
        /// Constructor for the Book, this also sets the Variables in the book
        /// </summary>
        /// <param name="aTitle"></param>
        /// <param name="aAuthor"></param>
        /// <param name="aPrice"></param>
        /// <param name="aInStock"></param>
        public Book(string aTitle, string aAuthor, decimal aPrice, int aInStock)
        {
            _Title = aTitle;
            _Author = aAuthor;
            _Price = aPrice;
            _InStock = aInStock;
        }

        /// <summary>
        /// Returns a string with the title,Author,Price in kronor and how many exist in the stock
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _Title + ", " + _Author + ", " + _Price +"kr, " + _InStock + " exists in stock";
        }

        /// <summary>
        /// Returns the title of the book
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return _Title;
        }

        /// <summary>
        /// returns the Author of the book
        /// </summary>
        /// <returns></returns>
        public string GetAuthor()
        {
            return _Author;
        }

        /// <summary>
        /// returns the price of the book
        /// </summary>
        /// <returns></returns>
        public decimal GetPrice()
        {
            return _Price;
        }
    }
}
