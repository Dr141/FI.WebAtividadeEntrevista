using FI.AtividadeEntrevista.DAL.Beneficiarios;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Incluir(DML.Beneficiario beneficiario)
        {
            new DaoBeneficiario().Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            new DaoBeneficiario().Alterar(beneficiario);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            new DaoBeneficiario().Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        /// <param name="idCliente">id do cliente</param>
        public List<DML.Beneficiario> Pesquisa(long idCliente)
        {
            return new DaoBeneficiario().Pesquisa(idCliente);
        }
    }
}