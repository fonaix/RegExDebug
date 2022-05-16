using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegExDebug.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        private RelayCommand<object> _linkCommand;

        public RelayCommand<object> LinkCommand
        {
            get
            {
                if (_linkCommand == null)
                {
                    _linkCommand = new RelayCommand<object>(LinkAction);
                }
                return _linkCommand;
            }
            set => _linkCommand = value;
        }

        private void LinkAction(object obj)
        {
            Process.Start(obj.ToString());
        }
    }
}
