   M S B A M L   `   `   `  ����L  HCaseManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CaseManagementAppCaseManagementApp  5   	   Y1clr-namespace:CaseManagementApp.Models.ViewModels#CaseManagementApp.Models.ViewModels  5   	   "    CaseManagementApp.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CaseManagementApp     5   	   G	viewModel1clr-namespace:CaseManagementApp.Models.ViewModels     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Title$ CaseManager��5
   	   $	��800��6   $
��1200��6*    � DataContext 5      =  7CaseManagementApp.Models.ViewModels.MainWindowViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#2d3142$��Stretch=�6:   .+�5      	�    TemplateMenuButtons#��[  6   $��Start��6G   $��Center=�6~    d� 	IsChecked$
 TrueY�6�   
������ $
��15 0q�6�   ���#9EC5AB$��Poppins)�6�   $��Light"�6�   $��	0 20 0 20q�6  $��0q�6%   � Cursor$
 Hand��69  ��6W   �� *StartScreenCommand+69  	� 5      #��[  6   $��New User��6H   $��12(�6�   $
��Left=�6�   ��6[   �� *CreateUserCommand+6�   	� 5      #��[  6   $��New Case��6H   $��12(�6�   $��0 0 0 20q�6�   $
��Left=�6�   ��6[   �� *CreateCaseCommand+6�   	� 5      #��[  6   $��View Customers��6H   $��12(�6�   $
��Left=�6�   ��6a   �� *CustomersViewCommand+6�   	� 5      #��[  6   $��View Admins��6H   $��12(�6�   $
��Left=�6�   ��6^   �� *AdminsViewCommand+6�   	� 5      #��[  6   $��
View Cases��6H   $��12(�6�   $
��Left=�6�   ��6]   �� *CasesViewCommand+6�   	� 5      #��[  6   $��Case Details��6G   $��12(�6�   $
��Left=�6�   $��	0 20 0 0 q�6�   ��6^   �� *CaseDetailsCommand+6�   5!      �� 5#   
   $��1��6   ��6)   �� *CurrentView+5$      5%      