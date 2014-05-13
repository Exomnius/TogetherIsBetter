using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogetherIsBetter.Model
{
    class Contracts
    {
        private static TIB_Model db;

        public static List<Contract> getContracts()
        {

            try
            {
                using (db = new TIB_Model())
                {
                    var query = from c in db.Contract select c;

                    List<Contract> Contracts = query.ToList();
                    return Contracts;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ContractFormula> getContractForumla()
        {

            try
            {
                using (db = new TIB_Model())
                {
                    var query = from c in db.ContractFormula select c;

                    List<ContractFormula> contractFormula = query.ToList();
                    return contractFormula;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
