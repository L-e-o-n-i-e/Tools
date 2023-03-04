using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{
    bool DEBUG_MODE = true;
    public Rigidbody rb;
    private void Awake()
    {
        //1. Make an extension function on a Vector2 which returns a random value between x & y
        float value = new Vector2(-10, 0).RandomBetween();
        float value1 = new Vector2(0, -8).RandomBetween();

        //if (DEBUG_MODE)
        //    Debug.Log($"Value : {value}, value 1 : {value1}");




        //3. Make an extension function that given a string array & label, returns a single string formated like:
        string[] arr = { "Pluie", "Prune", "Pruche" };
        string beautifulWords = arr.LabelAndEachElmtToString("Jolis Mots");

        //if (DEBUG_MODE)
        //    Debug.Log(beautifulWords);




        //4. Make an extension function same as above, but works on an array of any type
        int[] arr1 = { };
        string toShow = arr1.ArrayToStringWithLabel("Pays");
        //if (DEBUG_MODE)
        //    Debug.Log(toShow);
        float[] arr2 = { 3.5f };
        string toShow1 = arr2.ArrayToStringWithLabel("Float array");
        //if (DEBUG_MODE)
        //    Debug.Log(toShow1);




        //5. Make an extension function that mimics how .Contains works    
            //Test 1
        HashSet<int> d2 = new HashSet<int>() { 3, 8, 7, 45, 21 };
        bool contains = d2.ContainsThisElement<int>(45);
        if (contains)
            Debug.Log($"Element is in the collection");

            //Test 2
        Dictionary<int, string> d1 = new Dictionary<int, string>() { { 4, "r" }, { 300, "t" } };     
        bool contains1 = d1.ContainsThisElement(new KeyValuePair<int, string>( 4, "r"));
        if (contains1)
            Debug.Log($"Element {new KeyValuePair<int, string>( 4, "r")} is in the collection");




        //6. Make an extension function that runs a predicate on each element and returns if it is true for all elements
        bool result = d2.TrueForAll((a) => { return a > 2; });
        if (result)
        {
            Debug.Log("Predicate succeded!");
        }

        bool result1 = d1.TrueForAll((a) => { return a.Key > 2; });
        if (result1)
        {
            Debug.Log("Predicate2 succeded!");
        }


        //7. Make an extension function that runs a delegate on a collection of type T, and returns a collection of type G
        HashSet<int> intData = new HashSet<int>() { 8, 9, 23, 45, 6, 12, 2 };

            //Version 1
       string[] f = intData.ExtractFromCollection<int,string>((element) =>{return (element * 10).ToString();} );
        //foreach (string item in f)
        //{
        //    Debug.Log(item + " ");
        //}

            //Version 2
        IEnumerable<string> p = intData.ExtractFromCollectionToNewOne<int, string>((element) => { return (element * 2).ToString();});
        foreach (string item in p)
        {
            Debug.Log(item);
        }

    }

    private void Update()
    {
        //2. Make an extension function of rigidbody that given a float arguement, clamps speed at that threshold
        rb.ClampSpeed(8);
        //rb.ClampSpeed(-2);

        //if (DEBUG_MODE)
        //    Debug.Log($"Speed :  {rb.velocity.magnitude}");
    }

}
