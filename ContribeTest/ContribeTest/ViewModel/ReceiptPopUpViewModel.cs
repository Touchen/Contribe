using ContribeTest.Common;
using ContribeTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContribeTest.ViewModel
{
    class ReceiptPopUpViewModel:CommonBase
    {
        private static ReceiptPopUpViewModel _ReceiptPopUpViewModel;

        /// <summary>
        /// Constructor for the ReceiptPopUpViewModel
        /// </summary>
        public ReceiptPopUpViewModel()
        {
            _ReceiptPopUpViewModel = this;
        }

        /// <summary>
        /// Singleton pattern Used, because there should only exist and show one ReceiptViewModel
        /// </summary>
        /// <returns></returns>
        public static ReceiptPopUpViewModel GetInstance()
        {
            if(_ReceiptPopUpViewModel == null)
            {
                _ReceiptPopUpViewModel = new ReceiptPopUpViewModel();
            }
            return _ReceiptPopUpViewModel;
        }

        /// <summary>
        /// Asks the bookmanagers to the get Receipt View, The reason why Receipt and Order lists have been seperated is stated in the BookManager
        /// </summary>
        public ObservableCollection<string> ReceiptView
        {
            get { return BookManager.GetInstance().GetReceipt(); }
        }

        /// <summary>
        /// Asks the bookmanagers what the finalPrice is, the reason of why the FinalPrice and the TotalPrice have been seperated is stated in the Bookmaanger
        /// </summary>
        public string TotalTextBox
        {
            get { return BookManager.GetInstance().GetFinalPrice().ToString() + " Kr"; }

        }


    }
}
