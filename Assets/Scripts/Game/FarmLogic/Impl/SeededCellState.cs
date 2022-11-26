using System.Collections;
using UniRx;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class SeededCellState : IFarmCellState, IGUIHandler
    {
        private readonly FarmCellView _cellView;
        
        public SeededCellState(float timeToRipe, FarmCellView cellView)
        {
            _cellView = cellView;
            Observable.FromCoroutine(() => WaitRipening(timeToRipe))
                .DoOnCompleted(OnRipening)
                .Subscribe();
        }
        
        public void Tear(FarmCellView cellView, CellBlockParameters blockParameters)
        {
            Debug.Log("cellView.State RipedCellState");
        }

        public void Seed(FarmCellView cellView, CellBlockParameters blockParameters)
        {
            Debug.LogError("actually seed");
        }

        private IEnumerator WaitRipening(float timeToRipe)
        {
            _cellView.CellGUIView.SwitchGuiEnable(true);
            var time = 0f;
            
            do
            {
                time += Time.deltaTime;
                ShowTimeRipening(timeToRipe - time);
                yield return null;
            } while (time < timeToRipe);
        }

        private void OnRipening()
        {
            _cellView.CellGUIView.SwitchGuiEnable(false);
            _cellView.State = new RipedCellState();
        }

        public void ShowTimeRipening(float remainingTime)
        {
            _cellView.CellGUIView.TimeLabel.text = remainingTime.ToString("0.0");
        }
    }
}