using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_logic {
    public enum GameStatus {
        GameStartupStatus = 0,
        GameRunningStatus,
        GameOverStatus,
        GameCompleteStatus,
        GamePauzedStatus
    }

    public class Status {
        public GameStatus GameStatus { get; set; }
        public int CurrentLevel { get; set; }
        public int Balls { get; set; }
        public int Score { get; set; }
        public float TimeLeft_sec { get; set; }
        public bool TestingGame { get; set; }
        public int RemainingBricks { get; set; }

        public Status() {
            GameStatus = GameStatus.GameStartupStatus;
        }
        public void runTestGame() {
            TestingGame = true;
            CurrentLevel = 99;
            Score = 0;
            GameStatus = GameStatus.GameRunningStatus;
        }
        public void runGame() {
            // Reset all score and start the game
            TestingGame = false;
            CurrentLevel = 1;
            Balls = 3;
            Score = 0;
            TimeLeft_sec = 60 * 5;
            GameStatus = GameStatus.GameRunningStatus;
        }
        internal void pauzeGame() {
            GameStatus = GameStatus.GamePauzedStatus;
        }
        internal void resumeGame() {
            GameStatus = GameStatus.GameRunningStatus;
        }
        internal void addPoints(int points) {
            Score += points;
        }
    }
}