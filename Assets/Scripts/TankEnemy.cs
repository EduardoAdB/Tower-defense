using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyMovement
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
            hitPoints = hitPoints * 4;
        }

    }
}

