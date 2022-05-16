namespace TorteLand;

// TODO : to contravariant
public interface ICrudl<TKey, TValue>
{
    Task<TValue> CreateAsync(TValue value);
    Task<TValue> ReadAsync(TKey key);
    Task<TValue> UpdateAsync(TValue value);
    Task<TValue> DeleteAsync(TKey key);
    IAsyncEnumerable<TValue> ToAsyncEnumerable();
}