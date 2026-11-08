namespace Power655.Core;

/// <summary>Dữ liệu kỳ quay không hợp lệ (sai số lượng, trùng số, ngoài khoảng 1..55...).</summary>
public sealed class InvalidDrawException : Exception
{
    public InvalidDrawException(string message) : base(message) { }
}
