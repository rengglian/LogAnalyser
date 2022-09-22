using ImageAnalysis.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageAnalysis.Extension;

public static class ListExtensions
{
    public static ObservableCollection<Spot> ToObservableCollection(this List<Spot> list)
    {
        var resultList = new ObservableCollection<Spot>();
        list.ForEach(x => resultList.Add(x));
        return resultList;
    }
}

