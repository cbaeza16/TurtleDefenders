using UnityEngine;
using System;

[Serializable]
public class Bird {
    public string name;
    public int cost;
    public GameObject prefab;

    public Bird(string _name, int _cost, GameObject _prefab) {
        name = _name;
        cost = _cost;
        prefab = _prefab;

    }
}
