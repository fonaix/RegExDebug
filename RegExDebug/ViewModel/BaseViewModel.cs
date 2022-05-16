using System.ComponentModel;

namespace RegExDebug.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
        public void RaiseAndSetIfChanged<T>(ref T a, T v, string propertyName = null)
        {
            a = v;
            if (propertyName != null)
            {
                RaisePropertyChanged(propertyName);
            }
        }
    }
}
