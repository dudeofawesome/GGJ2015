using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour
{
	public enum MenuState
	{
		PROGRESSBAR,
		CLOCK,
		DIRECTIONALHIT
	};

	//clock var
	//is elapsed time in sec with ex:3.35
	public float elapsedTime = 0.0f;
	public GUIStyle customGuiStyle;

	//progress bar
	//SET float percentProgress from 0.0f to 1.0f
	public float percentProgress = 0.0f;
	public float progressBarX = 0.0f;
	public float progressBarY = 0.0f;
	public float progressBarWidth = 75.0f;
	public float progressBarHeight = 10.0f;

	//direction hit var
	//CALL fcn StartDirectionHit(3.0f, 45); to show arrow for 3 sec at 45degrees
	public float arrowWidth;
	public float arrowHeight;
	public Texture arrowTex;
    public float arrowAngle = 45;
    private Vector2 pivotPoint;
    private float pastElapsedTime = 0.0f;
    private float timeArrowIsDisplayed = 3.0f;
    private float arrowAlpha = 1;

	//public MenuState menuPosition = MenuState.PROGRESSBAR;

	// Use this for initialization
	void Start ()
	{
		StartDirectionHit(1.5f, 90);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if(percentProgress < 1f)
		//	percentProgress+=0.001f;

		elapsedTime = Time.fixedTime;
	}

	void DrawQuad(Rect position, Color color)
	{
     Texture2D texture = new Texture2D(1, 1);
     texture.SetPixel(0,0,color);
     texture.Apply();
     GUI.skin.box.normal.background = texture;
     GUI.Box(position, GUIContent.none);
 	}

 	void DrawClock(int x, int y, int width, int height)
 	{	
		GUI.Box (new Rect (), (elapsedTime.ToString("#.00")), customGuiStyle);
 	}

 	void DrawProgressBar()
 	{
		int edgeSize = 10;
		DrawQuad(new Rect(progressBarX-edgeSize/2, progressBarY-edgeSize/2, progressBarWidth+edgeSize, progressBarHeight+edgeSize), Color.white);
		DrawQuad(new Rect(progressBarX, progressBarY, percentProgress * progressBarWidth, progressBarHeight), Color.green);
 	}

 	public void StartDirectionHit(float tempTimeToDisplay, float angle)
 	{
 		arrowAngle = angle;
 		timeArrowIsDisplayed = tempTimeToDisplay;
 		pastElapsedTime = elapsedTime;
 	}

 	void DrawDirectionHit(float angle)
 	{
 		if(elapsedTime < pastElapsedTime + timeArrowIsDisplayed)
 		{
 			arrowAlpha  = 1.0f - ( (elapsedTime - pastElapsedTime) / timeArrowIsDisplayed );
 			arrowWidth += Random.Range(-1.0F, 1.0F);
			arrowHeight += Random.Range(-1.0F, 1.0F);
 			float arrowX = Screen.width/2 - arrowWidth/2;
 			float arrowY = Screen.height/2 - arrowHeight/2;
			pivotPoint = new Vector2(arrowX + arrowWidth/2, arrowY + arrowHeight/2);
			GUIUtility.RotateAroundPivot(angle, pivotPoint);
 			GUI.color = new Color(1.0f, 1.0f, 1.0f, arrowAlpha);
			GUI.DrawTexture(new Rect(arrowX, arrowY, arrowWidth, arrowHeight), arrowTex);
 		}
 		else
 		{
			GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
 		}
 	}

	void OnGUI ()
	{
		//clock
		DrawClock(780, 0, 50, 25);

 		//progress bar
		DrawProgressBar();

		//directional hit
		DrawDirectionHit(arrowAngle);
	}
}
