using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace BreakOut_logic {
    public class LevelManager {
        private CollisionManager collisionObjectsManager;
        private Wall level;
        private Status status;
        private bool levelLoadError;
        private bool loadingLevel;

        static readonly string LevelName = "level_";

        public LevelManager(CollisionManager collisionObjectsManager, Status status) {
            this.collisionObjectsManager = collisionObjectsManager;
            this.status = status;
            level = new Wall();
            level.CollisionManager = collisionObjectsManager;
            levelLoadError = false;
            loadingLevel = false;
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

        public async void loadNewScenario() {
            await load(LevelName + "1.xml");
        }

        public async Task<bool> load(String levelName) {
            try {
                levelLoadError = false;
                Level = await LevelLoader.load(levelName);
                return true;
            }
            catch (Exception ex) {
                levelLoadError = true;
                // TODO correct exceptions....
                //throw new Exception("Error while loading level: " + levelName);
            }

            return false;
        }

        public async void save(String levelName) {
            await LevelLoader.save(level, levelName);
        }

        public async void update() {
            if (!loadingLevel) { 
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
                    bool loadedNext = await load(LevelName + status.CurrentLevel.ToString() + ".xml");
                    if(loadedNext == false) {
                        // Failed to load next, for now game done.
                        status.GameStatus = GameStatus.GameCompleteStatus;
                    }
                }
            }
        }
    }
}