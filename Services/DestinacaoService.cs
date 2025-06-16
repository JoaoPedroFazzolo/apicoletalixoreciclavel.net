using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public class DestinacaoService : IDestinacaoService
    {
        private readonly IDestinacaoRepository _destinacaoRepository;

        public DestinacaoService(IDestinacaoRepository destinacaoRepository)
        {
            _destinacaoRepository = destinacaoRepository;
        }

        public IEnumerable<DestinacaoModel> ObterTodasDestinacoes()
        {
            return _destinacaoRepository.GetAll();
        }

        public DestinacaoModel ObterDestinacaoPorId(long id)
        {
            return _destinacaoRepository.GetById(id);
        }

        public IEnumerable<DestinacaoModel> ObterDestinacoesPorTipo(string tipo)
        {
            if (string.IsNullOrEmpty(tipo))
            {
                throw new ArgumentException("O tipo da destinação é obrigatório.");
            }

            return _destinacaoRepository.GetByTipo(tipo);
        }

        public IEnumerable<DestinacaoModel> ObterDestinacoesPorStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("O status da destinação é obrigatório.");
            }

            return _destinacaoRepository.GetByStatus(status);
        }

        public IEnumerable<DestinacaoModel> ObterDestinacoesAtivas()
        {
            return _destinacaoRepository.GetAtivas();
        }

        public IEnumerable<DestinacaoModel> ObterDestinacoesQuePermitemColeta()
        {
            return _destinacaoRepository.GetQuePermitemColeta();
        }

        public IEnumerable<DestinacaoModel> PesquisarPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome para pesquisa é obrigatório.");
            }

            return _destinacaoRepository.SearchByName(nome);
        }

        public void AdicionarDestinacao(DestinacaoModel destinacao)
        {
            if (string.IsNullOrEmpty(destinacao.Nome))
            {
                throw new ArgumentException("O nome da destinação é obrigatório.");
            }

            if (string.IsNullOrEmpty(destinacao.Tipo))
            {
                throw new ArgumentException("O tipo da destinação é obrigatório.");
            }

            if (string.IsNullOrEmpty(destinacao.Endereco))
            {
                throw new ArgumentException("O endereço da destinação é obrigatório.");
            }

            if (_destinacaoRepository.ExistsByName(destinacao.Nome))
            {
                throw new InvalidOperationException("Já existe uma destinação com este nome.");
            }

            destinacao.DataCadastro = DateTime.Now;
            destinacao.Status = "Ativo";
            _destinacaoRepository.Add(destinacao);
        }

        public void AtualizarDestinacao(DestinacaoModel destinacao)
        {
            if (string.IsNullOrEmpty(destinacao.Nome))
            {
                throw new ArgumentException("O nome da destinação é obrigatório.");
            }

            if (string.IsNullOrEmpty(destinacao.Tipo))
            {
                throw new ArgumentException("O tipo da destinação é obrigatório.");
            }

            if (string.IsNullOrEmpty(destinacao.Endereco))
            {
                throw new ArgumentException("O endereço da destinação é obrigatório.");
            }

            var destinacaoExistente = _destinacaoRepository.GetById(destinacao.DestinacaoId);
            if (destinacaoExistente == null)
            {
                throw new ArgumentException("Destinação não encontrada.");
            }

            var destinacaoComMesmoNome = _destinacaoRepository.SearchByName(destinacao.Nome)
                .FirstOrDefault(d => d.DestinacaoId != destinacao.DestinacaoId);
            
            if (destinacaoComMesmoNome != null)
            {
                throw new InvalidOperationException("Já existe uma destinação com este nome.");
            }

            _destinacaoRepository.Update(destinacao);
        }

        public void DeletarDestinacao(long id)
        {
            var destinacao = _destinacaoRepository.GetById(id);
            if (destinacao == null)
            {
                throw new ArgumentException("Destinação não encontrada.");
            }

            _destinacaoRepository.Delete(id);
        }

        public void AlterarStatus(long id, string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("O status é obrigatório.");
            }

            var statusValidos = new[] { "Ativo", "Inativo", "Suspenso" };
            if (!statusValidos.Contains(status))
            {
                throw new ArgumentException("Status inválido. Valores aceitos: Ativo, Inativo, Suspenso.");
            }

            var destinacao = _destinacaoRepository.GetById(id);
            if (destinacao == null)
            {
                throw new ArgumentException("Destinação não encontrada.");
            }

            _destinacaoRepository.AlterarStatus(id, status);
        }

        public IEnumerable<string> ObterTodosTipos()
        {
            return _destinacaoRepository.GetAllTipos();
        }
    }
}