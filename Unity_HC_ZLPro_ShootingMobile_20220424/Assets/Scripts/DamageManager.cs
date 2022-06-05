using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System.Collections;

namespace RED
{
    /// <summary>
    /// ���˺޲z
    /// </summary>
    public class DamageManager : MonoBehaviourPun
    {
        #region �򥻸��
        [SerializeField, Header("��q"), Range(0, 1000)]
        private float hp = 200;
        [SerializeField, Header("�����S��")]
        private GameObject goVFXHit;
        [SerializeField, Header("���ѵۦ⾹")]
        private Shader shaderDissolve;

        private float hpMax;

        private string nameBullet = "�l�u";

        // �ҫ��Ҧ������V����A�̭��]�t����y
        private SkinnedMeshRenderer[] smr;

        [HideInInspector]
        public Image imgHp;
        [HideInInspector]
        public TextMeshProUGUI textHp;
        #endregion

        private Material materialDissolve;
        private Systemcontrol systemControl;
        private SystemAttack systemAttack;

        private void Awake()
        {
            systemControl = GetComponent<Systemcontrol>();
            systemAttack = GetComponent<SystemAttack>();
            hpMax = hp;

            smr = GetComponentsInChildren<SkinnedMeshRenderer>();           // ���o�l����̪�����
            materialDissolve = new Material(shaderDissolve);                // �s�W ���ѵۦ⾹ ����y
            for (int i = 0; i < smr.Length; i++)                            // �Q�ΰj��ᤩ�Ҧ��l���� ���ѧ���y
            {
                smr[i].material = materialDissolve;
            }

            if (photonView.IsMine) textHp.text = hp.ToString();
        }

        // �i�J
        private void OnCollisionEnter(Collision collision)
        {
            // �p�G ���O�ۤv�����a���� �N���X
            if (!photonView.IsMine) return;
            // �p�G �I������W�� �]�t �l�u �N�B�z ����
            if (collision.gameObject.name.Contains(nameBullet))
            {
                // collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
                // point �I�쪫�󪺮y��
                Damage(collision.contacts[0].point);
            }
        }

        // ����
        private void OnCollisionStay(Collision collision)
        {
          
        }

        // ���}
        private void OnCollisionExit(Collision collision)
        {

        }

      
        private void Damage(Vector3 posHit)
        {
            #region ����
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            // ��q = �ƾ�.����(��q�A�̤p�ȡA�̤j��)
            hp = Mathf.Clamp(hp, 0, hpMax);
            textHp.text = hp.ToString();
            // �s�u.�ͦ�(�S�ġA�����y�СA����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
            #endregion

            // �p�G ��q <= 0 �N�z�L RPC �P�B�Ҧ��H������i�榺�`��k
            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);
        }


        // �ݭn�P�B����k�����K�[ PunRPC �ݩ� Remote Procedure Call ���ݵ{���I�s
        [PunRPC]
        private void Dead()
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            systemControl.enabled = false;
            systemAttack.enabled = false;
            systemControl.traDirectionIcon.gameObject.SetActive(false);

            float valueDissolve = 5;            // ���ѼƭȰ_�l��

            for (int i = 0; i < 25; i++)        // �j����滼��
            {
                valueDissolve -= 0.3f;          // ���Ѽƭȴ� 0.3
                materialDissolve.SetFloat("dissolve", valueDissolve);       // ��s�ۦ⾹�ݩʡA�`�N�n���� Reference
                yield return new WaitForSeconds(0.08f);         //  ����
            }
        }
    }
}

