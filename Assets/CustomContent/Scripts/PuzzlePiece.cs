using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public RawImage uiImage;
    public Text pawnLabel;
    public int xPos;
    public int yPos;
    public int pawnNumber;
    public float size;
    public int primaryX = 0, primaryY = 0;
    public bool isOnRightPlace;

    private PuzzleBoard gameBoard;
    private PuzzlePiece currentPieceFromBoard; //ссылка на элемент на доске

	// Use this for initialization
	void Start ()
    {

        gameBoard = FindObjectOfType<PuzzleBoard>();

        size = (gameBoard.mainImgSquareSize / (gameBoard.boardDimension * gameBoard.boardDimension)) * gameBoard.piecesMargin;

        //пячим фишку в зависимости от количества элементов
        //uiImage.transform.localScale = new Vector3(board.piecesMargin / board.boardDimension, board.piecesMargin / board.boardDimension, board.piecesMargin / board.boardDimension);
        uiImage.rectTransform.sizeDelta = new Vector2(size ,
                                                      size);
        uiImage.uvRect = new Rect(xPos * size, yPos * size, 100 / size , 100/size );

        if (pawnNumber == 0)
        {
            uiImage.enabled = false;
        }

        foreach (PuzzlePiece item in gameBoard.pieces)
        {
            if (item.pawnNumber == this.pawnNumber)
            {
                currentPieceFromBoard = item;
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        uiImage.uvRect = new Rect(0.35f * primaryX, 0.35f * primaryY, 0.33f, 0.33f);

        transform.position = transform.parent.transform.position + new Vector3(xPos * size, yPos * size, 0);



        LabelHeartworkClockwork();

        if (xPos == primaryX && yPos == primaryY)
        {
            pawnLabel.color = Color.green;
        }
        else
        {
            pawnLabel.color = Color.red;
        }
	}

    public void UpdatePositions(int x, int y)
    {
        xPos = x;
        yPos = y;
    }

    public void Clickerton()
    {
        if (pawnNumber != 0)
        {

            if (xPos - 1 >= 0 && gameBoard.pieces[xPos - 1, yPos].pawnNumber == 0)
            {
                //Debug.Log("Zero on the left " + (xPos - 1).ToString());
                SwapTiles(xPos, yPos, xPos - 1, yPos);
            }
            else
            {
               // Debug.Log("NO Zero on the left " + (xPos - 1).ToString());


                if (xPos + 1 <= gameBoard.boardDimension - 1 && gameBoard.pieces[xPos + 1, yPos].pawnNumber == 0)
                {
                    //Debug.Log("Zero on the rigth " + (xPos + 1).ToString());
                    SwapTiles(xPos, yPos, xPos + 1, yPos);
                }
                else
                {
                    //Debug.Log("NO Zero on the right " + (xPos - 1).ToString());



                    if (yPos - 1 >= 0 && gameBoard.pieces[xPos, yPos - 1].pawnNumber == 0)
                    {
                       // Debug.Log("Zero on the down " + (yPos - 1).ToString());
                        SwapTiles(xPos, yPos, xPos, yPos - 1);
                    }
                    else
                    {
                        //Debug.Log("NO Zero on the down " + (yPos - 1).ToString());



                        if (yPos + 1 <= gameBoard.boardDimension - 1 && gameBoard.pieces[xPos, yPos + 1].pawnNumber == 0)
                        {
                            //Debug.Log("Zero on the Up " + (yPos + 1).ToString());
                            SwapTiles(xPos, yPos, xPos, yPos + 1);
                        }
                        else
                        {
                           // Debug.Log("NO Zero on the Up " + (yPos + 1).ToString());
                        }

                    }

                }


            }


        }

        


    }

    

    private void SwapTiles(int oldX, int oldY, int newX, int newY)
    {
        //1
        PuzzlePiece tempOld;
        int tempNX, tempNY;
        int tempOX, tempOY;

        //2
        tempOld = gameBoard.pieces[oldX, oldY];
        tempOX = oldX;
        tempOY = oldY;

        tempNX = newX;
        tempNY = newY;

        //3
        gameBoard.pieces[oldX, oldY] = gameBoard.pieces[newX, newY];

        //4
        gameBoard.pieces[newX, newY] = tempOld;

        //5
        gameBoard.pieces[oldX, oldY].xPos = tempOX;
        gameBoard.pieces[oldX, oldY].yPos = tempOY;

        gameBoard.pieces[newX, newY].xPos = tempNX;
        gameBoard.pieces[newX, newY].yPos = tempNY;


    }

    private void LabelHeartworkClockwork()
    {
        if (currentPieceFromBoard != null)
        {
            pawnLabel.text = currentPieceFromBoard.name.ToString() + " (" + currentPieceFromBoard.xPos.ToString() + ";" + currentPieceFromBoard.yPos.ToString() + ")";
        }
        
        
            
        
        
    }

    
}
