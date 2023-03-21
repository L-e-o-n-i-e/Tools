using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    Dictionary<string, MethodInfo> myDict = new Dictionary<string, MethodInfo>();
    // Start is called before the first frame update
    void Start()
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        Dog dog = new Dog("Maya", 6, 3.7f, true);
        Type dogType = typeof(Dog);

        MethodInfo methodInfo = dogType.GetMethod("OutputDogName", bindingFlags);
        methodInfo.Invoke(dog, new object[] { });

        MethodInfo mi = dogType.GetMethod("SecretCatFunction", bindingFlags);
        mi.Invoke(dog, new object[] { });

        MethodInfo mi1 = dogType.GetMethod("SetDogIQ", bindingFlags);
        mi1.Invoke(dog, new object[] { 2.0f });

        //Dans PropertyInfo, il y a Get :  GetPropertyInfo et Set :  SetPropertyInfo 
        PropertyInfo pI = typeof(Dog).GetProperty("isADog", bindingFlags);
        MethodInfo isADogGetMethod = pI.GetGetMethod();
        MethodInfo isADogSetMethod = pI.GetSetMethod();

        //Dans ParameterInfo : 

    }
}

//We made our own custom Attribute that takes arguments in his constructor
//This attactches meta data to the class, just like when we set a tag in the inspector
[FindMe("Dogo", 4)]
public class Dog
{
    public string dogName;
    [FindMe("Age", 1)] int dogAge;
    [SerializeField] float IQ;
    [HideInInspector] bool isSecretlyACat;
    public bool isADog => !isSecretlyACat;

    public Dog(string dogName, int dogAge, float iQ, bool isSecretlyACat)
    {
        this.dogName = dogName;
        this.dogAge = dogAge;
        IQ = iQ;
        this.isSecretlyACat = isSecretlyACat;
    }

    public void OutputDogName()
    {
        Debug.Log("Name: " + dogName);
    }

    bool SecretCatFunction()
    {
        if (isSecretlyACat)
            Debug.Log("you found " + dogName + " secret cat function, it is a cat");
        else
            Debug.Log("you found " + dogName + " secret cat function, but is not a cat");

        return isSecretlyACat;
    }

    void SetDogIQ(float newIQ)
    {
        IQ = newIQ;
        Debug.Log($"{dogName} new IQ is: {IQ}");
    }

    void FindFieldWithAttribute()
    {
        FieldInfo[] allFields = typeof(Dog).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fi in allFields)
        {
            FindMeAttribute findMeAtt = fi.GetCustomAttribute<FindMeAttribute>();
            if (findMeAtt != null)
            {
                //Get the attribute of the field and use the membre inside the Attribute.
                string secretFieldInside = findMeAtt.someCustomData;
            }
        }
    }

}
