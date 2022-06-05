using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

namespace RED
{
    /// <summary>
    /// ��ƺ޲z
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        // �s�W���p�@�~�������}�A���ܧ� GAS ���n��s
        private string gasLink = "https://script.google.com/macros/s/AKfycbyergUCj2ViPJmUMDHz7MP6WhBHEHepDBh5AWNQv-zVu7568CpkF6x1_JMJkLtFHMNR/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;


        private void Start()
        {
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);
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

    }
}

