namespace quazimodo.Services.Interfaces
{
    public interface IStorageService
    {
        void SaveCount(int count);

        int GetCount();
    }
}