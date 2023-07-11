using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangTreeView.Yang.YangObjects
{
    internal interface IObject
    {
        string Name { get; set; }

        List<Uses> Uses { get; set; }
    }
}
