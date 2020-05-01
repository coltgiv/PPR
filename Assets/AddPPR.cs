using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPPR : MonoBehaviour
{
    private Material material;
    private Resolution[] resolutions;
    public float a = 1.4f;
    [Range(0.0f, 1.0f)]
    public float start = 0.3f;
    [Range(0.0f, 1.0f)]
    public float end = 0.7f;
    [Range(0.0f, 1.0f)]
    public float xShad = 0.05f;
    [Range(0.0f, 1.0f)]
    public float yShad = 0.1f;

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/PPRLab"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_ParamA", a);
        material.SetFloat("_Start", start);
        material.SetFloat("_End", end);
        material.SetFloat("_XShad", xShad);
        material.SetFloat("_YShad", yShad);
        Graphics.Blit(source, destination, material);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
