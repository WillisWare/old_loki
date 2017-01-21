namespace LOKI
{
    public class Item
    {
        private string ID;
        private string Nomenclature;
        private string PartNumber;
        private string SerialNumber;
        private string Location;
        private bool Exchange;
        private string NotesPAMS;
        private string NotesR14;

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

        public bool exchange
        {
            get
            {
                return Exchange;
            }

            set
            {
                Exchange = value;
            }
        }

        public string notesPAMS
        {
            get
            {
                return NotesPAMS;
            }

            set
            {
                NotesPAMS = value;
            }
        }

        public string notesR14
        {
            get
            {
                return NotesR14;
            }

            set
            {
                NotesR14 = value;
            }
        }
        #endregion
    }
}
