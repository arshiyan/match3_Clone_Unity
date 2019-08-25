using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundBrick : MonoBehaviour
{
	private string 		tileName;
	private GameObject 	tile;

	public 	GameObject 	GetTileInside()
	{
		return tile;
	}

	public	string 	GetTileNameInside()
	{
		return tileName;
	}

	public 	bool 	SomethingInside()
	{
		if (tileName == "None")
			return false;
		return true;
	}

	private void 	OnTriggerStay2D(Collider2D other)
	{
		tileName = other.gameObject.tag;
		tile = other.gameObject;
	}

	private void	OnTriggerExit2D(Collider2D other)
	{
		tileName = "None";
	}

	void	Awake()
	{
		tileName = "None";
	}    

    // Update is called once per frame
    void Update()
    {
        
    }
}
