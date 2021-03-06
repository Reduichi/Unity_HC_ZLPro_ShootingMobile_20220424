using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

// namespace 命名空間
namespace RED
{
    /// <summary>
    /// 控制系統 : 荒野亂鬥移動功能
    /// 虛擬搖桿控制角色移動
    /// </summary>
    public class Systemcontrol : MonoBehaviourPun
    {
        [SerializeField, Header("移動速度"), Range(0, 300)]
        private float speed = 10.5f;
        [SerializeField, Header("角色方向圖示範圍"), Range(0, 5)]
        private float rangeDirectionicon = 2.5f;
        [SerializeField, Header("角色旋轉速度"), Range(0, 100)]
        private float speedturn = 1.5f;
        [SerializeField, Header("動畫參數跑步")]
        private string parameterWalk = "開關跑步";
        [SerializeField, Header("畫布")]
        private GameObject goCanvas;
        [SerializeField, Header("畫布玩家資訊")]
        private GameObject goCanvasPlayerInfo;
        [SerializeField, Header("角色方向圖示")]
        private GameObject goDirection;

        private Rigidbody rig;
        private Animator ani;
        private Joystick joystick;
        private CinemachineVirtualCamera cvc;
        private SystemAttack systemAttack;
        private DamageManager damageManager;

        public Transform traDirectionIcon;

        private void Awake(Transform traDirectionicon)
		{
            rig = GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
            systemAttack = GetComponent<SystemAttack>();
            damageManager = GetComponent<DamageManager>();

            // 如果是連線進入的玩家 就生成玩家需要的物件
            if (photonView.IsMine)
            {
                PlayerUIFollow follow = Instantiate(goCanvasPlayerInfo).GetComponent<PlayerUIFollow>();
                follow.traPlayer = transform;
                traDirectionicon = Instantiate(goDirection).transform;      // 取得角色方向圖示

                // transform.Finf(子物件名稱) - 透過名稱搜尋子物件
                GameObject tempCanvas = Instantiate(goCanvas);
                joystick = tempCanvas.transform.Find("Dynamic Joystick").GetComponent<Joystick>();       // 取得畫布內的虛擬搖桿
                systemAttack.btnFire = tempCanvas.transform.Find("發射").GetComponent<Button>();

                cvc = GameObject.Find("CM 管理器").GetComponent<CinemachineVirtualCamera>();                        // 取得攝影機 CM 管理器
                cvc.Follow = transform;                                                                             // 指定追蹤物件

                damageManager.imgHp = GameObject.Find("圖片血量").GetComponent<Image>();
                damageManager.textHp = GameObject.Find("文字血量").GetComponent<TextMeshProUGUI>();
            }
            // 否則 不是進入的玩家 就關閉控制系統，避免控制到多個物件
            else
            {
                enabled = false;
            }
		}

		private void Update()
		{
            // GetJoystickValue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
            UpdateAnimation();

        }

		private void FixedUpdate()
		{
            Move();
		}

		/// <summary>
		/// 取得虛擬搖桿值
		/// </summary>
		private void GetJoystickValue()
        {
            print("<color=yellow>水平 : " + joystick.Horizontal + "</color>");
        }

        /// <summary>
        /// 移動功能
        /// </summary>
        private void Move() 
        {
            // 剛體.加速度 = 三維向量(x，y，z)
            rig.velocity = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical) * speed;
        }

        /// <summary>
        /// 變更角色圖示的座標
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            // 新座標 = 角色的座標 + 三維向量(虛擬搖桿的水平與垂直) * 方向圖示的範圍
            Vector3 pos = transform.position + new Vector3(-joystick.Horizontal, 0.5f, -joystick.Vertical) * rangeDirectionicon;
            object traDirectionicon = null;
            // 更新方向圖示的座標 = 新座標
            traDirectionIcon.position = pos;
        }

        /// <summary>
        /// 面向方向圖示
        /// </summary>
        private void LookDirectionIcon()
        {
            // 如果 垂直絕對值 小於 0.1 並且 水平絕對值 小於 0.1 就 不處理面向
            if (Mathf.Abs(joystick.Vertical) < 0.1f && Mathf.Abs(joystick.Horizontal) < 0.1f) return;

            // 取得面向角度 = 四位元.面向角度(方向圖示 - 角色) - 方向圖示與角色的向量
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            // 角色的角度 = 四位元.插值(角色的角度，面向角度，旋轉速度 * 一偵的時間)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedturn * Time.deltaTime);
            // 角色的歐拉角度 = 三維向量(0，原本的歐拉角度Y，0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnimation()
        {
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            ani.SetBool(parameterWalk, run);
        }
    }

}


