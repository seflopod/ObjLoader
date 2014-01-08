using System.Collections.Generic;
using System.IO;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers.Interfaces;
using ObjLoader.Loader.Common;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoader : LoaderBase, IObjLoader
    {
        private readonly IDataStore _dataStore;
        private readonly List<ITypeParser> _typeParsers = new List<ITypeParser>();

        private readonly List<string> _unrecognizedLines = new List<string>();

        public ObjLoader(
            IDataStore dataStore, 
            IFaceParser faceParser, 
            IGroupParser groupParser,
            INormalParser normalParser, 
            ITextureParser textureParser, 
            IVertexParser vertexParser,
            IMaterialLibraryParser materialLibraryParser, 
            IUseMaterialParser useMaterialParser)
        {
            _dataStore = dataStore;
            SetupTypeParsers(
                vertexParser,
                faceParser,
                normalParser,
                textureParser,
                groupParser,
                materialLibraryParser,
                useMaterialParser);
        }

        private void SetupTypeParsers(params ITypeParser[] parsers)
        {
            foreach (var parser in parsers)
            {
                _typeParsers.Add(parser);
            }
        }

        protected override void ParseLine(string keyword, string data)
        {
            foreach (var typeParser in _typeParsers)
            {
                if (typeParser.CanParse(keyword))
                {
                    typeParser.Parse(data);
                    return;
                }
            }

            _unrecognizedLines.Add(keyword + " " + data);
        }

        public LoadResult Load(Stream lineStream)
        {
            StartLoad(lineStream);
            if (ContextInfo.DirectoryName == null)
                ContextInfo.DirectoryName = "";
            return CreateResult();
        }

        //Addition to workaround some issues I'm having with Unity and getting to files
        //outside of their limits.  See if this works...
        //Peter Bartosch
        public LoadResult Load(string filePath)
        {
            FileStream lineStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if(!StringExtensions.IsNullOrWhiteSpace(filePath))
                ContextInfo.DirectoryName = Path.GetDirectoryName(filePath);
            return Load(lineStream);
        }

        private LoadResult CreateResult()
        {
            var result = new LoadResult
                             {
                                 Vertices = _dataStore.Vertices,
                                 Textures = _dataStore.Textures,
                                 Normals = _dataStore.Normals,
                                 Groups = _dataStore.Groups,
                                 Materials = _dataStore.Materials
                             };
            return result;
        }
    }
}