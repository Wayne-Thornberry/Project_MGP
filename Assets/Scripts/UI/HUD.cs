using System;
using UnityEngine;
using UnityEngine.UI;

namespace New.UI
{
    public class HUD : MonoBehaviour
    {
        public GameObject InfoPanel;

        public Text AIName;
        public Text AIHome;
        public Text AIDest;
        public Text AISpeed;
        public Text AIStopped;
        public Text AIRoadSpeed;

        public Text RouteAmount;
        public Text Route;
        public Text RouteWeight;
        public Text RouteCalc;
        public Text RouteSuccess;

        public Button DeleteBtn;

        private void Start()
        {
            GameConfig.HudReference = this;
            InfoPanel.SetActive(false);
            DeleteBtn.onClick.AddListener(() =>
            {
                if (Target != null)
                {
                    Target.Kill();
                    Target = null;
                }
            });
        }

        private void Update()
        {
            try
            {
                if (Target != null)
                {
                    this.AIName.text = Target.name;
                    this.AIHome.text = Target.Home.name;
                    this.AIDest.text = Target.Destination.name;
                    this.AISpeed.text = Target.AccelerationSpeed.ToString();
                    this.AIDest.text = Target.Destination.name;
                    this.AIStopped.text = (Target.AccelerationSpeed < 1f).ToString();
                    this.AIRoadSpeed.text = Target.NextDestination.Value.Speedlimit.ToString();
                }
            }
            catch (Exception e)
            {
                Target = null;
                InfoPanel.SetActive(false);
            }
        }

        public void UpdateHUD(AI ai)
        {
            InfoPanel.SetActive(true);
            this.Target = ai;
            
            RouteAmount.text = ai.Routes.Count.ToString();
            Route.text = "";
            RouteWeight.text = ai.SelectedRoute.Weight.ToString();
            RouteCalc.text = ai.SelectedRoute.CalcTime.ToString();
            RouteSuccess.text = ai.SelectedRoute.IsSuccessful.ToString();
        }

        public AI Target { get; set; }
    }
}