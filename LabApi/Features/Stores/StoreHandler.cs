using System;
using LabApi.Features.Wrappers;

namespace LabApi.Features.Stores;

internal class StoreHandler
{
    internal required Action<Player> AddPlayer { get; init; }

    internal required Action<Player> RemovePlayer { get; init; }

    internal required Action DestroyAll { get; init; }
}