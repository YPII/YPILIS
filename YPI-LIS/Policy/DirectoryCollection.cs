using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Policy
{
    public class DirectoryCollection : ObservableCollection<Directory>
    {
        public static async Task<DirectoryCollection> Build()
        {            
            DirectoryCollection directoryCollection = new DirectoryCollection();
            JObject result = await IPFS.FilesLs("/");
            JArray entries = (JArray)result["Entries"];
            foreach (JObject jObject in entries)
            {
                string subDirectoryName = jObject["Name"].ToString();
                Directory subDirectory = new Directory(subDirectoryName, "/" + subDirectoryName);
                directoryCollection.Add(subDirectory);
                await AddChildrenRecursive(subDirectory);
            }
            return directoryCollection;
        }

        private static async Task AddChildrenRecursive(Directory parentDirectory)
        {
            JObject result = await IPFS.FilesLs(parentDirectory.Path);
            JToken value = result["Entries"];
            if(value.Type != JTokenType.Null)
            {
                JArray entries = (JArray)result["Entries"];                
                foreach (JObject jObject in entries)
                {
                    string subDirectoryName = jObject["Name"].ToString();
                    Directory subDirectory = new Directory(subDirectoryName, parentDirectory.Path + "/" + subDirectoryName);
                    parentDirectory.Subdirectories.Add(subDirectory);
                    await AddChildrenRecursive(subDirectory);
                }                
            }            
        }
    }
}
