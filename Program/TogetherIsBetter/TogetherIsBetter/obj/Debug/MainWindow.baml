   M S B A M L   `   `   `  ����H  DMonthCalendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MonthCalendar;assembly=MonthCalendarMonthCalendar  5   	    TogetherIsBetter!   TogetherIsBetter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Amc2clr-namespace:MonthCalendar;assembly=MonthCalendar     5   	   -   5          � Title$  Overview��5   	   $	��840��6   $	��840��6'    � WindowStartupLocation$ CenterScreen=�6N   .��5      � ���#f0f8ff � RowDefinitions 5   
   �� 5	      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0 0 0 1q�6   ���#959595 J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ #939393��6#      Opacity$	 0.3Q�63      
BlurRadius$ 5Q�6A      ShadowDepth$ 2Q�6Q   	   	Direction$		 270Q�6a   ._�5      � 
�������$��0��6&   $��0��63   .+�5      �� -   5        
 ��Name$
 ActionButtons��5      $��0��64   $��0��6A   $��Stretch=�6Q   $��Right=�6m    �� Orientation$ 
Horizontal=�6�   $��0,0,9,0q�6�   $	��297��6�   .+�5      �� -   5         ��Name$ btnManagement��5      $
��5,18q�64   $��10, 5q�6B   $��center=�6R   ���	#FFF1F1F1.��5      ]� .+�5      �� $��images/gear.png��6$   $��15��6=   $��0,0,5,0q�6I   �� 5      -   5         ��Name$ btnManagementText��5   (   $��center=�6C   $��
Management��6^   5      5      �� 5      -   5        $ 
btnProfile��5      $��5q�61   $��10, 5q�6<   $��center=�6L   ���	#FFF1F1F1.��5       ]� .+�5!      �� $��images/user.png��6$   $��15��6=   $��0,0,5,0q�6I   �� 5"      $��center=�6(   	Profile6L   5#      5$      �� 5%      -   5        $ 	btnLogout��5%      $��5q�60   $��10, 5q�6;   $��center=�6K   ���	#FFF1F1F1.��5&      ]� .+�5'      �� $��images/logout.png��6$   $��15��6?   $��0,0,5,0q�6K   �� 5(      $��center=�6(   Logout6K   5)      5*      5+      �� 5,      $��0��6   $��0��6*   $��Stretch=�6:   $
��Left=�6V   $ 
Horizontal=�6q   $��15,0,0,0q�6�   .+�5-      �� $��images/logo.jpg��6   $��50��65   $��5q�6A   $��center=�6L   �� 5.      $��TogetherIsBetter��6   $��center=�67   $��20(�6R   $
��Bold"�6`   5/      50      51      (   "MonthCalendar.MonthCalendarControl  53   
   -   5          Name$ AptCalendar��53   "   $��1��67   $��0��6D   $��Stretch=�6T   $��10q�55   S   $��0,0,0,1q�6`   56      57      