namespace DuplicateFinderEngine.Data 
{
    /// <summary>
    /// Extends The <see cref="Enum"/> Functions.
    /// </summary>
    internal static class EnumEx
    {
        /// <summary>
        /// The Enum Operator For Any.
        /// </summary>
        /// <param name="Entry">The Source</param>
        /// <param name="Other">The Other Instance</param>
        /// <returns>The Results</returns>
        public static bool Any(this EntryKind Entry, EntryKind Other)
        {
            return (Entry & Other) > 0;
        }

        /// <summary>
        /// The Enum Operator For Has.
        /// </summary>
        /// <param name="Entry">The Source</param>
        /// <param name="Other">The Other Instance</param>
        /// <returns>The Results</returns>
        public static bool Has(this EntryKind Entry, EntryKind Other)
        {
            return (Entry & Other) == Other;
        }

        /// <summary>
        /// Sets the Enum Value.
        /// </summary>
        /// <param name="Entry">The Source</param>
        /// <param name="Other">The Other Instance</param>
        public static void Set(this ref EntryKind Entry, EntryKind Other)
        {
            Entry |= Other;
        }

        /// <summary>
        /// Sets the Enum Value.
        /// </summary>
        /// <param name="Entry">The Source</param>
        /// <param name="Other">The Other Instance</param>
        /// <param name="Reset">If The Values Should be Reset</param>
        public static void Set(this ref EntryKind Entry, EntryKind Other, bool Reset)
        {
            Entry = (Entry & ~Other) | (Reset ? Other : 0);
        }
    }
}
