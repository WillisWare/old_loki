using System;

namespace LOKI
{
    public class Inventory
    {
        private string Name;
        private R14 MasterR14 = new R14();
        private PAMS MasterPAMS;
        private PAMS MasterSNIP;

        #region Accessors

        public string name
        {
            get
            {
                return Name;
            }

            set
            {
                Name = value;
            }
        }

        public R14 masterR14
        {
            get
            {
                return MasterR14;
            }

            set
            {
                MasterR14 = value;
            }
        }

        public PAMS masterPAMS
        {
            get
            {
                return MasterPAMS;
            }

            set
            {
                MasterPAMS = value;
            }
        }

        public PAMS masterSNIP
        {
            get
            {
                return MasterSNIP;
            }

            set
            {
                MasterSNIP = value;
            }
        }

        #endregion

        #region Methods

        public void Rename()
        {
            DateTime today = DateTime.Now;
            this.name = String.Format("{0:MMM yyyy}", today);
        }

        #endregion
    }
}
