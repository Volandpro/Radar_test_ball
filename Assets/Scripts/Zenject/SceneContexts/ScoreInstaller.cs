using Player;

namespace Zenject.SceneContexts
{
    public class ScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallScore();
            InstallScoreSpeedChanger();
        }

        private void InstallScoreSpeedChanger()
        {
            ScoreSpeedChanger scoreSpeedChanger = Container.Instantiate<ScoreSpeedChanger>();
            Container.Bind<ScoreSpeedChanger>().FromInstance(scoreSpeedChanger);
        }

        private void InstallScore()
        {
            Score score = Container.Instantiate<Score>();
            Container.Bind<Score>().FromInstance(score);
        }
    }
}