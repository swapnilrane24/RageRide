﻿using UnityEngine;
using System.Collections.Generic;

public class ManageVariables : ScriptableObject {

    // Standart Vars
    [SerializeField]
    public string adMobInterstitialID, adMobBannerID, admobAppID, leaderBoardID;
    [SerializeField]
    public int showInterstitialAfter, bannerAdPoisiton;
    [SerializeField]
    public bool admobActive, googlePlayActive;

}
