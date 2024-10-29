using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Classe FastEnemy que herda de EnemyMovement
public class FastEnemy : EnemyMovement
{
    // Método chamado a cada frame
    private void Update()
    {
        MeiaVida(); // Verifica condição para ativar habilidade ao atingir metade da vida
        Updt();     // Chama o método Update da classe base
    }

    // Método chamado em intervalos fixos, ideal para física e movimentação
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o método FixedUpdate da classe base
    }

    // Sobrescrita do método MeiaVida da classe base, adaptado para FastEnemy
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida atingiram metade e se a habilidade ainda não está ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade ao atingir metade da vida
            baseSpeed = 6;          // Aumenta a velocidade base para 6
        }
    }
}
