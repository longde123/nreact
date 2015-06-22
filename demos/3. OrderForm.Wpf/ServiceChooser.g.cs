/*-----------------------------------------------------------------------------+
 | This file is generated by NReact.Csx. Do not modify, changes could be lost. |
 +-----------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

using NReact;

public partial class ServiceChooser : NComponent
{
  protected override object GetInitialState() 
  {
    return new { Total = 0 };
  }

  void AddTotal(int price) 
  {
    SetState( new { Total = State.Total + price });
  }

  public override NElement Render() 
  {
     IEnumerable items = Props.Items;
     var services = from dynamic s in items 
                    select New(typeof(Service), new { Name = s.Name, Price = s.Price, Active = s.Active, AddTotal = (Action<int>)AddTotal });

     return New(typeof(StackPanel), new { HorizontalAlignment = "Center", VerticalAlignment = "Center" }, 
              New(typeof(TextBlock), new { FontSize = "24", Text = "Our services" }), 

              New(typeof(StackPanel), null, 
                services
              ), 
              
              New(typeof(TextBlock), new { Text = "Total $" + State.Total, HorizontalAlignment = "Center" })
            );
  }
}

public class Service: NComponent 
{
  protected override object GetInitialState() 
  {
    return new { Active = false };
  }

  void ClickHandler(object sender, RoutedEventArgs args) 
  {
    var active = !State.Active;

    SetState(new { Active = active });
        
    ((Action<int>)Props.AddTotal)(active ? Props.Price : -Props.Price);
  }

  public override NElement Render() 
  {
    return  New(typeof(Button), new { Style = State.Active ? "Active" : null, Padding = "8,4", Click = (RoutedEventHandler)ClickHandler, Content = Props.Name + " $" + Props.Price });
  }
}