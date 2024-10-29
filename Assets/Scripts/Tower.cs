using System;
using UnityEngine;

// Define a classe Tower como serializável, permitindo que seja configurada e exibida no Inspector do Unity
[Serializable]
public class Tower
{
    // Nome da torre
    public string name;

    // Custo da torre
    public int cost;

    // Prefab da torre para instanciá-la no jogo
    public GameObject preFab;

    // Construtor que inicializa os valores dos atributos da torre
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;     // Define o nome da torre
        cost = _cost;     // Define o custo da torre
        preFab = _prefab; // Define o prefab da torre
    }
}
