using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace RED
{
    /// <summary>
    /// ���˺޲z
    /// </summary>
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("��q"), Range(0, 1000)]
        private float hp = 200;
        [SerializeField, Header("�����S��")]
        private GameObject goVFXHit;

        private float hpMax;

        private string nameBullet = "�l�u";
        public Image imgHp;

        private void Awake()
        {
            hpMax = hp;
        }

        // �i�J
        private void OnCollisionEnter(Collision collision)
        {
            // �p�G �I������W�� �]�t �l�u �N�B�z ����
            if (collision.gameObject.name.Contains(nameBullet))
            {
                // collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
                // point �I�쪫�󪺮y��
                Damage(collision.contacts[0].point;
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
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            // �s�u.�ͦ�(�S�ġA�����y�СA����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
        }

    }
}

