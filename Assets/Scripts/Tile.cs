using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public 	bool 	isSelected = false;
	public 	bool 	isChanging;
	public 	bool 	shouldDie = false;

	private Color fadeTransparency = new Color(255, 255, 255, 1f);
    private Color cacheColor = new Color(255, 255, 255, 1f);

    //public 	Sprite 	selectedSprite;
    //public 	Sprite 	simpleSprite;

    public GameObject selected;
    private GameObject selectedObj;

    private SpriteRenderer 		thisSprite;
	public 	GameManager 		gameScript;


	void OnMouseDown()
	{
		if (isChanging == false && gameScript.playersTurn)
		{
			isChanging = true;
			isSelected = true;
		}
	}

    private void Awake()
    {

        if(!transform.Find("Selected"))
        {
            selectedObj = GameObject.Instantiate(selected);
            selectedObj.name = "Selected";
            selectedObj.transform.SetParent(transform);
            selectedObj.transform.localPosition = new Vector3(0, 0, 1f);
            selectedObj.SetActive(false);
        }
        else
        {
            selectedObj = transform.Find("Selected").gameObject;
            selectedObj.SetActive(false);
        }
        
    }

    void Start()
	{
		isChanging = true;
        
        thisSprite = GetComponent<SpriteRenderer>();
        
		// gameScript = gameManObj.GetComponent<GameManager>();
	}



	void Update()
	{
        selectedObj.SetActive(isSelected);
        if (isSelected)
		{
			if (isChanging)
			{
				isChanging = false;
                //cacheColor = thisSprite.color;
                //thisSprite.color = fadeTransparency;
                //thisSprite.sprite = selectedSprite;
                

                gameScript.tilesClicked++;
			}
            
        }

		if (!isSelected)
		{
			if (isChanging)
			{
				isChanging = false;
                //thisSprite.color = cacheColor;
                //thisSprite.sprite = simpleSprite;
                
            }
            
        }

		if (shouldDie)
		{
			
			if (gameObject != null)
            {
               
                Destroy(gameObject);
            }
				

		}
		
	}
}
