using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CfgMessage : MonoBehaviour
{
    public enum MessageOwner {
        Player, Friend
    }

    public RectTransform myRect, msgRect, imgRect;
    public Text msgTxt;
    public Image msgBgImg;

    public Color msgFriendColor, msgPlayerColor;

    public void Configure(MessageOwner owner, string messageTxt, int extraLines) {
        msgBgImg.color = owner == MessageOwner.Player ? msgPlayerColor : msgFriendColor;

        msgTxt.text = messageTxt;

        var sdelta = myRect.sizeDelta;
        var amin = msgRect.anchorMin;
        var amax = msgRect.anchorMax;
        var alscale = imgRect.localScale;

        sdelta.y = 25 + 5 * extraLines;

        amin.x = owner == MessageOwner.Player ? 0.2f : 0;
        amax.x = owner == MessageOwner.Player ? 1f : 0.8f;
        alscale.x = owner == MessageOwner.Player ? -1f : 1f;
        alscale.y = owner == MessageOwner.Player ? -1f : 1f;

        myRect.sizeDelta = sdelta;
        msgRect.anchorMin = amin;
        msgRect.anchorMax = amax;
        imgRect.localScale = alscale;
    }
}
