using Power655.Core;

namespace Power655.Scraper;

public interface IDrawSource
{
    string Name { get; }

    /// <summary>Nguồn có dữ liệu cho game này không.</summary>
    bool Supports(Game game);

    /// <summary>Lấy các kỳ của <paramref name="game"/> ở trang <paramref name="page"/> (1 = mới nhất).</summary>
    Task<IReadOnlyList<Draw>> FetchPageAsync(Game game, int page, CancellationToken ct);
}

/// <summary>Nguồn trả lỗi nghiệp vụ hoặc dữ liệu không đọc được.</summary>
public sealed class DrawSourceException(string message, Exception? inner = null) : Exception(message, inner);
