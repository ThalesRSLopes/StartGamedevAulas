using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
    }

    #region Movement

    // OnMove realiza as ações de movimentação do player
    void OnMove()
    {
        // player.direction.sqrMagnitude retorna um valor inteiro que indica a quantidade de movimento que o player está realizando
        // caso seja 0, indica que o player está parado e, caso seja diferente de 0, o player está se movimentando

        // É feita a verificação da magnitude para definir a animação correta
        // Se a magnitude for maior que 0, define a animação 1, que é de andar
        if (player.direction.sqrMagnitude > 0)
        {
            // Se o player apertar para rolar enquanto anda, ao invés de definir a animação de andar (1), define a animação de rolar (3)
            if (player.isRolling)
            {
                anim.SetTrigger("isRoll");
            }
            // Se o player não estiver apertando para rolar, executa normalmente a ação de andar
            else
            {
                anim.SetInteger("transition", 1);
            }
        }
        // Caso a magnitude seja 0
        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void OnRun()
    {
        if (player.isRunning)
        {
            anim.SetInteger("transition", 2);
        }
    }

    #endregion
}
