using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq;


public class SPawner : MonoBehaviour {

    public GameObject[] items;
    public List<string> wordList = new List<string>();
    public List<wordOutput> wordsListed;
    public List<string> wordConfirm = new List<string>();

    [SerializeField]
    private char[] alphabetArray;

    [SerializeField]
    private int[] pointsArray;

    [SerializeField]
    private float[] alphabetWeights;

    private XmlDocument xmlDoc;
    [SerializeField]
    private TextAsset XMLInput;
    [SerializeField]
    private Sprite[] alphabetSprites;

    [SerializeField]
    private GameObject NextLetter;

    [SerializeField]
    private GameObject BonusWord;

    public int nextLetter;

    private WordContainer wordCollection;

    [SerializeField]
    private GameObject scoreOutput;

    [SerializeField]
    private GameObject levelOutput;


    private int level = 1;

    // Use this for initialization
    void Start () {
        wordsListed = new List<wordOutput>();
        firstSpawn();
        GridScript.scoreVisual = scoreOutput;
        GridScript.scoreText = scoreOutput.GetComponent<Text>();
        levelOutput.GetComponent<Text>().text = level.ToString();
        wordCollection = WordContainer.Load(Path.Combine(Application.dataPath, "Resources/WordList.xml"));
        newWord();


    }


    // Update is called once per frame
    void Update()
    {

    }

    void newWord()
    {
        int random = Random.Range(1, 276747);
        var word = wordCollection.Words[random];
        if(word.Length > 10)
        {
            newWord();
        }
        else
        {
            setnewWord(word);
        }
        
    }

    void setnewWord(string word)
    {
        BonusWord.GetComponent<Text>().text = word;
    }

    public void firstSpawn()
    {
        int j = 0;

        

        var createdItem = Instantiate(items[j], transform.position, Quaternion.identity) as GameObject;

        var index = randomiseLetter();

        createdItem.GetComponent<createdScript>().assignedChar = alphabetArray[index];
        createdItem.GetComponent<createdScript>().points = pointsArray[index];
        createdItem.GetComponent<GroupScript>().SpawnerGO = gameObject;
        createdItem.GetComponent<GroupScript>().level = level;
        createdItem.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = alphabetSprites[index];

        nextLetter = randomiseLetter();
        NextLetter.GetComponent<Text>().text = alphabetArray[nextLetter].ToString();
    }

    public int randomiseLetter()
    {
        float TotalFreq = 0;

        for (int i = 0; i < alphabetArray.Length; i++)
        {
            TotalFreq += alphabetWeights[i];
        }
        var roll = Random.Range(0, TotalFreq);

        var index = -1;

        for (int i = 0; i < alphabetArray.Length; i++)
        {
            if (roll <= alphabetWeights[i])
            {
                index = i;
                break;
            }

            roll -= alphabetWeights[i];
        }

        if (index == -1) index = alphabetWeights.Length - 1;

        return index;
    }

    public void newSpawn()
    {
        int j = 0;

        for (int i = 0; i < 2; i++)
        {
            var newV3 = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);
            var createdItem = Instantiate(items[j], newV3, Quaternion.identity) as GameObject;

            var index = nextLetter;
            levelOutput.GetComponent<Text>().text = level.ToString();

            createdItem.GetComponent<createdScript>().assignedChar = alphabetArray[index];
            createdItem.GetComponent<GroupScript>().SpawnerGO = gameObject;
            createdItem.GetComponent<GroupScript>().level = level;
            createdItem.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = alphabetSprites[index];
            createdItem.GetComponent<createdScript>().points = pointsArray[index];
            nextLetter = randomiseLetter();
            NextLetter.GetComponent<Text>().text = alphabetArray[nextLetter].ToString();
        }
        
    }

    public void wordCheck()
    {
        var countList = new List<int>();
        for(var i = 0; i < wordList.Count; i++)
        {
            var item = wordList[i];
            var found = wordCollection.Words.FirstOrDefault(t => t == item);
            
            if (found == null) continue;
            if (found.Length < 3) continue;
            wordsListed[i].isValid = true;
        }
        
        wordDelete();
    }

    public void wordDelete()
    {
        Debug.Log(wordsListed.Count);
        for(int i = 0; i < wordsListed.Count; i++)
        {
            if (wordsListed[i].isValid)
            {
                /*var list = wordsListed[i].ListGO;
                int count = list.Count;
                for (int j = 0; j < count; j++)
                {

                    int x = (int)list[j].transform.position.x;
                    int y = (int)list[j].transform.position.y;
                    GridScript.deleteTiles(x, y);
                }*/
                    var wordlength = wordsListed[i].word.Length;
                for (var j = 0; j < wordlength; j++)
                {
                    if (wordsListed[i].isHorizontal)
                    {
                        var x = wordsListed[i].xcoordstart + j;
                        var y = wordsListed[i].ycoordstart;
                        if (GridScript.grid[x, y] != null)
                        {
                            var pointsoutput = GridScript.grid[x, y].transform.parent.gameObject.GetComponent<createdScript>().points;
                            var scorecounter =+ GridScript.deleteTiles(x, y, pointsoutput);

                            if (scorecounter >= 25)
                            {
                                scorecounter = scorecounter - 25;
                                level = level + 1;
                            }
                        }
                    }
                    else
                    {
                        var x = wordsListed[i].xcoordstart;
                        var y = wordsListed[i].ycoordstart -j;
                        if (GridScript.grid[x, y] != null)
                        {
                            var pointsoutput = GridScript.grid[x, y].transform.parent.gameObject.GetComponent<createdScript>().points;
                            var scorecounter = +GridScript.deleteTiles(x, y, pointsoutput);

                            if (scorecounter >= 25)
                            {
                                scorecounter = scorecounter - 25;
                                level = level + 1;
                            }
                        }
                    }

                }

            }
        }
        GridScript.decreaseallRows();
        wordList.Clear();
        wordsListed.Clear();
        wordConfirm.Clear();
        if (GridScript.valid == false)
        {
            newSpawn();
        }
    }



}
