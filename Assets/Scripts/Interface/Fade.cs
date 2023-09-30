using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade Instance { get; private set; }
    public Animator anim;
    public Image imageMask;
    public float duration;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Destroy(gameObject);
            return;
        }

        imageMask.raycastTarget = false;
    }

    public void PlayFadeIn()
    {
        imageMask.raycastTarget = false;
        anim.Play("FadeIn");
    }

    public void PlayFadeOut()
    {
        imageMask.raycastTarget = true;
        anim.Play("FadeOut");
    }
}
