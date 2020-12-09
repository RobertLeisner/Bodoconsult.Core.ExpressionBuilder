using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Bodoconsult.Core.ExpressionBuilder.Helpers
{
    /// <summary>
    /// Simple class for local I18N handling
    /// </summary>
    public static class I18NHelper
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        static I18NHelper()
        {

            Languages = new Dictionary<string, string>();

            Translations = new Dictionary<string, string>();

            var assembly = Assembly.GetCallingAssembly();

            var folder = assembly.GetName().Name + ".Locales.";

            var len = folder.Length;

            var localeResources = assembly.GetManifestResourceNames().Where(x => x.StartsWith(folder, StringComparison.InvariantCultureIgnoreCase));

            foreach (var locales in localeResources)
            {
                var key = locales.Substring(len, locales.Length - len - 4).ToUpperInvariant();

                Languages.Add(new KeyValuePair<string, string>(key, locales));


            }
            LoadDefaultLanguage();
        }


        /// <summary>
        /// Get a text from a embedded resource file
        /// </summary>
        /// <param name="resourceName">resource name = file name</param>
        /// <returns></returns>
        public static string GetTextResource(string resourceName)
        {
            var ass = Assembly.GetCallingAssembly();
            var str = ass.GetManifestResourceStream(resourceName);

            if (str == null) return null;

            string s;

            using (var file = new StreamReader(str))
            {
                s = file.ReadToEnd();
            }

            return s;
        }

        /// <summary>
        /// Load the current thread language or english if thread language isn't existing
        /// </summary>
        public static void LoadDefaultLanguage()
        {

            var language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpperInvariant();

            if (Languages.All(x => x.Key != language)) language = "en";


            LoadLanguage(language);


        }

        /// <summary>
        /// Load a language by its 2-digit ISO code like EN, DE, ...
        /// </summary>
        /// <param name="isoCode">2-digit ISO code like EN, DE, ...</param>
        public static void LoadLanguage(string isoCode)
        {
            var lan = Languages.FirstOrDefault(x => x.Key == isoCode.ToUpperInvariant());

            Translations.Clear();

            var content = GetTextResource(lan.Value);

            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var s = line.Split('=');

                var p = new KeyValuePair<string, string>(s[0].Trim().ToUpperInvariant(), s[1].Trim());

                Translations.Add(p);
            }

        }


        private static IDictionary<string, string> Translations { get; set; }

        /// <summary>
        /// Available languages
        /// </summary>
        public static IDictionary<string, string> Languages { get; private set; }

        /// <summary>
        /// Current number of loaded translation (mainly for testing purposes
        /// </summary>
        public static int Count => Translations.Count;

        /// <summary>
        /// Get a translated string by its key
        /// </summary>
        /// <param name="key">key representing a translated string in locales files</param>
        /// <returns></returns>
        public static string GetString(string key)
        {

            if (string.IsNullOrEmpty(key)) return "$$KeyMissing$$";

            var success = Translations.TryGetValue(key.ToUpperInvariant(), out var result);

            return !success ? $"$${key}$$" : result;
        }

    }
}
