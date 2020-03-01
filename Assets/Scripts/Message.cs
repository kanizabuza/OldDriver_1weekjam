using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class Message : MonoBehaviour
{
    // メッセージUI
    [SerializeField] private Text messageText;
    //スキップボタン
    [SerializeField] private Button skipButton;
    //効果音
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip button;
    // 表示するメッセージ
    [SerializeField]
    [TextArea(1, 20)]
    private string allMessage = "おばあちゃん：おじいちゃんや、そろそろ私たちも年だし、免許返納を考えないかい？。\n"
        + "高齢者が起こす事故が多くニュースになっているし、いつか私たちも、と思うとねぇ<>"
        + "おじいちゃん：何を言ってるんだ！俺たちの地域では車を運転しないと生きていけないだろう！<>"
        + "おじちゃん：それに今日も車でお出かけしようっていう話だったじゃないか！！\n"
        + "車がない生活なんで考えられないぞ！<>"
        + "おじいちゃん：わしは免許返納なんて絶対にしないね！！+<>"
        + "おばあちゃん：!!\n" 
        + "気持ちはわかるけどねぇ、事故を起こしてからじゃおそいんじゃないかねぇ<>"
        + "おじいちゃん：わしが事故を起こすわけないじゃろう\n"
        + "何年運転してると思っているんじゃ！！<>"
        + "おばあちゃん：そうかねぇ………。\n"
        +"そこまでいうなら、今日も車で出かけるとするかねぇ<>"
        + "おじいちゃん：おう！いくぞ、ばあさん";

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
    [SerializeField] private Image ozichan2;
    [SerializeField] private Image obachan;

    private int ozichanNum = 0;
    // [SerializeField] private Animator omoideAnim;
    // [SerializeField] private Animator hideAnim;
    // [SerializeField] private Image QuestImage;

    private SceneLoader sceneLoader;

    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip button;

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
            if(ozichanNum >= 3) {
                ozichan.gameObject.SetActive(false);
                ozichan2.gameObject.SetActive(true);
            }
            // テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed) {
                messageText.text += splitMessage[messageNum][nowTextNum];
                if (messageText.text.Contains("おじい")) {
                    // omoideImage.gameObject.SetActive(true);
                    // omoideAnim.SetTrigger("isImageShow");
                    if (ozichanNum < 3) {
                        ozichan.color = new Color(1.0f, 1.0f, 1.0f);
                    } else {
                        ozichan2.color = new Color(1.0f, 1.0f, 1.0f);
                    }
                    obachan.color = new Color(0.3f, 0.3f, 0.3f);
                }
                if (messageText.text.Contains("おばあ")) {
                    if (ozichanNum < 3) {
                        ozichan.color = new Color(0.3f, 0.3f, 0.3f);
                    } else {
                        ozichan2.color = new Color(0.3f, 0.3f, 0.3f);
                    }
                    obachan.color = new Color(1.0f, 1.0f, 1.0f);
                } else if (messageText.text.Contains("-")) {
                    messageText.color = Color.black;
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
                audio.PlayOneShot(button);
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
                audio.PlayOneShot(button);
                nowTextNum = 0;
                messageNum++;
                messageText.text = "";
                clickIcon.enabled = false;
                elapsedTime = 0f;
                isOneMessage = false;
                ozichanNum++;
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
        //SceneManager.LoadScene("Kaiwa2");
        sceneLoader.LoadScene(SceneLoader.Scenes.Kaiwa2).Forget();
    }
}