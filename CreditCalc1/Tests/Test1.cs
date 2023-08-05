using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Models;

namespace WebApplication3.UnitTests.Models

{
    [TestClass]
    public class AnnPayCalculation_MonthAnnPay
    {
        private readonly AnnPayCalculation _MonthAnnPay;

        public AnnPayCalculation_MonthAnnPay()
        {
            _MonthAnnPay = new AnnPayCalculation();
        }

        [TestMethod]
        public void MonthAnnPay_ReturnFalse()
        {
            string kreditSumString = "10000";
            string kreditSrokString = "47";
            string kreditStavkaString = "10";
            string kreditStavkaType = "Год";
            var result = _MonthAnnPay.calculateMonthAnnPay(kreditSumString, kreditSrokString, kreditStavkaString, kreditStavkaType);
            double monthAnnPay = result.Item1;
            // месячная ставка
            double monthStavka = result.Item2;
            int kreditSrokInt = result.Item3;
            Assert.IsTrue(monthAnnPay == 258.02);
        }
    }
}
