using System;
using UnityEngine;

public class SignAssigner : MonoBehaviour
{
    private FirebaseRemoteConfigScript firebaseScript;
    public string AIPlayerMark;
    public string HUmanPlayerMark;
    public Marks AiPlayerMark;
    public Marks HumanPlayerMark;

    private void Awake()
    {
        
    }

    private void Start()
    {
        firebaseScript = FindObjectOfType<FirebaseRemoteConfigScript>();
        firebaseScript.OnRemoteConfigFetchComplete += AssignSigns;
    }

    public void AssignSigns(object sender, EventArgs e)
    {
        Debug.Log("Assigning Signs");
        var configData = firebaseScript.GetConfigData();
        AIPlayerMark =  configData["AiPlayerSign"].StringValue;
        HUmanPlayerMark = configData["HumanPlayerSign"].StringValue;
        GetSign();
    }

    public void GetSign()
    {
        if (AIPlayerMark == "x")
        {
            AiPlayerMark = Marks.x;
            HumanPlayerMark = Marks.o;
        }
        else
        {
            HumanPlayerMark = Marks.x;
            AiPlayerMark = Marks.o;
        }

        Debug.Log("AiplayerMark : " + AiPlayerMark);
    }
}
