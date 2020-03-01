using UniRx;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Texture tex;
    private GameStateManager stateManager;
    private PlayerHitDetector hitDetector;
    private bool isPlaying = true;
    private float preSpeed = 0;
    void Start()
    {
        stateManager = GameObject.Find("Manager").GetComponent<GameStateManager>();
        hitDetector = GameObject.Find("Player").GetComponent<PlayerHitDetector>();
        preSpeed = speed;

        Texture tex = GetComponent<Texture>();
        tex.wrapMode = TextureWrapMode.Repeat;
        Observable.EveryUpdate()
            .Where(_ => isPlaying == true)
            .Subscribe(_ => {
                if (hitDetector.IsStar) speed = 3;
                if (hitDetector.IsStar == false) speed = preSpeed;
                float y = Mathf.Repeat(Time.time * speed, 1);
                Vector2 offset = new Vector2(0, y);
                GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
            }).AddTo(this);
        
        stateManager.CurrentState
            .FirstOrDefault(x => x == GameState.Finish)
            .Subscribe(_ => isPlaying = false).AddTo(this);
    }
}
