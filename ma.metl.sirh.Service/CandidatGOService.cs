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
    public class CandidatGOService : EntityGOService<AGENT>, ICandidatGOService
    {
        IUnitOfWorkOrd _unitOfWork;
        ICandidatGORepository _candidatGORepository;

        public CandidatGOService(IUnitOfWorkOrd unitOfWork, ICandidatGORepository candidatGORepository)
            : base(unitOfWork, candidatGORepository)
        {
            _unitOfWork = unitOfWork;
            _candidatGORepository = candidatGORepository;
        }

        public CandidatGODto GetCandidatFromGipeOrd(string numDoti)
        {
            return _candidatGORepository.GetCandidatFromGipeOrd(numDoti);
        }

        public CandidatDto GetCandidatByNumDoti(string numDoti)
        {
            AutoMapper.Mapper.CreateMap<CandidatGODto, CandidatDto>();
            CandidatGODto candidatGODto = GetCandidatFromGipeOrd(numDoti);
            CandidatDto dto = null;
            if (candidatGODto != null)
            {
                dto = AutoMapper.Mapper.Map<CandidatDto>(candidatGODto); 
                dto.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                try
                {
                    dto.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);
                }
                catch (FormatException e)
                {
                    dto.DateNaissance = null;
                }
            }
            return dto;       
        }

        public CandidatDto GetCandidatByNumDoti(OdbcConnection connection, string numDoti)
        {
            AutoMapper.Mapper.CreateMap<CandidatGODto, CandidatDto>();
           
            CandidatGODto candidatGODto = GetCandidatFromGipeOrd(connection, numDoti);
            
            CandidatDto dto = null;
            
            if (candidatGODto != null)
            {
                dto = AutoMapper.Mapper.Map<CandidatDto>(candidatGODto);
                dto.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                try
                {
                    dto.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);
                }
                catch (FormatException e)
                {
                    dto.DateNaissance = null;
                }
            }
            return dto;
        }

        public CandidatGODto GetCandidatFromGipeOrd(OdbcConnection connection, string numDoti)
        {

            string queryCandidat = "SELECT  a.COD_AG , a.NOM_AG , a.PRENOM_AG, a.NOM_ARAB_AG , a.PRENOM_ARAB_AG, a.DAT_REC,"
                                   + "a.ANN_N_AG, a.MOI_N_AG , a.JOU_N_AG, a.SEX_AG, a.COD_LOC, s.ANC_GRADE, s.ANC_ELO, "
                                   + "a.COD_S_AF_FTF, a.CIN_A_AG, a.CIN_N_AG, s.COD_GRADE, s.COD_CATEG, s.COD_CORPS,"
                                   + "s.COD_CADRE FROM Agent a, SIT_ADM s WHERE a.COD_AG = s.COD_AG and a.COD_AG = " + numDoti + ";";

            CandidatGODto candidat = new CandidatGODto();

            // Create a dataset
            System.Data.DataSet dataSet = new System.Data.DataSet();
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter();
            OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(dataAdapter);
            dataAdapter.SelectCommand = new OdbcCommand(queryCandidat, connection);
            dataAdapter.Fill(dataSet);

            // Iterate through all the rows   
            var test = dataSet.Tables;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                foreach (System.Data.DataColumn column in dataSet.Tables[0].Columns)
                {
                    Console.WriteLine(dataSet.Tables[0].Rows[i][column.ColumnName]);
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "COD_AG":
                            candidat.NumDoti = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : dataSet.Tables[0].Rows[i][column.ColumnName].ToString();
                            break;
                        case "NOM_AG":
                            candidat.Nom = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "PRENOM_AG":
                            candidat.Prenom = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "NOM_ARAB_AG":
                            candidat.NomAR = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "PRENOM_ARAB_AG":
                            candidat.PrenomAR = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "DAT_REC":
                            candidat.DateRecrutement = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "ANN_N_AG":
                            candidat.AnnDateNaissance = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;
                        case "MOI_N_AG":
                            candidat.MoiDateNaissance = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;
                        case "JOU_N_AG":
                            candidat.JourDateNaissance = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;
                        case "SEX_AG":
                            candidat.Sexe = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "COD_LOC":
                            candidat.Localite = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;
                        case "ANC_GRADE":
                            candidat.AncGrade = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "ANC_ELO":
                            candidat.AncEchelon = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "COD_S_AF_FTF":
                            candidat.Direction = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "CIN_A_AG":
                            candidat.CINA = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "CIN_N_AG":
                            candidat.CINN = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? int.Parse("0") : int.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;
                        case "COD_GRADE":
                            candidat.CodeGrade = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "COD_CATEG":
                            candidat.CodeCateg = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "COD_CORPS":
                            candidat.CodeCorps = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "COD_CADRE":
                            candidat.CodeCadre = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                    }
                }
            }

            if (String.IsNullOrEmpty(candidat.NumDoti))
            {
                candidat = null;
            }
            return candidat;
        }
    }
}
