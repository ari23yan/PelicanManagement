using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities.Dataware
{
    [Table("VwDoctorsInformation", Schema = "Gnr")]
    public class DoctorsInformation
    {
        public bool Active { get; set; }
        public string NP { get; set; }
        public string FP { get; set; }
        public string NoNezam { get; set; }
        public string NameP { get; set; }
        public string Name { get; set; }
        public string CodeJ { get; set; }
        public string TakhsosKoli { get; set; }
        public string Takhsos { get; set; }
        public string CodeFarei { get; set; }
        public string Mobile { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string HomeTel { get; set; }
        public string ClinicTel { get; set; }
        public string Mail { get; set; }
        public DateTime? VisitTar { get; set; }
        public bool SmsSendPermit { get; set; }
        public bool OutpAdmitPer { get; set; }
        public string NoBirthCertificate { get; set; }
        public string AccNezam { get; set; }
        public string CenterCode { get; set; }
        public string GroupCode { get; set; }
        public string NationalCode { get; set; }
        public string PersCode { get; set; }
        public string DoctorNationalCode { get; set; }
        public string KindTakhsos { get; set; }
        public string AgeCategory { get; set; }
        public string NationalTariffCode { get; set; }
        public int? ClinicMaximumPhysiotherapyRequest { get; set; }
    }
}
