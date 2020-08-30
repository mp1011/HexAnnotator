using HexAnnotator.Models;
using System.Threading.Tasks;
using Windows.Storage;
using System;
using System.IO;

namespace HexAnnotator.Services
{
    public class FileReader
    {
        private async Task<StorageFile> GetFile(string filename)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var file = await localFolder.TryGetItemAsync(filename) as StorageFile;
            return file;
        }

        public async Task<BinaryFile> Read(string filename, Endian endian)
        {
            var file = await GetFile(filename);

            byte[] bytes;
            using (var stream = await file.OpenStreamForReadAsync())
            {
                bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
            }

            return new BinaryFile(endian, bytes);
        }
    }
}
