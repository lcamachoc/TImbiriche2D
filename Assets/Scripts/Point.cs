using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject Inner;
    private Vector2 _position;
    private bool renderline;
    [SerializeField] private Camera mainCamera;
    Color linecolor;
    public bool arriba;
    public bool abajo;
    public bool izquierda;
    public bool derecha;
    public Vector2 Pos => _position;
    private void Start()
    {
        mainCamera = Camera.main;

    }
    public void Init(Vector2 position)
    {
        this._position = position;
    }
    public void Update()
    {
        if (renderline)
        {
            if (!GameManager.Instance.clicked)
            {
                Vector3 mouseworld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseworld.z = 0f;
                if ((Mathf.Abs(this.transform.position.x - mouseworld.x) < 1.1f && Mathf.Abs(this.transform.position.x - mouseworld.x) > 0.8f) ^ (Mathf.Abs(this.transform.position.y - mouseworld.y) < 1.1f && Mathf.Abs(this.transform.position.y - mouseworld.y) > 0.8f))
                {
                    if (this.transform.position.y - mouseworld.y > 0.8f)
                    {
                        if (!abajo)
                        {
                            DrawLine(this.transform.position, mouseworld, linecolor);
                            renderline = false;
                            Inner.GetComponent<SpriteRenderer>().color = Color.white;
                            abajo = true;
                            BoardManager.Instance.enlace((int)(this.transform.position.x), (int)(this.transform.position.y - 1f), "arriba");
                            BoardManager.Instance.calculate((int)this.transform.position.x, (int)this.transform.position.y);
                        }
                        else
                        {
                            GameManager.Instance.SwitchPlayer();
                        }
                    }
                    if (this.transform.position.y - mouseworld.y < -0.8f)
                    {
                        if (!arriba)
                        {
                            DrawLine(this.transform.position, mouseworld, linecolor);
                            renderline = false;
                            Inner.GetComponent<SpriteRenderer>().color = Color.white;
                            arriba = true;
                            BoardManager.Instance.enlace((int)(this.transform.position.x), (int)(this.transform.position.y + 1f), "abajo");
                            BoardManager.Instance.calculate((int)this.transform.position.x, (int)this.transform.position.y);
                        }
                        else
                        {
                            GameManager.Instance.SwitchPlayer();
                        }
                    }
                    if (this.transform.position.x - mouseworld.x < -0.8f)
                    {
                        if (!derecha)
                        {
                            DrawLine(this.transform.position, mouseworld, linecolor);
                            renderline = false;
                            Inner.GetComponent<SpriteRenderer>().color = Color.white;
                            derecha = true;
                            BoardManager.Instance.enlace((int)(this.transform.position.x + 1f), (int)(this.transform.position.y), "izquierda");
                            BoardManager.Instance.calculate((int)this.transform.position.x, (int)this.transform.position.y);
                        }
                        else
                        {
                            GameManager.Instance.SwitchPlayer();
                        }
                    }
                    if (this.transform.position.x - mouseworld.x > 0.8f)
                    {
                        if (!izquierda)
                        {
                            DrawLine(this.transform.position, mouseworld, linecolor);
                            renderline = false;
                            Inner.GetComponent<SpriteRenderer>().color = Color.white;
                            izquierda = true;
                            BoardManager.Instance.enlace((int)(this.transform.position.x - 1f), (int)(this.transform.position.y), "derecha");
                            BoardManager.Instance.calculate((int)this.transform.position.x, (int)this.transform.position.y);
                        }
                        else
                        {
                            GameManager.Instance.SwitchPlayer();
                        }
                    }
                }
                else
                {
                    GameManager.Instance.SwitchPlayer();
                    GameManager.Instance.updateClick();
                }

            }
        }
    }
    private void OnMouseDown()
    {


        if (GameManager.Instance.GetGameState == GameManager.GameState.player1)
        {
            Inner.GetComponent<SpriteRenderer>().color = Color.blue;
            linecolor = Color.blue;
        }

        else
        {
            Inner.GetComponent<SpriteRenderer>().color = Color.red;
            linecolor = Color.red;
        }
        if (!GameManager.Instance.clicked)
        {
            renderline = true;
        }
        else
        {
            Inner.GetComponent<SpriteRenderer>().color = Color.white;
        }
        BoardManager.Instance.SetPoint(this);
    }
    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material.color = color;
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
