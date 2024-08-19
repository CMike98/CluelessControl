using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluelessControl
{
    public class ChequeSettings
    {
        public List<BaseCheque> ChequeList
        {
            get;
            private set;
        }

        private ChequeSettings(List<BaseCheque> chequeList)
        {
            ChequeList = chequeList;
        }

        internal static ChequeSettings Create()
        {
            return new ChequeSettings(new List<BaseCheque>());
        }

        internal static ChequeSettings Create(List<BaseCheque> chequeList)
        {
            if (chequeList == null)
                throw new ArgumentNullException(nameof(chequeList));
            if (chequeList.Any(cheque => cheque == null))
                throw new ArgumentException($"At least one of the cheques on the list is null.", nameof(chequeList));
            return new ChequeSettings(chequeList);
        }
    }
}
