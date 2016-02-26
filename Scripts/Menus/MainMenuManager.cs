using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    //AsyncOperation async;
    string levelName = "Game";
    public AudioClip startJingleClip;
    float volume = 1f;
    bool stopping = false;

    void Start() {
        DontDestroyOnLoad(gameObject);
        volume = GetComponent<AudioSource>().volume / 2f;
    }

    void Update() {
        /*if (async != null && async.isDone) {
            activateScene();
        }*/
        if (stopping && volume > 0) {
            GetComponent<AudioSource>().volume = volume;
            volume -= 0.03f;
        }
        else if (stopping) {
            beginLoading();
        }
    }

    public void startGame() {
        stopping = true;
        GetComponent<AudioSource>().volume = volume;
        //GetComponent<AudioSource>().Stop();
        Application.LoadLevel("Loading");
        //GetComponent<AudioSource>().Stop();
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = 1f;
        source.loop = false;
        source.clip = startJingleClip;
        source.Play();
        
        //Invoke("beginLoading", 0.3f);
    }

    void beginLoading() {
        //StartCoroutine("load");
        Application.LoadLevel(levelName);
        activateScene();
    }

    /*IEnumerator load() {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        
        async = Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }*/

    public void activateScene() {
        GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
    }
}
