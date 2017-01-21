using System.Collections.Generic;

namespace LOKI
{
    public class R14
    {
        private List<R14Entry> Entries = new List<R14Entry>();
        
        #region Accessors

        public List<R14Entry> entries
        {
            get
            {
                return Entries;
            }

            set
            {
                Entries = value;
            }
        }

        #endregion
    }
}
