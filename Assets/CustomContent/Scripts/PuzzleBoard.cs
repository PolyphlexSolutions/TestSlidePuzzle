using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleBoard : MonoBehaviour
{
    //DOWNLOAD IMAGE
    public Texture downloadedImg;
    public bool isImageDownloaded = false;

    

    //BOARD PARAMETERS
    public GameObject canvas;
    public GameObject[] screens;
    public PuzzlePiece[,] pieces;
    public int[,] winPositions = { {0,1,2}, {3,4,5}, {6,7,8} };
    public int piecesOnPlaceCount = 0;
    public int boardDimension;
    public int mainImgSquareSize;
    public float piecesMargin;
    

    private bool isAssembled = false;
    public bool allowInteraction = true;


    public PuzzlePiece puzzlePiecePrefab;

	// Use this for initialization
	void Start ()
    {
       
        StartCoroutine(StartWait());
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        SetPiecesPositionalValues();
        CheckForWin();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintArrayOntoDebug();
            Debug.Log(piecesOnPlaceCount.ToString() + "pieces");
        }
		

	}

    private void SetPiecesPositionalValues()
    {



        if (isAssembled)
        {
            for (int x = 0; x < boardDimension; x++)
            {
                for (int y = 0; y < boardDimension; y++)
                {
                    pieces[x, y].UpdatePositions(x, y);
                }
            }
        }
        
    }
    

    private void CheckForWin()
    {
        if (isAssembled)
        {
            foreach (PuzzlePiece item in pieces)
            {
                if (!item.isOnRightPlace)
                {
                    break;
                }
                WinRoutine();
            }
        }
    }

    private void WinRoutine()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(true);
    }

    private void AssembleBoard()
    {
        if (isImageDownloaded)
        {
            pieces = new PuzzlePiece[boardDimension , boardDimension];
            
            float size = (mainImgSquareSize / (boardDimension * boardDimension)) * piecesMargin;

            int bar = 0;

            int tempX = 0, tempY = 0;

            for (int y = 0; y < boardDimension; y++)
            {
                tempY = y;



                for (int x = 0; x < boardDimension; x++)
                {
                    tempX = x;
                    PuzzlePiece foo = Instantiate(puzzlePiecePrefab);

                    foo.transform.parent = canvas.transform;

                    foo.pawnNumber = bar;
                    foo.gameObject.name = "Piece " + bar.ToString();
                    bar++;

                    foo.primaryX = tempX;
                    foo.primaryY = tempY;

                    
                    foo.uiImage.texture = downloadedImg;
                    

                    pieces[tempX, tempY] = foo;

                    foo = null;
                    

                }


            }

            

            

            System.Random randy = new System.Random();

            MixArray(randy, pieces);
        }
        
    }

    private void MixArray(System.Random random, PuzzlePiece[,] array)
    {
        

        for (int i = array.Length - 1; i > 0; i--)
        {
            int lengthRow = array.GetLength(1);

            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = random.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            PuzzlePiece temp = array[i0, i1];

            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;
        }
        isAssembled = true;

    }

    

    private IEnumerator StartWait()
    {
        if (!isImageDownloaded)
        {
            yield return new WaitForSeconds(0.01f);
            //Debug.Log("Wait iteration with NULL");
            StartCoroutine(StartWait());
        }
        else
        {
            AssembleBoard();
            yield return null;
        }

    }

    private void PrintArrayOntoDebug()
    {
        for (int x = 3; x > 0; x--)
        {
            Debug.Log(pieces[0,x - 1].pawnNumber.ToString() + " " + pieces[1, x - 1].pawnNumber.ToString() + " " + pieces[2, x - 1].pawnNumber.ToString() + "\n");
            
        }
    }
}
