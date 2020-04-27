using System;
using System.Linq;
using DuplicateFinderEngine.FFProbeWrapper;
using System.Collections.Generic;
using ProtoBuf;

namespace DuplicateFinderEngine.Data 
{	
	[ProtoContract]
	public sealed class FileEntry 
    {
		public FileEntry(string file) 
        {
			Path = file;
			var fi = new System.IO.FileInfo(file);
			Folder = fi.Directory.FullName;
			var extension = fi.Extension;
			IsImage = FileHelper.ImageExtensions.Any(x => extension.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}
        
		[ProtoMember(1)]
		public string Path { get; set; }
        
		[ProtoMember(2)]
		public string Folder;
        
		[ProtoMember(3)]
		public List<byte[]>? grayBytes;
        
		[ProtoMember(4)]
		public MediaInfo? mediaInfo;
        
		[ProtoMember(5)]
		public EntryFlags Flags;

		public bool IsImage 
        {
			get => Flags.Has(EntryFlags.IsImage);
			protected set => Flags.Set(EntryFlags.IsImage, value);
		}
	}
}
