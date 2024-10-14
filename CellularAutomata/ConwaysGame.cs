using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class ConwaysGame : MonoBehaviour
{
    public ComputeShader shader;
    [Space]
    public RawImage renderImage;
    public int textureSize = 128;
    public int blurSize = 1;

    private RenderTexture resultTexture;
    private bool isRuning = false;

    private void Start()
    {
        resultTexture = GenerateImage();
        StartCoroutine(RunConwayGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isRuning = !isRuning;
    }

    public void GenearatePerlin()
    {
        resultTexture = GenerateImage();

        // Genero el perlin
        shader.SetTexture(0, "ResultPerlin", resultTexture);
        shader.SetInt("randSeed", Random.Range(0, 99999));

        int threadGroupsX = Mathf.CeilToInt(resultTexture.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(resultTexture.height / 8.0f);
        shader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        renderImage.texture = resultTexture;
    }

    [ContextMenu("Blur")]
    public void BlurPerlin()
    {
        int kernelIndex = shader.FindKernel("Blur");
        RenderTexture blurTexture = GenerateImage();

        shader.SetTexture(kernelIndex, "SampleConway", resultTexture);
        shader.SetTexture(kernelIndex, "ResultBlur", blurTexture);
        shader.SetInt("blurSize", blurSize);

        int threadGroupsX = Mathf.CeilToInt(resultTexture.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(resultTexture.height / 8.0f);
        shader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);

        renderImage.texture = blurTexture;
    }

    [ContextMenu("Next state")]
    public void NextState()
    {
        // Genero el perlin
        shader.SetTexture(1, "ResultConway", resultTexture);

        int threadGroupsX = Mathf.CeilToInt(resultTexture.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(resultTexture.height / 8.0f);
        shader.Dispatch(1, threadGroupsX, threadGroupsY, 1);
    }

    RenderTexture GenerateImage()
    {
        RenderTexture finalTexture = null;
        finalTexture = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.ARGB32);
        finalTexture.filterMode = FilterMode.Point;
        finalTexture.enableRandomWrite = true;
        finalTexture.Create();

        return finalTexture;
    }

    IEnumerator RunConwayGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (isRuning)
            {
                NextState();
                BlurPerlin();
            }
        }
    }
}

[CustomEditor(typeof(ConwaysGame))]
public class ConwaysGameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ConwaysGame conwaysGame = (ConwaysGame)target;
        if (GUILayout.Button("ResetGame"))
        {
            conwaysGame.GenearatePerlin();
        }

        if (GUILayout.Button("Next State"))
        {
            conwaysGame.NextState();
            conwaysGame.BlurPerlin();
        }

        DrawDefaultInspector();
    }
}