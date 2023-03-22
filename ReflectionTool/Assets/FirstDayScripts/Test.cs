using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class Test : MonoBehaviour
{
    

    [ExposeMethodInEditor]
    void Start()
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        //Defining the BindingFlags for not having to type them all everytime.


        Banana b = new Banana(33, "Pakistan", 10, true);
        Banana b2 = new Banana(120, "Colombia", 2, false);
        Banana b3 = new Banana(5, "Hawaii", 90, false);


        //We extract the type.
        Type bananaType = typeof(Banana);
        //Second way of doing it :
        bananaType = b.GetType();
        //Other way:
        bananaType = Type.GetType("Banana");


        //Extract something from it :
        //Field Info
        FieldInfo bananaFieldInfo = bananaType.GetField("id", bindingFlags);
        int bananaId = (int)bananaFieldInfo.GetValue(b3);
        Debug.Log(bananaId.ToString());

        //FieldInfo to extract the name, which is a private property
        FieldInfo bananaFieldInfo2 = bananaType.GetField("country", bindingFlags);
        string bananaId2 = (string)bananaFieldInfo2.GetValue(b2);
        Debug.Log(bananaId2);

        OutPutField<Banana>("width", b3);


    }

   
    public static void OutPutField<T>(string field, object obj)
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        Type myObjType = obj.GetType();
        FieldInfo fieldInfo = myObjType.GetField(field, bindingFlags);
        Debug.Log("OutputField : " + fieldInfo.GetValue(obj).ToString());
    }
}

public class Banana
{
    public int id;
    private string country;
    [SerializeField] float width;
    [HideInInspector] bool isMure;
    private bool isOpen;
    public bool IsOpen { get => isOpen; set => isOpen = value; }

    public Banana() { }

    public Banana(int id, string country, float width, bool isMure)
    {
        this.id = id;
        this.country = country;
        this.width = width;
        this.isMure = isMure;
        this.isOpen = false;
    }


    public void OpenTheBanana()
    {
        Debug.Log($"The banana {id} is open.");
        this.isOpen = true;
    }

    public void ThrowTheBanana(int distance)
    {
        Debug.Log($"The banana was thrown {distance} meters away.");
    }

    float GetWidth()
    {
        return width;
    }

    void OutputCountry()
    {
        Debug.Log(country);
    }
}
