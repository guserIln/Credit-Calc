using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                // срок кредита
                int kreditSrokInt = int.Parse(kreditSrokString);
                // кредитная ставка
                // если выбран день, то ставка умножается на число дней в году
                double kreditStavka = (kreditStavkaType == "День" ? double.Parse(kreditStavkaString) * 365 : double.Parse(kreditStavkaString));


                // месячная ставка
                double monthStavka = kreditStavka / 12;
                // коэффициент
                double koefAnn = (monthStavka / 100) * Math.Pow(1 + (monthStavka / 100), kreditSrokInt) / (Math.Pow(1 + (monthStavka / 100), kreditSrokInt) - 1);
                // Ежемесячный аннуитентный платеж
                double monthAnnPay = koefAnn * kreditSum;
                // платеж за год
                double yearAnnPay = monthAnnPay * kreditSrokInt;

                double ostatok = yearAnnPay;
                // массив остатков
                double[] ostatoks = new double[kreditSrokInt];
                DateTime[] dateTimes = new DateTime[kreditSrokInt];
                int[] index = new int[kreditSrokInt];
                // тело кредита
                double[] telo = new double[kreditSrokInt];
                // размеры платежей по проценту
                double[] percent = new double[kreditSrokInt];
                double[] osplt = new double[kreditSrokInt];
                //ViewBag.Message = "Your application description page.";
                for (int i = 0; i<kreditSrokInt; i++)
                {
                    // Вычисление остатка
                    ostatok -= monthAnnPay;
                    Console.WriteLine(i + " " + monthAnnPay + " " + ostatok);
                    ostatoks[i] = ostatok;
                    index[i] = i;
                    // telo[i] = monthAnnPay - Math.Abs(1 - monthStavka) * monthAnnPay;
                    // Количество дней в месяце
                    int kol = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(i).Month);
                    //  percent[i] = ostatok * kreditStavka / 100 * kol / 365;
                    // Размер платежа по проценту
                    percent[i] = monthAnnPay * (1 - Math.Pow((1 + monthStavka / 100), i - kreditSrokInt));
                    // Размер платежа по телу
                    telo[i] = monthAnnPay - percent[i];
                    //osplt[i] = monthAnnPay * Math.Pow(1 + monthStavka, i - kreditSrokInt - 1);
                }
                // передача итоговой суммы во View
                ViewBag.ItogSumma = monthAnnPay * kreditSrokInt;
                //ViewBag.Index = index;
                // передача остатков во View
                ViewBag.Ostatok = ostatoks;
                // ежемесячный аннуитетный платеж
                ViewBag.MonthAnnPay = monthAnnPay;
              
                ViewBag.Telo = telo;
                // передача размеров платежа по проценту во View
                ViewBag.Percent = percent;
                // переплата по кредиту
                ViewBag.Pereplata = ViewBag.ItogSumma - kreditSum;
             //   ViewBag.OSPLT = osplt;
                //return View();
            }
            catch (ArithmeticException ex)
            {
                //return View();
            }
            catch (ArgumentNullException ex)
            {
                ViewBag.Message = "Ошибка: неверно введены сумма, ставка, срок кредита. Требуется указать целое значение для срока кредита. Перейдите на главную страницу и повторите ввод";
            }
            catch (FormatException ex)
            {
                ViewBag.Message = "Ошибка: неверно введены сумма, ставка, срок кредита. Требуется указать целое значение для срока кредита. Перейдите на главную страницу и повторите ввод";
            }
            finally
            {
               // return View();
            }
            return View();
           
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}