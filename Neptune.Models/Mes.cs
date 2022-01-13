﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Mes
    {
        public MesTransacao MesTransacao { get; private set; }
        public SaldoUltimoDiaMesAnterior SaldoUltimoDiaMesAnterior { get; private set; }
        public DateTime UltimoDiaMesAnterior 
        { 
            get 
            {
                return MesTransacao.ObterUltimoDiaMesAnterior();
            } 
        }
        public List<Dia> Dias { get; private set; } = new List<Dia>();

        public Mes(MesTransacao mesTransacao, SaldoUltimoDiaMesAnterior saldoUltimoDiaMesAnterior, List<Transacao> transacoes)
        {
            MesTransacao = mesTransacao;
            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;

            var transacoesDiaGrupo = transacoes.GroupBy(x => x.Data.Day).ToList();
            decimal saldoDiaAnterior = 0;

            for(int i = 0; i < transacoesDiaGrupo.Count(); i++)
            {
                var transacoesDia = transacoesDiaGrupo[i];

                var dataDia = new DateTime(mesTransacao.Ano, mesTransacao.Mes, transacoesDia.Key);
                Dia dia = null;

                if (i == 0)
                {
                    dia = new Dia(dataDia, transacoesDia.ToList(), saldoUltimoDiaMesAnterior.Valor);
                }
                else
                {
                    dia = new Dia(dataDia, transacoesDia.ToList(), saldoDiaAnterior);    
                }
                saldoDiaAnterior = dia.SaldoDoDia;

                Dias.Add(dia);
            }
        }
    }

    public class SaldoUltimoDiaMesAnterior
    {
        public decimal Valor 
        {
            get
            { 
                return Contas.Sum(x => x.Valor);
            } 
        }

        public List<SaldoUltimoDiaMesAnteriorConta> Contas { get; private set; } = new List<SaldoUltimoDiaMesAnteriorConta>();

        public SaldoUltimoDiaMesAnterior(List<SaldoUltimoDiaMesAnteriorConta> saldosUltimoDiaMesAnteriorConta)
        {
            Contas = saldosUltimoDiaMesAnteriorConta;
        }
    }

    public class SaldoUltimoDiaMesAnteriorConta
    {
        public int ContaId { get; private set; }
        public decimal Valor { get; private set; }

        public SaldoUltimoDiaMesAnteriorConta(int contaId, decimal valor)
        {
            ContaId = contaId;
            Valor = valor;
        }
    }
}
