using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace DoorManagement.Common
{
    public static class CompositionHelper
    {
        public static CompositionContainer Container { get; set; }

        static CompositionHelper()
        {
            Container = new CompositionContainer();
            Compose();
        }

        /// <summary>
        /// Will get all the exports and add them to container based on Catalogs
        /// </summary>
        public static void Compose()
        {
            // Assembly list needed for imports
            var assemblyList = new List<string>()
            {
                "DoorManagement.ViewModels.dll",
                "DoorManagement.ServiceAccessLayer.dll"
            };

            var catalog = new AggregateCatalog();

            //Get current exe fullpath
            FileInfo info = new FileInfo(Assembly.GetExecutingAssembly().Location);

            foreach (var assemblyName in assemblyList)
            {
                var assemblyPath = Path.Combine(info.Directory.FullName, assemblyName);
                var assembly = Assembly.LoadFile(assemblyPath);
                catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            }

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                Container.ComposeParts();
            }
            catch (SystemException)
            {
                //log
            }
        }
    }
}
