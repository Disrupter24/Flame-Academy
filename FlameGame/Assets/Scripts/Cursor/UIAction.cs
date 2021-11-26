using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class UIAction
{
    //potentialy done by one action like OnCursorChange(newCursorState)
    public static Action OnCursorIdle;
    //public static Action OnCursorOverMaterialTypeA;
    //public static Action OnCursorOverMaterialTypeB;
    //public static Action OnCursorOverWorker;
    public static Action OnCursorWorkerSelected;
    //public static Action OnCursorMaterialSelected;
    public static Action OnCursorStartSelection;
    public static Action OnCursorDrawWood;
    public static Action OnCursorDrawGrass;
    public static Action OnCursorScrollStop; 

    public static Action OnCursorWorkerMove;
    //public static Action OnCursorWorkerDrop;
    //public static Action OnCursorWorkerHarvest;
    public static Action <UICursorManager.Direction> OnCursorScroll;
    //scroll up down left right cursor
    //cancel cursor (animation?) 

}
