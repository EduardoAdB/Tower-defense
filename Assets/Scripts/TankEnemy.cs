using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe TankEnemy que herda da classe EnemyMovement
public class TankEnemy : EnemyMovement
{
    // Método chamado a cada frame
    private void Update()
    {
        MeiaVida(); // Verifica condição para ativação de habilidade ao atingir metade da vida
        Updt();     // Chama o método Update da classe base
    }

    // Método chamado em intervalos fixos, ideal para física e movimentação
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o método FixedUpdate da classe base
    }

    // Sobrescrita do método MeiaVida da classe base, adaptado para o TankEnemy
    public override void MeiaVida()
    {
        // Verifica se os pontos de vida atingiram metade e se a habilidade ainda não está ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true;     // Ativa a habilidade ao atingir metade da vida
            hitPoints = hitPoints * 4;  // Aumenta os pontos de vida 4 vezes
        }
    }
}
