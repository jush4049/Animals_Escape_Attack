using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFruit : MonoBehaviour
{
    public GameObject FruitApple;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(FruitApple);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
