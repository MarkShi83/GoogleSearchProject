namespace GoogleSearch.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using GoogleSearch.Models;

    /// <summary>
    /// The Data service.
    /// This service save the search result into the xml file. And also return the search result from xml file.
    /// This is service can be replace with database in the future. 
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// The file_ path.
        /// </summary>
        private const string FILE_PATH = "/Data/history.xml";

        /// <summary>
        /// The file_ path_all.
        /// </summary>
        private const string FILE_PATH_ALL = "/Data/allhistory.xml";

        /// <summary>
        /// The _root path.
        /// </summary>
        private readonly string _rootPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// The save async.
        /// </summary>
        /// <param name="searchResults">
        /// The search results.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<bool> SaveAsync(IEnumerable<SearchResult> searchResults)
        {
            try
            {
                var results = searchResults.ToList();
                    results.AddRange(await GetAsync());
                await WriteToXmlFile<List<SearchResult>>(_rootPath + FILE_PATH, results);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The save async.
        /// </summary>
        /// <param name="searchResults">
        /// The search results.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<bool> SaveAllAsync(IEnumerable<SearchResult> searchResults)
        {
            try
            {
                var results = searchResults.ToList();
                results.AddRange(await GetAllAsync());
                await WriteToXmlFile<List<SearchResult>>(_rootPath + FILE_PATH_ALL, results);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The search async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public async Task<IEnumerable<SearchResult>> GetAsync()
        {
          return await ReadFromXmlFile<List<SearchResult>>(_rootPath + FILE_PATH);
        }

        /// <summary>
        /// The search async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public async Task<IEnumerable<SearchResult>> GetAllAsync()
        {
            return await ReadFromXmlFile<List<SearchResult>>(_rootPath + FILE_PATH_ALL);
        }

        /// <summary>
        /// The write to xml file.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="objectToWrite">
        /// The object to write.
        /// </param>
        /// <param name="append">
        /// The append.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private static async Task WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (var writer = new StreamWriter(filePath, append))
            {
                var serializer = new XmlSerializer(typeof(T));
                
                await Task.Run(() => serializer.Serialize(writer, objectToWrite));
            }
        }

        /// <summary>
        /// The read from xml file.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private static async Task<T> ReadFromXmlFile<T>(string filePath) where T : new()
        {
            using (var reader = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                return await Task.Run(() => (T)serializer.Deserialize(reader));
            }
        }
    }
}