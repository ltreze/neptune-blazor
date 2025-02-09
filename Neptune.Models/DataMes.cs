﻿using System;

namespace Neptune.Domain
{
    public struct DataMes : IComparable
    {
        public int Ano { get; private set; }
        public int Mes { get; private set; }

        public DataMes(int ano, int mes)
        {
            Ano = ano;
            Mes = mes;
        }

        public int NumMesAnterior
        {
            get => Mes == 1 ? 12 : Mes - 1;
        }

        public int NumMesSeguinte
        {
            get => Mes == 12 ? 1 : Mes + 1;
        }

        public int NumAnoDoMesAnterior
        {
            get => new DateTime(Ano, Mes, 1).AddMonths(-1).Year;
        }

        public int NumAnoDoMesSeguinte
        {
            get => new DateTime(Ano, Mes, 1).AddMonths(1).Year;
        }

        public DateTime UltimoDiaDoMesAnterior
        {
            get => new DateTime(Ano, Mes, 1).AddDays(-1);
        }

        public string UltimoDiaDoMesAnteriorFormat
        {
            get => UltimoDiaDoMesAnterior.ToString("dd/MM/yyyy");
        }

        public DataMes ObterMesAnterior() => new DataMes(NumAnoDoMesAnterior, NumMesAnterior);
        public DataMes ObterMesSeguinte() => new DataMes(NumAnoDoMesSeguinte, NumMesSeguinte);

        public int CompareTo(object obj)
        {
            if (((DataMes)obj).Ano == this.Ano && ((DataMes)obj).Mes == this.Mes)
                return 0;
            return -1;
        }
    }
}
