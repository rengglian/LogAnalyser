using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
