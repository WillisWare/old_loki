using System.Collections.Generic;

namespace LOKI
{
    public class R14Entry
    {
        private string DOC;
        private string NSN;
        private string PSM;
        private string ASC;
        private int Auth;
        private int InUse;
        private double Price;
        private bool IsComplete;
        private List<Item> Items = new List<Item>();

        #region Accessors

        public string doc
        {
            get
            {
                return DOC;
            }

            set
            {
                DOC = value;
            }
        }

        public string nsn
        {
            get
            {
                return NSN;
            }

            set
            {
                NSN = value;
            }
        }

        public string psm
        {
            get
            {
                return PSM;
            }

            set
            {
                PSM = value;
            }
        }

        public string asc
        {
            get
            {
                return ASC;
            }

            set
            {
                ASC = value;
            }
        }

        public int auth
        {
            get
            {
                return Auth;
            }

            set
            {
                Auth = value;
            }
        }

        public int inUse
        {
            get
            {
                return InUse;
            }

            set
            {
                InUse = value;
            }
        }

        public double price
        {
            get
            {
                return Price;
            }

            set
            {
                Price = value;
            }
        }

        public bool isComplete
        {
            get
            {
                return IsComplete;
            }

            set
            {
                IsComplete = value;
            }
        }

        public List<Item> items
        {
            get
            {
                return Items;
            }

            set
            {
                Items = value;
            }
        }

        #endregion

        #region Functions

        public override string ToString()
        {
            string Value;

            Value = "DOC: " + DOC + " (" + PSM + ")\n\n" +
                "NSN: " + NSN + "\n\n" +
                "ASC: " + ASC + "\n\n" + 
                "Auth: " + Auth + ", In Use: " + InUse + "\n\n" +
                "Price: " + Price;

            return Value;
        }

        #endregion
    }
}