using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe BossEnemy que herda da classe EnemyMovement
public class BossEnemy : EnemyMovement, IMeiaVida
{
    // Método chamado a cada frame
    private void Update()
    {
        MeiaVida(); // Verifica se o boss atingiu metade da vida para ativar habilidade
        Updt();     // Chama o método Update da classe base
    }

    // Método chamado em intervalos fixos (ideal para física e movimento)
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama o método FixedUpdate da classe base
    }

    // Sobrescrita do método MeiaVida da classe base
    public virtual void MeiaVida()
    {
        // Condicional para verificar se os pontos de vida atingiram metade e se a habilidade ainda não está ativa
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true; // Ativa a habilidade quando condição é atendida
        }
    }
}
