using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using ma.metl.sirh.Model.Dto;

namespace ma.metl.sirh.Repository
{
    public class ReunionRepository : GenericRepository<Reunion>, IReunionRepository
    {
        public ReunionRepository(sirhContext context)
            : base(context)
        {

        }
        public Reunion GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public List<Reunion> GetReunionByDate(int idCom)
        {
            return FindBy(x => x.DateReunion > DateTime.Now && x.CommissionId == idCom).ToList(); 
        }

        public List<EventDto> GetListReunion(DateTime fromDate, DateTime toDate)
        {
            sirhContext db = new sirhContext();
            List<Reunion> rslt = (from x in db.Reunion
                                   where x.DateReunion >= fromDate && DbFunctions.AddMinutes(x.DateReunion,x.Duree) <= toDate
                                    select x).ToList();

            List<EventDto> result = new List<EventDto>();
            foreach (var item in rslt)
            {
                EventDto rec = new EventDto();
                rec.ID = Convert.ToInt32(item.Id);
                rec.CommissionId = Convert.ToInt32(item.Commission.Id); 
                rec.StartDateString = item.DateReunion.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                rec.EndDateString = item.DateReunion.AddMinutes(item.Duree).ToString("s"); // field AppointmentLength is in minutes
                rec.Title = item.OrdreJour;
                rec.Duree = item.Duree.ToString() + " mins";
                rec.DureeString = item.DureeString;
                rec.StatusString = item.StatusENUM;
                rec.Heure = item.DateReunion.ToString("hh:mm");
                rec.StatusString = item.StatusENUM;
                rec.StatusColor = Enums.GetEnumDescription<ReunionStatus>(rec.StatusString);
                string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                rec.StatusColor = ColorCode;
                result.Add(rec);
            }

            return result;

        }

        public List<EventDto> GetListReunion(DateTime fromDate, DateTime toDate, int commissionId, int gradeId, string annee, string examenType)
        {
            sirhContext db = new sirhContext();
            List<Reunion> rslt = (from x in db.Reunion
                                  where x.DateReunion >= fromDate && DbFunctions.AddMinutes(x.DateReunion, x.Duree) <= toDate
                                    //&& x.CommissionId == commissionId
                                    //&& x.Commission.GradeId == gradeId
                                    //&& x.Commission.Annee == annee
                                    //&& x.Commission.EcritOrOral == examenType
                                  select x).ToList();

            if (commissionId != 0)
            {
                rslt = (from x in rslt
                        where x.CommissionId == commissionId
                        select x).ToList();
            }

            if (gradeId != 0)
            {
                rslt = (from x in rslt
                        where x.Commission.GradeId == gradeId
                        select x).ToList();
            }

            if (annee != "")
            {
                rslt = (from x in rslt
                        where x.Commission.Annee == annee
                        select x).ToList();
            }

            if (examenType != "")
            {
                rslt = (from x in rslt
                        where x.Commission.EcritOrOral == examenType
                        select x).ToList();
            }

            List<EventDto> result = new List<EventDto>();
            foreach (var item in rslt)
            {
                EventDto rec = new EventDto();
                rec.ID = Convert.ToInt32(item.Id);
                rec.CommissionId = Convert.ToInt32(item.Commission.Id);
                rec.StartDateString = item.DateReunion.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                rec.EndDateString = item.DateReunion.AddMinutes(item.Duree).ToString("s"); // field AppointmentLength is in minutes
                rec.Title = item.OrdreJour;
                rec.Duree = item.Duree.ToString() + " mins";
                rec.DureeString = item.DureeString;
                rec.StatusString = item.StatusENUM;
                rec.Heure = item.DateReunion.ToString("hh:mm");
                rec.StatusString = item.StatusENUM;
                rec.StatusColor = Enums.GetEnumDescription<ReunionStatus>(rec.StatusString);
                string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                rec.StatusColor = ColorCode;
                result.Add(rec);
            }

            return result;

        }
    }
}
