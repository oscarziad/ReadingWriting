using Newtonsoft.Json;
using ReadingWriting.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ReadingWriting.Services
{
    public static class UWPService
    {
        public static async Task CreateTxtFile(string name)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;

            StorageFile storageFile = await storageFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
        }
        public static async Task WriteTxtFile(Windows.Storage.StorageFile file, Person person)
        {
            try
            {
                await Windows.Storage.FileIO.WriteTextAsync(file, $"Your name is {person.Name}, is {person.Age} years old and lives in {person.City}");
            }
            catch { }
        }
        public static async Task WriteXmlFile(Windows.Storage.StorageFile file, Person person)
        {
            try
            {
                using (IRandomAccessStream writeStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    System.IO.Stream s = writeStream.AsStreamForWrite();
                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                    settings.Async = true;
                    using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(s, settings))
                    {
                        writer.WriteStartElement("Person");
                        writer.WriteElementString("Name", person.Name);
                        writer.WriteElementString("Age", Convert.ToString(person.Age));
                        writer.WriteElementString("City", person.City);
                        writer.Flush();
                        await writer.FlushAsync();
                    }
                }
            }
            catch { }
        }
        public static async Task WriteJsonFile(Windows.Storage.StorageFile file, Person person)
        {
            try
            {
                string json = JsonConvert.SerializeObject(person);
                await Windows.Storage.FileIO.WriteTextAsync(file, json);

            }
            catch { }
        }
        public static async Task WriteCsvFile(Windows.Storage.StorageFile file, Person person)
        {
            try
            {
                var lines = new List<string>();
                lines.Add(person.Name);
                lines.Add(Convert.ToString(person.Age));
                lines.Add(person.City);

                await Windows.Storage.FileIO.AppendLinesAsync(file, lines);
            }
            catch { }
        }
    }
}

