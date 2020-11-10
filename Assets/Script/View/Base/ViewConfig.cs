using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    None,
    EmptyView,
    LoadingView ,
    HomeView,
    IngameView,
    ResultView
}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices = {
    
        ViewIndex.EmptyView,
        ViewIndex.LoadingView,
        ViewIndex.HomeView,
        ViewIndex.IngameView,
        
      };
}
public class ViewParam
{

}


