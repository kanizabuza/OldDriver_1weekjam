using UniRx;
using UnityEngine;

public class BackForKaiwa : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private bool isPlaying = true;
    private float preSpeed = 0;
    void Start()
    {
        preSpeed = speed;

        Observable.EveryUpdate()
            .Where(_ => isPlaying == true)
            .Subscribe(_ => {
                float y = Mathf.Repeat(Time.time * speed, 1);
                Vector2 offset = new Vector2(0, y);
                GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
            }).AddTo(this);
    }
}
