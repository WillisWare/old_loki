using System.Collections.Generic;

namespace LOKI
{
    public class PAMS
    {
        private List<PAMSEntry> Entries = new List<PAMSEntry>();

        #region Accessors

        public List<PAMSEntry> entries
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
