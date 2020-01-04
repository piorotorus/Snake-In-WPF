using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Code;
using SnakeClassLibrary;

namespace SnakeTestProject
{
    [TestClass]
    public class SnakeGameTest
    {
        [TestMethod]
        public void StartGameTest()
        {
            SnakeGame.StartGame();
            Assert.IsTrue(GameState.score == 0);


        }
    }
}
