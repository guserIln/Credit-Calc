using System;

namespace WebApplication3.Models
{
    public class AnnPayCalculation
    {
        public Tuple<double, double, int> calculateMonthAnnPay(String kreditSumString, String kreditSrokString, String kreditStavkaString, String kreditStavkaType)
        {
                // ����� �������
                double kreditSum = double.Parse(kreditSumString);
                // ���� �������
                int kreditSrokInt = int.Parse(kreditSrokString);
                // ��������� ������
                // ���� ������ ����, �� ������ ���������� �� ����� ���� � ����
                double kreditStavka = (kreditStavkaType == "����" ? double.Parse(kreditStavkaString) * 365 : double.Parse(kreditStavkaString));

                // �������� ������
                double monthStavka = kreditStavka / 12;
                // �����������
                double koefAnn = (monthStavka / 100) * Math.Pow(1 + (monthStavka / 100), kreditSrokInt) / (Math.Pow(1 + (monthStavka / 100), kreditSrokInt) - 1);
                // ����������� ������������ ������
                double monthAnnPay = koefAnn * kreditSum;
                var ItogSummaData = new Tuple<double, double, int>(monthAnnPay, monthStavka, kreditSrokInt);

               return ItogSummaData;
        }
        public Tuple<double[], double[], double[]> calculateOstatokTeloPercent(String kreditSumString, String kreditSrokString, String kreditStavkaString, String kreditStavkaType)
        {
            var ItogSummaData = calculateMonthAnnPay(kreditSumString, kreditSrokString, kreditStavkaString, kreditStavkaType);
            // ������ �� ���
            double monthAnnPay = ItogSummaData.Item1;
            double monthStavka = ItogSummaData.Item2;
            int kreditSrokInt = ItogSummaData.Item3;
            double yearAnnPay = monthAnnPay * kreditSrokInt;
            double ostatok = yearAnnPay;
            // ������ ��������
            double[] ostatoks = new double[kreditSrokInt];
            double[] percent = new double[kreditSrokInt];
            int[] index = new int[kreditSrokInt];
            double[] telo = new double[kreditSrokInt];
            for (int i = 0; i < kreditSrokInt; i++)
            {
                // ���������� �������
                ostatok -= monthAnnPay;

                ostatoks[i] = ostatok;
                index[i] = i;
                // ������ ������� �� ��������
                percent[i] = monthAnnPay * (1 - Math.Pow((1 + monthStavka / 100), i - kreditSrokInt));
                // ������ ������� �� ����
                telo[i] = monthAnnPay - percent[i];
            }
            // �������� �������� �� View
            var result = new Tuple<double[], double[], double[]>(ostatoks, percent, telo);
            return result;
        }
    }
}
