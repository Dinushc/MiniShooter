namespace CommonScripts
{
    using UnityEngine;
    using System.Collections.Generic;

    public static class ServiceLocator
    {
        private static Dictionary<string, Object> _controllers = new Dictionary<string, Object>();

        public static void RegisterController<T>(T controller) where T : Object
        {
            if (!_controllers.ContainsKey(typeof(T).ToString()))
            {
                _controllers.Add(typeof(T).ToString(), controller);
            }
        }

        public static T GetController<T>() where T : Object
        {
            return (T)GetService(_controllers, typeof(T).ToString());
        }

        public static void Clear()
        {
            _controllers.Clear();
        }

        private static Object GetService(Dictionary<string, Object> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return null;
        }

    }

}