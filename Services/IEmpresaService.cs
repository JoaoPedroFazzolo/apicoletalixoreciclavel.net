using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public interface IEmpresaDestinacaoService
    {
        IEnumerable<EmpresaDestinacaoModel> ObterTodasEmpresas();
        EmpresaDestinacaoModel ObterEmpresaPorId(long id);
        void AdicionarEmpresa(EmpresaDestinacaoModel empresa);
        void AtualizarEmpresa(EmpresaDestinacaoModel empresa);
        void DeletarEmpresa(long id);
        IEnumerable<EmpresaDestinacaoModel> PesquisarPorNome(string nome);
    }
}