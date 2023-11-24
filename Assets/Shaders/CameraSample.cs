using UnityEngine;

[ExecuteInEditMode]
public class CVDAccessiblitySample : MonoBehaviour
{

    public enum CVDType {
        None = 0,
        Proto,
        Deutero,
        Trito
    }

    public CVDType type = CVDType.None;

    [Range(0, 1)]
    public float intensity;
    private Material material;

    void Awake()
    {
        material = new Material(Shader.Find("Custom/CVD"));
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if (intensity == 0 || type == 0)
        {
            Graphics.Blit(src, dest);
            return;
        }

        material.SetInt("_Mode", (int)type);

        material.SetFloat("_Blend", intensity);
        Graphics.Blit(src, dest, material);
    }
}
