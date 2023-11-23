using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : SingletonMonobehavious<LoadingUI> {
   [SerializeField] private Image imgFill;

   private void Start() {
      if (Instance != null && Instance != this) {
         Destroy(this.gameObject);
         return;
      }
      
      DontDestroyOnLoad(this.gameObject);
   }

   public void SetFill(float value) {
      imgFill.fillAmount = value;
   }

   public void Hide() {
      //Debug.Log("Hide");
      gameObject.SetActive(false);
   }

   public void Show() {
      //Debug.Log("Show");
      gameObject.SetActive(true);
   }
}
