using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Justin.FrameWork.Entities;
namespace Justin.SalaryCalculator.Entities
{
    public class SalaryInfo
    {

        public SalaryInfo(double totalSalary, double quotedSalary, List<PayPercent> insurancesPayPercent, RevenuePolicy revenuePloicy)
        {
            this.TotalSalary = totalSalary;
            this.QuotedSalary = quotedSalary;
            this.InsurancesPayPercent = insurancesPayPercent;
            this.RevenuePolicy = revenuePloicy;
            this.Insurances = this.InsurancesPayPercent.Select(row => new Insurance(row)).ToList();
        }
        public double TotalSalary { get; private set; }
        public double QuotedSalary { get; private set; }
        public List<PayPercent> InsurancesPayPercent { get; private set; }
        public List<Insurance> Insurances { get; private set; }

        public double TotalInsurance { get; private set; }
        public double RevenueSalary { get; private set; }
        public RevenuePolicy RevenuePolicy { get; set; }
        public RevenueInfo[] RevenueLeveles { get; set; }
        public double[] Revenue { get; set; }

        public double[] FinalSalary { get; set; }

        public void Calculate()
        {
            foreach (Insurance insurance in Insurances)
            {
                insurance.Calculate(this.QuotedSalary);
            }

            this.TotalInsurance = this.Insurances.Where(row => row.Enable).Sum(row => row.PersonPayMoney);
            this.RevenueSalary = this.TotalSalary - this.TotalInsurance;

            this.RevenueLeveles = new RevenueInfo[2];

            List<RevenueLevel> levelesOfBefore = this.RevenuePolicy.RevenuePolicyBefore.Leveles.Where(row => (row.Max >= this.RevenueSalary - 2000) & (row.Min <= this.RevenueSalary - 2000)).ToList();
            List<RevenueLevel> levelesOfAfter = this.RevenuePolicy.RevenuePolicyAfter.Leveles.Where(row => (row.Max >= this.RevenueSalary - 3500) & (row.Min <= this.RevenueSalary - 3500)).ToList();
            this.RevenueLeveles[0] = new RevenueInfo() { Leveles = levelesOfBefore, RevenueBase = this.RevenuePolicy.RevenuePolicyBefore.RevenueBase };
            this.RevenueLeveles[1] = new RevenueInfo() { Leveles = levelesOfAfter, RevenueBase = this.RevenuePolicy.RevenuePolicyAfter.RevenueBase };

            this.Revenue = new double[2];
            this.Revenue[0] = (this.RevenueSalary - this.RevenueLeveles[0].RevenueBase) * this.RevenueLeveles[0].Leveles[0].Percent - this.RevenueLeveles[0].Leveles[0].Add;
            this.Revenue[1] = (this.RevenueSalary - this.RevenueLeveles[1].RevenueBase) * this.RevenueLeveles[1].Leveles[0].Percent - this.RevenueLeveles[1].Leveles[0].Add;

            this.FinalSalary = new double[2];
            this.FinalSalary[0] = this.TotalSalary - this.TotalInsurance - this.Revenue[0];
            this.FinalSalary[1] = this.TotalSalary - this.TotalInsurance - this.Revenue[1];
        }


    }

    public class Insurance
    {
        public Insurance(PayPercent payPercent)
        {
            this.PayPercent = payPercent;
            this.PersonPayMoney = 0;
            this.CompanyPayMoney = 0;
            this.Enable = true;
        }
        public PayPercent PayPercent { get; set; }
        public double PersonPayMoney { get; set; }
        public double CompanyPayMoney { get; set; }
        public bool Enable { get; set; }

        public void Calculate(double quotedInsurance)
        {
            this.CompanyPayMoney = quotedInsurance * this.PayPercent.CompanyPayPercent;
            this.PersonPayMoney = quotedInsurance * this.PayPercent.PersonPayPercent;
        }
    }
    [XmlRoot("InsurancePolicy")]
    public class InsurancePolicy
    {
        public InsurancePolicy()
        {
            this.YearsInsurancePolicies = new List<YearInsurancePolicy>();
        }
        [XmlArray("Years"), XmlArrayItem("Year")]
        public List<YearInsurancePolicy> YearsInsurancePolicies { get; set; }
    }

    [XmlRoot("Year")]
    public class YearInsurancePolicy
    {
        public YearInsurancePolicy() { }
        [XmlAttribute("Value")]
        public int Year { get; set; }
        [XmlArray("ResidenceTypes"), XmlArrayItem("ResidenceType")]
        public List<ResidenceTypeInsurancePolicy> ResidenceTypesPayPercent { get; set; }
    }
    [XmlRoot("ResidenceType")]
    public class ResidenceTypeInsurancePolicy
    {
        public ResidenceTypeInsurancePolicy() { }
        [XmlAttribute("Value")]
        public ResidenceType ResidenceType { get; set; }
        [XmlArray("InsuranceTypes"), XmlArrayItem("InsuranceType")]
        public List<PayPercent> InsurancesPayPercent { get; set; }
    }
    //[XmlRoot("InsuranceType")]
    //public class InsurancePayPercent
    //{
    //    public InsurancePayPercent() { }
    //    [XmlAttribute("Value")]
    //    public InsuranceType InsuranceType { get; set; }
    //    public PayPercent PayPercent { get; set; }

    //    public InsurancePayPercent(InsuranceType type, double company, double person)
    //    {
    //        this.InsuranceType = type;
    //        this.PayPercent = new PayPercent() { InsuranceType = type, CompanyPayPercent = company, PersonPayPercent = person };
    //    }
    //}
    public class PayPercent
    {
        public PayPercent() { }
        public PayPercent(InsuranceType type, double companyPayPercent, double personPayPercent)
        {
            this.InsuranceType = type;
            this.CompanyPayPercent = companyPayPercent;
            this.PersonPayPercent = personPayPercent;
        }
        [XmlAttribute("Value")]
        public InsuranceType InsuranceType { get; set; }
        [XmlAttribute()]
        public double CompanyPayPercent { get; set; }
        [XmlAttribute()]
        public double PersonPayPercent { get; set; }


    }
    public enum ResidenceType
    {
        OutsideTownCitizen,
        InsideTownCitizen,
        OutsideTownMigrantWorker,
        InsideTownMigrantWorker,
        OutsideTownPeasant,
        InsideTownPeasant,
    }
    public enum InsuranceType
    {
        [Display("工伤保险")]
        InjuryInsurance,
        [Display("失业保险")]
        UnEmployInsurance,
        [Display("医疗保险")]
        MedicalInsurance,
        [Display("养老保险")]
        EndowmentInsurance,
        [Display("生育保险")]
        MaternityInsurance,
        [Display("公积金")]
        AccumulationFund,
    }
}
