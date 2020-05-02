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
    
    [SerializeField]
    private float _ellipseHorizontalRadii = 0.3f;
    [SerializeField]
    private float _ellipseVerticalRadii = 0.3f;
    [SerializeField]
    private float _ellipseShadingOffset = 0.1f;

    [SerializeField] private Vector2 _ellipsePosition;
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
        
        material.SetFloat("_ellipseHorizontalRadii", _ellipseHorizontalRadii);
        material.SetFloat("_ellipseVerticalRadii", _ellipseVerticalRadii);
        material.SetFloat("_ellipseShadingOffset", _ellipseShadingOffset);
        material.SetVector("_ellipsePosition", _ellipsePosition);

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
