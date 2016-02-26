using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
    public float speedX;
    public float speedY;

    float life;
    float fade;

    SpriteRenderer spriteRenderer;

    //WorldManager wm;
	// Use this for initialization
	void Start () {
        speedX *= Random.Range(0.8f, 1.2f);
        speedY *= Random.Range(-1.1f, 1.1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 15000;

        

        fade = 0;

        //wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();

        transform.position = Camera.main.transform.position + new Vector3(-Camera.main.orthographicSize * 2f, Random.Range(-Camera.main.orthographicSize / 2f, Camera.main.orthographicSize / 2f), 10);

        life = Camera.main.orthographicSize * 4f / speedX;

        Invoke("destroy", life);
    }
	
	// Update is called once per frame
	void Update () {
        
        fade += Time.deltaTime;

        if (fade < life - 1f) {
            spriteRenderer.color = new Color(1f, 1f, 1f, fade);
        }
        else {
            spriteRenderer.color = new Color(1f, 1f, 1f, life - fade);
        }
        transform.Translate(new Vector3(speedX, speedY, 0) * Time.deltaTime);
	}

    void destroy() {
        Destroy(gameObject);
    }
}
