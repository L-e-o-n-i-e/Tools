using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomCollection<T> where T : IComparable
{
    T[] arr;
    int currentIndex = 0;

    public void Add(T toAdd)
    {
        arr[currentIndex] = toAdd;
    }

    public void Remove(int removeAtIndex)
    {
        try
        {
            for (int i = removeAtIndex; i < currentIndex - 1; i++)
            {
                arr[i] = arr[i + 1];
            }

            currentIndex--;
        }
        catch 
        {
            throw new System.Exception("Out of bounds");

        }
    }

}
