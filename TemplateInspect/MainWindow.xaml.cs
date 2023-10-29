using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace TemplateInspect
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Inspect();
        }

        private void Inspect()
        {
            Type controlType = typeof(Control);
            List<Type> types = new List<Type>();

            Assembly assembly = Assembly.GetAssembly(controlType);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
                {
                    types.Add(type);
                }
            }

            lbTypes.ItemsSource = types;
        }

        private void lbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Type type = lbTypes.SelectedItem as Type;

                ConstructorInfo info = type.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                control.Visibility = Visibility.Collapsed;
                grid.Children.Add(control);

                ControlTemplate template = control.Template;

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                txtTemplate.AppendText(sb.ToString());

                grid.Children.Remove(control);
            }
            catch (Exception err)
            { 
                txtTemplate.AppendText("<< Error generating template: " + err.Message + ">>");
            }
        }
    }
}
