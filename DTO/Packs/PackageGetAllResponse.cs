using System;
using System.Collections.Generic;
using CursorsDesktop.DTO.Styles;

namespace CursorsDesktop.DTO.Packs;

public record PackagesGetAllResponse(
    int Id,
    string Name,
    string Description,
    string PathToIcon,
    DateTime CreatedAt,
    List<StyleResponse> Styles
);