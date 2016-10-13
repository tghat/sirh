using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ma.metl.sirh.Model
{
    public class MyDefaultModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (modelType == typeof(Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement>))
                return new Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement>(new List<ParametrageClassement>(), new ParametrageClassement());

            if (modelType == typeof(Tuple<IEnumerable<Commission>>))
                return new Tuple<IEnumerable<Commission>>(new List<Commission>());

            if (modelType == typeof(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto>))
                return new Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto>(new List<CandidatResultatDto>(), new CandidatCritereRechDto(), new CandidatResultatDto(), new CandidatDto());

            if (modelType == typeof(Tuple<CritereRechercheDto, IEnumerable<CandidatResultatDto>, IEnumerable<MatiereDto>, Email>))
                return new Tuple<CritereRechercheDto, IEnumerable<CandidatResultatDto>, IEnumerable<MatiereDto>, Email>(new CritereRechercheDto(), new List<CandidatResultatDto>(), new List<MatiereDto>(), new Email());

            if (modelType == typeof(Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota>))
                return new Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota>(new List<ParametrageQuota>(), new ParametrageQuota());

            if (modelType == typeof(Tuple<ProgrammeModel, IEnumerable<Flux>>))
                return new Tuple<ProgrammeModel, IEnumerable<Flux>>(new ProgrammeModel(), new List<Flux>());

            if (modelType == typeof(Tuple<CandidatCritereRechDto, IEnumerable<QuotaDto>>))
                return new Tuple<CandidatCritereRechDto, IEnumerable<QuotaDto>>(new CandidatCritereRechDto(), new List<QuotaDto>());

            if (modelType == typeof(Tuple<IEnumerable<CentreExamen>, CentreExamen>))
                return new Tuple<IEnumerable<CentreExamen>, CentreExamen>(new List<CentreExamen>(), new CentreExamen());

            if (modelType == typeof(Tuple<IEnumerable<Examen>, Examen>))
                return new Tuple<IEnumerable<Examen>, Examen>(new List<Examen>(), new Examen());

            if (modelType == typeof(Tuple<IEnumerable<Specialite>, Specialite>))
                return new Tuple<IEnumerable<Specialite>, Specialite>(new List<Specialite>(), new Specialite());

            if (modelType == typeof(Tuple<IEnumerable<Matiere>, Matiere>))
                return new Tuple<IEnumerable<Matiere>, Matiere>(new List<Matiere>(), new Matiere());

            if (modelType == typeof(Tuple<ProgrammeModel, EventDto>))
                return new Tuple<ProgrammeModel, EventDto>(new ProgrammeModel(), new EventDto());

            if (modelType == typeof(ViewModel))
                return new ViewModel();

            if (modelType == typeof(Users))
                return new Users();

            if (modelType == typeof(CritereRechercheDto))
                return new CritereRechercheDto();

            if (modelType == typeof(CandidatResultatDto))
                return new CandidatResultatDto();

            if (modelType == typeof(CandidatGODto))
                return new CandidatGODto();

            if (modelType == typeof(Commission))
                return new Commission();

            if (modelType == typeof(Reunion))
                return new Reunion();

            if (modelType == typeof(MembreCommission))
                return new MembreCommission();

            if (modelType == typeof(Convocation))
                return new Convocation();

            if (modelType == typeof(Tuple<ProgrammeModel, Publication, IEnumerable<Publication>>))
                return new Tuple<ProgrammeModel, Publication, IEnumerable<Publication>> (new ProgrammeModel(), new Publication(), new List<Publication>());

            if (modelType == typeof(Tuple<MembreCommission, Commission, Reunion, IEnumerable<Grade>>))
                return new Tuple<MembreCommission, Commission, Reunion, IEnumerable<Grade>>(new MembreCommission(), new Commission(), new Reunion(), new List<Grade>());

            if (modelType == typeof(Tuple<Candidat, DetailAvancement, Examen, IEnumerable<MatiereDto>>))
                return new Tuple<Candidat, DetailAvancement, Examen, IEnumerable<MatiereDto>>(new Candidat(), new DetailAvancement(), new Examen(), new List<MatiereDto>());

            if (modelType == typeof(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto>))
                return new Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto>(new List<CandidatResultatDto>(), new CandidatCritereRechDto());

            if (modelType == typeof(Tuple<InterfacageActeCandidatRechercheVM, List<InterfacageActeCandidatResultatVM>>))
                return new Tuple<InterfacageActeCandidatRechercheVM, List<InterfacageActeCandidatResultatVM>>(new InterfacageActeCandidatRechercheVM(), new List<InterfacageActeCandidatResultatVM>());

            if (modelType == typeof(Tuple<CritereRapportViewModel, List<RapportViewModel>>))
                return new Tuple<CritereRapportViewModel, List<RapportViewModel>>(new CritereRapportViewModel(), new List<RapportViewModel>());

            return CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}
