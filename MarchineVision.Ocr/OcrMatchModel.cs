using MarchineVision.Ocr.View;
using MarchineVision.Ocr.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarchineVision.Ocr
{
    public class OcrMatchModel : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry service)
        {         
            service.RegisterForNavigation<QrCodeView, QrCodeViewModel>();
            service.RegisterForNavigation<BarCodeView, BarCodeViewModel>();
        }
    }
}
