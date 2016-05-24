using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_logic {
    public enum GameStatus {
        GameStartupStatus = 0,
        GameRunningStatus,
        GamePauzedStatus
    }

    public class Status {
        private GameStatus gameStatus = GameStatus.GameStartupStatus;
        private int currentLevel = 0;
        private int score = 0;
        private float timeLeft_sec = 0;

        public void update(float elapsedTimeMs) {
            throw new System.NotImplementedException();
        }
        public GameStatus GameStatus {
           get { return gameStatus;  }
        }
        public void newGame() {
            // Reset all score and start the game
            currentLevel = 0;
            score = 0;
            timeLeft_sec = 60 * 5;
            gameStatus = GameStatus.GameRunningStatus;
        }
        internal void pauzeGame() {
            gameStatus = GameStatus.GamePauzedStatus;
        }
    }
}