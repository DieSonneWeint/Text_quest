using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Text_quest
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ViewModel VM = new ViewModel();
        int proc = 0;
        List<string> potion = new List<string>();
        public Window1()
        {
            InitializeComponent();
            Background = VM.LoadTemp();
            TB.Text = VM.NextLine();
        }
        private void Button_Click(object sender, RoutedEventArgs e) // кнопка назад
        {
           TB.Text = VM.BackLine();         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // кнопка далее 
        {           
            TB.Text = VM.NextLine();
            if (VM.CheckOpenEvents() == 1)
            {
                ButtonVisibilityOrDisable(1);
            }
            else if (VM.CheckOpenEvents() == 2)
            {
                B_SwordEvent_end.Visibility = Visibility.Visible;
                B_SwordEvent_T.Visibility = Visibility.Visible;
            }
            else if (VM.CheckOpenEvents() == 3)
            {
                VM.ArmorEvemt();
                VM.SaveTemp();
                EndEv();
            }
            else if (VM.CheckOpenEvents() == 4)
            {
                B_FlowerOFLife.Visibility = Visibility.Visible;
                B_FlowerOfMane.Visibility = Visibility.Visible;
                B_Romsh.Visibility = Visibility.Visible;
            }
            else if (VM.CheckOpenEvents() == 5)
            {
                Background = new ImageBrush(new BitmapImage(new Uri(VM.LoadEvent(4), UriKind.Relative)));
                TB.Text = VM.NextLine();
            }
            else if (!VM.CheckEvent(4) && !VM.CheckEvent(5) && !VM.CheckEvent(6)) ButtonVisibilityOrDisable(1);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // загрука события с мечом
        {
            Background = new ImageBrush(new BitmapImage(new Uri(VM.LoadEvent(1), UriKind.Relative)));
            ButtonVisibilityOrDisable(2);
            ButtonVisibilityOrDisable(4);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // загрузка события с броней
        {
          Background = new ImageBrush(new BitmapImage(new Uri( VM.LoadEvent(2),UriKind.Relative)));
          ButtonVisibilityOrDisable(2);
           ButtonVisibilityOrDisable(4);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)// загрузка события с зельем
        {
          Background = new ImageBrush(new BitmapImage(new Uri(VM.LoadEvent(3), UriKind.Relative)));
          ButtonVisibilityOrDisable(2);
          ButtonVisibilityOrDisable(4);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // кнопка заточки
        {
            proc += 10;
            TB.Text = $"Прогресс заточки {proc}";
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) // конпка завершения заточки
        {
            if (VM.Sword_Event(proc)== 1) 
            {
                TB.Text = "Лезвине недостаточно острое, продолжайте заточку";
            }
            if(VM.Sword_Event(proc) == 2) 
            {
                TB.Text = "Вы отлчино заточили лезвие";
                B_SwordEvent_end.Visibility = Visibility.Collapsed;
                B_SwordEvent_T.Visibility = Visibility.Collapsed;             
                EndEv();
            }
            if(VM.Sword_Event(proc) == 3) 
            {
                TB.Text = "Вы перестарались и сломали клинок, поgробуйте заточить другой меч";
                proc = 0;
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e) // Ромашка 
        {
            potion.Add("1");
            B_Romsh.IsEnabled = false;
            CheckPotionEv();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e) // Цветок маны
        {
            potion.Add("2");
            B_FlowerOfMane.IsEnabled = false;
            CheckPotionEv();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e) // Цветок жизни
        {
            potion.Add("3");
            B_FlowerOFLife.IsEnabled = false;
            CheckPotionEv();
        }
        private void CheckPotionEv() // проверка для события с зельями
        {
            if (potion.Count == 3)
            {
                string str = string.Join("", potion);
                if (VM.Potion_Event(str) == 1)
                {
                    TB.Text = "Вы приготовили зелье";
                    B_FlowerOFLife.Visibility = Visibility.Collapsed;
                    B_FlowerOfMane.Visibility = Visibility.Collapsed;
                    B_Romsh.Visibility = Visibility.Collapsed;         
                    EndEv();            
                }
                else
                {
                    potion.Clear();
                    B_FlowerOFLife.IsEnabled = true;
                    B_FlowerOfMane.IsEnabled|= true;
                    B_Romsh.IsEnabled|= true;
                    TB.Text = "Не получилось приготовить зелье, попробуйте в другом порядке";
                }
            }
        }
        private void ButtonVisibilityOrDisable(int var) // функция скрытия/открытия кнопок
        {
            if (var == 1) 
            {
                if (!VM.CheckEvent(2)) B_ARMOR.Visibility = Visibility.Visible;
                if (!VM.CheckEvent(3)) B_ALCH.Visibility = Visibility.Visible;
                if (!VM.CheckEvent(1)) B_SWORD.Visibility = Visibility.Visible;
            }
            if (var == 2)
            {
                B_ARMOR.Visibility = Visibility.Collapsed;
                B_ALCH.Visibility = Visibility.Collapsed;
                B_SWORD.Visibility = Visibility.Collapsed;
            }
            if (var == 3)
            {
                B_Next.IsEnabled = false;
                B_Back.IsEnabled = false;
            }
            if(var == 4)
            {
                B_Next.IsEnabled = true;
                B_Back.IsEnabled= true;
            }
        }
        private  void EndEv() 
        {
            
            if (VM.CheckOpenEvents() == 5)
            {

                Background = new ImageBrush(new BitmapImage(new Uri(VM.LoadEvent(4), UriKind.Relative)));
                ButtonVisibilityOrDisable(4);
                
            }
            else
            {              
                Background = new ImageBrush(new BitmapImage(new Uri(VM.LoadEvent(-1), UriKind.Relative)));
                ButtonVisibilityOrDisable(3);
                ButtonVisibilityOrDisable(1);
            }
            TB.Text = VM.NextLine();
            VM.SaveTemp();
        }
    }
}
