using UnityEngine;

public class TextureToRT : MonoBehaviour
{
    public Texture2D mainTex;

    public Texture2D tex1;
    public Vector2 offset1 = new Vector2(506, 563);
    public Vector3 rotScale1 = new Vector3(0, 1, 1);
    [Range(0, 1)]
    public float hue1 = 0.5f;
    [Range(0, 1)]
    public float saturate1 = 0.5f;
    [Range(0, 1)]
    public float heavy1 = 0.5f;

    public Texture2D tex2;
    public Vector2 offset2 = new Vector2(506, 620);
    public Vector3 rotScale2 = new Vector3(0, 1, 1);

    [Range(0, 1)]
    public float hue2 = 0.5f;
    [Range(0, 1)]
    public float saturate2 = 0.5f;
    [Range(0, 1)]
    public float heavy2 = 0.5f;

    public Texture2D tex3;
    public Vector2 offset3 = new Vector2(510.2f, 363.4f);
    public Vector3 rotScale3 = new Vector3(0, 1, 1);
    [Range(0, 1)]
    public float hue3 = 0.5f;
    [Range(0, 1)]
    public float saturate3 = 0.5f;
    [Range(0, 1)]
    public float heavy3 = 0.5f;
    
    public Texture2D tex4;
    public Vector2 offset4 = new Vector2(510.2f, 363.4f);
    public Vector3 rotScale4 = new Vector3(0, 1, 1);
    [Range(0, 1)]
    public float hue4 = 0.5f;
    [Range(0, 1)]
    public float saturate4 = 0.5f;
    [Range(0, 1)]
    public float heavy4 = 0.5f;
    
    public Texture2D tex5;
    public Vector2 offset5 = new Vector2(510.2f, 363.4f);
    public Vector3 rotScale5 = new Vector3(0, 1, 1);
    [Range(0, 1)]
    public float hue5 = 0.5f;
    [Range(0, 1)]
    public float saturate5 = 0.5f;
    [Range(0, 1)]
    public float heavy5 = 0.5f;
    public RenderTexture mainRt;
    public Material mat;
    public Material outputMat;

    void Start()
    {
        if (mainRt == null)
        {
            mainRt = new RenderTexture(mainTex.width, mainTex.height, 0, 0, RenderTextureReadWrite.Linear)
            {
                name = "_FaceTex",
                hideFlags = HideFlags.DontSave,
                filterMode = mainTex.filterMode,
                wrapMode = mainTex.wrapMode,
                anisoLevel = 0,
                autoGenerateMips = false,
                useMipMap = false
            };
            mainRt.Create();
        }
        mat = new Material(Shader.Find("Custom/FaceMakeup"));
    }

    void Update()
    {
        Shader.SetGlobalTexture("_Part1_Tex", tex1);
        Shader.SetGlobalVector("_Part1_Offset", offset1);
        Shader.SetGlobalVector("_Part1_RotScale", rotScale1);
        Shader.SetGlobalVector("_Part1_HSB", new Vector3(hue1, saturate1, heavy1));

        Shader.SetGlobalTexture("_Part2_Tex", tex2);
        Shader.SetGlobalVector("_Part2_Offset", offset2);
        Shader.SetGlobalVector("_Part2_RotScale", rotScale2);
        Shader.SetGlobalVector("_Part2_HSB", new Vector3(hue2, saturate2, heavy2));

        Shader.SetGlobalTexture("_Part3_Tex", tex3);
        Shader.SetGlobalVector("_Part3_Offset", offset3);
        Shader.SetGlobalVector("_Part3_RotScale", rotScale3);
        Shader.SetGlobalVector("_Part3_HSB", new Vector3(hue3, saturate3, heavy3));

        Shader.SetGlobalTexture("_Part4_Tex", tex4);
        Shader.SetGlobalVector("_Part4_Offset", offset4);
        Shader.SetGlobalVector("_Part4_RotScale", rotScale4);
        Shader.SetGlobalVector("_Part4_HSB", new Vector3(hue4, saturate4, heavy4));

        Shader.SetGlobalTexture("_Part5_Tex", tex5);
        Shader.SetGlobalVector("_Part5_Offset", offset5);
        Shader.SetGlobalVector("_Part5_RotScale", rotScale5);
        Shader.SetGlobalVector("_Part5_HSB", new Vector3(hue5, saturate5, heavy5));

        Graphics.Blit(mainTex, mainRt, mat);
        outputMat.SetTexture("_MainTex", mainRt);
    }

}
