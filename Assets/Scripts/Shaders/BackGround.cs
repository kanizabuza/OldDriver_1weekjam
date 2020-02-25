using UniRx;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;

    void Start()
    {
        Observable.EveryUpdate()
            .Subscribe(_ => {
                float y = Mathf.Repeat(Time.time * speed, 1);
                Vector2 offset = new Vector2(0, y);
                GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
            });
    }
}
