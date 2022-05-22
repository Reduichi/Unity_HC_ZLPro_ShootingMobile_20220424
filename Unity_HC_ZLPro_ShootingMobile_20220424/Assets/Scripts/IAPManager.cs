using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace RED
{
    public class IAPManager : MonoBehaviour
    {
        [SerializeField, Header("�ʶR�ֽ����s")]
        private IAPButton iapBuySkinRed;
        [SerializeField, Header("�ʶR���ܰT��")]
        private Text textIAPTip;

        /// <summary>
        /// �֦�����ֽ�
        /// </summary>
        private bool hasSkinRed;

		private void Awake()
		{
            // ����ֽ����ʫ��s �ʶR���\�� �K�[��ť�� (�ʶR���\��k)
            iapBuySkinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
            // ����ֽ����ʫ��s �ʶR���ѫ� �K�[��ť�� (�ʶR���Ѥ�k)
            iapBuySkinRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed);

        }

		/// <summary>
		/// �ʶR���\
		/// </summary>
		private void PurchaseCompleteSkinRed(Product product)
        {
            textIAPTip.text = "����ֽ��A�ʶR���\";

            // �B�z�ʶR���\�᪺�欰
            hasSkinRed = true;

            // �������I�s���ä��ʰT��
            // ����I�s(��k�W�١A����ɶ�)
            Invoke("HiddenIAPTip", 3);
        }

        /// <summary>
        /// �ʶR����
        /// </summary>
        private void PurchaseFailedSkinRed(Product prodct, PurchaseFailureReason reason)
        {
            textIAPTip.text = "����ֽ��A�ʶR���ѡA��] : " + reason;

            Invoke("HiddenIAPTip", 3);
        }

        /// <summary>
        /// ���ä��ʴ��ܰT��
        /// </summary>
        private void HiddenIAPTip()
        {
            textIAPTip.text = "";
        }

    }
}


