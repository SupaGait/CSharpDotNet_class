using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_logic {
    public class LevelManager {
        private CollisionManager collisionObjectsManager;
        private Wall level;

        public LevelManager(CollisionManager collisionObjectsManager) {
            this.collisionObjectsManager = collisionObjectsManager;
            level = new Wall();
            level.CollisionManager = collisionObjectsManager;
        }

        public Wall Level {
            get {
                return level;
            }
            set {
                level.clearWall();
                level = value;
                level.fillWall(collisionObjectsManager);
            }
        }

        public void load() {
            throw new System.NotImplementedException();
        }

        public void save() {
            throw new System.NotImplementedException();
        }

        public void reload() {
            throw new System.NotImplementedException();
        }

        public void update() {
            // Update wall
            level.update();
        }
    }
}