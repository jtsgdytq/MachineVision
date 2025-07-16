
using MachineVision.Ocr.View;
using MachineVision.Ocr.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Ocr
{
    public class OcrMatchModel : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry service)
        {
            service.RegisterForNavigation<BarCodeView, BarCodeViewModel>();
            service.RegisterForNavigation<QrCodeView, QrCodeViewModel>();
        }
    }
}
