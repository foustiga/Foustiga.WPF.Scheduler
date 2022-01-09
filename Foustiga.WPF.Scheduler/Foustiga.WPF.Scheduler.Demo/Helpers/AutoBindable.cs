using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Foustiga.WPF.Scheduler.Demo.Helpers
{
    /// <summary>
    /// Base class to implement object that can be bound to
    /// </summary>
    [Serializable]
    public abstract class AutoBindable : INotifyPropertyChanged, IDisposable
    {
        //private Dictionary<string, object> _properties = new Dictionary<string, object>();
        private ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();
        //private ConcurrentDictionary<string, object> _propertiesOldValue = new ConcurrentDictionary<string, object>();
        //_properties.AddOrUpdate(propertyname, value, (key, oldValue) => value);

        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetProperty<T>(T defaultValue, [CallerMemberName] string propertyname = null)
        {
            Debug.Assert(propertyname != null, "propertyname != null");
            object value;// = defaultValue;
            if (_properties.TryGetValue(propertyname, out value))
            { return value == null ? default(T) : (T)value; }
            else { return defaultValue; }
        }
        protected T GetProperty<T>([CallerMemberName] string propertyname = null)
        {
            Debug.Assert(propertyname != null, "propertyname != null");
            object value = null;
            if (_properties.TryGetValue(propertyname, out value))
                return value == null ? default(T) : (T)value;
            return default(T);
        }

        /// <summary>
        /// Sets the value of a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        protected bool SetProperty<T>(T value, [CallerMemberName] string propertyname = null)
        {
            Debug.Assert(propertyname != null, "propertyname != null");
            if (_properties.ContainsKey(propertyname) && Equals(value, GetProperty<T>(default(T), propertyname)))
            {
                return false; 
            }
            else {
             //   object oldValue = default(T);
             //   if (_propertiesOldValue.TryGetValue(propertyname, out oldValue))
             //   {
             //       _propertiesOldValue.AddOrUpdate(propertyname, GetProperty<T>(default(T), propertyname), (key, oldValue) => GetProperty<T>(default(T), propertyname));
             //   }
             //   else {
             //       _propertiesOldValue.AddOrUpdate(propertyname, oldValue, (key, oldValue) => oldValue);
             //   }
             //   //if (Equals(value, Get<T>(default(T),  name)))
             //   //    return;
             ////   _propertiesOldValue.AddOrUpdate(propertyname, oldValue, (key, oldValue) => oldValue);
             //   _properties.AddOrUpdate(propertyname, value, (key, oldValue) => value);
             //   //   OnPropertyChanged<T>( (T)oldValue, value, propertyname);


                _properties.AddOrUpdate(propertyname, value, (key, oldValue) => value);
                OnPropertyChanged(propertyname);
                return true;
            }
        }
        //protected T GetPropertyOldValue<T>([CallerMemberName] string propertyname = null)
        //{
        //    object oldValue = null;
        //    if ( _propertiesOldValue.TryGetValue(propertyname, out oldValue) ) { return (T)oldValue; }
        //    else { return default(T); }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
     //       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //protected virtual void OnPropertyChanged<T>(T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedExtendedEventArgs<T>(oldValue, newValue, propertyName));
        //}

        /// <summary>Raises the <see cref="PropertyChanged"/> event.</summary>
        /// <param name="propertyName">The property name of the property that has changed.
        /// This optional parameter can be skipped because the compiler is able to create it automatically.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }
        //protected void RaisePropertyChanged<T>(T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        //{
        //    OnPropertyChanged<T>((T)oldValue, newValue, propertyName);
        //}




        #region IDisposable implementation
        private bool _disposed = false;// To detect redundant calls        
        private SafeHandle _safeHandle = new Microsoft.Win32.SafeHandles.SafeFileHandle(IntPtr.Zero, true); // Instantiate a SafeHandle instance.       
        public void Dispose() => Dispose(true);// Public implementation of Dispose pattern callable by consumers.        
        protected virtual void Dispose(bool disposing)// Protected implementation of Dispose pattern.
        {
            if (_disposed) { return; }
            if (disposing) { _safeHandle?.Dispose(); }// Dispose managed state (managed objects).
            _disposed = true;
        }
        #endregion IDisposable implementation




        //protected void SetNotifyingProperty<T>(Expression<Func<T>> expression, ref T field, T value)
        //{
        //    if (field == null || !field.Equals(value))
        //    {
        //        T oldValue = field;
        //        field = value;
        //        OnPropertyChanged(this, new PropertyChangedExtendedEventArgs<T>(oldValue, value, GetPropertyName(expression)));
        //    }
        //}
        //public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //        handler(sender, e);
        //}
        //protected string GetPropertyName<T>(Expression<Func<T>> expression)
        //{
        //    MemberExpression memberExpression = (MemberExpression)expression.Body;
        //    return memberExpression.Member.Name;
        //}

    }


    //public sealed class PropertyChangedExtendedEventArgs<T> : PropertyChangedEventArgs
    //{
    //    public PropertyChangedExtendedEventArgs(T oldValue, T newValue, string propertyName) : base(propertyName)
    //    {
    //        OldValue = oldValue;
    //        NewValue = newValue;
    //    }
    //    public T OldValue { get; }
    //    public T NewValue { get; }
    //}
}
