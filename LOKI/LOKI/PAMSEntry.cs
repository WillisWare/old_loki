namespace LOKI
{
    public class PAMSEntry
    {
        private string ID;
        private string PartNumber;
        private string SerialNumber;
        private string Nomenclature;
        private string Location;
        private string Notes;

        #region Accessors

        public string id
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }

        public string partNumber
        {
            get
            {
                return PartNumber;
            }

            set
            {
                PartNumber = value;
            }
        }

        public string serialNumber
        {
            get
            {
                return SerialNumber;
            }

            set
            {
                SerialNumber = value;
            }
        }

        public string nomenclature
        {
            get
            {
                return Nomenclature;
            }

            set
            {
                Nomenclature = value;
            }
        }

        public string location
        {
            get
            {
                return Location;
            }

            set
            {
                Location = value;
            }
        }

        public string notes
        {
            get
            {
                return Notes;
            }

            set
            {
                Notes = value;
            }
        }

        #endregion
    }
}
