using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScript : MonoBehaviour{

    private float scrollSpeed = 0.1f;
    private MeshRenderer meshRender;
    private Vector2 savedOffset;

    // Start is called before the first frame update
    void Start(){
        Time.timeScale = 1;
    }

    private void Awake(){
        meshRender = GetComponent<MeshRenderer>();
        savedOffset = meshRender.sharedMaterial.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update(){
        float x = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(x, 0);
        meshRender.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    private void OnDisable(){
        meshRender.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
