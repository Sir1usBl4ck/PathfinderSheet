using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserInterface.Models;

namespace UserInterface.UserControls
{
    /// <summary>
    /// Interaction logic for SkillControl.xaml
    /// </summary>
    public partial class SkillControl : UserControl
    {
        public Skill Skill
        {
            get { return (Skill)GetValue(SkillProperty); }
            set { SetValue(SkillProperty, value); }
        }


        public static readonly DependencyProperty SkillProperty =
            DependencyProperty.Register("Skill", typeof(Skill), typeof(SkillControl), new PropertyMetadata(null, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SkillControl control = d as SkillControl;


                control.nameTextBlock.Text = (e.NewValue as Skill).Name;
                control.bonusTextBlock.Text = (e.NewValue as Skill).Bonus.ToString();
                control.rankTextBlock.Text = (e.NewValue as Skill).Rank.ToString();
        }

        


        public SkillControl()
        {
            InitializeComponent();
        }
    }
}
