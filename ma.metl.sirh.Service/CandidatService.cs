using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Repository;
using System.Collections.Generic;
using System;
using AutoMapper;
using System.Data.Odbc;
using System.Linq;


namespace ma.metl.sirh.Service
{
    public class CandidatService : EntityService<Candidat>, ICandidatService
    {
        IUnitOfWork _unitOfWork;
        ICandidatRepository _candidatRepository;
        ICandidatGORepository _candidatGORepository;
        IGradeRepository _gradeRepository;
        IFluxRepository _fluxRepository;
        IDetailAvancementRepository _detailAvancementRepository;
        ICandidatGOService _candidatGOService;

        public CandidatService(IUnitOfWork unitOfWork, ICandidatRepository candidatRepository, ICandidatGORepository candidatGORepository, IGradeRepository gradeRepository, IFluxRepository fluxRepository, IDetailAvancementRepository detailAvancementRepository, ICandidatGOService candidatGOService)
            : base(unitOfWork, candidatRepository)
        {
            _unitOfWork = unitOfWork;
            _candidatRepository = candidatRepository;
            _candidatGORepository = candidatGORepository;
            _gradeRepository = gradeRepository;
            _fluxRepository = fluxRepository;
            _detailAvancementRepository = detailAvancementRepository;
            _candidatGOService = candidatGOService;
        }

        public List<CandidatResultatDto> GetCandidatAuChoixById(int id)
        {
            return _candidatRepository.GetCandidatAuChoixById(id);
        }

        public List<CandidatResultatDto> GetCandidatExamenById(int id)
        {
            return _candidatRepository.GetCandidatExamenById(id);
        }

        public Candidat GetById(int id)
        {
            return _candidatRepository.GetById(id);
        }

        public CandidatDto GetCandidatById(int id)
        {
            Candidat candidat = _candidatRepository.GetById(id);

            CandidatDto dto = new CandidatDto();

            if (candidat != null)
            {
                Mapper.CreateMap<Candidat, CandidatDto>();
                dto = Mapper.Map<CandidatDto>(candidat);
            }

            return dto;
        }

        public CandidatDto GetCandidatById(int id, int detailAvancementId)
        {
            Candidat candidat = _candidatRepository.GetById(id);
            DetailAvancement detail = _detailAvancementRepository.GetById(detailAvancementId);

            CandidatDto dto = new CandidatDto();

            if (candidat != null)
            {
                Mapper.CreateMap<Candidat, CandidatDto>();
                dto = Mapper.Map<CandidatDto>(candidat);
                dto.ancienGradeId = detail.GradeIdAncien;
                dto.nouveauGradeId = detail.GradeIdNouveau;
            }

            return dto;
        }

        public CandidatDto GetCandidatByNumDotti(string numDotti, int detailAvancementId)
        {
            CandidatGODto candidatGoDto = _candidatGORepository.GetCandidatFromGipeOrd(numDotti);

            DetailAvancement detail = _detailAvancementRepository.GetById(detailAvancementId);

            CandidatDto dto = new CandidatDto();

            if (candidatGoDto != null)
            {
                Mapper.CreateMap<CandidatGODto, CandidatDto>();
                dto = Mapper.Map<CandidatDto>(candidatGoDto);
                dto.CIN = candidatGoDto.CINA + "" + candidatGoDto.CINN;
                dto.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGoDto.JourDateNaissance != 0 ? candidatGoDto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGoDto.MoiDateNaissance != 0 ? candidatGoDto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGoDto.AnnDateNaissance), "dd/MM/yyyy", null);
                dto.ancienGradeId = detail.GradeIdAncien;
                dto.nouveauGradeId = detail.GradeIdNouveau;
            }

            return dto;
        }

        public CandidatDto GetCandidatByNumDotti(OdbcConnection connection, string numDotti, int detailAvancementId)
        {
            CandidatGODto candidatGoDto = _candidatGOService.GetCandidatFromGipeOrd(connection, numDotti);

            DetailAvancement detail = _detailAvancementRepository.GetById(detailAvancementId);

            CandidatDto dto = new CandidatDto();

            if (candidatGoDto != null)
            {
                Mapper.CreateMap<CandidatGODto, CandidatDto>();
                dto = Mapper.Map<CandidatDto>(candidatGoDto);
                dto.CIN = candidatGoDto.CINA + "" + candidatGoDto.CINN;
                dto.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGoDto.JourDateNaissance != 0 ? candidatGoDto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGoDto.MoiDateNaissance != 0 ? candidatGoDto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGoDto.AnnDateNaissance), "dd/MM/yyyy", null);
                dto.ancienGradeId = detail.GradeIdAncien;
                dto.nouveauGradeId = detail.GradeIdNouveau;
            }

            return dto;
        }

        public CandidatDto GetCandidatByNumDoti(string numDoti)
        {
            AutoMapper.Mapper.CreateMap<Candidat, CandidatDto>();
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            CandidatDto dto = null;
            if (candidat != null)
            {
                dto = AutoMapper.Mapper.Map<CandidatDto>(candidat);
                dto.Direction = candidat.Direction.Description;
                dto.AncienGrade = candidat.Grade.Description;
            }
            return dto;
        }

        public bool isExistingCandidat(string numDoti)
        {
            string annee = DateTime.Now.Year.ToString();
            return _candidatRepository.isExistingCandidat(numDoti, annee);
        }

        public Candidat GetByNumDoti(string numDoti)
        {
            return _candidatRepository.GetByNumDoti(numDoti);
        }

        //Récupération de la liste des candidats pour avancement au choix
        public List<CandidatResultatDto> GetAllCandidatAuChoix(CandidatCritereRechDto dto)
        {

            DateTime? AncienneteGrade = null;
            DateTime? AncienneteEchelon = null;
            DateTime dateRefAnciennete = new DateTime();

            if (dto.AnneeProm != null)
            {
                dateRefAnciennete = DateTime.ParseExact("31/12/" + dto.AnneeProm, "dd/MM/yyyy", null);

            }
            else
            {
                dateRefAnciennete = DateTime.ParseExact("31/12/" + DateTime.Now.Year, "dd/MM/yyyy", null);
            }

            if (dto.AncienneteGrade != 0)
            {
                AncienneteGrade = DateUtilsService.addYear(dateRefAnciennete, -dto.AncienneteGrade);
            }

            if (dto.AncienneteEchelon != 0)
            {
                AncienneteEchelon = DateUtilsService.addYear(dateRefAnciennete, -dto.AncienneteEchelon);
            }

            return _candidatRepository.GetAllCandidatAuChoix(dto, AncienneteGrade, AncienneteEchelon);
        }

        //Récupération de la liste des candidats pour avancement au choix
        public List<CandidatResultatDto> GetAllCandidatAuChoix()
        {
            return _candidatRepository.GetAllCandidatAuChoix();
        }

        //Récupération de la liste des candidats pour avancement sur examen
        public List<CandidatResultatDto> GetAllCandidatSurExamen()
        {
            return _candidatRepository.GetAllCandidatSurExamen();
        }

        //Récupération de la liste des candidats pour avancement sur examen
        public List<CandidatResultatDto> GetAllCandidatExamen(CandidatCritereRechDto dto)
        {

            DateTime? AncienneteGrade = null;
            DateTime? AncienneteEchelon = null;
            DateTime dateRefAnciennete = new DateTime();

            if (dto.AnneeProm != null)
            {
                dateRefAnciennete = DateTime.ParseExact("31/12/" + dto.AnneeProm, "dd/MM/yyyy", null);

            }
            else
            {
                dateRefAnciennete = DateTime.ParseExact("31/12/" + DateTime.Now.Year, "dd/MM/yyyy", null);
            }

            if (dto.AncienneteGrade != 0)
            {
                AncienneteGrade = DateUtilsService.addYear(dateRefAnciennete, -dto.AncienneteGrade);
            }

            if (dto.AncienneteEchelon != 0)
            {
                AncienneteEchelon = DateUtilsService.addYear(dateRefAnciennete, -dto.AncienneteEchelon);
            }

            return _candidatRepository.GetAllCandidatExamen(dto, AncienneteGrade, AncienneteEchelon);
        }

        public List<CandidatResultatDto> GetAllCandidatSurExamen(string anneProm, int Ngrade)
        {
            return _candidatRepository.GetAllCandidatSurExamen(anneProm, Ngrade);
        }

        public List<CandidatResultatDto> GetAllCandidatures(CandidatCritereRechDto dto)
        {
            return _candidatRepository.GetAllCandidatures(dto);
        }

        //Récupération de la synthese d'un candidat
        public CandidatResultatDto GetSyntheseCandidat(int id)
        {
            AutoMapper.Mapper.CreateMap<Candidat, CandidatResultatDto>();
            Candidat candidat = _candidatRepository.GetById(id);
            CandidatResultatDto dto = null;
            if (candidat != null)
            {
                dto = AutoMapper.Mapper.Map<CandidatResultatDto>(candidat);
                dto.Direction = candidat.Direction.Description;
                dto.Localite = candidat.Localite.Intitule;
                dto.AncienGrade = candidat.Grade.Description;
            }

            return dto;
        }

        //Creation d'un nouveau candidat
        public void creerCandidat(CandidatDto dto)
        {
            var numDoti = dto.NumDoti;

            //Récupération du candidat GipeOrd
            CandidatGODto candidatGODto = _candidatGORepository.GetCandidatFromGipeOrd(numDoti);
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            Candidat nouveauCandidat = null;

            if (candidatGODto != null)
            {
                //Récupération du grade
                Grade ancienGrade = _gradeRepository.GetGradeByCode(candidatGODto.CodeCateg, candidatGODto.CodeCorps, candidatGODto.CodeCadre, candidatGODto.CodeGrade);

                //Creation du flux correspondant
                Flux nouveauflux = new Flux();
                nouveauflux.TypeFlux = "AC";
                nouveauflux.Etat = "Valide";
                nouveauflux.Source = "SIRH";
                nouveauflux.name = "SIRH_AC_" + DateTime.Now.Year.ToString();
                nouveauflux.dateIntegration = DateTime.Now;
                nouveauflux.nbrLigneRejete = 0;
                nouveauflux.nbrTotalLigne = 0;
                nouveauflux.CreatedDate = DateTime.Now;
                nouveauflux.UpdatedDate = DateTime.Now;

                //Creation du detail d'avancement
                DetailAvancement nouveauDetailAvancement = new DetailAvancement();
                nouveauDetailAvancement.Flux = nouveauflux;
                nouveauDetailAvancement.Annee = DateTime.Now.Year.ToString();
                nouveauDetailAvancement.DateAncienGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.DateEff = DateTime.Now;
                nouveauDetailAvancement.Note = 0;
                nouveauDetailAvancement.CreatedDate = DateTime.Now;
                nouveauDetailAvancement.UpdatedDate = DateTime.Now;
                nouveauDetailAvancement.AncienneteGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.AncienneteEchelon = candidatGODto.AncEchelon ?? DateTime.Now;
                nouveauDetailAvancement.AncienGrade = ancienGrade;
                nouveauDetailAvancement.GradeIdNouveau = dto.GradeId;
                nouveauDetailAvancement.Statut = "EnCours";
                nouveauDetailAvancement.DecisionCap = "EnCours";

                //Modification du candidat si ill existe deja 
                if (candidat != null)
                {
                    nouveauDetailAvancement.Candidat = candidat;
                }

                else
                {
                    //Creation nouveau candidat
                    nouveauCandidat = new Candidat();
                    nouveauCandidat.NumDoti = candidatGODto.NumDoti;
                    nouveauCandidat.Nom = candidatGODto.Nom;
                    nouveauCandidat.Prenom = candidatGODto.Prenom;
                    nouveauCandidat.NomAR = candidatGODto.NomAR;
                    nouveauCandidat.PrenomAR = candidatGODto.PrenomAR;
                    nouveauCandidat.TelPersonnel = candidatGODto.TelPersonnel;
                    nouveauCandidat.Sexe = candidatGODto.Sexe;
                    nouveauCandidat.DateRecrutement = candidatGODto.DateRecrutement;
                    nouveauCandidat.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                    nouveauCandidat.LocaliteId = (long)candidatGODto.Localite;
                    nouveauCandidat.DirectionId = 1;
                    nouveauCandidat.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance != 0 ? candidatGODto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance != 0 ? candidatGODto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);

                    //Récupération du grade                   
                    nouveauCandidat.Grade = ancienGrade;
                    nouveauDetailAvancement.Candidat = nouveauCandidat;
                }

                _detailAvancementRepository.Add(nouveauDetailAvancement);
                _unitOfWork.Commit();
            }
        }

       //Creation d'un nouveau candidat
        public DetailAvancement creerCandidat(OdbcConnection connection, CandidatDto dto)
        {
            var numDoti = dto.NumDoti;

            //Récupération du candidat GipeOrd
            CandidatGODto candidatGODto = _candidatGOService.GetCandidatFromGipeOrd(connection, numDoti);
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            Candidat nouveauCandidat = null;

            if (candidatGODto != null)
            {
                //Récupération du grade
                Grade ancienGrade = _gradeRepository.GetGradeByCode(candidatGODto.CodeCateg, candidatGODto.CodeCorps, candidatGODto.CodeCadre, candidatGODto.CodeGrade);

                //Creation du flux correspondant
                Flux nouveauflux = new Flux();
                nouveauflux.TypeFlux = "AC";
                nouveauflux.Etat = "Valide";
                nouveauflux.Source = "SIRH";
                nouveauflux.name = "SIRH_AC_" + DateTime.Now.Year.ToString();
                nouveauflux.dateIntegration = DateTime.Now;
                nouveauflux.nbrLigneRejete = 0;
                nouveauflux.nbrTotalLigne = 0;
                nouveauflux.CreatedDate = DateTime.Now;
                nouveauflux.UpdatedDate = DateTime.Now;

                //Creation du detail d'avancement
                DetailAvancement nouveauDetailAvancement = new DetailAvancement();
                nouveauDetailAvancement.Flux = nouveauflux;
                nouveauDetailAvancement.Annee = DateTime.Now.Year.ToString();
                nouveauDetailAvancement.DateAncienGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.DateEff = DateTime.Now;
                nouveauDetailAvancement.Note = 0;
                nouveauDetailAvancement.CreatedDate = DateTime.Now;
                nouveauDetailAvancement.UpdatedDate = DateTime.Now;
                nouveauDetailAvancement.AncienneteGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.AncienneteEchelon = candidatGODto.AncEchelon ?? DateTime.Now;
                nouveauDetailAvancement.AncienGrade = ancienGrade;
                nouveauDetailAvancement.GradeIdNouveau = dto.GradeId;
                nouveauDetailAvancement.Statut = "EnCours";
                nouveauDetailAvancement.DecisionCap = "EnCours";

                //Modification du candidat si ill existe deja 
                if (candidat != null)
                {
                    nouveauDetailAvancement.Candidat = candidat;
                }

                else
                {
                    //Creation nouveau candidat
                    nouveauCandidat = new Candidat();
                    nouveauCandidat.NumDoti = candidatGODto.NumDoti;
                    nouveauCandidat.Nom = candidatGODto.Nom;
                    nouveauCandidat.Prenom = candidatGODto.Prenom;
                    nouveauCandidat.NomAR = candidatGODto.NomAR;
                    nouveauCandidat.PrenomAR = candidatGODto.PrenomAR;
                    nouveauCandidat.TelPersonnel = candidatGODto.TelPersonnel;
                    nouveauCandidat.Sexe = candidatGODto.Sexe;
                    nouveauCandidat.DateRecrutement = candidatGODto.DateRecrutement;
                    nouveauCandidat.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                    nouveauCandidat.LocaliteId = (long)candidatGODto.Localite;
                    nouveauCandidat.DirectionId = 1;
                    nouveauCandidat.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance != 0 ? candidatGODto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance != 0 ? candidatGODto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);

                    //Récupération du grade                   
                    nouveauCandidat.Grade = ancienGrade;
                    nouveauDetailAvancement.Candidat = nouveauCandidat;
                }

                DetailAvancement detail = _detailAvancementRepository.Add(nouveauDetailAvancement);
                _unitOfWork.Commit();
                return detail;
            }
            return null;
        }

        //Creation d'un nouveau candidat
        public void modifierCandidat(OdbcConnection connexion, CandidatDto dto)
        {

            //Récupération du candidat GipeOrd
            var numDoti = dto.NumDoti;
            CandidatGODto candidatGODto = _candidatGOService.GetCandidatFromGipeOrd(connexion, numDoti);
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            DetailAvancement detail = _detailAvancementRepository.GetById((int)dto.detailAvancementId);

            if (detail != null && candidatGODto != null && candidat != null)
            {
                candidat.NumDoti = candidatGODto.NumDoti;
                candidat.Nom = candidatGODto.Nom;
                candidat.Prenom = candidatGODto.Prenom;
                candidat.NomAR = candidatGODto.NomAR;
                candidat.PrenomAR = candidatGODto.PrenomAR;
                candidat.TelPersonnel = candidatGODto.TelPersonnel;
                candidat.Sexe = candidatGODto.Sexe;
                candidat.DateRecrutement = candidatGODto.DateRecrutement;
                candidat.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                candidat.LocaliteId = (long)candidatGODto.Localite;
                candidat.DirectionId = 1;
                candidat.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance != 0 ? candidatGODto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance != 0 ? candidatGODto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);
                candidat.Nom = candidatGODto.Nom;
                candidat.Prenom = candidatGODto.Prenom;
                candidat.Sexe = candidatGODto.Sexe;
                candidat.DateRecrutement = candidatGODto.DateRecrutement;

                detail.Candidat = candidat;
                _detailAvancementRepository.Save();
            }
        }

        //Creation d'un nouveau candidat
        public void modifierCandidat(CandidatDto dto)
        {

            //Récupération du candidat GipeOrd
            var numDoti = dto.NumDoti;
            CandidatGODto candidatGODto = _candidatGORepository.GetCandidatFromGipeOrd(numDoti);
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            DetailAvancement detail = _detailAvancementRepository.GetById((int)dto.detailAvancementId);

            if (detail != null && candidatGODto != null && candidat != null)
            {
                candidat.NumDoti = candidatGODto.NumDoti;
                candidat.Nom = candidatGODto.Nom;
                candidat.Prenom = candidatGODto.Prenom;
                candidat.NomAR = candidatGODto.NomAR;
                candidat.PrenomAR = candidatGODto.PrenomAR;
                candidat.TelPersonnel = candidatGODto.TelPersonnel;
                candidat.Sexe = candidatGODto.Sexe;
                candidat.DateRecrutement = candidatGODto.DateRecrutement;
                candidat.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                candidat.LocaliteId = (long)candidatGODto.Localite;
                candidat.DirectionId = 1;
                candidat.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance != 0 ? candidatGODto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance != 0 ? candidatGODto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);
                candidat.Nom = candidatGODto.Nom;
                candidat.Prenom = candidatGODto.Prenom;
                candidat.Sexe = candidatGODto.Sexe;
                candidat.DateRecrutement = candidatGODto.DateRecrutement;

                detail.Candidat = candidat;
                _detailAvancementRepository.Save();
            }
        }


        

        //Creation d'un nouveau candidat
        public DetailAvancement creerCandidatExamen(CandidatDto dto)
        {
            var numDoti = dto.NumDoti;

            //Récupération du candidat GipeOrd
            CandidatGODto candidatGODto = _candidatGORepository.GetCandidatFromGipeOrd(numDoti);
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            Candidat nouveauCandidat = null;

            if (candidatGODto != null)
            {
                //Récupération du grade
                Grade ancienGrade = _gradeRepository.GetGradeByCode(candidatGODto.CodeCateg, candidatGODto.CodeCorps, candidatGODto.CodeCadre, candidatGODto.CodeGrade);

                //Creation du flux correspondant
                Flux nouveauflux = new Flux();
                nouveauflux.TypeFlux = "AE";
                nouveauflux.Etat = "Valide";
                nouveauflux.Source = "SIRH";
                nouveauflux.name = "SIRH_AE_" + DateTime.Now.Year.ToString();
                nouveauflux.dateIntegration = DateTime.Now;
                nouveauflux.nbrLigneRejete = 0;
                nouveauflux.nbrTotalLigne = 0;
                nouveauflux.CreatedDate = DateTime.Now;
                nouveauflux.UpdatedDate = DateTime.Now;

                //Creation du detail d'avancement
                DetailAvancement nouveauDetailAvancement = new DetailAvancement();
                nouveauDetailAvancement.Flux = nouveauflux;
                nouveauDetailAvancement.Annee = DateTime.Now.Year.ToString();
                nouveauDetailAvancement.DateAncienGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.DateEff = DateTime.Now;
                nouveauDetailAvancement.Note = 0;
                nouveauDetailAvancement.CreatedDate = DateTime.Now;
                nouveauDetailAvancement.UpdatedDate = DateTime.Now;
                nouveauDetailAvancement.AncienneteGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.AncienneteEchelon = candidatGODto.AncEchelon ?? DateTime.Now;
                nouveauDetailAvancement.AncienGrade = ancienGrade;
                nouveauDetailAvancement.GradeIdNouveau = dto.GradeId;
                nouveauDetailAvancement.Statut = "EnCours";
                nouveauDetailAvancement.DecisionCap = "EnCours";

                //Modification du candidat si ill existe deja 
                if (candidat != null)
                {
                    nouveauDetailAvancement.Candidat = candidat;
                }

                else
                {
                    //Creation nouveau candidat
                    nouveauCandidat = new Candidat();
                    nouveauCandidat.NumDoti = candidatGODto.NumDoti;
                    nouveauCandidat.Nom = candidatGODto.Nom;
                    nouveauCandidat.Prenom = candidatGODto.Prenom;
                    nouveauCandidat.NomAR = candidatGODto.NomAR;
                    nouveauCandidat.PrenomAR = candidatGODto.PrenomAR;
                    nouveauCandidat.TelPersonnel = candidatGODto.TelPersonnel;
                    nouveauCandidat.Sexe = candidatGODto.Sexe;
                    nouveauCandidat.DateRecrutement = candidatGODto.DateRecrutement;
                    nouveauCandidat.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                    nouveauCandidat.LocaliteId = (long)candidatGODto.Localite;
                    nouveauCandidat.DirectionId = 1;
                    if (nouveauCandidat.DateNaissance != null)
                    {
                        nouveauCandidat.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance != 0 ? candidatGODto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance != 0 ? candidatGODto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);
                    }

                    //Récupération du grade                   
                    nouveauCandidat.Grade = ancienGrade;
                    nouveauDetailAvancement.Candidat = nouveauCandidat;
                }

                DetailAvancement detail = _detailAvancementRepository.Add(nouveauDetailAvancement);
                _unitOfWork.Commit();
                return detail;
            }
            return null;
        }

        //Creation d'un nouveau candidat
        public DetailAvancement creerCandidatExamen(OdbcConnection connection, CandidatDto dto)
        {
            var numDoti = dto.NumDoti;

            //Récupération du candidat GipeOrd
            CandidatGODto candidatGODto = _candidatGOService.GetCandidatFromGipeOrd(connection, numDoti);
            Candidat candidat = _candidatRepository.GetByNumDoti(numDoti);
            Candidat nouveauCandidat = null;

            if (candidatGODto != null)
            {
                //Récupération du grade
                Grade ancienGrade = _gradeRepository.GetGradeByCode(candidatGODto.CodeCateg, candidatGODto.CodeCorps, candidatGODto.CodeCadre, candidatGODto.CodeGrade);

                //Creation du flux correspondant
                Flux nouveauflux = new Flux();
                nouveauflux.TypeFlux = "AE";
                nouveauflux.Etat = "Valide";
                nouveauflux.Source = "SIRH";
                nouveauflux.name = "SIRH_AE_" + DateTime.Now.Year.ToString();
                nouveauflux.dateIntegration = DateTime.Now;
                nouveauflux.nbrLigneRejete = 0;
                nouveauflux.nbrTotalLigne = 0;
                nouveauflux.CreatedDate = DateTime.Now;
                nouveauflux.UpdatedDate = DateTime.Now;

                //Creation du detail d'avancement
                DetailAvancement nouveauDetailAvancement = new DetailAvancement();
                nouveauDetailAvancement.Flux = nouveauflux;
                nouveauDetailAvancement.Annee = DateTime.Now.Year.ToString();
                nouveauDetailAvancement.DateAncienGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.DateEff = DateTime.Now;
                nouveauDetailAvancement.Note = 0;
                nouveauDetailAvancement.CreatedDate = DateTime.Now;
                nouveauDetailAvancement.UpdatedDate = DateTime.Now;
                nouveauDetailAvancement.AncienneteGrade = candidatGODto.AncGrade ?? DateTime.Now;
                nouveauDetailAvancement.AncienneteEchelon = candidatGODto.AncEchelon ?? DateTime.Now;
                nouveauDetailAvancement.AncienGrade = ancienGrade;
                nouveauDetailAvancement.GradeIdNouveau = dto.GradeId;
                nouveauDetailAvancement.Statut = "EnCours";
                nouveauDetailAvancement.DecisionCap = "EnCours";

                //Modification du candidat si ill existe deja 
                if (candidat != null)
                {
                    nouveauDetailAvancement.Candidat = candidat;
                }

                else
                {
                    //Creation nouveau candidat
                    nouveauCandidat = new Candidat();
                    nouveauCandidat.NumDoti = candidatGODto.NumDoti;
                    nouveauCandidat.Nom = candidatGODto.Nom;
                    nouveauCandidat.Prenom = candidatGODto.Prenom;
                    nouveauCandidat.NomAR = candidatGODto.NomAR;
                    nouveauCandidat.PrenomAR = candidatGODto.PrenomAR;
                    nouveauCandidat.TelPersonnel = candidatGODto.TelPersonnel;
                    nouveauCandidat.Sexe = candidatGODto.Sexe;
                    nouveauCandidat.DateRecrutement = candidatGODto.DateRecrutement;
                    nouveauCandidat.CIN = candidatGODto.CINA + "" + candidatGODto.CINN;
                    nouveauCandidat.LocaliteId = (long)candidatGODto.Localite;
                    nouveauCandidat.DirectionId = 1;
                    nouveauCandidat.DateNaissance = DateTime.ParseExact(String.Format("{0:00}", candidatGODto.JourDateNaissance != 0 ? candidatGODto.JourDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.MoiDateNaissance != 0 ? candidatGODto.MoiDateNaissance : 1) + "/" + String.Format("{0:00}", candidatGODto.AnnDateNaissance), "dd/MM/yyyy", null);

                    //Récupération du grade                   
                    nouveauCandidat.Grade = ancienGrade;
                    nouveauDetailAvancement.Candidat = nouveauCandidat;
                }

                DetailAvancement detail =_detailAvancementRepository.Add(nouveauDetailAvancement);
                _unitOfWork.Commit();
                return detail; 
            }
            return null; 
        }

        public List<InterfacageActeCandidatResultatVM> GetCandidatsByCriteria(string annee, long? gradeId, string statut)
        {
            var candidats = from c in _candidatRepository.GetAll()
                            join da in _detailAvancementRepository.GetAll() on c.Id equals da.CandidatId
                            select new InterfacageActeCandidatResultatVM(){
                                CandidatId = c.Id,
                                DetailAvancementId = da.Id,
                                NumDoti = c.NumDoti,
                                CandidatNom = c.Nom,
                                CandidatPrenom = c.Prenom,
                                DeatilAvancementStatut = da.Statut,
                                Annee = da.Annee,
                                GradeId = da.GradeIdNouveau,
                                Statut = da.Statut
                            };

            if (!string.IsNullOrEmpty(annee))
                candidats = candidats.Where( x => x.Annee.ToLower().Equals(annee.ToLower()));

            if (gradeId != null && gradeId != 0)
                candidats = candidats.Where( x => x.GradeId == gradeId);

            if (!string.IsNullOrEmpty(statut))
                candidats = candidats.Where( x => x.Statut.ToLower().Equals(statut.ToLower()));

            return candidats.ToList<InterfacageActeCandidatResultatVM>();
        }
    }
}
