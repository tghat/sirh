using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace ma.metl.sirh.Service
{
    public class ActeGOService : EntityGOService<ACTE>, IActeGOService
    {
        OdbcConnection connection;

        IUnitOfWork _unitOfWork;
        IActeGORepository _acteGORepository;

        public ActeGOService(IUnitOfWork unitOfWork, IActeGORepository acteGORepository)
            : base(unitOfWork, acteGORepository)
        {
            _unitOfWork = unitOfWork;
            _acteGORepository = acteGORepository;
            //connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
        }

        public ACTE GetLastActeByNumDotti(int numDoti)
        {
            return GetLastActeByNumDotti(connection,numDoti);
        }

        ACTE GetLastActeByNumDotti(OdbcConnection connection, int NumDoti)
        {
            var acte = new ACTE();

            string query = "SELECT a.REF_ARRETE, a.ANN_VISA, a.STADE " +
                           "FROM ACTE_EVENT_HIST aeh " +
                           "INNER JOIN ACTE a ON aeh.ACTE_REF_ARR = a.REF_ARRETE AND aeh.ACTE_ANN_VISA = a.ANN_VISA " +
                           "WHERE aeh.AGENT_CODE = " + NumDoti + " AND " + "ROWNUM <=1" +
                           "ORDER BY aeh.CREATION_DATE DESC;";

            OdbcCommand command = new OdbcCommand(query, connection);

            //connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    acte.REF_ARRETE = int.Parse(reader.GetString(0));
                    acte.ANN_VISA = byte.Parse(reader.GetString(1));
                    acte.STADE = reader.GetString(2);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return acte;
        }

        protected OdbcConnection GetConnection(string connectString)
        {
            OdbcConnection connection = null;

            // Open a connection
            try
            {
                connection = new OdbcConnection(connectString);
                connection.Open();
            }
            catch (OdbcException ex)
            {
                var exp = ex.Message;
                return null;
            }
            return connection;
        }
    }
}
