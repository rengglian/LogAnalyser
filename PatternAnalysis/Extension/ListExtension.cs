using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatternAnalysis.Extension
{
    public static class ListExtensions
    {
        public static ObservableCollection<Point> ToObservableCollection(this List<Point> list)
        {
            var resultList = new ObservableCollection<Point>();
            list.ForEach(x => resultList.Add(x));
            return resultList;
        }
    }

}
