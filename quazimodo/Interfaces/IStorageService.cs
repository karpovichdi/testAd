namespace quazimodo.Interfaces
{
    public interface IStorageService
    {
        void SaveCount(int count);

        int GetCount();
    }
}