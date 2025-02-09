﻿using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public interface ITransacaoService
    {
        Task<List<Transacao>> ObterTodas();
        Task<Transacao> AdicionarTransacao(Transacao transacao);
        Task<List<Mes>> ObterTodosMeses(List<Conta> contas);
    }
}
