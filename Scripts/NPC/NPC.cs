using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private float speed;
    public float initialSpeed;

    private int index;
    private Animator anim;

    public List<Transform> paths = new List<Transform>();

    void Start()
    {
        speed = initialSpeed;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogControl.instance.isShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }

        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        // Verifica se a posição entre o NPC e o ponto de chegada é muito próximo a zero
        if(Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            index = Random.Range(0, paths.Count);
        }

        Vector2 direction = paths[index].position - transform.position;

        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
