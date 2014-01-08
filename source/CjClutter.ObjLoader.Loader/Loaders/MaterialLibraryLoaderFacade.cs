using System.IO;
using ObjLoader.Loader.Common;
namespace ObjLoader.Loader.Loaders
{
    public class MaterialLibraryLoaderFacade : IMaterialLibraryLoaderFacade
    {
        private readonly IMaterialLibraryLoader _loader;
        private readonly IMaterialStreamProvider _materialStreamProvider;

        public MaterialLibraryLoaderFacade(IMaterialLibraryLoader loader, IMaterialStreamProvider materialStreamProvider)
        {
            _loader = loader;
            _materialStreamProvider = materialStreamProvider;
        }

        //I don't think this is the best place for it, but I'm gonna give it a shot
        //Added in the context info to correct file path issues
        public void Load(string materialFileName)
        {
            string fullName;
            if (!StringExtensions.IsNullOrWhiteSpace(ContextInfo.DirectoryName))
            {
                bool useForwardSlash = (ContextInfo.DirectoryName.Contains("/"));
                if (useForwardSlash)
                {
                    fullName = ContextInfo.DirectoryName + "/" + Path.GetFileName(materialFileName);
                }
                else
                {
                    fullName = ContextInfo.DirectoryName + "\\" + Path.GetFileName(materialFileName);
                }
            }
            else
            {
                fullName = materialFileName;
            }

            using (var stream = _materialStreamProvider.Open(fullName))
            {
                if (stream != null)
                {
                    _loader.Load(stream);    
                }
            }
        }
    }
}