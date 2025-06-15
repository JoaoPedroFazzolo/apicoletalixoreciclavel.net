using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public interface IAlertaService
    {
        IEnumerable<AlertaModel> ObterTodosAlertas();
        AlertaModel ObterAlertaPorId(long id);
        IEnumerable<AlertaModel> ObterAlertasPorStatus(string status);
        IEnumerable<AlertaModel> ObterAlertasPorTipo(string tipo);
        IEnumerable<AlertaModel> ObterAlertasPorUsuario(long usuarioId);
        IEnumerable<AlertaModel> ObterAlertasPorPeriodo(DateTime dataInicio, DateTime dataFim);
        void AdicionarAlerta(AlertaModel alerta);
        void AtualizarAlerta(AlertaModel alerta);
        void DeletarAlerta(long id);
        void AtualizarStatusAlerta(long id, string novoStatus);
        long ContarAlertasPorStatus(string status);
    }
}