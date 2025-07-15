using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch.Share
{
    public class TemplateResult:BindableBase
    {
        public TemplateResult()
        {
            Results = new  ObservableCollection<MatchTemplateResult>();

           

        }
        public bool IsSuccess { get; set; }


        public string Message { get; set; }

        public ObservableCollection<MatchTemplateResult> Results { get; set; }


        public class MatchSetting : BindableBase
        {

            public MatchSetting()
            {
                IsShowCenter = true;
                IsShowText = true;
                IsDdetectionRange = true;
            }


            private bool isShowCenter;

            public bool IsShowCenter
            {
                get { return isShowCenter; }
                set { isShowCenter = value; 
                    RaisePropertyChanged(nameof(IsShowCenter));
                }
            }

            private bool isShowText;

            public bool IsShowText
            {
                get { return isShowText; }
                set { isShowText = value;  RaisePropertyChanged(); }
            }


            private bool isShowDetectionRange;

            public bool IsDdetectionRange
            {
                get { return isShowDetectionRange; }
                set { isShowDetectionRange = value; RaisePropertyChanged(); }
            }


        }


    }

    
    
 
}
