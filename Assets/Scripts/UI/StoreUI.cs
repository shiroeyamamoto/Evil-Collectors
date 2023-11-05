using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : UIBase {
   [SerializeField] private Button btnBackHome;
   protected override void Start() {
      base.Start();
      btnBackHome.onClick.AddListener(() => {
         GameManager.Instance.LoadSceneMenu();
      });
   }
}
