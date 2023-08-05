using System;
using System.Web.Mvc;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            try
            {   
                string kreditSumString = Request.Params["sum"];
                string kreditSrokString = Request.Params["srok"];
                string kreditStavkaString = Request.Params["stavka"];
                string kreditStavkaType = Request.Params["type_stavka"];
                // сумма кредита
                double kreditSum = double.Parse(kreditSumString);
                AnnPayCalculation calc = new AnnPayCalculation();
                var resultAnnPay = calc.calculateMonthAnnPay(kreditSumString, kreditSrokString, kreditStavkaString, kreditStavkaType);
                // платеж за год
                var result = calc.calculateOstatokTeloPercent(kreditSumString, kreditSrokString, kreditStavkaString, kreditStavkaType);
                // Ежемесячный аннуитентный платеж
                double monthAnnPay = resultAnnPay.Item1;
                // месячная ставка
                double monthStavka = resultAnnPay.Item2;    
                int kreditSrokInt = resultAnnPay.Item3;
                // передача итоговой суммы во View
                ViewBag.ItogSumma = monthAnnPay * kreditSrokInt;
                // ежемесячный аннуитетный платеж
                ViewBag.MonthAnnPay = monthAnnPay;
                // передача остатков во View
                ViewBag.Ostatok = result.Item1;
                // передача размеров платежа по проценту во View
                ViewBag.Percent = result.Item2;
                ViewBag.Telo = result.Item3;
                // переплата по кредиту
                ViewBag.Pereplata = ViewBag.ItogSumma - kreditSum;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Message = "Ошибка: неверно введены сумма, ставка, срок кредита. Требуется указать целое значение для срока кредита. Перейдите на главную страницу и повторите ввод";
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Message = "Ошибка: неверно введены сумма, ставка, срок кредита. Требуется указать целое значение для срока кредита. Перейдите на главную страницу и повторите ввод";
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";
            return View();
        }
    }
}