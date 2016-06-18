using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace BreakOut_logic {
    public class LevelLoader {
        public static async Task<Wall> load(string levelName) {
            Wall level;
            // Deserialize the level
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            XmlSerializer xs = new XmlSerializer(typeof(Wall));
            using (Stream stream = await storageFolder.OpenStreamForReadAsync(levelName)) {
                level = xs.Deserialize(stream) as Wall;
            }
            return level;
        }

        public static async Task save(Wall level, string levelName) {
            // Serialize the level
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile levelFile = await storageFolder.CreateFileAsync(levelName, CreationCollisionOption.ReplaceExisting);

            XmlSerializer xs = new XmlSerializer(typeof(Wall));
            using (Stream stream = await levelFile.OpenStreamForWriteAsync()) {
                xs.Serialize(stream, level);
            }
        }
    }
}
