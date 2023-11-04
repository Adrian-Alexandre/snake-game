using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{

    //Definir os limites para criar as comidas
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    //Prefab da comida
    public GameObject foodPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        //define o local em que a comida será criada
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderTop.position.y, borderBottom.position.y);

        //Cria a comida
        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

    public void StartSpawnFood()
    {
        InvokeRepeating("Spawn", 3, 4);
    }
}
