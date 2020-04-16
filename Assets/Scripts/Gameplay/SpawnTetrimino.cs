using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetrimino : MonoBehaviour
{
    public GameObject[] Tetriminoes;
    // Start is called before the first frame update
    GameObject fallingBlock = null;

    void Start()
    {
        NewTetrimino();
    }

    // Update is called once per frame
    public void NewTetrimino()
    {
        fallingBlock = Instantiate(Tetriminoes[Random.Range(0, Tetriminoes.Length)], transform.position, Quaternion.identity);
    }

    public void SendToFallingBlock(string message)
    {
        fallingBlock.SendMessage(message);
    }
}
