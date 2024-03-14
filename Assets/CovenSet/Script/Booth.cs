using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booth : MonoBehaviour
{
    public Renderer boardRenderer;
    public Texture boardImg;

    Material boardMaterial;

    // Start is called before the first frame update
    void Start()
    {
        boardMaterial = boardRenderer.material;

        SetBoardImage(boardImg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoardImage(Texture img)
    {      
        boardMaterial.SetTexture("_BaseMap", img);
    }
}
