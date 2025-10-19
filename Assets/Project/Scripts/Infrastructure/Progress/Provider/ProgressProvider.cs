using Project.Scripts.Infrastructure.Progress.Data;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Progress.Provider
{
    public class ProgressProvider : IProgressProvider
    {
        private const string Highscore = "HighScore";
        public ProgressData ProgressData { get; private set; }

        public void SetProgressData(ProgressData data)
        {
            ProgressData = data;
            SaveProgress();
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetFloat(Highscore, ProgressData.HighScore);
        }

        public void LoadProgress()
        {
            if (HasProgress())
            {
                SetProgressData(new ProgressData()
                {
                    HighScore = PlayerPrefs.GetFloat(Highscore)
                });
            }
            else
            {
                SetProgressData(new ProgressData());
            }
        }

        public bool HasProgress()
        {
            return PlayerPrefs.HasKey(Highscore);
        }

        public void DeleteProgress()
        {
            SetProgressData(new ProgressData());
        }
    }
}