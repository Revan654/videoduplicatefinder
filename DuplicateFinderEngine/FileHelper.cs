using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DuplicateFinderEngine 
{
	public static class FileHelper 
    {
		public static readonly IEnumerable<string> ImageExtensions = new[] { "jpg", "jpeg", "png", "gif", "bmp", "tiff", "ico" };
		public static readonly IEnumerable<string> VideoExtensions = new[]
        {
			"asf",
            "avi",
            "dat",
            "divx",
            "dsm",
            "evo",
            "f4v",
            "flv",
            "m1v",
            "m2ts",
            "m2v",
            "m4a",
            "mj2",
            "mjpeg",
            "mjpg",
            "mkv",
            "moov",
            "mov",
            "mp4",
            "mpeg",
            "mpg",
            "mpv",
            "nut",
            "ogg",
            "ogm",
            "qt",
            "rm",
            "rmvb",
            "swf",
            "ts",
            "vob",
            "webm",
            "wmv",
            "xvid"
		};
        
		public static readonly IEnumerable<string>  AllExtensions = VideoExtensions.Concat(ImageExtensions);       

		public static readonly string CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
		// '' <summary>
		// '' This method starts at the specified directory.
		// '' It traverses all subdirectories.
		// '' It returns a List of those directories.
		// '' </summary>
		public static IEnumerable<string> GetFilesRecursive(string initial, bool ignoreReadonly, bool recursive, bool includeImages, List<string> excludeFolders) 
        {
			try 
            {
				var files = Directory
                    .EnumerateFiles(initial)
                    .Where(f => (includeImages ? AllExtensions : VideoExtensions)
                    .Any(x => f.EndsWith(x, StringComparison.OrdinalIgnoreCase)));
                
				if (recursive)
					files = files
                    .Concat(Directory.EnumerateDirectories(initial)
                    .Where(d => !excludeFolders.Any(x => d.Equals(x, StringComparison.OrdinalIgnoreCase)))
                    .Where(d => !ignoreReadonly || (new DirectoryInfo(d).Attributes & FileAttributes.ReadOnly) is 0)
                    .SelectMany(d => GetFilesRecursive(d, ignoreReadonly, recursive: true, includeImages, excludeFolders)));
                
				return files;
			}
            
			catch (Exception ex)
            {
				Logger.Instance.Info(string.Format(Properties.Resources.SkippedErrorReason, initial, ex.Message));
                return Enumerable.Empty<string>();
			}
		}


		/// <summary>
		/// Copies file or folder to target destination and remain the folder structure
		/// </summary>
		/// <param name="pSource"></param>
		/// <param name="pDest"></param>
		/// <param name="pOverwriteDest"></param>
		/// <param name="pMove"></param>
		/// <param name="errors"></param>
		public static void CopyFile(IEnumerable<string> pSource, string pDest, bool pOverwriteDest, bool pMove, out int errors) 
        {
			var destDirectory = Path.GetDirectoryName(pDest) ?? string.Empty;
            
			Directory.CreateDirectory(destDirectory);            
			errors = 0;
            
			foreach (var s in pSource) 
            {
				try 
                {
					var name = Path.GetFileNameWithoutExtension(s);
					var ext = Path.GetExtension(s);
					var temppath = Path.Combine(pDest, name + ext);
					var counter = 0;
                    
					while (File.Exists(temppath)) 
                    {
						temppath = Path.Combine(pDest, name + '_' + counter + ext);
						counter++;
					}

					if (pMove) 
                    {
						if (pOverwriteDest && File.Exists(temppath)) 
                        {
							File.Copy(s, temppath, true);
							File.Delete(s);
						}
                        
						else
							File.Move(s, temppath);
					}
                    
					else
						File.Copy(s, temppath, pOverwriteDest);
				}
                
				catch (Exception e) 
                {
					Logger.Instance.Info(string.Format(Properties.Resources.FailedToCopyToReason, pSource, pDest, e.Message));
					errors++;
				}
			}

		}
	}
}
