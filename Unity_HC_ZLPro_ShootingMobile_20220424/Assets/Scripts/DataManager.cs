using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

namespace RED
{
    /// <summary>
    /// 資料管理
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        // 新增部署作業內的網址，有變更 GAS 都要更新
        private string gasLink = "https://script.google.com/macros/s/AKfycbyaRySi6O1wT4wTdrLPw6eyfVs3Id8tRCsuGglzm--1Is0TTr3vJ-Fc_DSHiR4rQHoh/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;
        private TMP_Inputfield inputField;


        private void Start()
        {
            textPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGetData = GameObject.Find("取得玩家資料按鈕").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);

            inputField = GameObject.Find("更新玩家名稱").GetComponent<TMP_Inputfield>();
            inputField.onEndEdit.AddListener(SetGASData);
        }

        /// <summary>
        /// 取得GA資料
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "取得");

            StartCoroutine(StarGetGASData());
                
        }

        private IEnumerator StarGetGASData()
        {
            // 新增網頁連線要求 (gasLink，表單資料)
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                // 等待網頁連線要求
                yield return www.SendWebRequest();
                // 玩家名稱 =連線要求下載的文字訊息
                textPlayerName.text = www.downloadHandler.text;
            }
        }

        private void SetGASData(string value)
        {
            form = new WWWForm();
            form.AddField("method", "設定");
            form.AddField("playerName", inputField.text);

            StartCoroutine(StartSetGASData());
        }

        private IEnumerator StartSetGASData()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                yield return www.SendWebRequest();
                textPlayerName.text = inputField.text;
                print(www.downloadHandler.text);
            }
        }

    }

    internal class TMP_Inputfield
    {
        internal readonly object onEndEdit;
    }
}

