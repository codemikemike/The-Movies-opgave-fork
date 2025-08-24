using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace The_Movie.ViewModels
{
    //SMID INotifyPropertyChanged herover i, så alle ViewModels kan bruge det
    // Før: Hver ViewModel havde sin egen INotifyPropertyChanged ❌
    // Nu: Alle ViewModels arver fra BaseViewModel ✅

    /*ICommand: Button actions(knap klik → ViewModel metode)
INotifyPropertyChanged: Data change notifications(ViewModel → View)
Data Binding: Automatic data sync(View ↔ ViewModel)

INotifyPropertyChanged er kun én retning af data broen!
Den fulde bro er:

View → ViewModel: Automatisk via binding
ViewModel → View: Via INotifyPropertyChanged*/

    public class BaseViewModel
    {
    }
}
