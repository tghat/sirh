using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class CandidatGORepository : GenericRepositoryOrd<GipeOrdContext,AGENT>, ICandidatGORepository
    {
        public CandidatGODto GetCandidatFromGipeOrd(string NumDoti)
        {
            var db1 = new GipeOrdContext();
            int numDoti = Convert.ToInt32(NumDoti);
            var query = (from ag in db1.AGENTs
                         join sitAdm in db1.SIT_ADM on ag.COD_AG equals sitAdm.COD_AG
                         where ag.COD_AG == numDoti
                         select new CandidatGODto
                         {
                             NumDoti = ag.COD_AG.ToString(),
                             Nom = ag.NOM_AG,
                             Prenom = ag.PRENOM_AG,
                             NomAR = ag.NOM_ARAB_AG,
                             PrenomAR = ag.PRENOM_ARAB_AG,
                             DateRecrutement = ag.DAT_REC,
                             AnnDateNaissance = ag.ANN_N_AG,
                             MoiDateNaissance = ag.MOI_N_AG,
                             JourDateNaissance = ag.JOU_N_AG,
                             Sexe = ag.SEX_AG,
                             Localite = ag.COD_LOC,
                             AncGrade = sitAdm.ANC_GRADE,
                             AncEchelon = sitAdm.ANC_ELO,
                             Direction = ag.COD_S_AF_FTF, 
                             CINA = ag.CIN_A_AG,
                             CINN = ag.CIN_N_AG,
                             CodeGrade = sitAdm.COD_GRADE,
                             CodeCateg = sitAdm.COD_CATEG,
                             CodeCorps = sitAdm.COD_CORPS,
                             CodeCadre = sitAdm.COD_CADRE
                         });
            CandidatGODto candidat = null;
            try
            {
                candidat = query.FirstOrDefault();
            }
            catch (Exception e) {
                e.GetBaseException();
            }

            candidat = query.FirstOrDefault();
            //var list = db1.MVT_SIT_ADM.Where(x => x.M_TYP_MVT.Equals("SA") && x.M_COD_AG.Equals(NumDoti)).ToList();
            return candidat;
        }     
    }
}
