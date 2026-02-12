using BusinessLayer.DTOs;
using DataAccessLayer.DBContext;

namespace BusinessLayer.Interfaces
{
    public interface IHelpdeskService
    {
        Task<IEnumerable<Priority>> GetActivePrioritiesAsync(int companyId, int regionId);
        Task<IEnumerable<HelpDeskCategory>> GetActivecategoryAsync(int companyId, int regionId);
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<int> SubmitTicketAsync(TicketRequestDto dto);
        Task SendTicketEmailToManagerAsync(int ticketId);
        Task<IEnumerable<object>> GetMyTicketsAsync(int userId);
        Task<IEnumerable<ManagerTicketDto>> GetManagerTicketsAsync(int managerId);
        Task UpdateTicketStatusAsync(UpdateTicketStatusDto dto);
        Task SendTicketStatusEmailToEmployeeAsync(int ticketId);
        Task<IEnumerable<UserProfileDto>> GetEmployeesByManagerAsync(int managerId);


    }
}
