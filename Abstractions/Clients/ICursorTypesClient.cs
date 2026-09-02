using System.Threading.Tasks;
using CursorsDesktop.DTO.CursorType;

namespace CursorsDesktop.Abstractions.Clients;

public interface ICursorTypesClient
{
    Task<CursorTypesGetAllResponse> GetAllAsync();
}