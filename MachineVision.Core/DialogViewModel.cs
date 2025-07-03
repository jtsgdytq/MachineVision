using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core
{
    public class DialogViewModel : BindableBase, IDialogAware
    {
        public DialogCloseListener RequestClose {  get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
