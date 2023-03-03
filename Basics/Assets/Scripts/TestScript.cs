using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        CustomCollection<int> myCustomCollection = new CustomCollection<int>();
        myCustomCollection.Add(3);

        CustomCollection<string> customCollection2 = new CustomCollection<string>();
        customCollection2.Add("hello");

        CustomCollection<IceCream> customCollection3 = new CustomCollection<IceCream>();
        customCollection3.Remove(2);
    }
}

public class IceCream : IComparable
{
    public int chocolateChips;
    public float sugar;

    public int CompareTo(object obj)
    {
        //I need to be able to compare ice cream to something else.
        //If object is of type iceCream, cast it, and save it into this variable
        if (obj is IceCream otherIceCream)
        {
            if (this.sugar > otherIceCream.sugar)
                return 1;
            else if (this.sugar < otherIceCream.sugar)
                return -1;
            else
                return 0;
        }

        throw new System.Exception($"Cannot compare {this.GetType().Name} to " + obj.GetType());
    }
}
