using System;

namespace DuplicateFinderEngine.Data
{
 	/// <summary>
    /// Set The Entry Type.
    /// </summary>
	[Flags]
	public enum EntryFlags 
	{
		/// <summary>
        /// Sets The Entry to Image.
        /// </summary>
		IsImage = 1,
		
		/// <summary>
        /// Sets The Entry as Excluded.
        /// </summary>
		ManuallyExcluded = 2,
		
		/// <summary>
        /// Sets The Error For Thumbnail.
        /// </summary>
		ThumbnailError = 4,
		
		/// <summary>
        /// Sets The Error For Metadata.
        /// </summary>
		MetadataError = 8,
		
		/// <summary>
        /// Sets The Error as Being to Dark.
        /// </summary>
		TooDark = 16,

		/// <summary>
        /// Sets The Entry For Errors Which Contains Any The Entries.
        /// </summary>
		AllErrors = ThumbnailError | MetadataError | TooDark,
	}
}
