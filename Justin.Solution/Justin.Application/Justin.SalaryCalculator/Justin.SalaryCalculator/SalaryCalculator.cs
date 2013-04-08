using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Justin.FrameWork.Helper;
using Justin.FrameWork.Extensions;
using Justin.SalaryCalculator.Entities;

namespace Justin.SalaryCalculator
{
    public partial class SalaryCalculator : Form
    {
        public SalaryCalculator()
        {
            InitializeComponent();
        }

        private RevenuePolicy RevenuePolicy { get; set; }
        private InsurancePolicy InsurancePolicy { get; set; }
        private List<PayPercent> MyInsurancesPayPercent { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Constants.InsurancePayPercentFileName))
            {
                LoadInsurancePayPercent();
            } LoadRevenuePolicy();
            LoadInsurancePolicy();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtTotalSalary.Text))
            {
                ShowMessage("请输入基本工资");
                return;
            }
            if (string.IsNullOrEmpty(txtQuotedSalary.Text))
            {
                ShowMessage("请输入社保基数");
                return;
            }
            double totalSalary;
            double.TryParse(txtTotalSalary.Text, out totalSalary);
            double quotedSalary;
            double.TryParse(txtQuotedSalary.Text, out quotedSalary);
            List<PayPercent> insurancesPayPercent = this.MyInsurancesPayPercent;
            if (insurancesPayPercent == null || insurancesPayPercent.Count < 1)
            {
                insurancesPayPercent = InsurancePolicy.YearsInsurancePolicies[0].ResidenceTypesPayPercent[0].InsurancesPayPercent;
            }
            SalaryInfo salaryInfo = new SalaryInfo(totalSalary, quotedSalary, insurancesPayPercent, this.RevenuePolicy);

            salaryInfo.Calculate();

            string[] salaryString = GetSalaryString(salaryInfo);
            txtCompanyMessage.Text = salaryString[0];
            txtPersonMessage.Text = salaryString[1];
        }

        #region 函数

        private void LoadRevenuePolicy()
        {
            string content = File.ReadAllText(Constants.RevenuePolicyFileName, Encoding.UTF8);
            this.RevenuePolicy = SerializeHelper.XmlDeserialize<RevenuePolicy>(content);
        }
        private void LoadInsurancePolicy()
        {
            string content = File.ReadAllText(Constants.InsurancePolicyFileName, Encoding.UTF8);
            this.InsurancePolicy = SerializeHelper.XmlDeserialize<InsurancePolicy>(content);
        }
        private void LoadInsurancePayPercent()
        {
            string content = File.ReadAllText(Constants.InsurancePayPercentFileName, Encoding.UTF8);
            this.MyInsurancesPayPercent = SerializeHelper.XmlDeserialize<List<PayPercent>>(content);
        }


        private InsurancePolicy CreateDefaultInsurancePolicy()
        {
            InsurancePolicy insurancePolicy = new InsurancePolicy();
            YearInsurancePolicy policyOf2012 = new YearInsurancePolicy() { Year = 2012 };
            List<PayPercent> insurances = new List<PayPercent>();
            //养老
            insurances.Add(new PayPercent(InsuranceType.EndowmentInsurance, 0.2, 0.08));
            //失业
            insurances.Add(new PayPercent(InsuranceType.UnEmployInsurance, 0.015, 0.005));
            //工伤
            insurances.Add(new PayPercent(InsuranceType.InjuryInsurance, 0.005, 0));
            //生育
            insurances.Add(new PayPercent(InsuranceType.MaternityInsurance, 0.008, 0));
            //医疗
            insurances.Add(new PayPercent(InsuranceType.MedicalInsurance, 0.09, 0.02));
            insurances.Add(new PayPercent(InsuranceType.MedicalInsurance, 0.01, 0));
            //公积金
            insurances.Add(new PayPercent(InsuranceType.AccumulationFund, 0.12, 0.12));
            policyOf2012.ResidenceTypesPayPercent = new List<ResidenceTypeInsurancePolicy>();

            policyOf2012.ResidenceTypesPayPercent.Add(new ResidenceTypeInsurancePolicy()
            {
                ResidenceType = ResidenceType.OutsideTownCitizen,
                InsurancesPayPercent = insurances
            });
            insurancePolicy.YearsInsurancePolicies.Add(policyOf2012);

            string fileName = Constants.InsurancePolicyFileName;
            try
            {
                string content = SerializeHelper.XmlSerialize<InsurancePolicy>(insurancePolicy);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                File.AppendAllText(fileName, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {

            }
            return insurancePolicy;
        }
        private void CreateMyInsurancesPayPercentFile()
        {
            string fileName = Constants.InsurancePayPercentFileName;
            try
            {
                string content = SerializeHelper.XmlSerialize<List<PayPercent>>(this.MyInsurancesPayPercent);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                File.AppendAllText(fileName, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {

            }
        }
        private void ShowMessage(string msg)
        {
            toolStripStatusMessage.Text = msg;
        }

        private string[] GetSalaryString(SalaryInfo salaryInfo)
        {
            StringBuilder sbCompany = new StringBuilder();
            StringBuilder sbPerson = new StringBuilder();

            string salary = string.Format("总工资：{0}{2}社保基数{1}{2}保险+公积金", salaryInfo.TotalSalary, salaryInfo.QuotedSalary, Environment.NewLine);
            sbCompany.Append(salary).AppendLine();
            sbPerson.Append(salary).AppendLine();
            foreach (Insurance insurance in salaryInfo.Insurances)
            {
                sbCompany.AppendFormat("{0}:{1}￥", insurance.PayPercent.InsuranceType.GetDisplay(), insurance.CompanyPayMoney).AppendLine();
                sbPerson.AppendFormat("{0}:{1}￥", insurance.PayPercent.InsuranceType.GetDisplay(), insurance.PersonPayMoney).AppendLine();
            }

            sbCompany.AppendFormat("应税￥：{0}", salaryInfo.RevenueSalary - salaryInfo.RevenueLeveles[0].RevenueBase).AppendLine();
            sbPerson.AppendFormat("应税￥：{0}", salaryInfo.RevenueSalary - salaryInfo.RevenueLeveles[1].RevenueBase).AppendLine();

            sbCompany.AppendFormat("扣税比例：{0}", salaryInfo.RevenueLeveles[0].Leveles[0].Percent).AppendLine();
            sbPerson.AppendFormat("扣税比例：{0}", salaryInfo.RevenueLeveles[1].Leveles[0].Percent).AppendLine();


            sbCompany.AppendFormat("扣税￥：{0}", salaryInfo.Revenue[0]).AppendLine();
            sbPerson.AppendFormat("扣税￥：{0}", salaryInfo.Revenue[1]).AppendLine();

            sbCompany.AppendFormat("实发￥：{0}", salaryInfo.FinalSalary[0]).AppendLine();
            sbPerson.AppendFormat("实发￥：{0}", salaryInfo.FinalSalary[1]).AppendLine();
            string[] salaryString = new string[2];
            salaryString[0] = sbCompany.ToString();
            salaryString[1] = sbPerson.ToString();
            return salaryString;
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CreateMyInsurancesPayPercentFile();
        }

    }
}
