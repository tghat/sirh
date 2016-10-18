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
using System.Data;

namespace ma.metl.sirh.Service
{
    public class ActeEventHistGOService : EntityGOService<ACTE_EVENT_HIST>, IActeEventHistGOService
    {
        OdbcConnection connection;

        IUnitOfWork _unitOfWork;
        IActeEventHistGORepository _acteEventHistGORepository;
        IGradeRepository _gradeRepository;
        IGradeGORepository _gradeGORepository;

        public ActeEventHistGOService(
            IUnitOfWork unitOfWork, 
            IActeEventHistGORepository acteEventHistGORepository,
            IGradeRepository gradeRepository,
            IGradeGORepository gradeGORepository)
            : base(unitOfWork, acteEventHistGORepository)
        {
            _unitOfWork = unitOfWork;
            _acteEventHistGORepository = acteEventHistGORepository;
            _gradeRepository = gradeRepository;
            _gradeGORepository = gradeGORepository;
            //connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
        }

        public List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti)
        {
            return GetEventsByNumDoti(connection,NumDoti);
        }

        public List<ACTE_EVENT_HIST> GetActeEventHistByCriteria(
            int? userCode, 
            int? gradeId, 
            string operationId, 
            int? refArrete, 
            DateTime? dateOperation)
        {
            var actes = (from a in GetAllActeEventHist(connection)
                         select a).ToList();

            if (userCode != null)
                actes = actes.Where(x => x.CREATED_BY == userCode.Value).ToList();

            if (gradeId != null) {
                var gradeLocal = _gradeRepository.GetById(gradeId.Value);
                var gradeGo = new TAB_GRADE();
                if (gradeLocal != null) {
                    gradeGo = GetGOGradeByCriteria(
                        connection,
                        gradeLocal.CodeCateg,
                        gradeLocal.CodeCorps,
                        gradeLocal.CodeCadre,
                        gradeLocal.CodeGrade);
                }

                actes = actes.Where(x => 
                    x.GRADE_CADRE_COD == gradeGo.COD_CADRE &&
                    x.GRADE_CATEG_COD == gradeGo.COD_CATEG &&
                    x.GRADE_CORPS_COD == gradeGo.COD_CORPS &&
                    x.GRADE_GRADE_COD == gradeGo.COD_GRADE).ToList();
            }

            if (!string.IsNullOrEmpty(operationId))
                actes = actes.Where(x => x.ACTE_STADE == operationId).ToList();

            if (refArrete != null)
                actes = actes.Where(x => x.ACTE_REF_ARR == refArrete.Value).ToList();

            if (dateOperation != null)
                actes = actes.Where(x => x.CREATION_DATE.Value.ToShortDateString() == dateOperation.Value.ToShortDateString()).ToList();

            return actes;
        }

        List<ACTE_EVENT_HIST> GetAllActeEventHist(OdbcConnection connection)
        {
            var aeh_list = new List<ACTE_EVENT_HIST>();

            string query = "SELECT * FROM ACTE_EVENT_HIST;";

            OdbcCommand command = new OdbcCommand(query, connection);

            if (connection.State == ConnectionState.Closed) connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    aeh_list.Add(new ACTE_EVENT_HIST()
                    {
                        ID = int.Parse(reader.GetString(0)),
                        AGENT_CODE = int.Parse(reader.GetString(1)),
                        ACTE_REF_ARR = int.Parse(reader.GetString(2)),
                        ACTE_ANN_VISA = byte.Parse(reader.GetString(3)),
                        ACTE_STADE = reader.GetString(4),
                        CREATION_DATE = DateTime.Parse(reader.GetString(5)),
                        CREATED_BY = short.Parse(reader.GetString(6)),
                        GRADE_CADRE_COD = reader.GetString(7),
                        GRADE_CATEG_COD = reader.GetString(8),
                        GRADE_CORPS_COD = reader.GetString(9),
                        GRADE_GRADE_COD = reader.GetString(10)
                    });
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return aeh_list;
        }

        List<ACTE_EVENT_HIST> GetEventsByNumDoti(OdbcConnection connection, int NumDoti)
        {
            var aeh_list = new List<ACTE_EVENT_HIST>();

            string query = "SELECT * FROM ACTE_EVENT_HIST aeh WHERE aeh.AGENT_CODE = " + NumDoti + " ORDER BY aeh.CREATION_DATE DESC;";

            OdbcCommand command = new OdbcCommand(query, connection);

            if (connection.State == ConnectionState.Closed) connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    aeh_list.Add(new ACTE_EVENT_HIST()
                    {
                        ID = int.Parse(reader.GetString(0)),
                        AGENT_CODE = int.Parse(reader.GetString(1)),
                        ACTE_REF_ARR = int.Parse(reader.GetString(2)),
                        ACTE_ANN_VISA = byte.Parse(reader.GetString(3)),
                        ACTE_STADE = reader.GetString(4),
                        CREATION_DATE = DateTime.Parse(reader.GetString(5)),
                        CREATED_BY = short.Parse(reader.GetString(6)),
                        GRADE_CADRE_COD = reader.GetString(7),
                        GRADE_CATEG_COD = reader.GetString(8),
                        GRADE_CORPS_COD = reader.GetString(9),
                        GRADE_GRADE_COD = reader.GetString(10)
                    });
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return aeh_list;
        }

        public List<ACTE_EVENT_HIST> GetAllActeEventHist()
        {
            return GetAllActeEventHist(connection);
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

        TAB_GRADE GetGOGradeByCriteria(OdbcConnection connection, string code_categ, string code_corps, string code_cadre, string code_grade) {

            var go_grade = new TAB_GRADE();

            string query = "SELECT * " +
                           "FROM TAB_GRADE g " +
                           "WHERE g.COD_CATEG = " + code_categ + " AND " +
                            "g.COD_CORPS = " + code_corps + " AND " +
                            "g.COD_CADRE = " + code_cadre + " AND " +
                            "g.COD_GRADE = " + code_grade + " AND " +
                            "ROWNUM <=1;";

            OdbcCommand command = new OdbcCommand(query, connection);

            if (connection.State == ConnectionState.Closed) connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    go_grade.COD_CATEG = reader.GetString(0);
                    go_grade.COD_CORPS = reader.GetString(1);
                    go_grade.COD_CADRE = reader.GetString(2);
                    go_grade.COD_GRADE = reader.GetString(3);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return go_grade;
        
        }
    }
}
