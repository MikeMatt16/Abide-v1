using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Abide.AddOnApi.Wpf.Halo2
{
    /// <summary>
    /// Defines a generalized Halo 2 retail tool button AddOn.
    /// </summary>
    public interface IToolButton : IHaloAddOn
    {
        ICommand ClickCommand { get; }
    }
}
