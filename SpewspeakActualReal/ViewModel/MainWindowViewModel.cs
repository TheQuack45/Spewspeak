using SpewspeakActualReal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpewspeakActualReal.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Members definition
        private ICommand smartifyCommand;
        public ICommand SmartifyCommand
        {
            get
            {
                if (smartifyCommand == null)
                {
                    //smartifyCommand = new RelayCommand(sentence => { this.CallConvertSentence(sentence.ToString()); });
                    smartifyCommand = new AsyncDelegateCommand(sentence => { this.CallConvertSentence(sentence.ToString()); });
                }
                return smartifyCommand;
            }
        }

        private string convertedResult;
        public string ConvertedResult
        {
            get { return convertedResult; }
            set { convertedResult = value; }
        }

        private bool isConverting;
        public bool IsConverting
        {
            get { return isConverting; }
            set { isConverting = value; }
        }
        #endregion

        #region Constructors definition
        public MainWindowViewModel()
        {
           
        }
        #endregion

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private void CallConvertSentence(string sentence)
        {
            this.IsConverting = true;
            RaisePropertyChanged("IsConverting");

            this.ConvertedResult = Conversion.ConvertSentence(sentence);
            RaisePropertyChanged("ConvertedResult");

            this.IsConverting = false;
            RaisePropertyChanged("IsConverting");
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
