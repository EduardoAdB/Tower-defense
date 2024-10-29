using System; // Importa classes e estruturas do sistema
using UnityEngine; // Importa a biblioteca principal do Unity

[Serializable] // Permite que a classe seja serializada para ser usada em inspetores ou arquivos
public class Tower
{
    // Atributos da torre
    public string name; // Nome da torre
    public int cost; // Custo de construção da torre
    public GameObject preFab; // Prefab da torre que será instanciado no jogo

    // Construtor da classe Tower
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name; // Inicializa o nome da torre
        cost = _cost; // Inicializa o custo da torre
        preFab = _prefab; // Inicializa o prefab da torre
    }
}
