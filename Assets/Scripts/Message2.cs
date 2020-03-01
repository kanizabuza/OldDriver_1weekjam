using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class Message2 : MonoBehaviour
{
    // メッセージUI
    [SerializeField] private Text messageText;
    //スキップボタン
    [SerializeField] private Button skipButton;
    // 表示するメッセージ
    [SerializeField]
    [TextArea(1, 20)]
    private string allMessage = "おばあちゃん：おじいちゃんや、これ、逆走してはいないかい？？。\n"
        + "さっきから、同じ方向に進んでいる車が見当たらないんじゃが……<>"
        + "おじいちゃん：何を言っとるんじゃ！！！俺たちが進んでいる方向があっているに決まってるじゃろう！！<>"
        + "おばあちゃん：いったん止まって、確認したほうがいいぞい\n"
        + "事故が起こってからじゃ遅いとおもうんじゃ！<>"
        + "おじいちゃん：フッフッフッフ…<>"
        + "おばあちゃん：おじいさん…？<>" 
        + "？おじいちゃん：ぶつからなければ事故とはいわないんじゃ！！！！！<>"
        + "おばあちゃん：おじいさんっ！？！？！？<>"
        + "？おじいちゃん：風になるぜぇぇぇぇぇ！！！<>"
        +"おばあちゃん：いやぁぁぁぁぁぁぁぁぁぁ！！！";

    //使用する分割文字列
    [SerializeField] private string splitString = "<>";
    //分割したメッセージ
    private string[] splitMessage;
    //分割したメッセージの何番目か
    private int messageNum;
    //テキストスピード
    [SerializeField] private float textSpeed = 0.1f;
    //経過時間
    private float elapsedTime = 0f;
    //今見ている文字番号
    private int nowTextNum = 0;
    //マウスクリックを促すアイコン
    [SerializeField] private Image clickIcon;
    // 思い出の写真
   // [SerializeField] private GameObject omoideImage;
    // クリックアイコンの点滅秒数
    [SerializeField] private float clickFlashTime = 0.2f;
    //1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //メッセージをすべて表示したかどうか
    private bool isEndMessage = false;
    [SerializeField] private Image ozichan;
    [SerializeField] private Image newOzichan;
    [SerializeField] private Image obachan;
    [SerializeField] private Image obachan2;
    private bool isOzichan = true;
    private int obachanNum = 0;

    private SceneLoader sceneLoader;

    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip button;
    [SerializeField] private AudioClip oji;

    // [SerializeField] private Animator omoideAnim;
    // [SerializeField] private Animator hideAnim;
    // [SerializeField] private Image QuestImage;

    // Start is called before the first frame update
    void Start()
    {
        clickIcon.enabled = false;
        messageText.text = "";
        SetMessage(allMessage);
        //omoideAnim.GetComponent<Animator>();
        //hideAnim.GetComponent<Animator>();
        // messageText.GetComponent<Text>();
        skipButton.onClick.AddListener(() => {
            audio.PlayOneShot(button);
            Invoke("ChangeScene", 0.1f);
        });

        sceneLoader = GameObject.Find("FadeCanvas").GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEndMessage || allMessage == null) {
            return;
        }
        if (!isOneMessage) {
            // テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed) {
                messageText.text += splitMessage[messageNum][nowTextNum];
                if(obachanNum > 3) {
                    obachan.gameObject.SetActive(false);
                    obachan2.gameObject.SetActive(true);
                }
                if (messageText.text.Contains("おじ")) {
                    if (isOzichan) {
                        // omoideImage.gameObject.SetActive(true);
                        // omoideAnim.SetTrigger("isImageShow");
                        ozichan.color = new Color(1.0f, 1.0f, 1.0f);
                        if (obachanNum < 4) {
                            obachan.color = new Color(0.3f, 0.3f, 0.3f);
                        } else {
                            obachan2.color = new Color(0.3f, 0.3f, 0.3f);
                        }
                    }
                }
                if (messageText.text.Contains("おば")) {
                    if (isOzichan) {
                        ozichan.color = new Color(0.3f, 0.3f, 0.3f);
                    } else {
                        newOzichan.color = new Color(0.3f, 0.3f, 0.3f);
                    }
                    if (obachanNum < 4) {
                        obachan.color = new Color(1.0f, 1.0f, 1.0f);
                    } else {
                        obachan2.color = new Color(1.0f, 1.0f, 1.0f);
                    }
                } else if (messageText.text.Contains("-")) {
                    messageText.color = Color.black;
                }

                if (messageText.text.Contains("？おじ")) {
                    audio.PlayOneShot(oji);
                    Debug.Log("1");
                    // omoideImage.gameObject.SetActive(true);
                    // omoideAnim.SetTrigger("isImageShow");
                    ozichan.gameObject.SetActive(false);
                    newOzichan.gameObject.SetActive(true);
                    isOzichan = false;
                    newOzichan.color = new Color(1.0f, 1.0f, 1.0f);
                    if (obachanNum < 4) {
                        obachan.color = new Color(0.3f, 0.3f, 0.3f);
                    } else {
                        obachan2.color = new Color(0.3f, 0.3f, 0.3f);
                    }
                }
                nowTextNum++;
                elapsedTime = 0f;

                //メッセージを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length) {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //メッセージ表示中にマウスの左ボタンを押したら一括表示
            if (Input.GetMouseButtonDown(0)) {
                //ここまでに表示しているテキストに残りのメッセージを足す
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
        } else {
            elapsedTime += Time.deltaTime;
            //クリックアイコンを点滅する時間を超えたとき、反転させる
            if (elapsedTime >= clickFlashTime) {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0f;
            }

            //マウスクリックされたら次の文字表示処理
            if (Input.GetMouseButtonDown(0)) {
                if (messageNum == 1) {
                   // omoideImage.gameObject.SetActive(true);
                   // omoideAnim.SetTrigger("isImageShow");
                }
                nowTextNum = 0;
                messageNum++;
                messageText.text = "";
                clickIcon.enabled = false;
                elapsedTime = 0f;
                isOneMessage = false;
                obachanNum++;
                //メッセージ全部表示されていたらゲームオブジェクト自体の削除
                if (messageNum >= splitMessage.Length) {
                    isEndMessage = true;
                    //hideAnim.SetTrigger("hide");
                    Invoke("ChangeScene", 3f);
                }
            }
        }
    }

    void SetMessage(string message)
    {
        this.allMessage = message;

        //分割文字列で一回表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }

    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void ChangeScene()
    {
        //SceneManager.LoadScene("Main");
        sceneLoader.LoadScene(SceneLoader.Scenes.Main).Forget();
    }
}