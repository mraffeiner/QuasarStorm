using System.Collections.Generic;
using UnityEngine;

public class Database<T> : ScriptableObject
{
    public List<T> objects = new List<T>();
}
