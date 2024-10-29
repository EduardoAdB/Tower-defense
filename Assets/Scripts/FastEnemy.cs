using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FastEnemy : EnemyMovement
{
    private void Update()
    {
        MeiaVida();
        Updt();
    }
    private void FixedUpdate()
    {
        FxdUpdate();
    }
    public override void MeiaVida()
    {
        if (hitPoints == metadeDaVida && habilidadeAtiva == false)
        {
            habilidadeAtiva = true;
            baseSpeed = 4;
        }

    }
}
