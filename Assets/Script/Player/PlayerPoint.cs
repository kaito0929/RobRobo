using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public HeadPartsMaterialChange headPartsMaterialChange;
    public BodyPartsMaterialChange bodyPartsMaterialChange;
    public ArmPartsMaterialChange armPartsMaterialChange;
    public LegPartsMaterialChange legPartsMaterialChange;

    public int myPoint;
    public int PlayerRank;

    // Use this for initialization
    void Start ()
    {
        PlayerRank = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        myPoint = headPartsMaterialChange.HeadPartsPoint + bodyPartsMaterialChange.BodyPartsPoint +
                  armPartsMaterialChange.ArmPartsPoint + legPartsMaterialChange.LegPartsPoint;
	}
}
