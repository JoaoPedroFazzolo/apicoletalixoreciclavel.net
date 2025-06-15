using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _alertaRepository;

        public AlertaService(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        public IEnumerable<AlertaModel> ObterTodosAlertas()
        {
            return _alertaRepository.GetAll();
        }

        public AlertaModel ObterAlertaPorId(long id)
        {
            return _alertaRepository.GetById(id);
        }

        public IEnumerable<AlertaModel> ObterAlertasPorStatus(string status)
        {
            return _alertaRepository.GetByStatus(status);
        }

        public IEnumerable<AlertaModel> ObterAlertasPorTipo(string tipo)
        {
            return _alertaRepository.GetByTipo(tipo);
        }

        public IEnumerable<AlertaModel> ObterAlertasPorUsuario(long usuarioId)
        {
            return _alertaRepository.GetByUsuario(usuarioId);
        }

        public IEnumerable<AlertaModel> ObterAlertasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _alertaRepository.GetByPeriodo(dataInicio, dataFim);
        }

        public void AdicionarAlerta(AlertaModel alerta)
        {
            alerta.DataAlerta = DateTime.Now;
            _alertaRepository.Add(alerta);
        }

        public void AtualizarAlerta(AlertaModel alerta)
        {
            _alertaRepository.Update(alerta);
        }

        public void DeletarAlerta(long id)
        {
            _alertaRepository.Delete(id);
        }

        public void AtualizarStatusAlerta(long id, string novoStatus)
        {
            var alerta = _alertaRepository.GetById(id);
            if (alerta != null)
            {
                alerta.Status = novoStatus;
                _alertaRepository.Update(alerta);
            }
        }

        public long ContarAlertasPorStatus(string status)
        {
            return _alertaRepository.CountByStatus(status);
        }
    }
}