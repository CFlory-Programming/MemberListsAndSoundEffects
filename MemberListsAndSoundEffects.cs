using System.Collections;
using System.Collections.Generic;
using EasyElements;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class LightMeUpML : MonoBehaviour
{
    static public Color highlight = Color.white;
    Material mat;
    Renderer rend;
    static Color storedColor;
    static Color startingColor;
    static public bool resetting;
    static ColorChangerML colorChanger;
    static List<LightMeUpML> members = new List<LightMeUpML>();
    static BallPushML ball;

    private void Awake()
    {
        members.Add(this);
        Renderer r = members[0].gameObject.GetComponent<Renderer>();
        startingColor = r.material.color;
        colorChanger = FindAnyObjectByType<ColorChangerML>();
        ball = FindAnyObjectByType<BallPushML>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        mat.SetColor("_EmissionColor", Color.black);
        rend.material= mat;
    }
static bool Complete()
{
    Renderer firstCubeRenderer = members[0].gameObject.GetComponent<Renderer>();
    Color storedColor = firstCubeRenderer.material.color;
    foreach (LightMeUpML cube in members)
    {
        Renderer eachCubeRenderer = cube.gameObject.GetComponent<Renderer>();
        if (eachCubeRenderer.material.color != storedColor)
        {
            return false;
        }
    }
        
        return true;
    }

    private void playSound()
    {
        ball.PlaySound(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        rend.material.color = highlight;
        if (Complete() && resetting == false)
        {
            resetting = true;
            playSound();
            Invoke(nameof(CallReset), 5);
        }
        //mat.SetColor("_EmissionColor", highlight);
        //rend.material = mat;
    }
    void CallReset()
    {
        Reset();
    }
    static void Reset()
    {
        foreach (LightMeUpML cube in members)
        {
            Renderer eachCubeRenderer = cube.gameObject.GetComponent<Renderer>();
            eachCubeRenderer.material.color = startingColor;
        }
        resetting = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        //mat.SetColor("_EmissionColor", Color.black);
        //rend.material = mat;
    }
}