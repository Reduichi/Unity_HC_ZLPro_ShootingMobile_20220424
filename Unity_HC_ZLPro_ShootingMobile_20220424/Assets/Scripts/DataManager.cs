using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

namespace RED
{
    /// <summary>
    /// ��ƺ޲z
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        // �s�W���p�@�~�������}�A���ܧ� GAS ���n��s
        private string gasLink = "https://script.google.com/macros/s/AKfycbyaRySi6O1wT4wTdrLPw6eyfVs3Id8tRCsuGglzm--1Is0TTr3vJ-Fc_DSHiR4rQHoh/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;
        private TMP_Inputfield inputField;


        private void Start()
        {
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);

            inputField = GameObject.Find("��s���a�W��").GetComponent<TMP_Inputfield>();
            inputField.onEndEdit.AddListener(SetGASData);
        }

        /// <summary>
        /// ���oGA���
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(StarGetGASData());
                
        }

        private IEnumerator StarGetGASData()
        {
            // �s�W�����s�u�n�D (gasLink�A�����)
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                // ���ݺ����s�u�n�D
                yield return www.SendWebRequest();
                // ���a�W�� =�s�u�n�D�U������r�T��
                textPlayerName.text = www.downloadHandler.text;
            }
        }

        private void SetGASData(string value)
        {
            form = new WWWForm();
            form.AddField("method", "�]�w");
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

