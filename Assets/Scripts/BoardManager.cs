using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public int Width = 4;
    public int Height = 4;
    public Point PointPrefab;
    Point[,] puntos;
    bool[,] llenos;
    bool hizopunto = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Width = PlayerPrefs.GetInt("size");
        Height = PlayerPrefs.GetInt("size");
        puntos = new Point[Height, Width];
        llenos = new bool[Height-1, Width-1];
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                var p = new Vector2(i, j);
                Point instancia = Instantiate(PointPrefab, p, Quaternion.identity);
                puntos[i, j] = instancia;
            }
        }
        var center = new Vector2((float)Height / 2 - 0.5f, (float)Width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    }

    public void SetPoint(Point p)
    {
        if (GameManager.Instance.clicked)
        {
            GameManager.Instance.SwitchPlayer();
        }
        GameManager.Instance.updateClick();
    }
    public void calculate(int i, int j)
    {
        if (puntos[i, j].arriba)
        {
            if (puntos[i, j].izquierda)
            {
                if (i > 0 && j < Height-1)
                {
                    if (puntos[i - 1, j + 1].abajo)
                    {
                        if (puntos[i - 1, j + 1].derecha)
                        {
                            if (!llenos[i-1, j])
                            {
                                llenos[i-1, j] = true;
                                GameManager.Instance.addScore(i - 1, j);
                                hizopunto = true;
                            }
                        }
                    }
                }
            }
            if (puntos[i, j].derecha)
            {
                if (i < Width - 1 && j < Height -1)
                {
                    if (puntos[i + 1, j + 1].abajo)
                    {
                        if (puntos[i + 1, j + 1].izquierda)
                        {
                            if (!llenos[i, j])
                            {
                                llenos[i, j] = true;
                                GameManager.Instance.addScore(i, j);
                                hizopunto = true;
                            }
                        }
                    }
                }
            }
        }
        if (puntos[i, j].abajo)
        {
            if (puntos[i, j].izquierda)
            {
                if (i > 0 && j > 0)
                {
                    if (puntos[i - 1, j - 1].arriba)
                    {
                        if (puntos[i - 1, j - 1].derecha)
                        {
                            if (!llenos[i-1, j-1])
                            {
                                llenos[i-1, j-1] = true;
                                GameManager.Instance.addScore(i - 1, j - 1);
                                hizopunto = true;
                            }
                        }
                    }
                }
            }
            if (puntos[i, j].derecha)
            {
                if (i < Width-1 && j >0)
                {
                    if (puntos[i + 1, j - 1].arriba)
                    {
                        if (puntos[i + 1, j - 1].izquierda)
                        {
                            if (!llenos[i, j-1])
                            {
                                llenos[i, j-1] = true;
                                GameManager.Instance.addScore(i, j - 1);
                                hizopunto = true;
                            }
                        }
                    }
                }
            }
        }
        if (hizopunto)
        {
            GameManager.Instance.SwitchPlayer();
            hizopunto = false;
        }
    }
    public void enlace(int i, int j, string direccion)
    {
        if (direccion == "arriba")
        {
            puntos[i, j].arriba = true;
        }
        if (direccion == "abajo")
        {
            puntos[i, j].abajo = true;
        }
        if (direccion == "izquierda")
        {
            puntos[i, j].izquierda = true;
        }
        if (direccion == "derecha")
        {
            puntos[i, j].derecha = true;
        }
    }
}
