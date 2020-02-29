using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    private PlayerHitDetector hitDetector;
    private PostProcessVolume ppv;

    public void StarEffect()
    {
        ChromaticAberration ca = ScriptableObject.CreateInstance<ChromaticAberration>();
        ca.enabled.Override(true);
        ca.intensity.Override(0.8f);
        ppv = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, ca);
    }

    public void FinishStar()
    {
        ChromaticAberration ca = ScriptableObject.CreateInstance<ChromaticAberration>();
        ca.enabled.Override(true);
        ca.intensity.Override(0);
        ppv = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, ca);
    }

    private void OnDestroy()
    {
        //RuntimeUtilities.DestroyVolume(ppv, true, true);
    }
}
