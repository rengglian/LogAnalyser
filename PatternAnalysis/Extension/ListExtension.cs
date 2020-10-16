using OxyPlot;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatternAnalysis.Extension
{
    public static class ListExtensions
    {
        public static ObservableCollection<DataPoint> ToObservableCollection(this List<DataPoint> list)
        {
            var resultList = new ObservableCollection<DataPoint>();
            list.ForEach(x => resultList.Add(x));
            return resultList;
        }
    }

}
