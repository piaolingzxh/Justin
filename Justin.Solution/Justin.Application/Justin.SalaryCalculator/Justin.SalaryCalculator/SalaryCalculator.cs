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
            txtSalaryMessage.Text = salaryString[5];
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
            StringBuilder sbBeforeSalaryChanged = new StringBuilder().AppendLine("税改前：");
            StringBuilder sbAfterSalaryChanged = new StringBuilder().AppendLine("税改后：");
            StringBuilder salaryMsg = new StringBuilder();
            int padCount = 17;

            string salaryBaseInfo = string.Format("总工资：{0}{2}社保基数:{1}{2}保险+公积金", salaryInfo.TotalSalary, salaryInfo.QuotedSalary, Environment.NewLine);
            salaryMsg.AppendLine(salaryBaseInfo).Append("公司部分".PadRight(padCount, ' ')).Append("个人部分").AppendLine();
            sbCompany.AppendLine("公司部分：");
            sbPerson.AppendLine("个人部分：");
            foreach (Insurance insurance in salaryInfo.Insurances)
            {
                string project = insurance.PayPercent.InsuranceType.GetDisplay().PadLeft(4, ' ');
                sbCompany.AppendFormat("{0}:{1}￥", project, insurance.CompanyPayMoney).AppendLine();
                sbPerson.AppendFormat("{0}:{1}￥", project, insurance.PersonPayMoney).AppendLine();
                salaryMsg.AppendFormat("{0}:￥{1}￥{2}", project, insurance.CompanyPayMoney.ToString().PadRight(10, ' '), insurance.PersonPayMoney).AppendLine();
            }
            salaryMsg.Append("税前".PadRight(padCount, ' ')).Append("税后").AppendLine();
            int shouldSalary1 = (int)(salaryInfo.RevenueSalary - salaryInfo.RevenueLeveles[0].RevenueBase);
            int shouldSalary2 = (int)(salaryInfo.RevenueSalary - salaryInfo.RevenueLeveles[1].RevenueBase);

            salaryMsg.AppendFormat("应税：{0}￥{1}￥", shouldSalary1.ToString().PadLeft(7, ' '), shouldSalary2.ToString().PadLeft(7, ' ')).AppendLine();
            sbBeforeSalaryChanged.AppendFormat("应税￥：{0}", shouldSalary1).AppendLine();
            sbAfterSalaryChanged.AppendFormat("应税￥：{0}", shouldSalary2).AppendLine();

            double p1 = salaryInfo.RevenueLeveles[0].Leveles[0].Percent;
            double p2 = salaryInfo.RevenueLeveles[1].Leveles[0].Percent;
            salaryMsg.AppendFormat("扣税比例：{0}%{1}%", p1.ToString().PadLeft(4, ' '), p2.ToString().PadLeft(7, ' ')).AppendLine();
            sbBeforeSalaryChanged.AppendFormat("扣税比例：{0}", p1).AppendLine();
            sbAfterSalaryChanged.AppendFormat("扣税比例：{0}", p2).AppendLine();

            int s1 = (int)salaryInfo.Revenue[0];
            int s2 = (int)salaryInfo.Revenue[1];
            salaryMsg.AppendFormat("扣税：{0}￥{1}￥", s1.ToString().PadLeft(7, ' '), s2.ToString().PadLeft(7, ' ')).AppendLine();
            sbBeforeSalaryChanged.AppendFormat("扣税￥：{0}", s1).AppendLine();
            sbAfterSalaryChanged.AppendFormat("扣税￥：{0}", s2).AppendLine();


            int f1 = (int)salaryInfo.FinalSalary[0];
            int f2 = (int)salaryInfo.FinalSalary[1];
            salaryMsg.AppendFormat("实发：{0}￥{1}￥", f1.ToString().PadLeft(7, ' '), f2.ToString().PadLeft(7, ' '));
            sbBeforeSalaryChanged.AppendFormat("实发￥：{0}", f1).AppendLine();
            sbAfterSalaryChanged.AppendFormat("实发￥：{0}", f2).AppendLine();

            string[] salaryString = new string[6];
            salaryString[0] = salaryBaseInfo;
            salaryString[1] = sbCompany.ToString();
            salaryString[2] = sbPerson.ToString();
            salaryString[3] = sbBeforeSalaryChanged.ToString();
            salaryString[4] = sbAfterSalaryChanged.ToString();
            salaryString[5] = salaryMsg.ToString();


            return salaryString;
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CreateMyInsurancesPayPercentFile();
        }

    }
}
