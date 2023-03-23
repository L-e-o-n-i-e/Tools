using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace ExtensionFunctions
{
    public static class ExtensionFuncs              //static required
    {
        //1. Make an extension function on a Vector2 which returns a random value between x & y
        public static float RandomBetween(this Vector2 v2)
        {
            return (v2.x < v2.y) ? UnityEngine.Random.Range(v2.x, v2.y) : UnityEngine.Random.Range(v2.y, v2.x);
        }

        //2. Make an extension function of rigidbody that given a float arguement, clamps speed at that threshold

        public static void ClampSpeed(this Rigidbody rb, float speed)
        {
            if (speed >= 0)
            {
                if (rb.velocity.magnitude > speed)
                    rb.velocity = rb.velocity.normalized * speed;
            }
            else
            {
                throw new Exception("Speed cannot be under 0");
            }

        }


        //3. Make an extension function that given a string array & label, returns a single string formated like:
        //label: elem1,elem2,elem3
        public static string LabelAndEachElmtToString(this string[] arr, string label)
        {
            if (arr == null)
                throw new Exception("The collection passed to LabelAndEachElmtToString is null");

            string toRet = label + " : ";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != arr.Length - 1)
                    toRet += arr[i] + ", ";
                else
                    toRet += arr[i];
            }

            return toRet;
        }

        //4. Make an extension function same as above, but works on an array of any type
        public static string ArrayToStringWithLabel<T>(this T[] arr, string label)
        {
            if (arr == null)
                throw new Exception("The array passed to ArrayToStringWithLabel() is null");

            string toRet = label + " : ";

            if (arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i != arr.Length - 1)
                        toRet += arr[i].ToString() + ", ";
                    else
                        toRet += arr[i].ToString();
                }
            }
            else
                toRet += "is empty";

            return toRet;
        }

        //5. Make an extension function that mimics how .Contains works     
        public static bool ContainsThisElement<T>(this IEnumerable<T> collection, T value)
        {
            bool contains = false;

            if (collection.Count<T>() == 0)
                throw new Exception($"The collection passed to ContainsThisElement() is empty.");

            foreach (T item in collection)
            {
                if (item.Equals(value))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }

        //6. Make an extension function that runs a predicate on each element and returns if it is true for all elements
        //Predicate : Predicate <float, int> delegate that always returns a bool. returns true if all elements are true to a confdition   ex : arr.TrueForAll((a) => {return a > 0 })
        //Test script run each method at least once.
        public static bool TrueForAll<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            bool condition = true;

            if (collection.Count<T>() == 0)
                throw new Exception($"The collection passed to ContainsThisElement() is empty.");

            foreach (T item in collection)
            {
                if (!predicate.Invoke(item))
                {
                    condition = false;
                    break;
                }
            }
            return condition;
        }

        //7. Make an extension function that runs a delegate on a collection of type T, and returns a collection of type G
        //Examples:

        //    Vector3[] velocitiesOfEachRb = arrayOfRigidbodiesIHave.CollectionFrom((rb) => { return rb.velocity; });

        //Vector3[] positionsOfGameObjects = arrayOfGameObjects.CollectionFrom((go) => { return go.position; });
        //Delegate on a collection of type T and returns collection of type G ex : CollectionFrom((rb) =? {return rb.velocity;}); Must work for int, float, bool, string
        //Test script run each method at least once.

        //T[]  --- T     return default T;

        public static TResult[] ExtractFromCollection<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> func)
        {
            if (collection.Count<T>() == 0)
                throw new Exception("Collection passed in GetCollectionOf function is empty");

            TResult[] toRet = new TResult[collection.Count<T>()];
            int i = 0;

            foreach (T item in collection)
            {
                try
                {
                    TResult result = func.Invoke(item);
                    toRet[i] = result;
                    i++;
                }
                catch (Exception e)
                {
                    Debug.Log($"Error in TrueForAll function : {e.Message}");
                }
            }

            return toRet;                                                                       //cannot return null, int cannot be null.
        }


        public static IEnumerable<TResult> ExtractFromCollectionToNewOne<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> func)
        {
            if (collection.Count<T>() == 0)
                throw new Exception("Collection passed in GetCollectionOf function is empty");

            List<TResult> toRet = new List<TResult>();

            foreach (T item in collection)
            {
                for (int i = 0; i < collection.Count<T>(); i++)
                {
                    try
                    {
                        toRet.Add(func.Invoke(item));
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"Error in TrueForAll function : {e.Message}");
                    }

                }
            }

            return toRet;                                                                       //cannot return null, int cannot be null.
        }


        /*
            Examples shown in class
            An extension function that returns the average of a vector3
            An extension function that absolutes all values in an int array
            An extension function of rigidbody that given a float arguement, returns if speed is above that threshold
            Our own Contains function
            An extension function that runs a delegate on each element in an int array and replaces the int
            An extension function that extends type T array and randomizes the array

            */



        public static float Average(this Vector3 v3)
        {
            return (v3.x + v3.y + v3.z) / 3;
        }

        public static int[] Absolute(this int[] arr)
        {
            int[] toRet = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                toRet[i] = Mathf.Abs(arr[i]);
            }

            return toRet;
        }

        public static bool SpeedSupassedTreshold(this Rigidbody rb, float speed)
        {
            return rb.velocity.magnitude > speed;
        }


        public delegate float FloatArrDelegate(float value);                //exemple of a standard delegate


        public static float[] RunDelgOnEachElmt(this float[] arr, Func<float, float> func) //Always returns something
        {
            float[] toRet = new float[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                toRet[i] = func.Invoke(arr[i]);
            }

            return toRet;
        }

        public static T[] ShufflelingArray<T>(this T[] arr) //Always returns something
        {
            T[] toRet = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                toRet[i] = arr[i];                  //TODO Random the position of the element in the array
            }

            return toRet;
        }

        public static float[] ToFloatArr(this Vector3 v3)
        {
            float[] arr = new float[3];
            arr[0] = v3.x;
            arr[1] = v3.y;
            arr[2] = v3.z;

            return arr;
        }

        public static Vector3 ToVector3(this float[] arr)
        {
            Vector3 v3 = new Vector3();

            if (arr.Length == 3)
            {
                v3.x = arr[0];
                v3.y = arr[1];
                v3.z = arr[2];
            }
            return v3;
        }

        public static Quaternion ToQuaternion(this float[] arr)
        {
            Quaternion q4 = new Quaternion();

            if (arr.Length == 4)
            {
                q4.x = arr[0];
                q4.y = arr[1];
                q4.z = arr[2];
                q4.w = arr[3];
            }

            return q4;
        }

        public static float[] ToArrOf4(this Quaternion q)
        {
            float[] arr = new float[4];
            arr[0] = q.x;
            arr[1] = q.y;
            arr[2] = q.z;
            arr[3] = q.w;
            return arr;
        }
        public static Vector3 RandomStartPosition(Transform worldBounds)
        {
            float x = UnityEngine.Random.Range(-worldBounds.localScale.x, worldBounds.localScale.x);
            float y = UnityEngine.Random.Range(-worldBounds.localScale.y, worldBounds.localScale.y);

            return new Vector3(x, y, 0);
        }
    }
}