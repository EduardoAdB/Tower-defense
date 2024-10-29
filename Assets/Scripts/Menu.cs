using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;  // Refer�ncia ao elemento de UI que exibe a quantidade de moeda
    [SerializeField] Animator anim;  // Refer�ncia ao componente Animator para controlar anima��es do menu

    private bool isMenuOpen = true;  // Indica se o menu est� aberto ou fechado

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;  // Inverte o estado do menu (abre se estiver fechado e vice-versa)
        anim.SetBool("Menu Open", isMenuOpen);  // Define a anima��o de abrir ou fechar o menu com base no estado atual
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();  // Atualiza o texto do UI com a moeda atual do jogador
    }
}
