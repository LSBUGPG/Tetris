﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    public Vector3 rotationPoint;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    public float hackCoolTime = 5;
    public float[] hackTimer;
    public bool hacked;
    public bool coolingHack;
    private static Transform[,] grid = new Transform[width, height];


    // Start is called before the first frame update
    void Start()
    {
        hacked = false;
        coolingHack = true;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
            MoveLeft();
         }
         else if (Input.GetKeyDown(KeyCode.RightArrow))
         {
            MoveRight();
         }
         else if (Input.GetKeyDown(KeyCode.UpArrow))
         {
            Rotate();
         }

         if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
         {
            MoveDown();
         }
    }

    public void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        if (!ValidMove())
            transform.position -= new Vector3(-1, 0, 0);
    }

    public void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
        if (!ValidMove())
            transform.position -= new Vector3(1, 0, 0);
    }

    public void Rotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!ValidMove())
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }

    public void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);
        if (!ValidMove())
        {
            transform.position -= new Vector3(0, -1, 0);
            AddToGrid();
            CheckForLines();

            this.enabled = false;
            FindObjectOfType<SpawnTetrimino>().NewTetrimino();
        }

        // Hacker();

        previousTime = Time.time;
    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }

        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }

        return true;
    }

    void Hacker()
    {
       /* int hackWaitTime = Random.Range(0, hackTimer.Length);

        if (coolingHack == true)
        {
            hackTimer = null;
        }

        if (hacked == true)
        {
            fallTime = 0;
        } */
    }    
}
