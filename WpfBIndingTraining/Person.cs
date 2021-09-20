using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfBIndingTraining
{
    public class Person : INotifyPropertyChanged,INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            var h = this.PropertyChanged;
            if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        private int age;

        public int Age
        {
            get { return this.age; }
            set { this.SetProperty(ref this.age, value); 
            if (value < 0)
                {
                    this.errors["Age"] = new[] { "年齢を入力してください" };
                }
                else
                {
                    this.errors["Age"] = null;
                }
                this.OnErrorsChanged();
            
            }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set
            {
                this.SetProperty(ref this.name, value);
                if (string.IsNullOrEmpty(value))
                {
                    this.errors["Name"] = new[] { "名前を入力してください" };
                }
                else
                {
                    this.errors["Name"] = null;
                }
                this.OnErrorsChanged();
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors
        {
            get { return this.errors.Values.Any(e => e != null); }
        }
        private Dictionary<string, IEnumerable> errors = new Dictionary<string, IEnumerable>();
        private void OnErrorsChanged([CallerMemberName] string propertynName = null)
        {
            var h = this.ErrorsChanged;
            if (h != null) { h(this, new DataErrorsChangedEventArgs(propertynName)); }
        }
        public IEnumerable GetErros(string properyName)
        {
            IEnumerable error = null;
            this.errors.TryGetValue(properyName, out error);
            return error;
        }

    }
}
