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
    public class UserGOService : EntityGOService<UTILISATEUR>, ma.metl.sirh.Service.IUserGOService
    {
        OdbcConnection connection;
        IUnitOfWork _unitOfWork;
        IUserGORepository _userGORepository;

        public UserGOService(IUnitOfWork unitOfWork, IUserGORepository userGORepository)
            : base(unitOfWork, userGORepository)
        {
            _unitOfWork = unitOfWork;
            _userGORepository = userGORepository;
            //connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
        }

        public UTILISATEUR GetUserByCode(int code)
        {
            return GetUserByCode(connection,code);
        }

        UTILISATEUR GetUserByCode(OdbcConnection connection, int code)
        {
            var user = new UTILISATEUR();

            string query = "SELECT * " +
                           "FROM UTILISATEURS u " +
                           "WHERE u.COD_USR = " + code + " AND " + "ROWNUM <=1;";

            OdbcCommand command = new OdbcCommand(query, connection);

            if (connection.State == ConnectionState.Closed) connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    user.LIB_USR = reader.GetString(1);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return user;
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

        public List<UTILISATEUR> GetAllUsers() { 
            return GetAllUsers(connection);
        }

        List<UTILISATEUR> GetAllUsers(OdbcConnection connection)
        {
            var users = new List<UTILISATEUR>();

            string query = "SELECT * FROM UTILISATEURS;";

            OdbcCommand command = new OdbcCommand(query, connection);

            if (connection.State == ConnectionState.Closed) connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    users.Add(new UTILISATEUR()
                    {
                        COD_USR = short.Parse(reader.GetString(0)),
                        LIB_USR = reader.GetString(1),
                    });
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return users;
        }
        
    }
}
