   M S B A M L   `   `   `  ����L  HCaseManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CaseManagementApp.ViewsCaseManagementApp.Views  5      C&clr-namespace:CaseManagementApp.ModelsCaseManagementApp.Models  5      +    %CaseManagementApp.Views.StartUpScreen   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:CaseManagementApp.Views     5      <	ViewModel&clr-namespace:CaseManagementApp.Models     5      .��5      � ���#161925  � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $	��.5* �6   .+�5   
   �� $��0��6   $!��Case management application��6   $��20(�6C   
�������6Q   �� 5   
   $��1��6   $��-Most recent cases-��6   $��20(�6;   ���#9EC5AB$
��Bold"�6^   $��Center=�6p   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      $	��.5* �6#   �� 5      $��2* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6      TemplateColumnLabel#��[  6$   $��Case Id��6Q   �� 5      $��1��6   #��[  6$   $��Subject��6Q   �� 5      $��2��6   #��[  6$   $��Date Created��6Q   �� 5       $��3��6   #��[  6$   $��Status��6Q   5!      �� 5"   
   -   5         ��Name$ lvRecentCases��5"      $��3��6*   ���#161925$��Stretch=�6L    � 	Resources 5#      #  CaseManagementApp.Models.Case  LatestCases��%   &	         5$      6!   5%       � DataContext 5&      �� 5'       �� Source# [ 6   ��5)      �� 5*      .R�5+      � $��Stretch=�6    5,      �� 5-      $	��.5* �6/   �� 5.      $��2* �6/   �� 5/      $��* �6/   �� 50      $��* �6/   .+�52      �� $��0��6    
�������$��18(�6^   $��Center=�6l   ��60   �� *CaseId+6l   �� 53      $��1��6    
�������$��18(�6_   $
��Left=�6m   ��60   �� *	Subject+6m   �� 54      $��2��6    
�������$��18(�6c   $��Center=�6q   ��60   �� *CreatedDate+6q   �� 55      $��3��6    
�������$��18(�6]   $��Center=�6k   ��60   �� *State+6k   56      57      59      � 5:   
   $��4��6   $��Stretch=�6    5;      �� 5<      $��* �6#   �� 5=      $��* �6#   �� 5>      $��* �6#   �� 5?      $��* �6#     5A      �� 5B      $
��auto �6    .+�5D      �� $��0��6   .+�5E      �� $��Total cases��6   $��16(�6.   
�������$��0 0 0 1q�6P   ���#9EC5AB$��Center=�6   �� 5F      -   5         ��Name$ lblTotalCases��5F      $��14(�6/   
�������$��Center=�6P   5G      �� 5H      $��1��6   .+�5I      �� $��Cases Waiting��6   $��16(�60   
�������$��0 0 0 1q�6R   ���#9EC5AB$��Center=�6�   �� 5J      -   5        $ lblCasesWaiting��5J      $��14(�61   
�������$��Center=�6R   5K      �� 5L      $��2��6   .+�5M      �� $��Cases Opened��6   $��16(�6/   
�������$��0 0 0 1q�6Q   ���#9EC5AB$��Center=�6�   �� 5N      -   5        $ lblCasesOpened��5N      $��14(�60   
�������$��Center=�6Q   5O      �� 5P      $��3��6   .+�5Q      �� $��Cases Closed��6   $��16(�6/   
�������$��0 0 0 1q�6Q   ���#9EC5AB$��Center=�6�   �� 5R      -   5        $ lblCasesClosed��5R      $��14(�60   
�������$��Center=�6Q   5S      5T      5W      5X      