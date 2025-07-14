using AlgoPaws.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace AlgoPaws.Algorithms
{
    internal interface ISortingAlgorithm
    {
        string Name { get; }
        Task Sort(List<NumberItem> items, int speed, CancellationToken cancellationToken);
    }
}
