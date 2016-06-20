using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BreakOut_logic {
    public class LevelLoader {
        public static async Task<Wall> load(string levelName, bool loadTutorialLevel) {
            Wall level;
            if (loadTutorialLevel) {
                // Deserialize the level from app level folder
                string levelPath = @"ms-appx:///levels/" + levelName;
                Uri uriRelative = new Uri(levelPath);
                StorageFile levelFile = await StorageFile.GetFileFromApplicationUriAsync(uriRelative);

                XmlSerializer xs = new XmlSerializer(typeof(Wall));
                using (Stream stream = await levelFile.OpenStreamForReadAsync()) {
                    level = xs.Deserialize(stream) as Wall;
                }
            }
            else {
                // Deserialize the level from app storage folder
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

                XmlSerializer xs = new XmlSerializer(typeof(Wall));
                using (Stream stream = await storageFolder.OpenStreamForReadAsync(levelName)) {
                    level = xs.Deserialize(stream) as Wall;
                }
            }
            return level;
        }

        public static async Task save(Wall level, string levelName) {

            // Store in local app storage...
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile levelFile = await storageFolder.CreateFileAsync(levelName, CreationCollisionOption.ReplaceExisting);
            
            // Serialize the level
            XmlSerializer xs = new XmlSerializer(typeof(Wall));
            using (Stream stream = await levelFile.OpenStreamForWriteAsync()) {
                xs.Serialize(stream, level);
            }
        }

        public static async Task save(Wall level, StorageFile storageFile) {
            // Serialize the level
            XmlSerializer xs = new XmlSerializer(typeof(Wall));
            using (Stream stream = await storageFile.OpenStreamForWriteAsync()) {
                xs.Serialize(stream, level);
            }
        }
        public static async Task<Wall> load(StorageFile storageFile) {
            Wall level;
            XmlSerializer xs = new XmlSerializer(typeof(Wall));
            using (Stream stream = await storageFile.OpenStreamForReadAsync()) {
                level = xs.Deserialize(stream) as Wall;
            }
            return level;
        }
    }
}
