   M S B A M L   `   `   `  ����
  TMMTMS    TMMTMS.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $	��720��5   	   $
��1080��6     � WindowStartupLocation$  CenterScreen=�6#    � WindowStyle$
 None=�6H    � AllowsTransparency$
 True��5   	   
������  � 	Resources 5	         	BoolToVis��%   &	        �� 5
   
   6'   5      .��5      �� -   5        ���#E6E6E6 �� CornerRadius$ 15��5   "   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#   .+�5      �  � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      $��* �6$   .+�5      �� $��1��6   ���#333333$ 	0 50 0 15��6;   ._�5      ]� .+�5      �� $	��150��6!   $��80��6-    �� Padding$ 10q�69   ���#FFFFFF$ 	0 50 0 15��6[   $	��Top=�5      $��
-10 30 0 0q�62   ._�5       �� ��5!   "   �� 5"   &    �� ImageSource$' !/Images/TMM_Electric_Original.png��61   5$      5%      �� 5(      $��25q�6$   $	��Top=�60   $	��0.8��6E   ���#FFFFFF6R   �� 5+      $	��Top=�6%   .+�5,      ��   
menuButton#��[ 6%   .��5-   "   �� 	 �� Orientation$	 
Horizontal=�6-   .+�5/   &   �� $
��Team��60   50   #   51      �� 52      #��[ 6%   .��53   "   �� $	 
Horizontal=�6-   .+�55   &   �� $��Anwesenheitsliste��60   56   #   57      �� 58      #��[ 6%   .��59   "   �� $	 
Horizontal=�6-   .+�5;   &   �� $��	Protokoll��60   5<   #   5=      5>      5?      5@      5A      � 5D      $��1��6   $��30 20 20 20q�6#    5E      �� 5F      $
��auto �6$   �� 5G      $
��auto �6$   �� 5H      $
��auto �6$   �� 5I      $
��auto �6$   �� 5J      $
��auto �6$   �� 5K      $��* �6$   �� 5L      $
��auto �6$   .+�5P      � $	��300��6   $
��Left=�6#   .+�5Q      �� $
��20 0q�6    $��Center=�6/   ���#b0b9c6
 J� IsHitTestVisible$
 False��6_    K� ZIndex$ 1��5R       $��Suche Teammitglied...��61   y�6N   ��  �� ElementName$ 	txtSearch�� �� Path$ Text.IsEmpty� �� 	Converter# [  �� 5T      -   5         ��Name$ 	txtSearch��5T        textboxSearch#��[ 61   5U      �� 5X      $
��Team��6   $��1��6(   $��32(�65   $��SemiBold"�6C   $��0 20 0 0q�6Y   $
��Left=�5Y      ���#12151867   �� 5\        	addButton#��[ 6   .��5]      �� $	 
Horizontal=�6!   .+�5^      �� $$��Neues Teammitglied hinzufügen��6$   5_      5`      �� 5c      $��1��6   $��3��6'   ���#dae2ea$��	0 -1 0 10q�6I   �� 5f      $��2��6   $	 
Horizontal=�6*   $��0 10 0 0q�6C   .+�5g      �� $��Aktiv��6     	tabButton#��[ 6-   ���#784ff26P   �� 5h      $��Alumni��6   #��[ 6.   �� 5i      $��	Ehemalige��6   #��[ 61   5j      �� 5m      $��68 Teammitglieder��6   $��4��65   $��18(�6B   $��SemiBold"�6P   ���#121518$
��Left=�5n   1   $��Center=�6L   �� 5p      $��Filter nach��6   $��4��6/   $��13(�6<   $��SemiBold"�6J   $��	0 0 215 0q�6`   ���#121518$��Right=�5q   1   $��Center=�6M   � 5t      $	��200��6   $��Right=�6#   $��4��6?   .+�5u      �� $
��15 0q�6    $��Center=�6/   ���#b0b9c6$
 False��6_   $ 1��5v       $��Filter nach...��61   y�6G   �� $ 	txtSearch��$ Text.IsEmpty�# [  �� 5x      -   5        $ 	txtFilter��5x        textboxFilter#��[ 61   5y      &   System.Windows.Controls.DataGrid  5|      -   5          Name$ membersDataGrid��5}   W     DataGridStlye#���  5|        DataGridCellStyle1   	CellStyle# �  6C     DataGridRowStyle1   RowStyle# �  5}      $��5��6J      Columns 5~      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6G      HeaderTemplate 5�      �� 5�   "   .R�5�   &   �� 5�   #   5�      0  *System.Windows.Controls.DataGridTextColumn  5�         Header$ #��6-      
IsReadOnly$
 True��68   $ False��6J   $
 auto 6`   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $ Member��61   $
 True��6A   $ * 6S      CellTemplate 5�      �� 5�   "   .R�5�   &   �� $	 
Horizontal=�61   .+�5�   *   �� $��25��61   $��25��6<   $ 50��6H   $��0 0 10 0q�6Z   
���  ��._�5�   .   �� $	��...��68   $��Center=�6C   $��Center=�6^   ���#FFFFFF$��0 0 0 1q�5�   M   5�   +   �� 5�   *   $	��...��64   $��Center=�6?   5�   '   5�   #   5�        5�      $ Position��6-   $
 True��6?   $ * 6Q     5�      $ HS-Mail��6-   $
 True��6>   $ * 6P     5�      $ Handynummer��6-   $
 True��6B   $ * 6T     5�      $ Operationen��61   $
 True��6F   $
 auto 6X    5�      �� 5�   "   5�   #   5�      5�      5�      5�      5�      5�      