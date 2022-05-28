namespace TorteLand;

// TODO : to contravariant
public interface IAcrud<TKey, TValue>
{
    IAsyncEnumerable<TValue> AllAsync();
    Task<TValue> CreateAsync(TValue value);
    Task<TValue> ReadAsync(TKey key);
    Task<TValue> UpdateAsync(TValue value);
    Task<TValue> DeleteAsync(TKey key);
}