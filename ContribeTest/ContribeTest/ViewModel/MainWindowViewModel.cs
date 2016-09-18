using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ContribeTest.Common;
using ContribeTest.Command;
using System.Collections.ObjectModel;
using ContribeTest.Model;
using ContribeTest.View;

namespace ContribeTest.ViewModel
{
    class MainWindowViewModel:CommonBase
    {
        private static MainWindowViewModel _MainWindowViewModel;
        private string _SearchTextBox = string.Empty;
        private int _SelectedIndexBookView = -1;
        private int _SelectedIndexOrderView = -1;
        private BookManager _BookManager;
        private ReceiptPopUp _ReceiptPopUp;

        /// <summary>
        /// Constructor for the MainWindowViewModel, needs to be public
        /// </summary>
        public MainWindowViewModel()
        {
            _MainWindowViewModel = this;
            _BookManager = BookManager.GetInstance();
        }


        /// <summary>
        /// Singleton patterns used because there should only exist one MainWindow
        /// </summary>
        /// <returns>the Instance of hte MainWindowViewModel</returns>
        public static MainWindowViewModel GetInstance()
        {
            return _MainWindowViewModel;
        }



        /// <summary>
        /// Used to Add new books, checks if a Book have been selected and adds it to the order list (this is a summary of Command_Add_CanExecute and Command_Add_Execute)
        /// </summary>
        public ICommand Command_Add { get { return new NoArgCommands(Command_Add_CanExecute, Command_Add_Execute); } }

        private bool Command_Add_CanExecute()
        {
            if (_SelectedIndexBookView == -1)
            {
                return false;
            }
            return true;
        }

        private void Command_Add_Execute()
        {
            _BookManager.Add(_SelectedIndexBookView);
            RaisePropertyChanged("OrderView");
            RaisePropertyChanged("TotalTextBox");
        }


        /// <summary>
        /// This is used to remove a book from the order list, checks if a book in the order list have beeen selected and tells Bookmanager to remove it from order list(summary of 
        /// Command_Delete_CanExecute and Command_Delete_Execute)
        /// </summary>
        public ICommand Command_Delete { get { return new NoArgCommands(Command_Delete_CanExecute, Command_Delete_Execute); } }

        private bool Command_Delete_CanExecute()
        {
            if (_SelectedIndexOrderView == -1)
            {
                return false;
            }
            return true;
        }
        
        private void Command_Delete_Execute()
        {
            _BookManager.Remove(_SelectedIndexOrderView);
            _SelectedIndexOrderView = -1;
            RaisePropertyChanged("OrderView");
            RaisePropertyChanged("OrderViewIndex");
            RaisePropertyChanged("TotalTextBox");
        }


        /// <summary>
        /// Checks if the OrderList size contains atleast one book and creates a PopUp (summary of Command_Order_CanExecute and Command_Order_Execute)
        /// </summary>
        public ICommand Command_Order { get { return new NoArgCommands(Command_Order_CanExecute, Command_Order_Execute); } }

        private bool Command_Order_CanExecute()
        {
            if(_BookManager.GetSizeOfOrderList() <= 0)
            {
                return false;
            }
            return true;
        }

        private void Command_Order_Execute()
        {
            if (_ReceiptPopUp != null)
            {
                _ReceiptPopUp.Close();
            }
            _ReceiptPopUp = new ReceiptPopUp();
            _ReceiptPopUp.Show();
     
        }

        /// <summary>
        /// checks if there's something in the Search textboc and if so, the button is pressable and will tell BookManager to take foward the books with the title or author(summary of 
        /// Command_Search_CanExecute and Command_Search_Execute)
        /// </summary>
        public ICommand Command_Search { get { return new NoArgCommands(Command_Search_CanExecute, Command_Search_Execute); } }

        private bool Command_Search_CanExecute()
        {
            if (_SearchTextBox != "")
            {
                return true;
            }
            return false;
        }

        private void Command_Search_Execute()
        {
            _BookManager.GetBooks(_SearchTextBox);
            RaisePropertyChanged("BookView");
            _SelectedIndexBookView = -1;
            RaisePropertyChanged("BookViewIndex");
        }



        /// <summary>
        /// Asks BookManager what the Totalprice is and returns it to the View to show it with " Kr"
        /// </summary>
        public string TotalTextBox
        {
            get { return _BookManager.GetTotPrice().ToString() + " Kr"; }

        }

        /// <summary>
        /// Sets the searchbox for the View and also checks if it becomes emtpy. If it becomes empty it will ask BookManager to updates the BookList to make the program feel more smooth.
        /// It also updates the Bookview so the new books are shown
        /// </summary>
        public string SearchTextBox
        {
            get { return _SearchTextBox; }
            set
            {
                _SearchTextBox = value;
                RaisePropertyChanged("SearchTextBox");
                if (_SearchTextBox == string.Empty)
                {
                    _BookManager.GetBooks("");
                    RaisePropertyChanged("BookView");
                }
            }
        }
        
        /// <summary>
        /// Asks the Bookmanager for the Booklist to be able to show it to the screen.
        /// </summary>
        public ObservableCollection<string> BookView
        {
            get { return _BookManager.GetBookList(); }
        }

        /// <summary>
        /// used to set the value of which index the book have in the Booklist.
        /// </summary>
        public int BookViewIndex
        {
            get { return _SelectedIndexBookView; }
            set
            {
                _SelectedIndexBookView = value;
            }
        }


        /// <summary>
        ///  Asks the Bookmanager for the Orderlist to be able to show it to the screen.
        /// </summary>
        public ObservableCollection<string> OrderView
        {
            get { return _BookManager.GetOrderList(); }
        }

        /// <summary>
        /// used to set the value of which index the book have in the Orderlist.
        /// </summary>
        public int OrderViewIndex
        {
            get { return _SelectedIndexOrderView; }
            set
            {
                _SelectedIndexOrderView = value;
            }
        }

    }
}
