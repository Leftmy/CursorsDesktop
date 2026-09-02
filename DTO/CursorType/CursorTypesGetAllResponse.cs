using System.Collections.Generic;

namespace CursorsDesktop.DTO.CursorType;

public record CursorTypesGetAllResponse(
    IReadOnlyList<CursorTypeResponse> CursorTypes
);