using System;

namespace WebApplication3.Models
{
    public class AnnPayCalculation
    {
        public Tuple<double, double, int> calculateMonthAnnPay(String kreditSumString, String kreditSrokString, String kreditStavkaString, String kreditStavkaType)
        {
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
                var ItogSummaData = new Tuple<double, double, int>(monthAnnPay, monthStavka, kreditSrokInt);

               return ItogSummaData;
        }
        public Tuple<double[], double[], double[]> calculateOstatokTeloPercent(String kreditSumString, String kreditSrokString, String kreditStavkaString, String kreditStavkaType)
        {
            var ItogSummaData = calculateMonthAnnPay(kreditSumString, kreditSrokString, kreditStavkaString, kreditStavkaType);
            // платеж за год
            double monthAnnPay = ItogSummaData.Item1;
            double monthStavka = ItogSummaData.Item2;
            int kreditSrokInt = ItogSummaData.Item3;
            double yearAnnPay = monthAnnPay * kreditSrokInt;
            double ostatok = yearAnnPay;
            // массив остатков
            double[] ostatoks = new double[kreditSrokInt];
            double[] percent = new double[kreditSrokInt];
            int[] index = new int[kreditSrokInt];
            double[] telo = new double[kreditSrokInt];
            for (int i = 0; i < kreditSrokInt; i++)
            {
                // Вычисление остатка
                ostatok -= monthAnnPay;

                ostatoks[i] = ostatok;
                index[i] = i;
                // Размер платежа по проценту
                percent[i] = monthAnnPay * (1 - Math.Pow((1 + monthStavka / 100), i - kreditSrokInt));
                // Размер платежа по телу
                telo[i] = monthAnnPay - percent[i];
            }
            // передача остатков во View
            var result = new Tuple<double[], double[], double[]>(ostatoks, percent, telo);
            return result;
        }
    }
}
