using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public class EmpresaDestinacaoService : IEmpresaDestinacaoService
    {
        private readonly IEmpresaDestinacaoRepository _empresaDestinacaoRepository;

        public EmpresaDestinacaoService(IEmpresaDestinacaoRepository empresaDestinacaoRepository)
        {
            _empresaDestinacaoRepository = empresaDestinacaoRepository;
        }

        public IEnumerable<EmpresaDestinacaoModel> ObterTodasEmpresas()
        {
            return _empresaDestinacaoRepository.GetAll();
        }

        public EmpresaDestinacaoModel ObterEmpresaPorId(long id)
        {
            return _empresaDestinacaoRepository.GetById(id);
        }

        public void AdicionarEmpresa(EmpresaDestinacaoModel empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.Nome))
            {
                throw new ArgumentException("O nome da empresa é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(empresa.Endereco))
            {
                throw new ArgumentException("O endereço da empresa é obrigatório.");
            }

            if (_empresaDestinacaoRepository.ExistsByName(empresa.Nome))
            {
                throw new InvalidOperationException("Já existe uma empresa com este nome.");
            }

            empresa.DataCriacao = DateTime.Now;
            _empresaDestinacaoRepository.Add(empresa);
        }

        public void AtualizarEmpresa(EmpresaDestinacaoModel empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.Nome))
            {
                throw new ArgumentException("O nome da empresa é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(empresa.Endereco))
            {
                throw new ArgumentException("O endereço da empresa é obrigatório.");
            }

            var empresaExistente = _empresaDestinacaoRepository.GetById(empresa.EmpresaDestinacaoId);
            if (empresaExistente == null)
            {
                throw new ArgumentException("Empresa não encontrada.");
            }

            var empresaComMesmoNome = _empresaDestinacaoRepository.SearchByName(empresa.Nome)
                .FirstOrDefault(e => e.EmpresaDestinacaoId != empresa.EmpresaDestinacaoId);
            
            if (empresaComMesmoNome != null)
            {
                throw new InvalidOperationException("Já existe uma empresa com este nome.");
            }

            _empresaDestinacaoRepository.Update(empresa);
        }

        public void DeletarEmpresa(long id)
        {
            var empresa = _empresaDestinacaoRepository.GetById(id);
            if (empresa == null)
            {
                throw new ArgumentException("Empresa não encontrada.");
            }

            _empresaDestinacaoRepository.Delete(id);
        }

        public IEnumerable<EmpresaDestinacaoModel> PesquisarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome para pesquisa é obrigatório.");
            }

            return _empresaDestinacaoRepository.SearchByName(nome);
        }
    }
}