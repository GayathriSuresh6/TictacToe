using Firebase.RemoteConfig;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Extensions;
using System.Collections.Generic;
using System.Linq;


public interface IRemoteConfig 
{
    public Dictionary<string, ConfigValue> GetConfigData();
}

public class FirebaseRemoteConfigScript : MonoBehaviour, IRemoteConfig
{
    private FirebaseRemoteConfig firebaseInstance;
    public event EventHandler OnRemoteConfigFetchComplete;
    private SignAssigner signAssigner;
    public bool isDataLoaded;

    

    private void Awake()
    {
        firebaseInstance = FirebaseRemoteConfig.DefaultInstance;
        signAssigner = FindObjectOfType<SignAssigner>();
        CheckRemoteConfigValues();
       
    }

    public Task CheckRemoteConfigValues()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = firebaseInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task => {
                //CallData();
                OnRemoteConfigFetchComplete?.Invoke(this, EventArgs.Empty);
            });
       
               
    }

    /*private void CallData()
    {
        Debug.Log("Inside call Data");
        signAssigner.AssignSigns();
    }*/

    public Dictionary<string, ConfigValue> GetConfigData()
    {
        Debug.Log("getting config data");
        var fetchedData = (Dictionary<string, ConfigValue>)firebaseInstance.AllValues;
        return fetchedData;
    }

 /*   private void Update()
    {
        if (isDataLoaded)
        {
            CallData();
            isDataLoaded = false;
        }
    }*/
}
