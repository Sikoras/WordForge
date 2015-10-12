using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridScript : MonoBehaviour {


    public static int w = 10;
    public static int h = 13;
    public static GameObject[,] grid = new GameObject[w, h];
    public static GameObject scoreVisual;
    public static Text scoreText;
    public static int scoreOutput;
    public static bool valid;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static Vector2 roundVec(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    public static void checkTop()
    {
        for(var i = 0; i < 10; i++)
        {
            if(grid[i,11] != null)
            {
                valid = true;
                Application.LoadLevel(5);
            }
        }
    }

    public static int deleteTiles(int x, int y, int points)
    {
        if (grid[x, y] != null)
        {
            var DestroyObject = grid[x, y].transform.parent.gameObject;
            Destroy(grid[x, y]);
            Destroy(DestroyObject);
            grid[x, y] = null;
            scoreOutput = scoreOutput + points;
            return points;
        }
        return points;
    }

    public static void decreaseRows (int y)
    {
        if (y > 0)
        {
            for (int x = 0; x < w; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y - 1] == null)
                    {
                        grid[x, y - 1] = grid[x, y];
                        grid[x, y] = null;
                        grid[x, y - 1].transform.parent.gameObject.transform.position += new Vector3(0, -1, 0);
                    }
                }
            }
        }
    }

    public static void decreaseallRows()
    {
        for(int i = 0; i < h; i++)
        {
            decreaseRows(i);
        }
        checkTop();
        scoreText.text = scoreOutput.ToString();
    }
}
