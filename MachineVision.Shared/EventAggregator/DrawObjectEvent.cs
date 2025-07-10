using MachineVision.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Shared.EventAggregator
{
    public class DrawObjectEvent:PubSubEvent<DrawObjectInfo>
    {
    }
}
