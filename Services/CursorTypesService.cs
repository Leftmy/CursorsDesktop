using System.Threading.Tasks;
using CursorsDesktop.Abstractions.Clients;
using CursorsDesktop.Abstractions.Services;
using CursorsDesktop.Data;
using CursorsDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CursorsDesktop.Services;

public class CursorTypesService(ICursorTypesClient cursorTypesClient) : ICursorTypesService
{
    private readonly ICursorTypesClient _cursorTypesClient = cursorTypesClient;

    public async Task SyncCursorTypesAsync()
    {
        var response = await _cursorTypesClient.GetAllAsync();
        var cursorTypes = response.CursorTypes;
        if (cursorTypes.Count == 0) return;

        using var db = new ApplicationDbContext();

        foreach (var cursorType in cursorTypes)
        {
            var localType = await db.CursorTypes
                .FirstOrDefaultAsync(t => t.SystemRole == cursorType.SystemRole);
            
            if (localType == null)
            {
                db.CursorTypes.Add(new CursorTypeModel
                {
                    Id = cursorType.Id,
                    Name = cursorType.Name,
                    SystemRole = cursorType.SystemRole,
                });
            }
            else
            {
                localType.Name = cursorType.Name;
            }

            await db.SaveChangesAsync();
        }
    }
}