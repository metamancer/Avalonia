using System;
using System.IO;
using System.Reflection;
using System.Text;
using Avalonia.Markup.Xaml.XamlIl;

#nullable enable

namespace Avalonia.Markup.Xaml
{
    public static class AvaloniaRuntimeXamlLoader
    {
        /// <summary>
        /// Loads XAML from a string.
        /// </summary>
        /// <param name="xaml">The string containing the XAML.</param>
        /// <param name="localAssembly">Default assembly for clr-namespace:.</param>
        /// <param name="rootInstance">The optional instance into which the XAML should be loaded.</param>
        /// <param name="uri">The URI of the XAML being loaded.</param>
        /// <param name="designMode">Indicates whether the XAML is being loaded in design mode.</param>
        /// <returns>The loaded object.</returns>
        public static object Load(string xaml, Assembly localAssembly = null, object rootInstance = null, Uri uri = null, bool designMode = false)
        {
            Contract.Requires<ArgumentNullException>(xaml != null);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xaml)))
            {
                return Load(stream, localAssembly, rootInstance, uri, designMode);
            }
        }
        
        /// <summary>
        /// Loads XAML from a string.
        /// </summary>
        /// <param name="xaml">The string containing the XAML.</param>
        /// <param name="configuration">Xaml loader configuration.</param>
        /// <returns>The loaded object.</returns>
        public static object Load(string xaml, RuntimeXamlLoaderConfiguration configuration)
        {
            Contract.Requires<ArgumentNullException>(xaml != null);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xaml)))
            {
                return Load(stream, configuration);
            }
        }

        /// <summary>
        /// Loads XAML from a stream.
        /// </summary>
        /// <param name="stream">The stream containing the XAML.</param>
        /// <param name="localAssembly">Default assembly for clr-namespace:</param>
        /// <param name="rootInstance">The optional instance into which the XAML should be loaded.</param>
        /// <param name="uri">The URI of the XAML being loaded.</param>
        /// <param name="designMode">Indicates whether the XAML is being loaded in design mode.</param>
        /// <returns>The loaded object.</returns>
        public static object Load(Stream stream, Assembly localAssembly, object rootInstance = null, Uri uri = null,
            bool designMode = false)
            => AvaloniaXamlIlRuntimeCompiler.Load(stream, localAssembly, rootInstance, uri, designMode, false);
        
        /// <summary>
        /// Loads XAML from a stream.
        /// </summary>
        /// <param name="stream">The stream containing the XAML.</param>
        /// <param name="configuration">Xaml loader configuration.</param>
        /// <returns>The loaded object.</returns>
        public static object Load(Stream stream, RuntimeXamlLoaderConfiguration configuration)
            => AvaloniaXamlIlRuntimeCompiler.Load(stream, configuration.LocalAssembly, configuration.RootInstance,
                configuration.BaseUri, configuration.DesignMode, configuration.UseCompiledBindingsByDefault);

        /// <summary>
        /// Parse XAML from a string.
        /// </summary>
        /// <param name="xaml">The string containing the XAML.</param>
        /// <param name="localAssembly">Default assembly for clr-namespace:.</param>
        /// <returns>The loaded object.</returns>
        public static object Parse(string xaml, Assembly localAssembly = null)
            => Load(xaml, localAssembly);

        /// <summary>
        /// Parse XAML from a string.
        /// </summary>
        /// <typeparam name="T">The type of the returned object.</typeparam>
        /// <param name="xaml">>The string containing the XAML.</param>
        /// <param name="localAssembly">>Default assembly for clr-namespace:.</param>
        /// <returns>The loaded object.</returns>
        public static T Parse<T>(string xaml, Assembly localAssembly = null)
            => (T)Parse(xaml, localAssembly);
            
    }
    
    public class RuntimeXamlLoaderConfiguration
    {
        /// <summary>
        /// The URI of the XAML being loaded.
        /// </summary>
        public Uri? BaseUri { get; set; }

        /// <summary>
        /// Default assembly for clr-namespace:.
        /// </summary>
        public Assembly LocalAssembly { get; set; }
            
        /// <summary>
        /// The optional instance into which the XAML should be loaded.
        /// </summary>
        public object? RootInstance { get; set; }
            
        /// <summary>
        /// Defines is CompiledBinding should be used by default.
        /// Default is 'false'.
        /// </summary>
        public bool UseCompiledBindingsByDefault { get; set; } = false;

        /// <summary>
        /// Indicates whether the XAML is being loaded in design mode.
        /// Default is 'false'.
        /// </summary>
        public bool DesignMode { get; set; } = false;
    }
}
