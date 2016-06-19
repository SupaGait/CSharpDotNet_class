using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Windows.Storage;

namespace BreakOut_logic {
    public class LevelManager {
        private CollisionManager collisionObjectsManager;
        private Wall level;
        private Status status;
        private bool levelLoadError;

        static readonly string LevelName = "level_";

        public LevelManager(CollisionManager collisionObjectsManager, Status status) {
            this.collisionObjectsManager = collisionObjectsManager;
            this.status = status;
            level = new Wall();
            level.CollisionManager = collisionObjectsManager;
            levelLoadError = false;
        }

        public Wall Level {
            get {
                return level;
            }
            set {
                level.clearWall();
                level = value;
                level.fillWall(collisionObjectsManager);
                level.RemainingBricks = level.Bricks.Count;
            }
        }

        public void loadNewScenario() {
            load(LevelName + "1.xml");
        }

        public async void load(String levelName) {
            try {
                levelLoadError = false;
                Level = await LevelLoader.load(levelName);
            }
            catch (Exception ex) {
                levelLoadError = true;
                // TODO correct exceptions....
                //throw new Exception("Error while loading level: " + levelName);
            }
        }

        public async void save(String levelName) {
            await LevelLoader.save(level, levelName);
        }

        public void update() {
            // Update wall
            level.update();

            // Update status
            status.RemainingBricks = level.RemainingBricks;

            // Check for a loose
            if(status.Balls <= 0) {
                status.GameStatus = GameStatus.GameOverStatus;
            }

            // Check for next level
            if (!status.TestingGame && !levelLoadError && level.RemainingBricks <= 0) {
                // Next level
                status.CurrentLevel = status.CurrentLevel + 1;
                load(LevelName + status.CurrentLevel.ToString() + ".xml");
            }
        }
    }
}