using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //direção em que a cobra vai se movimentar
    Vector2 dir = Vector2.right;

    //a cobra comeu algo?
    bool ate = false;

    //Tail prefab
    public GameObject tailPrefab;

    //Tail
    List<Transform> tail = new List<Transform>();

    //game controller
    GameObject gameController;


    private float speed;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        //InvokeRepeating("Move", 0.3f, 0.3f);
        speed = 0.3f;
        startTime = Time.time;
        StartCoroutine("NewMove");

    }

    // Update is called once per frame
    void Update()
    {
        //controles do game
        if (Input.GetKey(KeyCode.RightArrow)) dir = Vector2.right;
        else if (Input.GetKey(KeyCode.LeftArrow)) dir = -Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow)) dir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow)) dir = -Vector2.up;
    }

    void Move()
    {
        //salvando a coordenada atual;
        Vector2 v = transform.position;
        //movimentei a cabeça da cobra
        transform.Translate(dir);
        //tail
        if (ate)
        {
            //cria a cauda
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            //defino o elemento como inicio da cauda
            tail.Insert(0, g.transform);
            //comi
            ate = false;
        }
        else if (tail.Count > 0)
        {
            //muda a coordenada de tela do elemento
            tail[tail.Count - 1].position = v;
            tail.Insert(0, tail[tail.Count - 1]);
            tail.RemoveAt(tail.Count - 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        //food?
        if (coll.name.StartsWith("Food"))
        {
            //comeu a comida
            ate = true;
            //destruir a comida
            Destroy(coll.gameObject);
            //adiciona pontos
            gameController.GetComponent<GameController>().IncScore();
        }
        else
        {
            //Fim de jogo
            Debug.Log("Morreu!!!!!!");
            gameController.GetComponent<GameController>().GameOver();
        }
    }

    private IEnumerator NewMove()
    {
        while(true) 
        {
            Move();
            yield return new WaitForSeconds(speed);
            if(Time.time - startTime > 30)
            {
                if (speed > 0.03) speed -= 0.01f;
                startTime = Time.time;
            }
        }
    }
}
