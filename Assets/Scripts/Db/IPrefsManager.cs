using System;

namespace Db
{
    public interface IPrefsManager
    {
        void SetValue<T>(string key, T val);
        T GetValue<T>(string key);
        T GetValue<T>(string key, T defaultValue);
        event Action<int, string> HasUpdateValue;
        
        bool HasKey(string key);
        void DeleteKey(string key);
        void DeleteAll();
        void Save();
    }
}