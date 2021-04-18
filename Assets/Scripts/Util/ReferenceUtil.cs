using UnityEngine;

namespace AirSeaBattle.Util
{
    public static class ReferenceUtil
    {
        /// <summary>
        /// Test if an object is null. Display and message if the object is null then returns true.
        /// </summary>
        /// <param name="referenceObject">Object reference to be tested.</param>
        /// <param name="referenceName">Name of the object reference to be included on the message.</param>
        /// <param name="className">The class name to be included on the message.</param>
        /// <returns>True if the object is null.</returns>
        public static bool TestNullReferences<T>(T referenceObject, string referenceName, string className) where T : Object
        {
            if (referenceObject == null)
            {
                Debug.LogError($"Missing {referenceName} reference in {className}!");
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Test if an array of Objects is null or empty. Display and message if the object is null then returns true.
        /// </summary>
        /// <param name="referenceArray">Reference to an array of Objects to be tested.</param>
        /// <param name="referenceName">Name of the object reference to be included on the message.</param>
        /// <param name="className">The class name to be included on the message.</param>
        /// <returns>True if the array of Objects is null or empty.</returns>
        public static bool TestNullReferences<T>(T[] referenceArray, string referenceName, string className) where T : Object
        {
            if (referenceArray == null || referenceArray.Length == 0)
            {
                Debug.LogError($"Missing {referenceName} references in {className}!");
                return true;
            }

            return false;
        }
    }
}