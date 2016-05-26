using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_logic {
    public class LevelManager {
        public Level Level { get; set; }
        public LevelManager() {
            Level = new Level();
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
    }
}